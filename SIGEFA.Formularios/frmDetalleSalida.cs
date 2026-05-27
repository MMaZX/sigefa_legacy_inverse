using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmDetalleSalida : Office2007Form
{
	public double stock42;

	public bool consultorext;

	public int CodVendedor;

	public static List<int> seleccion = new List<int>();

	public int Proceso = 0;

	public int Seleccion = 0;

	public int Procede = 0;

	public int Tipo = 0;

	public int Moneda = 0;

	public int CodProducto = 0;

	public int codProveedor = 0;

	public int codTipodoc = 0;

	public int codTran = 0;

	public double tc = 0.0;

	public bool Cotización = false;

	public int CodCliente = 0;

	public int CodAlmacen = 0;

	public List<clsDetalleFacturaVenta> productoscargados = new List<clsDetalleFacturaVenta>();

	public List<clsDetalleCotizacion> productoscotizados = new List<clsDetalleCotizacion>();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto pro = new clsProducto();

	public clsDetalleNotaSalida detalle = new clsDetalleNotaSalida();

	private clsUnidadEquivalente uniequi = new clsUnidadEquivalente();

	private clsValidar ok = new clsValidar();

	public List<clsDetalleNotaSalida> productosNotaSalida = new List<clsDetalleNotaSalida>();

	private TextBox manipulado = new TextBox();

	private clsAdmListaPrecio AdmLista = new clsAdmListaPrecio();

	public clsListaPrecio listaprecio = new clsListaPrecio();

	public int alma = 0;

	private decimal factorconvert = default(decimal);

	public int Codlista = 0;

	private bool changeimporte = false;

	public decimal puInicio = default(decimal);

	private double precioprod = 0.0;

	public decimal stock;

	public double PrecioProducto;

	public decimal precio_Old = default(decimal);

	public bool bvalorventa = false;

	public decimal TipoCambio = default(decimal);

	public bool cliEspecial = false;

	public int codDetalle;

	public bool ventasinafectaciondestock = false;

	internal clsUsuario usuario_click = null;

	private decimal factor = default(decimal);

	private int undbase = 0;

	public static decimal cant = default(decimal);

	private IContainer components = null;

	private Button btnSalir;

	private ImageList imageList1;

	private Button btnGuardar;

	private GroupBox groupBox1;

	public TextBox txtReferencia;

	private Label label11;

	private Label label10;

	private Label label9;

	private Label label7;

	private Label label8;

	private Label label6;

	private Label label5;

	private TextBox txtStock;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	public TextBox txtCodigo;

	public TextBox txtDscto3;

	public TextBox txtDscto2;

	public TextBox txtPrecioNeto;

	public TextBox txtDscto1;

	public TextBox txtPrecio;

	public TextBox txtCantidad;

	public TextBox txtControlStock;

	public TextBox txtUnidad;

	public TextBox txtPrecioDscto;

	public TextBox txtUltimoPrecioCompra;

	private Label label14;

	public TextBox txtPrecioNetoDscto;

	private Label label13;

	public TextBox txtDescripcion;

	public TextBox txtUnd;

	public ComboBox cmbUnidad;

	public TextBox txtPrecioNetoDolares;

	private Label label16;

	private CheckBox checkBox1;

	private CheckBox checkBox2;

	private Panel panel1;

	private TextBox txtUbicacion;

	private Label label15;

	public TextBox txtPrecioMax;

	private TextBox txtStockMinimo;

	private Label label20;

	private Label label12;

	private Label label17;

	public TextBox txtcodlinea;

	private Label label18;

	public TextBox txtcodfamilia;

	private Label lbldias;

	public TextBox txtdias;

	private Label lbldisponibilidad;

	public ComboBox cmbdisponibilidad;

	private Label lblgananciamonto;

	public TextBox txtgananciamonto;

	private Label lblmarguenporcentaje;

	public TextBox txtgananciaporcentaje;

	public TextBox txtflete;

	private Label lblflete;

	private Label lbltotalcosto;

	public TextBox txttotalcosto;

	private Label lbldesestiva;

	public TextBox txtdesestiva;

	private CheckBox chbverganancia;

	private Label label19;

	public TextBox txtcodtipoprecio;

	private TextBox txtultimoprecio;

	private Label lblultimoprecio;

	private Button btnverstock;

	public frmDetalleSalida()
	{
		InitializeComponent();
	}

	private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.ShowDialog();
		}
	}

	private bool verificarDuplicidadProducto(bool tipoCarga)
	{
		bool encontrado = false;
		switch (Procede)
		{
		case 1:
		{
			frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
			if (form.dgvDetalle.Rows.Count > 0)
			{
				foreach (DataGridViewRow row2 in (IEnumerable)form.dgvDetalle.Rows)
				{
					int codigoIterado2 = Convert.ToInt32(row2.Cells[3].Value);
					int codigoBuscado2 = (tipoCarga ? Convert.ToInt32(txtReferencia.Text) : Convert.ToInt32(txtCodigo.Text));
					if (codigoBuscado2 == codigoIterado2)
					{
						MessageBox.Show("EL PRODUCTO SELECCIONADO CON CÓDIGO " + codigoIterado2 + " YA FUE AGREGADO A LA NOTA DE SALIDA", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						encontrado = true;
						break;
					}
				}
			}
			return encontrado;
		}
		case 2:
		{
			frmVenta formV = (frmVenta)Application.OpenForms["frmVenta"];
			if (formV.dgvDetalle.Rows.Count > 0)
			{
				foreach (DataGridViewRow row3 in (IEnumerable)formV.dgvDetalle.Rows)
				{
					int codigoIterado3 = Convert.ToInt32(row3.Cells[3].Value);
					int codigoBuscado3 = (tipoCarga ? Convert.ToInt32(txtReferencia.Text) : Convert.ToInt32(txtCodigo.Text));
					if (codigoBuscado3 == codigoIterado3)
					{
						MessageBox.Show("EL PRODUCTO SELECCIONADO CON CÓDIGO " + codigoIterado3 + " YA FUE AGREGADO A LA ORDEN DE VENTA, PUEDE MODIFICAR LA CANTIDAD DESDE LA PANTALLA PRINCIPAL", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						encontrado = true;
						break;
					}
				}
			}
			return encontrado;
		}
		case 3:
		{
			frmOrdenVenta formOV = (frmOrdenVenta)Application.OpenForms["frmOrdenVenta"];
			if (formOV.dgvDetalle.Rows.Count > 0)
			{
				foreach (DataGridViewRow row in (IEnumerable)formOV.dgvDetalle.Rows)
				{
					int codigoIterado = Convert.ToInt32(row.Cells[2].Value);
					int codigoBuscado = (tipoCarga ? Convert.ToInt32(txtReferencia.Text) : Convert.ToInt32(txtCodigo.Text));
					if (codigoBuscado == codigoIterado)
					{
						MessageBox.Show("EL PRODUCTO SELECCIONADO CON CÓDIGO " + codigoIterado + " YA FUE AGREGADO A LA ORDEN DE VENTA, PUEDE MODIFICAR LA CANTIDAD DESDE LA PANTALLA PRINCIPAL", "VALIDACIÓN", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						encontrado = true;
						break;
					}
				}
			}
			return encontrado;
		}
		default:
			return encontrado;
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtCodigo_TextChanged(object sender, EventArgs e)
	{
		if (!(txtCodigo.Text != "") || verificarDuplicidadProducto(tipoCarga: false))
		{
			return;
		}
		if (Procede == 2)
		{
			pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), alma, 2, 0, 0);
		}
		else if (ventasinafectaciondestock)
		{
			pro = AdmPro.CargaProductoDetalleSinAfectarStock(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 2, Codlista);
		}
		else if (Procede == 4)
		{
			pro = AdmPro.CargaProductoDetalleCotizacion(Convert.ToInt32(txtCodigo.Text), (Convert.ToInt32(Cotización) != 0) ? 1 : 0, frmLogin.iCodAlmacen, 2, Codlista);
		}
		else
		{
			pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 2, Codlista, 0);
		}
		if (pro == null)
		{
			MessageBox.Show("NO SE ENCONTRÓ NINGÚN PRODUCTO CON EL CÓDIGO INGRESADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (pro.CodTipoArticulo == 1 && pro.StockDisponible == 0m && !pro.Cotizacion)
		{
			MessageBox.Show("EL PRODUCTO SELECCIONADO NO TIENE STOCK", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		CodProducto = pro.CodProducto;
		txtReferencia.Text = pro.Referencia;
		txtDescripcion.Text = pro.Descripcion;
		txtUnidad.Text = pro.UnidadDescrip;
		CargaUnidades(cmbUnidad);
		stock = pro.StockDisponible;
		txtStock.Text = pro.StockDisponible.ToString();
		txtcodlinea.Text = pro.codli.ToString();
		txtcodfamilia.Text = pro.codfami.ToString();
		txtControlStock.Text = "";
		txtCantidad.Text = "";
		if (Moneda == 2)
		{
			txtPrecio.Text = pro.PrecioVentaSoles.ToString();
		}
		else if (Moneda == 1)
		{
			txtPrecio.Text = pro.PrecioVenta.ToString();
		}
		txtDscto2.Text = "";
		txtDscto3.Text = "";
		txtPrecioNeto.Text = "";
		changeimporte = false;
		switch (pro.CodControlStock)
		{
		case 1:
			txtControlStock.Enabled = false;
			txtCantidad.Enabled = true;
			break;
		case 2:
			txtControlStock.Enabled = true;
			txtCantidad.Enabled = true;
			break;
		case 3:
			txtControlStock.Enabled = true;
			txtCantidad.Enabled = false;
			txtCantidad.Text = "01";
			break;
		case 4:
			txtControlStock.Enabled = false;
			txtCantidad.Enabled = false;
			txtCantidad.Text = "01";
			break;
		}
		txtUbicacion.Text = pro.SUbicacion;
		txtStockMinimo.Text = pro.StockMinimo.ToString();
		if (!Cotización)
		{
			txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(CodProducto, 0, Convert.ToInt32(cmbUnidad.SelectedValue)).ToString();
		}
		else
		{
			txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProductoCotizacion(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue)).ToString();
		}
	}

	private void txtDscto1_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.00";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text):#,##0.00}";
			changeimporte = false;
		}
		ProcessTabKey(forward: true);
		btnGuardar.Focus();
	}

	private void txtDscto1_Leave(object sender, EventArgs e)
	{
		if (!(txtPrecio.Text != ""))
		{
			return;
		}
		if (txtDscto1.Text == "")
		{
			txtDscto1.Text = "0.00";
			return;
		}
		if (Convert.ToDecimal(txtDscto1.Text) < 0m)
		{
			MessageBox.Show("Descuento No Permitido, Verifique Dato!!!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			txtDscto1.Focus();
		}
		else if (pro.CodProducto > 0 && manipulado.Name != txtPrecio.Name)
		{
			if (Moneda == 2)
			{
				txtPrecio.Text = $"{Convert.ToDecimal(Convert.ToDecimal(txtPrecio.Text) - Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtDscto1.Text) / 100m):#,##0.00}";
			}
			else if (Moneda == 1)
			{
				txtPrecio.Text = $"{Convert.ToDecimal(txtPrecio.Text):#,##0.00}";
			}
		}
		double PrecioConDescuento = (Convert.ToDouble(PrecioProducto) - Convert.ToDouble(txtDscto1.Text)) * Convert.ToDouble(txtCantidad.Text);
		txtPrecioNeto.Text = $"{PrecioConDescuento:#,##0.00}";
		txtPrecio.Text = $"{Convert.ToDouble(PrecioProducto) - Convert.ToDouble(txtDscto1.Text):#,##0.00}";
		changeimporte = false;
	}

	private void txtPrecioNeto_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar == '\r' && txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.00";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.00";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.00";
			}
			if (txtCantidad.Text != "")
			{
				txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text):#,##0.00}";
			}
			ProcessTabKey(forward: true);
		}
	}

	private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar == '\r' && txtCantidad.Text != "")
		{
			btnGuardar.Focus();
		}
	}

	private void txtCantidad_Leave(object sender, EventArgs e)
	{
		if (Procede == 4 || Procede == 2)
		{
			if (Procede == 2)
			{
				if (txtCantidad.Text != "")
				{
					if (Convert.ToDecimal(txtCantidad.Text) == 0m)
					{
						MessageBox.Show("Ingrese una cantidad mayor a 0!", "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						txtCantidad.Focus();
					}
					if (txtPrecio.Text.Trim() != "")
					{
						if (txtDscto1.Text == "")
						{
							txtDscto1.Text = "0.00";
						}
						if (txtDscto2.Text == "")
						{
							txtDscto2.Text = "0.00";
						}
						if (txtDscto3.Text == "")
						{
							txtDscto3.Text = "0.00";
						}
						txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.00}";
						changeimporte = false;
					}
				}
				else
				{
					txtCantidad.Focus();
				}
			}
			else if (Procede == 4)
			{
				if (txtCantidad.Text != "" && txtCantidad.Text != "0.00" && txtPrecio.Text.Trim() != "")
				{
					if (txtDscto1.Text == "")
					{
						txtDscto1.Text = "0.00";
					}
					if (txtDscto2.Text == "")
					{
						txtDscto2.Text = "0.00";
					}
					if (txtDscto3.Text == "")
					{
						txtDscto3.Text = "0.00";
					}
					txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text):#,##0.00}";
					changeimporte = false;
					if (Convert.ToDecimal(txtCantidad.Text) <= Convert.ToDecimal(txtStock.Text))
					{
						cmbdisponibilidad.SelectedIndex = 0;
						lbldias.Visible = false;
						txtdias.Visible = false;
					}
					else
					{
						cmbdisponibilidad.SelectedIndex = 1;
					}
					CalcularGanancia();
				}
			}
			else if (txtCantidad.Text != "")
			{
				if (Convert.ToDecimal(txtCantidad.Text) == 0m)
				{
					txtCantidad.Focus();
				}
				if (txtPrecio.Text.Trim() != "")
				{
					if (txtDscto1.Text == "")
					{
						txtDscto1.Text = "0.00";
					}
					if (txtDscto2.Text == "")
					{
						txtDscto2.Text = "0.00";
					}
					if (txtDscto3.Text == "")
					{
						txtDscto3.Text = "0.00";
					}
					txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.00}";
					changeimporte = false;
					if (Convert.ToDecimal(txtCantidad.Text) <= Convert.ToDecimal(txtStock.Text))
					{
						cmbdisponibilidad.SelectedIndex = 0;
						lbldias.Visible = false;
						txtdias.Visible = false;
					}
					else
					{
						cmbdisponibilidad.SelectedIndex = 1;
					}
				}
			}
			else
			{
				txtCantidad.Focus();
			}
		}
		else
		{
			if (Procede == 4)
			{
				return;
			}
			if (txtCantidad.Text == "")
			{
				txtCantidad.Focus();
			}
			else
			{
				if (!(txtPrecio.Text != ""))
				{
					return;
				}
				double StockDisponible = Convert.ToDouble(txtStock.Text.Trim());
				double CantidadAVender = Convert.ToDouble(txtCantidad.Text.Trim());
				double StockMinimo = Convert.ToDouble(txtStockMinimo.Text.Trim());
				if (StockDisponible - CantidadAVender <= StockMinimo)
				{
					DialogResult dr = MessageBox.Show("El producto ha llegado a su stock mínimo con la cantidad ingresada, ¿Desea agregarlo de todas maneras?", "Agregar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
					if (dr == DialogResult.No)
					{
						Close();
					}
				}
				if (txtDscto1.Text == "")
				{
					txtDscto1.Text = "0.00";
				}
				if (txtDscto2.Text == "")
				{
					txtDscto2.Text = "0.00";
				}
				if (txtDscto3.Text == "")
				{
					txtDscto3.Text = "0.00";
				}
				txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text):#,##0.00}";
				changeimporte = false;
			}
		}
	}

	private void CalcularGanancia()
	{
		try
		{
			double precioUnitario = Convert.ToDouble(txtPrecio.Text.Trim());
			double preciocompra = Convert.ToDouble(txtUltimoPrecioCompra.Text.Trim());
			double totalcosto = Convert.ToDouble(txttotalcosto.Text.Trim());
			double Ganancia = (precioUnitario - totalcosto) / 1.18;
			txtgananciamonto.Text = Math.Round(Convert.ToDouble(txtCantidad.Text) * Ganancia, 2).ToString();
			if (preciocompra == 0.0 && totalcosto == 0.0)
			{
				txtgananciaporcentaje.Text = "100";
			}
			else
			{
				txtgananciaporcentaje.Text = Math.Round(Ganancia / (preciocompra / 1.18) * 100.0, 2).ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			decimal sumdet = default(decimal);
			if (txtCantidad.Text != "" && !cmbUnidad.Text.ToString().Contains("SERVICIOS") && cmbdisponibilidad.SelectedIndex == 0 && Convert.ToDecimal(txtCantidad.Text) > Convert.ToDecimal(txtStock.Text))
			{
				MessageBox.Show("Cantidad debe ser menor al stock", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (Procede == 1)
			{
				frmNotaSalida form = (frmNotaSalida)Application.OpenForms["frmNotaSalida"];
				puInicio = Convert.ToDecimal(txtPrecio.Text);
				decimal bruto = Convert.ToDecimal(txtCantidad.Text) * puInicio;
				decimal montodescuento = ((!(Convert.ToDecimal(txtDscto1.Text) > 0m)) ? (bruto - Convert.ToDecimal(txtPrecioNeto.Text)) : (puInicio - Convert.ToDecimal(txtPrecio.Text)));
				decimal precioventa;
				decimal valorventa;
				if (pro.ConIgv)
				{
					precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
					decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
					valorventa = precioventa / factorigv;
				}
				else
				{
					valorventa = Convert.ToDecimal(txtPrecioNeto.Text);
					precioventa = valorventa;
				}
				decimal precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
				decimal valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
				decimal igv = precioventa - valorventa;
				decimal maxPorcDescto = ((!(txtUltimoPrecioCompra.Text != "")) ? default(decimal) : Convert.ToDecimal(txtUltimoPrecioCompra.Text));
				if (form.dgvDetalle.Rows.Count < 1000)
				{
					if (Proceso == 1)
					{
						string Unidad = cmbUnidad.Text;
						if (cmbUnidad.Text.Contains("-"))
						{
							string[] AUnidad = cmbUnidad.Text.Split('-');
							Unidad = AUnidad[0].Trim();
						}
						form.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, txtUnd.Text, Unidad, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), "", puInicio, bruto, Convert.ToDouble(txtDscto1.Text), "", "", montodescuento, valorventa, igv, precioventa, precioreal, valorreal, maxPorcDescto);
						limpiarformulario();
						if (Seleccion == 2)
						{
							Close();
						}
					}
					else if (Proceso == 2)
					{
						form.dgvDetalle.CurrentRow.SetValues("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), "", puInicio, bruto, Convert.ToDouble(txtDscto1.Text), "", "", montodescuento, valorventa, igv, precioventa, precioreal, valorreal, maxPorcDescto);
						limpiarformulario();
						Close();
					}
				}
				else
				{
					MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else if (Procede == 2 || Procede == 42)
			{
				frmVenta form2 = (frmVenta)Application.OpenForms["frmVenta"];
				form2.btnEliminar.Enabled = true;
				decimal bruto = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
				decimal montodescuento = bruto - (bruto - Convert.ToDecimal(txtDscto1.Text) * Convert.ToDecimal(txtCantidad.Text));
				decimal precioventa;
				decimal valorventa;
				if (pro.TipoImpuesto == 1)
				{
					precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
					decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
					valorventa = precioventa / factorigv;
				}
				else
				{
					valorventa = Convert.ToDecimal(txtPrecioNeto.Text);
					precioventa = valorventa;
				}
				decimal precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
				decimal valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
				decimal igv = precioventa - valorventa;
				decimal maxPorcDescto = ((!(txtUltimoPrecioCompra.Text != "")) ? default(decimal) : Convert.ToDecimal(txtUltimoPrecioCompra.Text));
				if (form2.dgvDetalle.Rows.Count < 18)
				{
					if (Proceso == 1)
					{
						string Unidad2 = cmbUnidad.Text;
						if (cmbUnidad.Text.Contains("-"))
						{
							string[] AUnidad2 = cmbUnidad.Text.Split('-');
							Unidad2 = AUnidad2[0].Trim();
						}
						if (txtDscto2.Text == "")
						{
							txtDscto2.Text = "0";
							if (txtDscto3.Text == "")
							{
								txtDscto3.Text = "0";
							}
						}
						form2.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, txtUnd.Text, Unidad2, "", Convert.ToDecimal(txtCantidad.Text), Convert.ToDecimal(txtPrecio.Text), bruto, Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), montodescuento, valorventa, igv, precioventa, pro.CodSunat, precioreal, valorreal, Convert.ToDecimal(txtStock.Text), maxPorcDescto);
						form2.calculatotales();
						limpiarformulario();
						if (Seleccion == 2)
						{
							Close();
						}
					}
					else if (Proceso == 2)
					{
						form2.dgvDetalle.CurrentRow.SetValues("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text, Convert.ToDecimal(txtCantidad.Text), Convert.ToDecimal(puInicio), bruto, Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), montodescuento, valorventa, igv, precioventa, pro.CodSunat, precioreal, valorreal, Convert.ToDecimal(txtStock.Text), maxPorcDescto);
						form2.calculatotales();
						limpiarformulario();
						Close();
					}
				}
				else
				{
					MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else if (Procede == 3)
			{
				frmOrdenVenta form3 = (frmOrdenVenta)Application.OpenForms["frmOrdenVenta"];
				decimal bruto = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
				decimal montodescuento = bruto - Convert.ToDecimal(txtPrecioNeto.Text);
				decimal precioventa;
				decimal valorventa;
				if (pro.CodSunat == "10")
				{
					precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
					decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
					valorventa = precioventa / factorigv;
					form3.montogravadas += valorventa;
				}
				else
				{
					valorventa = Convert.ToDecimal(txtPrecioNeto.Text);
					if (pro.CodSunat == "21")
					{
						form3.montogratuitas += valorventa;
						precioventa = default(decimal);
					}
					else
					{
						if (pro.CodSunat == "30" || pro.CodSunat == "31" || pro.CodSunat == "32" || pro.CodSunat == "33" || pro.CodSunat == "34" || pro.CodSunat == "35" || pro.CodSunat == "36")
						{
							form3.montoinafectas += valorventa;
						}
						else if (pro.CodSunat == "20")
						{
							form3.montoexoneradas += valorventa;
						}
						precioventa = valorventa;
					}
				}
				decimal precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
				decimal valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
				decimal igv = precioventa - valorventa;
				if (Proceso == 1)
				{
					string Unidad3 = cmbUnidad.Text;
					if (cmbUnidad.Text.Contains("-"))
					{
						string[] AUnidad3 = cmbUnidad.Text.Split('-');
						Unidad3 = AUnidad3[0].Trim();
					}
					form3.dgvDetalle.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, txtUnd.Text, Unidad3, Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, 0, 0, 0, montodescuento, valorventa, igv, precioventa, valorreal, precioreal, precioventa, 0, 0, pro.CodSunat, frmLogin.iCodAlmacen, 0, 0, 0, txtcodlinea.Text, txtcodfamilia.Text);
					form3.lblCantidadProductos.Text = "Productos Agregados: " + form3.dgvDetalle.RowCount;
					limpiarformulario();
					if (Seleccion == 2)
					{
						Close();
					}
					txtCodigo.Clear();
				}
				else if (Proceso == 2)
				{
					form3.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, Convert.ToDouble(txtDscto1.Text), Convert.ToDouble(txtDscto2.Text), Convert.ToDouble(txtDscto3.Text), montodescuento, valorventa, igv, precioventa, precioreal, valorreal);
					limpiarformulario();
					Close();
				}
			}
			else if (Procede == 4)
			{
				frmGestionCotizacion form4 = (frmGestionCotizacion)Application.OpenForms["frmGestionCotizacion"];
				form4.btnEditar.Enabled = true;
				form4.btnEliminar.Enabled = true;
				decimal bruto = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
				decimal montodescuento = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtDscto1.Text);
				if (Cotización)
				{
					if (CodAlmacen == 1)
					{
						CodAlmacen = 1;
					}
					else
					{
						CodAlmacen = 3;
					}
				}
				decimal precioventa;
				decimal valorventa;
				if (pro.TipoImpuesto == 1)
				{
					precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
					decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
					valorventa = precioventa / factorigv;
				}
				else
				{
					valorventa = Convert.ToDecimal(txtPrecioNeto.Text);
					precioventa = valorventa;
				}
				decimal precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
				decimal valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
				decimal igv = precioventa - valorventa;
				decimal maxPorcDescto = Convert.ToDecimal(txtUltimoPrecioCompra.Text);
				string descripcionPro = "";
				descripcionPro = pro.Descripcion;
				string dias = "";
				dias = ((!(Convert.ToDecimal(txtCantidad.Text) > Convert.ToDecimal(txtStock.Text))) ? cmbdisponibilidad.SelectedItem.ToString() : ((!(txtdias.Text != "")) ? cmbdisponibilidad.SelectedItem.ToString() : (txtdias.Text + " " + cmbdisponibilidad.SelectedItem.ToString())));
				if (Proceso == 1)
				{
					string Unidad4 = cmbUnidad.Text;
					if (cmbUnidad.Text.Contains("-"))
					{
						string[] AUnidad4 = cmbUnidad.Text.Split('-');
						Unidad4 = AUnidad4[0].Trim();
					}
					form4.dgvDetalle.Rows.Add(0, 0, "", pro.CodProducto, pro.Referencia, descripcionPro, txtUnd.Text, Unidad4, Convert.ToDecimal(txtCantidad.Text), precioreal, Convert.ToDouble(txtPrecio.Text), montodescuento, valorventa, igv, precioventa, precioventa, Convert.ToDecimal(txtStock.Text), dias, Convert.ToDouble(txtgananciaporcentaje.Text), Convert.ToDouble(txtgananciamonto.Text), Convert.ToDouble(txttotalcosto.Text), "", Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), valorreal, maxPorcDescto, "", "", pro.codmarca, pro.Cotizacion, txtcodtipoprecio.Text, CodAlmacen);
					form4.calculatotales();
					limpiarformulario();
					if (Seleccion == 2)
					{
						Close();
					}
				}
				else if (Proceso == 2)
				{
					form4.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text, Convert.ToDecimal(txtCantidad.Text), puInicio, bruto, Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), montodescuento, valorventa, igv, precioventa, precioreal, valorreal, Convert.ToDecimal(txtStock.Text), maxPorcDescto);
					limpiarformulario();
					form4.actualizaimportes();
					Close();
				}
			}
			else if (Procede == 5)
			{
				frmVentaSeparacionAr form5 = (frmVentaSeparacionAr)Application.OpenForms["frmVentaSeparacionAr"];
				form5.btnEditar.Enabled = true;
				form5.btnEliminar.Enabled = true;
				decimal bruto = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
				decimal montodescuento = bruto - bruto * (1m - Convert.ToDecimal(txtDscto1.Text) / 100m);
				decimal precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
				decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
				decimal valorventa = precioventa / factorigv;
				decimal precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
				decimal valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
				decimal igv = precioventa - valorventa;
				decimal maxPorcDescto = Convert.ToDecimal(txtUltimoPrecioCompra.Text);
				if (form5.dgvDetalle.Rows.Count < 10)
				{
					if (Proceso == 1)
					{
						string Unidad5 = cmbUnidad.Text;
						if (cmbUnidad.Text.Contains("-"))
						{
							string[] AUnidad5 = cmbUnidad.Text.Split('-');
							Unidad5 = AUnidad5[0].Trim();
						}
						form5.dgvDetalle.Rows.Add("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, txtUnd.Text, Unidad5, txtControlStock.Text, Convert.ToDecimal(txtCantidad.Text), Convert.ToDecimal(txtPrecio.Text), bruto, Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), montodescuento, valorventa, igv, precioventa, precioreal, valorreal, Convert.ToDecimal(txtStock.Text), maxPorcDescto);
						form5.calculatotales();
						limpiarformulario();
						Close();
					}
					else if (Proceso == 2)
					{
						form5.dgvDetalle.CurrentRow.SetValues("", "", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text, Convert.ToDecimal(txtCantidad.Text), Convert.ToDecimal(puInicio), bruto, Convert.ToDecimal(txtDscto1.Text), Convert.ToDecimal(txtDscto2.Text), Convert.ToDecimal(txtDscto3.Text), montodescuento, valorventa, igv, precioventa, precioreal, valorreal, Convert.ToDecimal(txtStock.Text), maxPorcDescto);
						form5.calculatotales();
						limpiarformulario();
						Close();
					}
				}
				else
				{
					MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
			else
			{
				if (Procede != 41)
				{
					return;
				}
				frmConsultorExt form6 = (frmConsultorExt)Application.OpenForms["frmConsultorExt"];
				decimal bruto = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text);
				decimal montodescuento = bruto - Convert.ToDecimal(txtPrecioNeto.Text);
				decimal precioventa;
				decimal valorventa;
				if (pro.ConIgv)
				{
					precioventa = Convert.ToDecimal(txtPrecioNeto.Text);
					decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
					valorventa = precioventa / factorigv;
				}
				else
				{
					valorventa = Convert.ToDecimal(txtPrecioNeto.Text);
					precioventa = valorventa;
				}
				decimal precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
				decimal valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
				decimal igv = precioventa - valorventa;
				if (Proceso == 1)
				{
					form6.dgvDetalle.Rows.Add(0, pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), 0, 0, Convert.ToDouble(txtPrecio.Text), bruto, Convert.ToDouble(txtDscto1.Text), Convert.ToDouble(txtDscto2.Text), Convert.ToDouble(txtDscto3.Text), montodescuento, valorventa, igv, precioventa, precioreal, valorreal, 0);
					limpiarformulario();
					if (Seleccion == 2)
					{
						Close();
					}
				}
				else if (Proceso == 2)
				{
					form6.dgvDetalle.CurrentRow.SetValues(0, pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), 0, 0, Convert.ToDouble(txtPrecio.Text), bruto, Convert.ToDouble(txtDscto1.Text), Convert.ToDouble(txtDscto2.Text), Convert.ToDouble(txtDscto3.Text), montodescuento, valorventa, igv, precioventa, precioreal, valorreal, 0);
					limpiarformulario();
					Close();
				}
				else if (Proceso == 3)
				{
					form6.dgvDetalle.CurrentRow.SetValues(codDetalle, pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), 0, 0, Convert.ToDouble(txtPrecio.Text), bruto, Convert.ToDouble(txtDscto1.Text), Convert.ToDouble(txtDscto2.Text), Convert.ToDouble(txtDscto3.Text), montodescuento, valorventa, igv, precioventa, precioreal, valorreal, 0);
					limpiarformulario();
					Close();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Ingrese Datos Correctamente!" + ex.Message, "Detalle Articulo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void CargaFilaDetalle()
	{
	}

	private void txtDscto2_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.00";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.00";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.00";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text):#,##0.00}";
			changeimporte = false;
		}
		ProcessTabKey(forward: true);
		btnGuardar.Focus();
	}

	private void txtDscto2_Leave(object sender, EventArgs e)
	{
		if (!(txtPrecio.Text != ""))
		{
			return;
		}
		if (txtDscto2.Text == "")
		{
			txtDscto2.Text = "0.00";
			return;
		}
		if (Convert.ToDecimal(txtDscto2.Text) < 0m)
		{
			MessageBox.Show("Descuento No Permitido, Verifique Dato!!!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			txtDscto1.Focus();
		}
		else if (pro.CodProducto > 0 && manipulado.Name != txtPrecio.Name)
		{
			if (Moneda == 2)
			{
				txtPrecio.Text = $"{Convert.ToDecimal(Convert.ToDecimal(txtPrecio.Text) + Convert.ToDecimal(txtPrecio.Text) * Convert.ToDecimal(txtDscto2.Text) / 100m):#,##0.00}";
			}
			else if (Moneda == 1)
			{
				txtPrecio.Text = $"{Convert.ToDecimal(txtPrecio.Text):#,##0.00}";
			}
		}
		double PrecioConAumento = (Convert.ToDouble(PrecioProducto) + Convert.ToDouble(txtDscto2.Text)) * (double)Convert.ToInt32(txtCantidad.Text);
		txtPrecioNeto.Text = $"{PrecioConAumento:#,##0.00}";
		txtPrecio.Text = $"{Convert.ToDouble(PrecioProducto) + Convert.ToDouble(txtDscto2.Text):#,##0.00}";
		changeimporte = false;
	}

	private void txtDscto3_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.00";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.00";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.00";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.00}";
			changeimporte = false;
		}
		ProcessTabKey(forward: true);
	}

	private void txtDscto3_Leave(object sender, EventArgs e)
	{
		if (txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.00";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.00";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.00";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.00}";
			changeimporte = false;
		}
	}

	private void txtControlStock_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void frmDetalleSalida_Shown(object sender, EventArgs e)
	{
		if (Seleccion == 2)
		{
			txtCantidad.Focus();
		}
		else if (Proceso == 1)
		{
			txtReferencia.Focus();
		}
		else if (Proceso == 2)
		{
			txtReferencia.ReadOnly = true;
			txtCantidad.Focus();
		}
		changeimporte = false;
	}

	private void limpiarformulario()
	{
		foreach (Control c in panel1.Controls)
		{
			if (c is TextBox)
			{
				c.Text = "";
			}
			cmbUnidad.SelectedIndex = -1;
		}
		txtReferencia.Focus();
	}

	private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void txtPrecio_Leave(object sender, EventArgs e)
	{
		if (!(txtPrecio.Text != "") || !(Convert.ToDecimal(txtPrecio.Text) != 0m))
		{
			return;
		}
		if (txtDscto1.Text == "")
		{
			txtDscto1.Text = "0.00";
		}
		if (!(txtCantidad.Text != "") || !(Convert.ToDecimal(txtCantidad.Text) != 0m))
		{
			return;
		}
		double precioUnitario = Convert.ToDouble(txtPrecio.Text.Trim());
		double ultimoPrecioCompra = Convert.ToDouble(txtUltimoPrecioCompra.Text.Trim());
		if (precioUnitario < ultimoPrecioCompra)
		{
			MessageBox.Show("El PRECIO UNITARIO del producto debe ser MAYOR O IGUAL al ÚLTIMO PRECIO DE COMPRA", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			txtPrecio.Focus();
			return;
		}
		if (Procede == 4)
		{
			CalcularGanancia();
		}
		txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text):#,##0.00}";
		changeimporte = false;
	}

	private void txtReferencia_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.F1)
			{
				frmProductosLista frm = new frmProductosLista();
				frm.Proceso = Proceso;
				frm.Procede = Procede;
				frm.CodLista = Codlista;
				frm.consultorext = consultorext;
				frm.CodVendedor = CodVendedor;
				frm.tc = tc;
				frm.Moneda = Moneda;
				frm.productosfactura = productoscargados;
				frm.productoscotizacion = productoscotizados;
				frm.productosNotaSalida = productosNotaSalida;
				frm.alma = frmLogin.iCodAlmacen;
				frm.codproveedor = codProveedor;
				frm.codtrans = codTipodoc;
				frm.ventasinafectarstock = ventasinafectaciondestock;
				if (frm.ShowDialog() == DialogResult.OK)
				{
					CodAlmacen = frm.GetCodigoAlmacen();
					Cotización = Convert.ToBoolean(frm.GetCodigoCotizacion());
					txtCodigo.Text = "";
					txtCodigo.Text = frm.GetCodigoProducto().ToString();
					if (cmbUnidad.Items.Count > 0)
					{
						cmbUnidad_SelectionChangeCommitted(sender, e);
					}
				}
			}
			if (e.KeyCode != Keys.Return || !(txtReferencia.Text != ""))
			{
				return;
			}
			CodAlmacen = 0;
			Cotización = false;
			if (verificarDuplicidadProducto(tipoCarga: true))
			{
				return;
			}
			if (Procede == 2)
			{
				pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtReferencia.Text), alma, 2, 0, 0);
			}
			else if (Procede == 4)
			{
				pro = AdmPro.CargaProductoDetalleCotizacion(Convert.ToInt32(txtReferencia.Text), 0, frmLogin.iCodAlmacen, 2, Codlista);
			}
			else
			{
				pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtReferencia.Text), frmLogin.iCodAlmacen, 2, Codlista, 0);
			}
			if (pro == null)
			{
				MessageBox.Show("NO SE ENCONTRÓ NINGÚN PRODUCTO CON EL CÓDIGO INGRESADO", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (pro.StockDisponible == 0m && Procede != 4)
			{
				MessageBox.Show("EL PRODUCTO SELECCIONADO NO TIENE STOCK", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			CodProducto = pro.CodProducto;
			txtReferencia.Text = pro.Referencia;
			txtDescripcion.Text = pro.Descripcion;
			txtUnidad.Text = pro.UnidadDescrip;
			CargaUnidades(cmbUnidad);
			stock = pro.StockDisponible;
			txtStock.Text = pro.StockDisponible.ToString();
			CodAlmacen = pro.CodAlmacen;
			txtControlStock.Text = "";
			txtCantidad.Text = "0.00";
			if (Moneda == 2)
			{
				txtPrecio.Text = pro.PrecioVentaSoles.ToString();
			}
			else if (Moneda == 1)
			{
				txtPrecio.Text = pro.PrecioVenta.ToString();
			}
			PrecioProducto = pro.PrecioVenta;
			txtDscto2.Text = "";
			txtDscto3.Text = "";
			txtPrecioNeto.Text = "";
			changeimporte = false;
			switch (pro.CodControlStock)
			{
			case 1:
				txtControlStock.Enabled = false;
				txtCantidad.Enabled = true;
				break;
			case 2:
				txtControlStock.Enabled = true;
				txtCantidad.Enabled = true;
				break;
			case 3:
				txtControlStock.Enabled = true;
				txtCantidad.Enabled = false;
				txtCantidad.Text = "01";
				break;
			case 4:
				txtControlStock.Enabled = false;
				txtCantidad.Enabled = false;
				txtCantidad.Text = "01";
				break;
			}
			if (cmbUnidad.Items.Count > 0)
			{
				cmbUnidad_SelectionChangeCommitted(sender, e);
			}
			txtUbicacion.Text = pro.SUbicacion;
			txtStockMinimo.Text = pro.StockMinimo.ToString();
			txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(CodProducto, 0, Convert.ToInt32(cmbUnidad.SelectedValue)).ToString();
		}
		catch (Exception ex)
		{
			MessageBox.Show("OCURRIÓ UN ERROR DURANTE LA OPERACIÓN: " + ex.Message, "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	public void CargaProducto(int CodPro)
	{
		try
		{
			pro = AdmPro.CargaProductoDetalle(CodPro, frmLogin.iCodAlmacen, 2, Codlista, 0);
			if (pro != null)
			{
				CodProducto = pro.CodProducto;
				txtReferencia.Text = pro.Referencia;
				txtDescripcion.Text = pro.Descripcion;
				txtUnidad.Text = pro.UnidadDescrip;
				txtcodfamilia.Text = pro.codfami.ToString();
				txtcodlinea.Text = pro.codli.ToString();
				CargaUnidades(cmbUnidad);
				stock = pro.StockDisponible;
				txtStock.Text = pro.StockDisponible.ToString();
				txtControlStock.Text = "";
				txtCantidad.Text = "0.00";
				if (pro.PrecioVariable)
				{
				}
				txtPrecio.Text = $"{0.0:#,##0.00}";
				if (pro.Oferta)
				{
					txtDscto1.Text = pro.PDescuento.ToString();
					txtDscto1.ReadOnly = true;
				}
				else
				{
					txtDscto1.Text = "";
				}
				txtDscto2.Text = "";
				txtDscto3.Text = "";
				if (pro.Oferta)
				{
					txtPrecioNeto.Text = pro.PrecioOferta.ToString();
				}
				else
				{
					txtPrecioNeto.Text = pro.PrecioVenta.ToString();
				}
				txtPrecioNeto.Text = "";
				changeimporte = false;
				switch (pro.CodControlStock)
				{
				case 1:
					txtControlStock.Enabled = false;
					txtCantidad.Enabled = true;
					break;
				case 2:
					txtControlStock.Enabled = true;
					txtCantidad.Enabled = true;
					break;
				case 3:
					txtControlStock.Enabled = true;
					txtCantidad.Enabled = false;
					txtCantidad.Text = "01";
					break;
				case 4:
					txtControlStock.Enabled = false;
					txtCantidad.Enabled = false;
					txtCantidad.Text = "01";
					break;
				}
				cmbUnidad.Enabled = true;
				btnGuardar.Enabled = true;
			}
			else
			{
				txtReferencia.Focus();
				txtReferencia.Text = "";
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	public bool BuscaProducto()
	{
		return false;
	}

	private void frmDetalleSalida_Load(object sender, EventArgs e)
	{
		cmbUnidad.Enabled = true;
		if (Procede == 41)
		{
			txtUltimoPrecioCompra.Text = "0";
		}
		if (Procede == 4)
		{
			btnverstock.Visible = true;
			lbldias.Visible = false;
			txtdias.Visible = false;
			lbldisponibilidad.Visible = true;
			cmbdisponibilidad.Visible = true;
			txtPrecio.ReadOnly = true;
			chbverganancia.Visible = true;
			if (frmLogin.iNivelUser == 1)
			{
				chbverganancia.Checked = true;
				lblflete.Visible = true;
				txtflete.Visible = true;
				lbldesestiva.Visible = true;
				txtdesestiva.Visible = true;
				lblultimoprecio.Visible = true;
				txtultimoprecio.Visible = true;
				lbltotalcosto.Visible = true;
				txttotalcosto.Visible = true;
				lblgananciamonto.Visible = true;
				lblmarguenporcentaje.Visible = true;
				txtgananciamonto.Visible = true;
				txtgananciaporcentaje.Visible = true;
			}
			cmbdisponibilidad.SelectedIndex = -1;
			checkBox1.Checked = false;
		}
	}

	private void CargaUnidades(ComboBox combo)
	{
		cmbUnidad.Enabled = true;
		if (Procede == 12)
		{
			combo.DataSource = AdmPro.MuestraUnidadesEquivalentesCompra(CodProducto, frmLogin.iCodAlmacen);
			combo.DisplayMember = "descripcion";
			combo.ValueMember = "codUnidadMedida";
			combo.SelectedValue = pro.CodUnidadMedida;
		}
		else if (Procede == 4)
		{
			DataTable unidad = new DataTable();
			unidad = (DataTable)(combo.DataSource = AdmPro.MuestraUnidadesEquivalentesVentaCotizacion(CodProducto, frmLogin.iCodAlmacen, (Convert.ToInt32(Cotización) != 0) ? 1 : 0));
			combo.DisplayMember = "descripcion";
			combo.ValueMember = "codUnidadEquivalente";
			combo.SelectedValue = pro.CodUnidadMedida;
			txtcodtipoprecio.Text = Convert.ToString(unidad.Rows[0]["codtipo"]);
		}
		else
		{
			combo.DataSource = AdmPro.MuestraUnidadesEquivalentesVenta(CodProducto, frmLogin.iCodAlmacen);
			combo.DisplayMember = "descripcion";
			combo.ValueMember = "codUnidadEquivalente";
			combo.SelectedValue = pro.CodUnidadMedida;
		}
		if (combo.Items.Count > 0)
		{
			combo.SelectedIndex = 0;
		}
		txtStock.Visible = true;
		label4.Visible = true;
	}

	public void cmbUnidad_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			if (Procede == 7)
			{
				undbase = AdmPro.UnidadBase(CodProducto, frmLogin.iCodAlmacen);
				uniequi = AdmPro.PrecioVenta(Convert.ToInt32(cmbUnidad.SelectedValue), frmLogin.iCodAlmacen);
				if (uniequi.CodUnidad == undbase)
				{
					factor = AdmPro.FactorProducto(CodProducto, undbase, uniequi.CodUnidad, 0);
					txtCantidad.Text = (cant * factor).ToString();
				}
				else
				{
					factor = AdmPro.FactorProducto(CodProducto, undbase, uniequi.CodUnidad, 1);
					txtCantidad.Text = (cant / factor).ToString();
				}
				txtStock.Text = $"{uniequi.Stock:###0.0000}";
				txtUnd.Text = uniequi.CodUnidad.ToString();
				txtPrecio.Text = $"{uniequi.Precio:#,##0.00000}";
				txtPrecioNeto.Text = "0.00";
				precio_Old = Convert.ToDecimal(txtPrecio.Text);
				puInicio = Convert.ToDecimal(txtPrecio.Text);
				btnGuardar.Enabled = true;
				txtCantidad.Enabled = true;
				txtCantidad.ReadOnly = true;
				txtPrecio.Enabled = true;
				txtDscto1.Enabled = true;
				txtDscto2.Enabled = true;
				txtDscto3.Enabled = true;
				txtPrecioNeto.Enabled = true;
				if (frmLogin.iCodUser == 5)
				{
					txtPrecioNeto.Enabled = true;
				}
			}
			else if (Procede == 12)
			{
				pro = AdmPro.PrecioPromedio(CodProducto, frmLogin.iCodAlmacen);
				decimal a = Convert.ToDecimal(txtCantidad.Text);
				txtUnd.Text = cmbUnidad.SelectedValue.ToString();
				uniequi = AdmPro.Factor(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue), pro.CodUnidadMedida);
				decimal sto = stock / uniequi.Factor;
				txtStock.Text = $"{sto:#,##0.00}";
				txtPrecio.Text = $"{pro.PrecioProm * uniequi.Factor:#,##0.00000}";
				precio_Old = Convert.ToDecimal(txtPrecio.Text);
				puInicio = Convert.ToDecimal(txtPrecio.Text);
				a *= Convert.ToDecimal(txtPrecio.Text);
				txtPrecioNeto.Text = $"{a:#,##0.00}";
			}
			else
			{
				if (ventasinafectaciondestock)
				{
					uniequi = AdmPro.PrecioVentaSinStock(Convert.ToInt32(cmbUnidad.SelectedValue), frmLogin.iCodAlmacen);
				}
				else if (Cotización)
				{
					uniequi = AdmPro.PrecioVentaCotización(Convert.ToInt32(cmbUnidad.SelectedValue), frmLogin.iCodAlmacen, Convert.ToInt32(Cotización));
				}
				else if (Procede == 4)
				{
					uniequi = AdmPro.PrecioVenta(Convert.ToInt32(cmbUnidad.SelectedValue), CodAlmacen);
				}
				else
				{
					uniequi = AdmPro.PrecioVenta(Convert.ToInt32(cmbUnidad.SelectedValue), frmLogin.iCodAlmacen);
				}
				if (uniequi != null)
				{
					txtStock.Text = $"{uniequi.Stock:###0.0000}";
					txtUnd.Text = uniequi.CodUnidad.ToString();
					if (!cliEspecial)
					{
						txtPrecio.Text = $"{uniequi.Precio:#,##0.00000}";
						txtPrecio.Enabled = true;
						PrecioProducto = Convert.ToDouble(uniequi.Precio);
					}
					else
					{
						txtPrecio.Text = $"{uniequi.Precio:#,##0.00000}";
						txtPrecio.Enabled = true;
					}
					if (Procede != 4)
					{
						txtCantidad.Text = "0.00";
						txtCantidad.Focus();
					}
					txtPrecioNeto.Text = "0.00";
					precio_Old = Convert.ToDecimal(txtPrecio.Text);
					puInicio = Convert.ToDecimal(txtPrecio.Text);
					btnGuardar.Enabled = true;
					txtCantidad.Enabled = true;
					txtDscto1.Enabled = true;
					txtDscto2.Enabled = true;
					txtDscto3.Enabled = true;
					txtPrecioNeto.Enabled = false;
					if (frmLogin.iCodUser == 5)
					{
						txtPrecioNeto.Enabled = true;
					}
					if (!Cotización)
					{
						txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(CodProducto, 0, Convert.ToInt32(cmbUnidad.SelectedValue)).ToString();
					}
					else
					{
						txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProductoCotizacion(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue)).ToString();
					}
					if (Procede == 4)
					{
						txtcodtipoprecio.Text = uniequi.Tipo.ToString();
						if (CodCliente != 0)
						{
							txtultimoprecio.Text = AdmPro.UltimoPrecioVentaProductoCotizacion(CodCliente, CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue)).ToString();
						}
						else
						{
							txtultimoprecio.Text = "0";
						}
						if (!Cotización)
						{
							decimal flete = default(decimal);
							decimal desestiva = default(decimal);
							DataTable costos = AdmPro.CostoTotalProducto(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue));
							flete = Convert.ToDecimal(costos.Rows[0]["flete"]);
							desestiva = Convert.ToDecimal(costos.Rows[0]["desestiva"]);
							txtflete.Text = flete.ToString();
							txtdesestiva.Text = desestiva.ToString();
							txttotalcosto.Text = Math.Round(flete + desestiva + Convert.ToDecimal(txtUltimoPrecioCompra.Text), 2).ToString();
						}
						else
						{
							decimal flete2 = default(decimal);
							decimal desestiva2 = default(decimal);
							DataTable costos2 = AdmPro.CostoTotalProductoCotizacion(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue));
							flete2 = Convert.ToDecimal(costos2.Rows[0]["flete"]);
							desestiva2 = Convert.ToDecimal(costos2.Rows[0]["desestiva"]);
							txtflete.Text = flete2.ToString();
							txtdesestiva.Text = desestiva2.ToString();
							txttotalcosto.Text = Math.Round(flete2 + desestiva2 + Convert.ToDecimal(txtUltimoPrecioCompra.Text), 2).ToString();
						}
					}
				}
			}
			txtPrecio.Enabled = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtReferencia_Leave(object sender, EventArgs e)
	{
	}

	private void txtPrecioNeto_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void txtPrecioNeto_TextChanged(object sender, EventArgs e)
	{
		changeimporte = true;
	}

	private void txtPrecioNeto_Leave(object sender, EventArgs e)
	{
		if (txtPrecioNeto.Text != "" && changeimporte)
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.00";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.00";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.00";
			}
			if (txtCantidad.Text != "")
			{
				txtPrecio.Text = $"{Convert.ToDouble(txtPrecioNeto.Text) / (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0) / (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) / (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) / Convert.ToDouble(txtCantidad.Text):#,##0.00}";
				changeimporte = false;
			}
		}
	}

	private void txtPrecio_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			manipulado = (TextBox)sender;
		}
	}

	private void txtDscto1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			manipulado = (TextBox)sender;
		}
	}

	private void checkBox2_CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox2.Checked)
		{
			checkBox1.Checked = false;
			label7.Visible = true;
			txtDscto1.Visible = true;
			txtDscto2.Visible = false;
			label10.Visible = false;
			txtDscto1.Focus();
		}
		else
		{
			txtDscto1.Text = "0.00";
			txtDscto1.Visible = false;
			label7.Visible = false;
			txtPrecio.Text = $"{PrecioProducto:#,##0.00}";
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * (double)Convert.ToInt32(txtCantidad.Text):#,##0.00}";
		}
	}

	private void txtDscto2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			manipulado = (TextBox)sender;
		}
	}

	private void txtReferencia_ImeModeChanged(object sender, EventArgs e)
	{
	}

	private void cmbdisponibilidad_SelectionChangeCommitted(object sender, EventArgs e)
	{
		int cod = Convert.ToInt32(cmbdisponibilidad.SelectedIndex);
		if (cod == 4)
		{
			lbldias.Visible = true;
			txtdias.Visible = true;
		}
		else
		{
			lbldias.Visible = false;
			txtdias.Visible = false;
		}
	}

	private void chbverganancia_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			if (chbverganancia.Checked && frmLogin.iNivelUser != 1)
			{
				DialogResult dr = DialogResult.None;
				frmAutorizacion frm = new frmAutorizacion();
				dr = frm.ShowDialog();
				if (dr == DialogResult.OK && usuario_click != null)
				{
					lbldesestiva.Visible = true;
					txtdesestiva.Visible = true;
					lblflete.Visible = true;
					txtflete.Visible = true;
					lbltotalcosto.Visible = true;
					lblmarguenporcentaje.Visible = true;
					lblgananciamonto.Visible = true;
					txtgananciamonto.Visible = true;
					txtgananciaporcentaje.Visible = true;
					txttotalcosto.Visible = true;
				}
				else
				{
					chbverganancia.Checked = false;
				}
			}
			else if (chbverganancia.Checked && frmLogin.iNivelUser == 1)
			{
				lbldesestiva.Visible = true;
				txtdesestiva.Visible = true;
				lblflete.Visible = true;
				txtflete.Visible = true;
				lbltotalcosto.Visible = true;
				lblmarguenporcentaje.Visible = true;
				lblgananciamonto.Visible = true;
				txtgananciamonto.Visible = true;
				txtgananciaporcentaje.Visible = true;
				txttotalcosto.Visible = true;
			}
			else
			{
				lbldesestiva.Visible = false;
				txtdesestiva.Visible = false;
				lblflete.Visible = false;
				txtflete.Visible = false;
				lbltotalcosto.Visible = false;
				lblmarguenporcentaje.Visible = false;
				lblgananciamonto.Visible = false;
				txtgananciamonto.Visible = false;
				txtgananciaporcentaje.Visible = false;
				txttotalcosto.Visible = false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		if (checkBox1.Checked)
		{
			checkBox2.Checked = false;
			label7.Visible = false;
			txtDscto1.Visible = false;
			txtDscto2.Visible = true;
			label10.Visible = true;
			txtDscto2.Focus();
		}
		else
		{
			txtDscto2.Text = "0.00";
			txtDscto2.Visible = false;
			label10.Visible = false;
			txtPrecio.Text = $"{PrecioProducto:#,##0.00}";
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * (double)Convert.ToInt32(txtCantidad.Text):#,##0.00}";
		}
	}

	private void btnverstock_Click(object sender, EventArgs e)
	{
		frmMuestraStock frm = new frmMuestraStock();
		frm.CodProducto = CodProducto;
		frm.CodAlmacen = CodAlmacen;
		frm.ShowDialog();
	}

	private void txtReferencia_KeyUp(object sender, KeyEventArgs e)
	{
	}

	private void txtStock_TextChanged(object sender, EventArgs e)
	{
	}

	public static implicit operator frmDetalleSalida(frmGestionCotizacion v)
	{
		throw new NotImplementedException();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmDetalleSalida));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.panel1 = new System.Windows.Forms.Panel();
		this.btnverstock = new System.Windows.Forms.Button();
		this.txtultimoprecio = new System.Windows.Forms.TextBox();
		this.lblultimoprecio = new System.Windows.Forms.Label();
		this.label19 = new System.Windows.Forms.Label();
		this.txtcodtipoprecio = new System.Windows.Forms.TextBox();
		this.chbverganancia = new System.Windows.Forms.CheckBox();
		this.lbltotalcosto = new System.Windows.Forms.Label();
		this.txttotalcosto = new System.Windows.Forms.TextBox();
		this.lbldesestiva = new System.Windows.Forms.Label();
		this.txtdesestiva = new System.Windows.Forms.TextBox();
		this.lblflete = new System.Windows.Forms.Label();
		this.txtflete = new System.Windows.Forms.TextBox();
		this.lblgananciamonto = new System.Windows.Forms.Label();
		this.txtgananciamonto = new System.Windows.Forms.TextBox();
		this.lblmarguenporcentaje = new System.Windows.Forms.Label();
		this.txtgananciaporcentaje = new System.Windows.Forms.TextBox();
		this.lbldisponibilidad = new System.Windows.Forms.Label();
		this.cmbdisponibilidad = new System.Windows.Forms.ComboBox();
		this.checkBox2 = new System.Windows.Forms.CheckBox();
		this.lbldias = new System.Windows.Forms.Label();
		this.txtdias = new System.Windows.Forms.TextBox();
		this.txtStockMinimo = new System.Windows.Forms.TextBox();
		this.label20 = new System.Windows.Forms.Label();
		this.txtUbicacion = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtReferencia = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtStock = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtCantidad = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtPrecio = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.cmbUnidad = new System.Windows.Forms.ComboBox();
		this.txtDscto1 = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.txtUltimoPrecioCompra = new System.Windows.Forms.TextBox();
		this.txtPrecioNeto = new System.Windows.Forms.TextBox();
		this.txtPrecioDscto = new System.Windows.Forms.TextBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtcodlinea = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtDscto2 = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtDscto3 = new System.Windows.Forms.TextBox();
		this.label18 = new System.Windows.Forms.Label();
		this.txtcodfamilia = new System.Windows.Forms.TextBox();
		this.txtPrecioNetoDscto = new System.Windows.Forms.TextBox();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.label14 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.txtUnidad = new System.Windows.Forms.TextBox();
		this.txtControlStock = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.txtUnd = new System.Windows.Forms.TextBox();
		this.txtPrecioNetoDolares = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtPrecioMax = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		this.panel1.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
		this.groupBox1.Controls.Add(this.panel1);
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(2, 13);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(871, 219);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Ingresar Artículo";
		this.panel1.AutoScroll = true;
		this.panel1.Controls.Add(this.btnverstock);
		this.panel1.Controls.Add(this.txtultimoprecio);
		this.panel1.Controls.Add(this.lblultimoprecio);
		this.panel1.Controls.Add(this.label19);
		this.panel1.Controls.Add(this.txtcodtipoprecio);
		this.panel1.Controls.Add(this.chbverganancia);
		this.panel1.Controls.Add(this.lbltotalcosto);
		this.panel1.Controls.Add(this.txttotalcosto);
		this.panel1.Controls.Add(this.lbldesestiva);
		this.panel1.Controls.Add(this.txtdesestiva);
		this.panel1.Controls.Add(this.lblflete);
		this.panel1.Controls.Add(this.txtflete);
		this.panel1.Controls.Add(this.lblgananciamonto);
		this.panel1.Controls.Add(this.txtgananciamonto);
		this.panel1.Controls.Add(this.lblmarguenporcentaje);
		this.panel1.Controls.Add(this.txtgananciaporcentaje);
		this.panel1.Controls.Add(this.lbldisponibilidad);
		this.panel1.Controls.Add(this.cmbdisponibilidad);
		this.panel1.Controls.Add(this.checkBox2);
		this.panel1.Controls.Add(this.lbldias);
		this.panel1.Controls.Add(this.txtdias);
		this.panel1.Controls.Add(this.txtStockMinimo);
		this.panel1.Controls.Add(this.label20);
		this.panel1.Controls.Add(this.txtUbicacion);
		this.panel1.Controls.Add(this.label15);
		this.panel1.Controls.Add(this.txtReferencia);
		this.panel1.Controls.Add(this.label1);
		this.panel1.Controls.Add(this.label2);
		this.panel1.Controls.Add(this.txtDescripcion);
		this.panel1.Controls.Add(this.label4);
		this.panel1.Controls.Add(this.txtStock);
		this.panel1.Controls.Add(this.label5);
		this.panel1.Controls.Add(this.txtCantidad);
		this.panel1.Controls.Add(this.label6);
		this.panel1.Controls.Add(this.label8);
		this.panel1.Controls.Add(this.txtPrecio);
		this.panel1.Controls.Add(this.label7);
		this.panel1.Controls.Add(this.cmbUnidad);
		this.panel1.Controls.Add(this.txtDscto1);
		this.panel1.Controls.Add(this.label12);
		this.panel1.Controls.Add(this.label9);
		this.panel1.Controls.Add(this.txtUltimoPrecioCompra);
		this.panel1.Controls.Add(this.txtPrecioNeto);
		this.panel1.Controls.Add(this.txtPrecioDscto);
		this.panel1.Location = new System.Drawing.Point(4, 22);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(862, 190);
		this.panel1.TabIndex = 38;
		this.btnverstock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.btnverstock.Location = new System.Drawing.Point(754, 78);
		this.btnverstock.Name = "btnverstock";
		this.btnverstock.Size = new System.Drawing.Size(97, 24);
		this.btnverstock.TabIndex = 58;
		this.btnverstock.Text = "Ver Stock";
		this.btnverstock.UseVisualStyleBackColor = true;
		this.btnverstock.Visible = false;
		this.btnverstock.Click += new System.EventHandler(btnverstock_Click);
		this.txtultimoprecio.Enabled = false;
		this.txtultimoprecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtultimoprecio.Location = new System.Drawing.Point(447, 130);
		this.txtultimoprecio.Name = "txtultimoprecio";
		this.txtultimoprecio.ReadOnly = true;
		this.txtultimoprecio.Size = new System.Drawing.Size(101, 22);
		this.txtultimoprecio.TabIndex = 57;
		this.txtultimoprecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtultimoprecio.Visible = false;
		this.lblultimoprecio.AutoSize = true;
		this.lblultimoprecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblultimoprecio.Location = new System.Drawing.Point(422, 111);
		this.lblultimoprecio.Name = "lblultimoprecio";
		this.lblultimoprecio.Size = new System.Drawing.Size(170, 16);
		this.lblultimoprecio.TabIndex = 56;
		this.lblultimoprecio.Text = "Ultimo Precio Cotizado:";
		this.lblultimoprecio.Visible = false;
		this.label19.AutoSize = true;
		this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label19.Location = new System.Drawing.Point(754, 111);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(97, 16);
		this.label19.TabIndex = 55;
		this.label19.Text = "Tipo Precio :";
		this.label19.Visible = false;
		this.txtcodtipoprecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcodtipoprecio.Location = new System.Drawing.Point(767, 130);
		this.txtcodtipoprecio.Name = "txtcodtipoprecio";
		this.txtcodtipoprecio.ReadOnly = true;
		this.txtcodtipoprecio.Size = new System.Drawing.Size(73, 22);
		this.txtcodtipoprecio.TabIndex = 54;
		this.txtcodtipoprecio.TabStop = false;
		this.txtcodtipoprecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtcodtipoprecio.Visible = false;
		this.chbverganancia.AutoSize = true;
		this.chbverganancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chbverganancia.Location = new System.Drawing.Point(447, 164);
		this.chbverganancia.Name = "chbverganancia";
		this.chbverganancia.Size = new System.Drawing.Size(124, 17);
		this.chbverganancia.TabIndex = 53;
		this.chbverganancia.Text = "Ver (%) Ganancia";
		this.chbverganancia.UseVisualStyleBackColor = true;
		this.chbverganancia.Visible = false;
		this.chbverganancia.CheckedChanged += new System.EventHandler(chbverganancia_CheckedChanged);
		this.lbltotalcosto.AutoSize = true;
		this.lbltotalcosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbltotalcosto.Location = new System.Drawing.Point(281, 164);
		this.lbltotalcosto.Name = "lbltotalcosto";
		this.lbltotalcosto.Size = new System.Drawing.Size(63, 13);
		this.lbltotalcosto.TabIndex = 52;
		this.lbltotalcosto.Text = "T. Costo :";
		this.lbltotalcosto.Visible = false;
		this.txttotalcosto.Enabled = false;
		this.txttotalcosto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txttotalcosto.Location = new System.Drawing.Point(345, 159);
		this.txttotalcosto.Name = "txttotalcosto";
		this.txttotalcosto.ReadOnly = true;
		this.txttotalcosto.Size = new System.Drawing.Size(73, 22);
		this.txttotalcosto.TabIndex = 51;
		this.txttotalcosto.TabStop = false;
		this.txttotalcosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txttotalcosto.Visible = false;
		this.lbldesestiva.AutoSize = true;
		this.lbldesestiva.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbldesestiva.Location = new System.Drawing.Point(135, 163);
		this.lbldesestiva.Name = "lbldesestiva";
		this.lbldesestiva.Size = new System.Drawing.Size(71, 13);
		this.lbldesestiva.TabIndex = 50;
		this.lbldesestiva.Text = "Desestiva :";
		this.lbldesestiva.Visible = false;
		this.txtdesestiva.Enabled = false;
		this.txtdesestiva.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtdesestiva.Location = new System.Drawing.Point(207, 158);
		this.txtdesestiva.Name = "txtdesestiva";
		this.txtdesestiva.ReadOnly = true;
		this.txtdesestiva.Size = new System.Drawing.Size(68, 22);
		this.txtdesestiva.TabIndex = 49;
		this.txtdesestiva.TabStop = false;
		this.txtdesestiva.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtdesestiva.Visible = false;
		this.lblflete.AutoSize = true;
		this.lblflete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblflete.Location = new System.Drawing.Point(7, 163);
		this.lblflete.Name = "lblflete";
		this.lblflete.Size = new System.Drawing.Size(43, 13);
		this.lblflete.TabIndex = 48;
		this.lblflete.Text = "Flete :";
		this.lblflete.Visible = false;
		this.txtflete.Enabled = false;
		this.txtflete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtflete.Location = new System.Drawing.Point(53, 158);
		this.txtflete.Name = "txtflete";
		this.txtflete.ReadOnly = true;
		this.txtflete.Size = new System.Drawing.Size(80, 22);
		this.txtflete.TabIndex = 47;
		this.txtflete.TabStop = false;
		this.txtflete.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtflete.Visible = false;
		this.lblgananciamonto.AutoSize = true;
		this.lblgananciamonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblgananciamonto.Location = new System.Drawing.Point(396, 61);
		this.lblgananciamonto.Name = "lblgananciamonto";
		this.lblgananciamonto.Size = new System.Drawing.Size(100, 16);
		this.lblgananciamonto.TabIndex = 46;
		this.lblgananciamonto.Text = "Ganancia(M):";
		this.lblgananciamonto.Visible = false;
		this.txtgananciamonto.Enabled = false;
		this.txtgananciamonto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtgananciamonto.Location = new System.Drawing.Point(396, 80);
		this.txtgananciamonto.Name = "txtgananciamonto";
		this.txtgananciamonto.ReadOnly = true;
		this.txtgananciamonto.Size = new System.Drawing.Size(95, 22);
		this.txtgananciamonto.TabIndex = 45;
		this.txtgananciamonto.TabStop = false;
		this.txtgananciamonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtgananciamonto.Visible = false;
		this.lblmarguenporcentaje.AutoSize = true;
		this.lblmarguenporcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblmarguenporcentaje.Location = new System.Drawing.Point(295, 61);
		this.lblmarguenporcentaje.Name = "lblmarguenporcentaje";
		this.lblmarguenporcentaje.Size = new System.Drawing.Size(101, 16);
		this.lblmarguenporcentaje.TabIndex = 44;
		this.lblmarguenporcentaje.Text = "Ganancia(%):";
		this.lblmarguenporcentaje.Visible = false;
		this.txtgananciaporcentaje.Enabled = false;
		this.txtgananciaporcentaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtgananciaporcentaje.Location = new System.Drawing.Point(295, 80);
		this.txtgananciaporcentaje.Name = "txtgananciaporcentaje";
		this.txtgananciaporcentaje.ReadOnly = true;
		this.txtgananciaporcentaje.Size = new System.Drawing.Size(95, 22);
		this.txtgananciaporcentaje.TabIndex = 43;
		this.txtgananciaporcentaje.TabStop = false;
		this.txtgananciaporcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtgananciaporcentaje.Visible = false;
		this.lbldisponibilidad.AutoSize = true;
		this.lbldisponibilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbldisponibilidad.Location = new System.Drawing.Point(498, 61);
		this.lbldisponibilidad.Name = "lbldisponibilidad";
		this.lbldisponibilidad.Size = new System.Drawing.Size(113, 16);
		this.lbldisponibilidad.TabIndex = 42;
		this.lbldisponibilidad.Text = "Disponibilidad:";
		this.lbldisponibilidad.Visible = false;
		this.cmbdisponibilidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbdisponibilidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbdisponibilidad.FormattingEnabled = true;
		this.cmbdisponibilidad.Items.AddRange(new object[5] { "Inmediata", "1 a 2   Días hábiles", "3 a 5   Días hábiles", "7 a 10 Días hábiles", "Días hábiles" });
		this.cmbdisponibilidad.Location = new System.Drawing.Point(498, 78);
		this.cmbdisponibilidad.Name = "cmbdisponibilidad";
		this.cmbdisponibilidad.Size = new System.Drawing.Size(142, 24);
		this.cmbdisponibilidad.TabIndex = 41;
		this.cmbdisponibilidad.Visible = false;
		this.cmbdisponibilidad.SelectionChangeCommitted += new System.EventHandler(cmbdisponibilidad_SelectionChangeCommitted);
		this.checkBox2.AutoSize = true;
		this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.checkBox2.Location = new System.Drawing.Point(610, 163);
		this.checkBox2.Name = "checkBox2";
		this.checkBox2.Size = new System.Drawing.Size(249, 16);
		this.checkBox2.TabIndex = 31;
		this.checkBox2.Text = "DESCUENTO SOBRE EL PRECIO UNITARIO";
		this.checkBox2.UseVisualStyleBackColor = true;
		this.checkBox2.Visible = false;
		this.checkBox2.CheckedChanged += new System.EventHandler(checkBox2_CheckedChanged);
		this.lbldias.AutoSize = true;
		this.lbldias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbldias.Location = new System.Drawing.Point(643, 56);
		this.lbldias.Name = "lbldias";
		this.lbldias.Size = new System.Drawing.Size(106, 16);
		this.lbldias.TabIndex = 40;
		this.lbldias.Text = "Dias Entrega :";
		this.lbldias.Visible = false;
		this.txtdias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtdias.Location = new System.Drawing.Point(646, 80);
		this.txtdias.Name = "txtdias";
		this.txtdias.Size = new System.Drawing.Size(102, 22);
		this.txtdias.TabIndex = 39;
		this.txtdias.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtdias.Visible = false;
		this.txtStockMinimo.Enabled = false;
		this.txtStockMinimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtStockMinimo.Location = new System.Drawing.Point(317, 130);
		this.txtStockMinimo.Name = "txtStockMinimo";
		this.txtStockMinimo.ReadOnly = true;
		this.txtStockMinimo.Size = new System.Drawing.Size(101, 22);
		this.txtStockMinimo.TabIndex = 34;
		this.txtStockMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label20.AutoSize = true;
		this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label20.Location = new System.Drawing.Point(314, 111);
		this.label20.Name = "label20";
		this.label20.Size = new System.Drawing.Size(104, 16);
		this.label20.TabIndex = 33;
		this.label20.Text = "Stock Mínimo:";
		this.txtUbicacion.Enabled = false;
		this.txtUbicacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtUbicacion.Location = new System.Drawing.Point(140, 130);
		this.txtUbicacion.Name = "txtUbicacion";
		this.txtUbicacion.ReadOnly = true;
		this.txtUbicacion.Size = new System.Drawing.Size(171, 22);
		this.txtUbicacion.TabIndex = 28;
		this.txtUbicacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label15.AutoSize = true;
		this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label15.Location = new System.Drawing.Point(140, 111);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(86, 16);
		this.label15.TabIndex = 27;
		this.label15.Text = "Ubicación :";
		this.txtReferencia.BackColor = System.Drawing.Color.PeachPuff;
		this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtReferencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtReferencia.Location = new System.Drawing.Point(7, 29);
		this.txtReferencia.Name = "txtReferencia";
		this.txtReferencia.Size = new System.Drawing.Size(110, 22);
		this.txtReferencia.TabIndex = 1;
		this.txtReferencia.TextChanged += new System.EventHandler(frmDetalleSalida_Load);
		this.txtReferencia.KeyDown += new System.Windows.Forms.KeyEventHandler(txtReferencia_KeyDown);
		this.txtReferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtReferencia_KeyPress);
		this.txtReferencia.KeyUp += new System.Windows.Forms.KeyEventHandler(txtReferencia_KeyUp);
		this.txtReferencia.Leave += new System.EventHandler(txtReferencia_Leave);
		this.txtReferencia.ImeModeChanged += new System.EventHandler(txtReferencia_ImeModeChanged);
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(4, 10);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(92, 16);
		this.label1.TabIndex = 0;
		this.label1.Text = "Referencia :";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(122, 10);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(99, 16);
		this.label2.TabIndex = 2;
		this.label2.Text = "Descripcion :";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Enabled = false;
		this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDescripcion.Location = new System.Drawing.Point(124, 29);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.ReadOnly = true;
		this.txtDescripcion.Size = new System.Drawing.Size(388, 22);
		this.txtDescripcion.TabIndex = 2;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(710, 8);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(134, 16);
		this.label4.TabIndex = 6;
		this.label4.Text = "Stock Disponible :";
		this.txtStock.Enabled = false;
		this.txtStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtStock.Location = new System.Drawing.Point(713, 27);
		this.txtStock.Name = "txtStock";
		this.txtStock.Size = new System.Drawing.Size(139, 22);
		this.txtStock.TabIndex = 3;
		this.txtStock.TextChanged += new System.EventHandler(txtStock_TextChanged);
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(7, 61);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(78, 16);
		this.label5.TabIndex = 8;
		this.label5.Text = "Cantidad :";
		this.txtCantidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtCantidad.Location = new System.Drawing.Point(10, 80);
		this.txtCantidad.Name = "txtCantidad";
		this.txtCantidad.Size = new System.Drawing.Size(80, 22);
		this.txtCantidad.TabIndex = 6;
		this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCantidad_KeyPress);
		this.txtCantidad.Leave += new System.EventHandler(txtCantidad_Leave);
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(515, 10);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(66, 16);
		this.label6.TabIndex = 10;
		this.label6.Text = "Unidad :";
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(92, 61);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(88, 16);
		this.label8.TabIndex = 12;
		this.label8.Text = "Precio Unit:";
		this.txtPrecio.Enabled = false;
		this.txtPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecio.Location = new System.Drawing.Point(94, 80);
		this.txtPrecio.Name = "txtPrecio";
		this.txtPrecio.Size = new System.Drawing.Size(91, 22);
		this.txtPrecio.TabIndex = 7;
		this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecio.KeyDown += new System.Windows.Forms.KeyEventHandler(txtPrecio_KeyDown);
		this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrecio_KeyPress);
		this.txtPrecio.Leave += new System.EventHandler(txtPrecio_Leave);
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(665, 111);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(83, 16);
		this.label7.TabIndex = 14;
		this.label7.Text = " Dscto S/. :";
		this.label7.Visible = false;
		this.cmbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbUnidad.FormattingEnabled = true;
		this.cmbUnidad.Location = new System.Drawing.Point(518, 29);
		this.cmbUnidad.Name = "cmbUnidad";
		this.cmbUnidad.Size = new System.Drawing.Size(171, 24);
		this.cmbUnidad.TabIndex = 19;
		this.cmbUnidad.SelectionChangeCommitted += new System.EventHandler(cmbUnidad_SelectionChangeCommitted);
		this.txtDscto1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDscto1.Location = new System.Drawing.Point(665, 130);
		this.txtDscto1.MaxLength = 4;
		this.txtDscto1.Name = "txtDscto1";
		this.txtDscto1.Size = new System.Drawing.Size(80, 22);
		this.txtDscto1.TabIndex = 8;
		this.txtDscto1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDscto1.Visible = false;
		this.txtDscto1.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDscto1_KeyDown);
		this.txtDscto1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDscto1_KeyPress);
		this.txtDscto1.Leave += new System.EventHandler(txtDscto1_Leave);
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(7, 111);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(127, 16);
		this.label12.TabIndex = 22;
		this.label12.Text = "Últim. P. Compra:";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(192, 61);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(97, 16);
		this.label9.TabIndex = 16;
		this.label9.Text = "Precio Total:";
		this.txtUltimoPrecioCompra.Enabled = false;
		this.txtUltimoPrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtUltimoPrecioCompra.Location = new System.Drawing.Point(10, 130);
		this.txtUltimoPrecioCompra.Name = "txtUltimoPrecioCompra";
		this.txtUltimoPrecioCompra.ReadOnly = true;
		this.txtUltimoPrecioCompra.Size = new System.Drawing.Size(124, 22);
		this.txtUltimoPrecioCompra.TabIndex = 26;
		this.txtUltimoPrecioCompra.TabStop = false;
		this.txtUltimoPrecioCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtPrecioNeto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioNeto.Location = new System.Drawing.Point(195, 80);
		this.txtPrecioNeto.Name = "txtPrecioNeto";
		this.txtPrecioNeto.ReadOnly = true;
		this.txtPrecioNeto.Size = new System.Drawing.Size(94, 22);
		this.txtPrecioNeto.TabIndex = 11;
		this.txtPrecioNeto.TabStop = false;
		this.txtPrecioNeto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioNeto.TextChanged += new System.EventHandler(txtPrecioNeto_TextChanged);
		this.txtPrecioNeto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrecioNeto_KeyPress);
		this.txtPrecioNeto.KeyUp += new System.Windows.Forms.KeyEventHandler(txtPrecioNeto_KeyUp);
		this.txtPrecioNeto.Leave += new System.EventHandler(txtPrecioNeto_Leave);
		this.txtPrecioDscto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioDscto.Location = new System.Drawing.Point(10, 80);
		this.txtPrecioDscto.Name = "txtPrecioDscto";
		this.txtPrecioDscto.ReadOnly = true;
		this.txtPrecioDscto.Size = new System.Drawing.Size(80, 22);
		this.txtPrecioDscto.TabIndex = 21;
		this.txtPrecioDscto.TabStop = false;
		this.txtPrecioDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioDscto.Visible = false;
		this.label17.AutoSize = true;
		this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label17.Location = new System.Drawing.Point(22, 262);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(68, 16);
		this.label17.TabIndex = 37;
		this.label17.Text = "codlinea";
		this.label17.Visible = false;
		this.txtcodlinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcodlinea.Location = new System.Drawing.Point(96, 259);
		this.txtcodlinea.Name = "txtcodlinea";
		this.txtcodlinea.Size = new System.Drawing.Size(27, 22);
		this.txtcodlinea.TabIndex = 35;
		this.txtcodlinea.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtcodlinea.Visible = false;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(159, 265);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(90, 16);
		this.label10.TabIndex = 18;
		this.label10.Text = "Aument S./ :";
		this.label10.Visible = false;
		this.txtDscto2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDscto2.Location = new System.Drawing.Point(255, 259);
		this.txtDscto2.Name = "txtDscto2";
		this.txtDscto2.Size = new System.Drawing.Size(27, 22);
		this.txtDscto2.TabIndex = 9;
		this.txtDscto2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDscto2.Visible = false;
		this.txtDscto2.KeyDown += new System.Windows.Forms.KeyEventHandler(txtDscto2_KeyDown);
		this.txtDscto2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDscto2_KeyPress);
		this.txtDscto2.Leave += new System.EventHandler(txtDscto2_Leave);
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(306, 262);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(85, 16);
		this.label11.TabIndex = 20;
		this.label11.Text = "% Dscto 3 :";
		this.label11.Visible = false;
		this.txtDscto3.Enabled = false;
		this.txtDscto3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDscto3.Location = new System.Drawing.Point(397, 259);
		this.txtDscto3.Name = "txtDscto3";
		this.txtDscto3.Size = new System.Drawing.Size(27, 22);
		this.txtDscto3.TabIndex = 10;
		this.txtDscto3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDscto3.Visible = false;
		this.txtDscto3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDscto3_KeyPress);
		this.txtDscto3.Leave += new System.EventHandler(txtDscto3_Leave);
		this.label18.AutoSize = true;
		this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label18.Location = new System.Drawing.Point(10, 231);
		this.label18.Name = "label18";
		this.label18.Size = new System.Drawing.Size(80, 16);
		this.label18.TabIndex = 38;
		this.label18.Text = "codfamilia";
		this.label18.Visible = false;
		this.txtcodfamilia.Enabled = false;
		this.txtcodfamilia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcodfamilia.Location = new System.Drawing.Point(98, 231);
		this.txtcodfamilia.Name = "txtcodfamilia";
		this.txtcodfamilia.Size = new System.Drawing.Size(25, 22);
		this.txtcodfamilia.TabIndex = 36;
		this.txtcodfamilia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtcodfamilia.Visible = false;
		this.txtPrecioNetoDscto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioNetoDscto.Location = new System.Drawing.Point(185, 239);
		this.txtPrecioNetoDscto.Name = "txtPrecioNetoDscto";
		this.txtPrecioNetoDscto.ReadOnly = true;
		this.txtPrecioNetoDscto.Size = new System.Drawing.Size(64, 22);
		this.txtPrecioNetoDscto.TabIndex = 24;
		this.txtPrecioNetoDscto.TabStop = false;
		this.txtPrecioNetoDscto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtPrecioNetoDscto.Visible = false;
		this.checkBox1.AutoSize = true;
		this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.checkBox1.Location = new System.Drawing.Point(266, 243);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(266, 17);
		this.checkBox1.TabIndex = 30;
		this.checkBox1.Text = "AUMENTO SOBRE EL PRECIO UNITARIO";
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.Visible = false;
		this.checkBox1.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(382, 307);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(72, 13);
		this.label14.TabIndex = 27;
		this.label14.Text = "% Max Dscto.";
		this.label14.Visible = false;
		this.label13.AutoSize = true;
		this.label13.Location = new System.Drawing.Point(556, 307);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(88, 13);
		this.label13.TabIndex = 23;
		this.label13.Text = "Max Dscto Total:";
		this.label13.Visible = false;
		this.txtUnidad.Enabled = false;
		this.txtUnidad.Location = new System.Drawing.Point(377, 323);
		this.txtUnidad.Name = "txtUnidad";
		this.txtUnidad.Size = new System.Drawing.Size(80, 20);
		this.txtUnidad.TabIndex = 4;
		this.txtUnidad.Visible = false;
		this.txtControlStock.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtControlStock.Location = new System.Drawing.Point(201, 13);
		this.txtControlStock.Name = "txtControlStock";
		this.txtControlStock.Size = new System.Drawing.Size(80, 20);
		this.txtControlStock.TabIndex = 5;
		this.txtControlStock.Visible = false;
		this.txtControlStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtControlStock_KeyPress);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(212, -3);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(69, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "Serie / Lote :";
		this.label3.Visible = false;
		this.txtCodigo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Location = new System.Drawing.Point(290, 13);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(80, 20);
		this.txtCodigo.TabIndex = 16;
		this.txtCodigo.Visible = false;
		this.txtCodigo.TextChanged += new System.EventHandler(txtCodigo_TextChanged);
		this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodigo_KeyPress);
		this.txtUnd.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtUnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtUnd.Location = new System.Drawing.Point(559, 323);
		this.txtUnd.Name = "txtUnd";
		this.txtUnd.Size = new System.Drawing.Size(84, 20);
		this.txtUnd.TabIndex = 20;
		this.txtUnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtUnd.Visible = false;
		this.txtPrecioNetoDolares.Location = new System.Drawing.Point(463, 323);
		this.txtPrecioNetoDolares.Name = "txtPrecioNetoDolares";
		this.txtPrecioNetoDolares.Size = new System.Drawing.Size(90, 20);
		this.txtPrecioNetoDolares.TabIndex = 21;
		this.txtPrecioNetoDolares.Visible = false;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(460, 307);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(81, 13);
		this.label16.TabIndex = 29;
		this.label16.Text = "Precio Total $/.";
		this.label16.Visible = false;
		this.txtPrecioMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioMax.Location = new System.Drawing.Point(650, 321);
		this.txtPrecioMax.Name = "txtPrecioMax";
		this.txtPrecioMax.ReadOnly = true;
		this.txtPrecioMax.Size = new System.Drawing.Size(69, 22);
		this.txtPrecioMax.TabIndex = 25;
		this.txtPrecioMax.TabStop = false;
		this.txtPrecioMax.Visible = false;
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(734, 234);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(123, 32);
		this.btnSalir.TabIndex = 18;
		this.btnSalir.Text = "CANCELAR";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = (System.Drawing.Image)resources.GetObject("btnGuardar.Image");
		this.btnGuardar.Location = new System.Drawing.Point(548, 234);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(180, 32);
		this.btnGuardar.TabIndex = 17;
		this.btnGuardar.Text = "AGREGAR A LISTA";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(869, 278);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.txtCodigo);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.txtControlStock);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.txtUnidad);
		base.Controls.Add(this.label13);
		base.Controls.Add(this.txtPrecioMax);
		base.Controls.Add(this.txtUnd);
		base.Controls.Add(this.txtPrecioNetoDolares);
		base.Controls.Add(this.txtPrecioNetoDscto);
		base.Controls.Add(this.txtcodfamilia);
		base.Controls.Add(this.label18);
		base.Controls.Add(this.txtcodlinea);
		base.Controls.Add(this.label17);
		base.Controls.Add(this.txtDscto2);
		base.Controls.Add(this.label10);
		base.Controls.Add(this.txtDscto3);
		base.Controls.Add(this.label11);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmDetalleSalida";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Detalle Salida";
		base.Load += new System.EventHandler(frmDetalleSalida_Load);
		base.Shown += new System.EventHandler(frmDetalleSalida_Shown);
		this.groupBox1.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.panel1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
