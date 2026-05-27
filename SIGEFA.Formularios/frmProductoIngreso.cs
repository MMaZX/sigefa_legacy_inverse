using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;

namespace SIGEFA.Formularios;

public class frmProductoIngreso : Office2007Form
{
	public static List<int> seleccion = new List<int>();

	public int Proceso = 0;

	public int repetido = 0;

	public int proce = 0;

	public int Procede = 0;

	public int Seleccion = 0;

	public int CodProducto = 0;

	public int codproveedor = 0;

	public bool bvalorventa = false;

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto pro = new clsProducto();

	private clsProducto pro1 = new clsProducto();

	public int CodLista = 0;

	private clsValidar ok = new clsValidar();

	public List<clsDetalleNotaIngreso> productoscargados = new List<clsDetalleNotaIngreso>();

	public DataTable data = new DataTable();

	private clsDetalleOrdenCompra deta = new clsDetalleOrdenCompra();

	private clsAdmOrdenCompra AdmOrden = new clsAdmOrdenCompra();

	public clsOrdenCompra Ord = new clsOrdenCompra();

	public bool Actualizar_De_Orden_De_Compra = false;

	public int index_dgv_a_actualizar = -1;

	public frmOrdenCompra formulario_o_c_padre;

	public frmGuiaRemisionCompra ventana_grc = null;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label1;

	private Label label6;

	private Label label5;

	private TextBox txtStock;

	private Label label4;

	private Label label3;

	private Label label2;

	private ImageList imageList1;

	private Button btnSalir;

	private Label label7;

	private Label label8;

	private Label label9;

	public TextBox txtCodigo;

	private Label label11;

	private Label label10;

	public TextBox txtReferencia;

	public TextBox txtUnidad;

	public TextBox txtCantidad;

	public TextBox txtControlStock;

	public TextBox txtDscto1;

	public TextBox txtPrecio;

	public TextBox txtPrecioNeto;

	public TextBox txtDscto3;

	public TextBox txtDscto2;

	public TextBox txtDescripcion;

	public Button btnGuardar;

	public ComboBox cmbUnidad;

	private CheckBox checkProrrateo;

	private Label label12;

	private TextBox txtUltimoPrecioCompra;

	private System.Windows.Forms.ToolTip toolTip1;

	private PictureBox pictureBox1;

	public frmProductoIngreso()
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

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtCodigo_TextChanged(object sender, EventArgs e)
	{
		if (!(txtCodigo.Text != ""))
		{
			return;
		}
		pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 1, CodLista, 0);
		if (pro == null)
		{
			MessageBox.Show("Ocurrio un error al seleccionar el producto", "Selección de producto", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		if (Procede == 10)
		{
			frmOrdenCompra form = (frmOrdenCompra)Application.OpenForms["frmOrdenCompra"];
			CodProducto = pro.CodProducto;
			txtReferencia.Text = pro.Referencia;
			txtDescripcion.Text = pro.Descripcion;
			txtUnidad.Text = pro.UnidadDescrip;
			CargaUnidades(cmbUnidad);
			cmbUnidad.SelectedValue = pro.CodUnidadMedida;
			txtPrecio.Text = pro.PrecioCompra.ToString();
			txtStock.Text = pro.StockDisponible.ToString();
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
				txtCantidad.Text = "";
				break;
			}
			txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue), 0).ToString();
		}
		else if (Procede == 6)
		{
			frmNotaIngreso form2 = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
			CodProducto = pro.CodProducto;
			txtReferencia.Text = pro.Referencia;
			txtDescripcion.Text = pro.Descripcion;
			txtUnidad.Text = pro.UnidadDescrip;
			CargaUnidades(cmbUnidad);
			cmbUnidad.SelectedValue = pro.CodUnidadMedida;
			txtStock.Text = pro.StockDisponible.ToString();
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
				txtCantidad.Text = "";
				break;
			}
			txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue), 0).ToString();
		}
		if (Procede == 90 || Procede == 91 || Procede == 92)
		{
			CodProducto = pro.CodProducto;
			txtReferencia.Text = pro.Referencia;
			txtDescripcion.Text = pro.Descripcion;
			txtUnidad.Text = pro.UnidadDescrip;
			CargaUnidades(cmbUnidad);
			cmbUnidad.SelectedValue = pro.CodUnidadMedida;
			txtStock.Text = pro.StockDisponible.ToString();
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
				txtCantidad.Text = "";
				break;
			}
			txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue), 0).ToString();
		}
	}

	private void txtDscto_KeyPress(object sender, KeyPressEventArgs e)
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
				txtDscto1.Text = "0.000";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0):#,##0.0000}";
		}
		ProcessTabKey(forward: true);
	}

	private void txtDscto_Leave(object sender, EventArgs e)
	{
		if (txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.000";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0):#,##0.0000}";
		}
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
		if (e.KeyChar != '\r')
		{
			return;
		}
		if (txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.000";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.000";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.000";
			}
			if (txtCantidad.Text != "")
			{
				txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.0000}";
			}
		}
		ProcessTabKey(forward: true);
	}

	private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
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
				txtDscto1.Text = "0.000";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.000";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.000";
			}
			if (txtCantidad.Text != "" || Convert.ToDecimal(txtCantidad.Text) != 0m)
			{
				txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.0000}";
			}
			else
			{
				txtCantidad.Focus();
			}
		}
		if (txtCantidad.Text != "")
		{
			if (Convert.ToDecimal(txtCantidad.Text) != 0m)
			{
				txtPrecio.Focus();
			}
			else
			{
				txtCantidad.Focus();
			}
		}
		else
		{
			txtCantidad.Focus();
		}
	}

	public void x()
	{
		DataTable data = new DataTable();
		data = AdmOrden.CargaDetalle(Convert.ToInt32(Ord.CodOrdenCompra));
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (cmbUnidad.Text != "")
			{
				if (txtCantidad.Text != "")
				{
					if (Convert.ToDecimal(txtCantidad.Text) != 0m)
					{
						if (Procede == 6)
						{
							frmNotaIngreso form = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
							if (!bvalorventa)
							{
								double bruto = Math.Round(Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text), 2, MidpointRounding.AwayFromZero);
								double montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
								double precioventa;
								double valorventa;
								if (pro.TipoImpuesto == 1)
								{
									precioventa = Convert.ToDouble(txtPrecioNeto.Text);
									double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
									valorventa = precioventa / factorigv;
									Close();
								}
								else
								{
									valorventa = Convert.ToDouble(txtPrecioNeto.Text);
									precioventa = valorventa;
								}
								double precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
								double valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
								double igv = precioventa - valorventa;
								double dsc1 = ((!(txtDscto1.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto1.Text));
								double dsc2 = ((!(txtDscto2.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto2.Text));
								double dsc3 = ((!(txtDscto3.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto3.Text));
								if (Proceso == 1)
								{
									form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, form.cmbMoneda.SelectedValue, cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, "0.00", igv, precioventa, precioventa, precioreal, valorreal, "", "", "", Convert.ToDouble(txtUltimoPrecioCompra.Text.Trim()), "", "", Convert.ToDouble(txtCantidad.Text));
									limpiarformulario();
									if (Seleccion == 2)
									{
										Close();
									}
								}
								else if (Proceso == 2)
								{
									form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "", cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "", Convert.ToDouble(txtUltimoPrecioCompra.Text.Trim()));
									limpiarformulario();
									Close();
								}
							}
							else
							{
								double bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
								double montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
								double precioventa;
								double valorventa;
								if (pro.TipoImpuesto == 1)
								{
									valorventa = Convert.ToDouble(txtPrecioNeto.Text);
									double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
									precioventa = valorventa * factorigv;
									Close();
								}
								else
								{
									valorventa = Convert.ToDouble(txtPrecioNeto.Text);
									precioventa = valorventa;
								}
								valorventa = Convert.ToDouble(txtPrecioNeto.Text);
								double precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
								double valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
								double igv = precioventa - valorventa;
								double dsc1 = ((!(txtDscto1.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto1.Text));
								double dsc2 = ((!(txtDscto2.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto2.Text));
								double dsc3 = ((!(txtDscto3.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto3.Text));
								if (Proceso == 1)
								{
									form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, form.cmbMoneda.SelectedValue, cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, "0.00", igv, precioventa, precioventa, precioreal, valorreal, "", "", "", Convert.ToDouble(txtUltimoPrecioCompra.Text.Trim()));
									limpiarformulario();
									if (Seleccion == 2)
									{
										Close();
									}
								}
								else if (Proceso == 2)
								{
									form.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "", cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "", Convert.ToDouble(txtUltimoPrecioCompra.Text.Trim()));
									limpiarformulario();
									Close();
								}
							}
						}
						else if (Procede == 7)
						{
							frmNotadeCredito form2 = (frmNotadeCredito)Application.OpenForms["frmNotadeCredito"];
							if (bvalorventa)
							{
								double bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
								double montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
								double valorventa;
								double precioventa;
								if (pro.ConIgv)
								{
									valorventa = Convert.ToDouble(txtPrecioNeto.Text);
									double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
									precioventa = valorventa * factorigv;
								}
								else
								{
									valorventa = Convert.ToDouble(txtPrecioNeto.Text);
									precioventa = valorventa;
								}
								double precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
								double valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
								double igv = precioventa - valorventa;
								double dsc1 = ((!(txtDscto1.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto1.Text));
								double dsc2 = ((!(txtDscto2.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto2.Text));
								double dsc3 = ((!(txtDscto3.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto3.Text));
								if (form2.dgvDetalle.Rows.Count < 10)
								{
									if (Proceso == 1)
									{
										form2.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "", cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "");
										limpiarformulario();
										if (Seleccion == 2)
										{
											Close();
										}
									}
									else if (Proceso == 2)
									{
										form2.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "", cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "");
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
								double bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
								double montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
								double precioventa;
								double valorventa;
								if (pro.ConIgv)
								{
									precioventa = Convert.ToDouble(txtPrecioNeto.Text);
									double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
									valorventa = precioventa / factorigv;
								}
								else
								{
									valorventa = Convert.ToDouble(txtPrecioNeto.Text);
									double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
									precioventa = valorventa * factorigv;
								}
								double precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
								double valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
								double igv = precioventa - valorventa;
								double dsc1 = ((!(txtDscto1.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto1.Text));
								double dsc2 = ((!(txtDscto2.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto2.Text));
								double dsc3 = ((!(txtDscto3.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto3.Text));
								if (form2.dgvDetalle.Rows.Count < 10)
								{
									if (Proceso == 1)
									{
										form2.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "", cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "");
										limpiarformulario();
										if (Seleccion == 2)
										{
											Close();
										}
									}
									else if (Proceso == 2)
									{
										form2.dgvDetalle.CurrentRow.SetValues("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, "", cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "");
										limpiarformulario();
										Close();
									}
								}
								else
								{
									MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
								}
							}
						}
						if (Procede == 10)
						{
							frmOrdenCompra form3 = new frmOrdenCompra();
							form3 = ((formulario_o_c_padre == null) ? formulario_o_c_padre : ((frmOrdenCompra)Application.OpenForms["frmOrdenCompra"]));
							if (bvalorventa)
							{
								double bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
								double montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
								double valorventa;
								double precioventa;
								if (pro.CodSunat == "10")
								{
									valorventa = Convert.ToDouble(bruto) - montodescuento;
									double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
									precioventa = valorventa * factorigv;
								}
								else
								{
									valorventa = Convert.ToDouble(txtPrecioNeto.Text);
									precioventa = valorventa;
								}
								double precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
								double valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
								double igv = precioventa - valorventa;
								double dsc1 = ((!(txtDscto1.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto1.Text));
								double dsc2 = ((!(txtDscto2.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto2.Text));
								double dsc3 = ((!(txtDscto3.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto3.Text));
								if (Proceso == 1)
								{
									if (Actualizar_De_Orden_De_Compra)
									{
										form3 = formulario_o_c_padre;
										form3.dgvDetalle.Rows.RemoveAt(index_dgv_a_actualizar);
										form3.dgvDetalle.Rows.Insert(index_dgv_a_actualizar, "", pro.CodProducto, pro.Referencia, pro.Descripcion, "", cmbUnidad.SelectedValue, cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "", 1, 1, form3.dos);
										Close();
									}
									else
									{
										form3.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, "", cmbUnidad.SelectedValue, cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "", 1, 1, form3.dos);
									}
									limpiarformulario();
									if (Seleccion == 2)
									{
										Close();
									}
								}
								else if (Proceso == 2)
								{
									form3.dgvDetalle.CurrentRow.SetValues(0, pro.CodProducto, pro.Referencia, pro.Descripcion, "", cmbUnidad.SelectedValue, cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "", 1, 1, form3.dos);
									limpiarformulario();
									Close();
								}
							}
							else
							{
								double bruto = Convert.ToDouble(txtCantidad.Text) * Convert.ToDouble(txtPrecio.Text);
								double montodescuento = bruto - Convert.ToDouble(txtPrecioNeto.Text);
								double precioventa;
								double valorventa;
								if (pro.ConIgv)
								{
									if (pro.ConIgv)
									{
										precioventa = Convert.ToDouble(txtPrecioNeto.Text);
										double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
										valorventa = precioventa / factorigv;
									}
									else
									{
										valorventa = Convert.ToDouble(txtPrecioNeto.Text);
										precioventa = valorventa;
									}
								}
								else if (pro.ConIgv)
								{
									valorventa = Convert.ToDouble(txtPrecioNeto.Text);
									double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
									precioventa = valorventa * factorigv;
								}
								else
								{
									precioventa = Convert.ToDouble(txtPrecioNeto.Text);
									double factorigv = frmLogin.Configuracion.IGV / 100.0 + 1.0;
									valorventa = precioventa / factorigv;
								}
								double precioreal = precioventa / Convert.ToDouble(txtCantidad.Text);
								double valorreal = valorventa / Convert.ToDouble(txtCantidad.Text);
								double igv = precioventa - valorventa;
								double dsc1 = ((!(txtDscto1.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto1.Text));
								double dsc2 = ((!(txtDscto2.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto2.Text));
								double dsc3 = ((!(txtDscto3.Text != "")) ? 0.0 : Convert.ToDouble(txtDscto3.Text));
								if (Proceso == 1)
								{
									if (Actualizar_De_Orden_De_Compra)
									{
										form3.dgvDetalle.Rows.RemoveAt(index_dgv_a_actualizar);
										form3.dgvDetalle.Rows.Insert(index_dgv_a_actualizar, "", pro.CodProducto, pro.Referencia, pro.Descripcion, "", cmbUnidad.SelectedValue, cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "", 1, 1, form3.dos);
										Close();
									}
									else
									{
										form3.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, "", cmbUnidad.SelectedValue, cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "", 1, 1, form3.dos);
									}
									limpiarformulario();
									if (Seleccion == 2)
									{
										Close();
									}
								}
								else if (Proceso == 2)
								{
									form3.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, "", cmbUnidad.SelectedValue, cmbUnidad.Text, "0", Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtPrecio.Text), bruto, dsc1, dsc2, dsc3, montodescuento, valorventa, valorventa, igv, 0.0, precioventa, precioventa, precioreal, valorreal, "", "", "", 1, 1, form3.dos);
									limpiarformulario();
									Close();
								}
							}
						}
						if (Procede != 90 && Procede != 91 && Procede != 92)
						{
							return;
						}
						if (ventana_grc != null)
						{
							clsDetalleGuiaRemisionCompra deta = new clsDetalleGuiaRemisionCompra();
							deta.ICodProducto = pro.CodProducto;
							deta.IUnidadIngresada = Convert.ToInt32(cmbUnidad.SelectedValue);
							deta.DCantidad = Convert.ToDouble(txtCantidad.Text);
							deta.DCantidadRespaldo = Convert.ToDouble(txtCantidad.Text);
							deta.FFechaIngreso = DateTime.Now;
							int estado = 1;
							if (Procede == 91)
							{
								estado = 3;
							}
							if (Procede == 92)
							{
								estado = 7;
							}
							deta.IEstado = estado;
							deta.ICOdUser = frmLogin.iCodUser;
							deta.FFechaRegistro = DateTime.Now;
							deta.SReferencia = pro.Referencia;
							deta.SDescripcion = pro.Descripcion;
							deta.SUnidad = cmbUnidad.Text;
							ventana_grc.detallegrc_añadir = deta;
							limpiarformulario();
							Close();
						}
						else
						{
							MessageBox.Show("Formulario No Encontrado para añadir elemento");
						}
					}
					else
					{
						txtCantidad.Focus();
					}
				}
				else
				{
					txtCantidad.Focus();
				}
			}
			else
			{
				MessageBox.Show("Falta Ingresar todas la unidades equivalentes", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
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
				txtDscto1.Text = "0.000";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.000";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.000";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.0000}";
		}
		ProcessTabKey(forward: true);
	}

	private void txtDscto2_Leave(object sender, EventArgs e)
	{
		if (txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.000";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.000";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.000";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.0000}";
		}
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
				txtDscto1.Text = "0.000";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.000";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.000";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.0000}";
		}
		ProcessTabKey(forward: true);
	}

	private void txtDscto3_Leave(object sender, EventArgs e)
	{
		if (txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.000";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.000";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.000";
			}
			txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) * (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0):#,##0.0000}";
		}
	}

	private void txtControlStock_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void frmDetalleIngreso_Shown(object sender, EventArgs e)
	{
		if (Proceso == 2)
		{
			txtCantidad.Focus();
		}
	}

	private void limpiarformulario()
	{
		foreach (Control c in groupBox1.Controls)
		{
			if (c is TextBox)
			{
				c.Text = "";
			}
		}
		txtReferencia.Focus();
	}

	private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void txtPrecio_Leave(object sender, EventArgs e)
	{
		if (txtPrecio.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.000";
			}
			if (txtCantidad.Text != "")
			{
				txtPrecioNeto.Text = $"{Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text) * (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0):#,##0.0000}";
			}
		}
		if (pro != null && txtCantidad.Text != "" && txtPrecio.Text != "")
		{
			btnGuardar.Enabled = true;
		}
	}

	private void txtReferencia_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtReferencia_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Proceso = Proceso;
			frm.Procede = Procede;
			frm.codproveedor = codproveedor;
			frm.bvalorventa = bvalorventa;
			frm.productoscargados = productoscargados;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				txtCodigo.Text = frm.GetCodigoProducto().ToString();
				Seleccion = 2;
				if (repetido == 1)
				{
					Close();
					Close();
				}
				else
				{
					cmbUnidad.Focus();
				}
			}
		}
		else if (e.KeyCode == Keys.F2)
		{
			frmRegistroProducto frm2 = new frmRegistroProducto();
			frm2.ShowDialog();
		}
	}

	private void txtReferencia_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtReferencia.Text != "" && BuscaProducto())
		{
			ProcessTabKey(forward: true);
		}
	}

	private bool verificaproductoscargados()
	{
		foreach (clsDetalleNotaIngreso det in productoscargados)
		{
			if (det.CodProEquals(pro.CodProducto))
			{
				return false;
			}
		}
		return true;
	}

	private bool BuscaProducto()
	{
		pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtReferencia.Text), frmLogin.iCodAlmacen, 1, CodLista, 0);
		if (pro != null)
		{
			if (verificaproductoscargados())
			{
				if (Procede == 10)
				{
					frmOrdenCompra form = (frmOrdenCompra)Application.OpenForms["frmOrdenCompra"];
					CodProducto = pro.CodProducto;
					txtReferencia.Text = pro.Referencia;
					txtDescripcion.Text = pro.Descripcion;
					txtUnidad.Text = pro.UnidadDescrip;
					CargaUnidades(cmbUnidad);
					cmbUnidad.SelectedValue = pro.CodUnidadMedida;
					txtStock.Text = pro.StockDisponible.ToString();
					txtControlStock.Text = "";
					txtCantidad.Text = "";
					txtPrecio.Text = "";
					txtDscto1.Text = "";
					txtDscto2.Text = "";
					txtDscto3.Text = "";
					txtPrecioNeto.Text = "";
					txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue), 0).ToString();
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
						break;
					case 4:
						txtControlStock.Enabled = false;
						txtCantidad.Enabled = false;
						break;
					}
					return true;
				}
				if (Procede == 6)
				{
					frmNotaIngreso form2 = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
					CodProducto = pro.CodProducto;
					txtReferencia.Text = pro.Referencia;
					txtDescripcion.Text = pro.Descripcion;
					txtUnidad.Text = pro.UnidadDescrip;
					CargaUnidades(cmbUnidad);
					cmbUnidad.SelectedValue = pro.CodUnidadMedida;
					txtStock.Text = pro.StockDisponible.ToString();
					txtControlStock.Text = "";
					txtCantidad.Text = "";
					txtPrecio.Text = "";
					txtDscto1.Text = "";
					txtDscto2.Text = "";
					txtDscto3.Text = "";
					txtPrecioNeto.Text = "";
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
						break;
					case 4:
						txtControlStock.Enabled = false;
						txtCantidad.Enabled = false;
						break;
					}
					txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue), 0).ToString();
					return true;
				}
				return false;
			}
			MessageBox.Show("El producto ya ha sido seleccionado", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CodProducto = 0;
			txtDescripcion.Text = "";
			txtUnidad.Text = "";
			cmbUnidad.SelectedIndex = -1;
			txtStock.Text = "";
			txtControlStock.Text = "";
			txtCantidad.Text = "";
			txtPrecio.Text = "";
			txtDscto1.Text = "";
			txtDscto2.Text = "";
			txtDscto3.Text = "";
			txtPrecioNeto.Text = "";
			return false;
		}
		MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		CodProducto = 0;
		txtDescripcion.Text = "";
		txtUnidad.Text = "";
		cmbUnidad.SelectedIndex = -1;
		txtStock.Text = "";
		txtControlStock.Text = "";
		txtCantidad.Text = "";
		txtPrecio.Text = "";
		txtDscto1.Text = "";
		txtDscto2.Text = "";
		txtDscto3.Text = "";
		txtPrecioNeto.Text = "";
		return false;
	}

	private void frmDetalleIngreso_Load(object sender, EventArgs e)
	{
		txtReferencia.Focus();
		if (Actualizar_De_Orden_De_Compra)
		{
			btnGuardar.Text = "ACTUALIZAR";
		}
	}

	private void CargaUnidades(ComboBox combo)
	{
		combo.DataSource = AdmPro.MuestraUnidadesEquivalentesCompra(pro.CodProducto, frmLogin.iCodAlmacen);
		combo.DisplayMember = "descripcion";
		combo.ValueMember = "codUnidadMedida";
	}

	private void txtReferencia_Leave(object sender, EventArgs e)
	{
		if (txtReferencia.Text != "" && !txtReferencia.ReadOnly)
		{
			txtCantidad.Focus();
		}
	}

	private void checkProrrateo_CheckedChanged(object sender, EventArgs e)
	{
		if (checkProrrateo.Checked)
		{
			txtPrecioNeto.ReadOnly = false;
			txtPrecio.ReadOnly = true;
			txtPrecioNeto.Focus();
		}
		else
		{
			txtPrecioNeto.ReadOnly = true;
			txtPrecio.ReadOnly = false;
		}
	}

	private void cmbUnidad_SelectionChangeCommitted(object sender, EventArgs e)
	{
		txtUltimoPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(CodProducto, Convert.ToInt32(cmbUnidad.SelectedValue), 0).ToString();
	}

	private void txtPrecioNeto_Leave(object sender, EventArgs e)
	{
		if (pro != null && txtCantidad.Text != "" && txtPrecio.Text != "")
		{
			btnGuardar.Enabled = true;
			btnGuardar.Focus();
		}
	}

	private void txtPrecioNeto_KeyUp(object sender, KeyEventArgs e)
	{
		if (txtPrecioNeto.Text != "")
		{
			if (txtDscto1.Text == "")
			{
				txtDscto1.Text = "0.000";
			}
			if (txtDscto2.Text == "")
			{
				txtDscto2.Text = "0.000";
			}
			if (txtDscto3.Text == "")
			{
				txtDscto3.Text = "0.000";
			}
			if (txtCantidad.Text != "" && !(Convert.ToDecimal(txtDscto1.Text) == 100m))
			{
				txtPrecio.Text = $"{Convert.ToDouble(txtPrecioNeto.Text) / (1.0 - Convert.ToDouble(txtDscto3.Text) / 100.0) / (1.0 - Convert.ToDouble(txtDscto2.Text) / 100.0) / (1.0 - Convert.ToDouble(txtDscto1.Text) / 100.0) / Convert.ToDouble(txtCantidad.Text):#,##0.0000}";
			}
		}
	}

	private void txtCantidad_TextChanged(object sender, EventArgs e)
	{
		if (txtCantidad.Text != "" && Procede == 8)
		{
			pro1 = AdmPro.CargaDatosProductoOrden(pro.CodProducto, frmLogin.iCodAlmacen, frmLogin.iCodUser, Convert.ToDecimal(txtCantidad.Text));
		}
		if (Procede == 90 || Procede == 91 || Procede == 92)
		{
			btnGuardar.Enabled = true;
		}
	}

	private void txtCantidad_Leave(object sender, EventArgs e)
	{
		if (txtCantidad.Text != "")
		{
			if (Convert.ToDecimal(txtCantidad.Text) != 0m)
			{
				if (checkProrrateo.Checked)
				{
					txtPrecioNeto.Focus();
				}
				else
				{
					txtPrecio.Focus();
				}
			}
			else
			{
				MessageBox.Show("Ingrese una cantidad correcta (mayor a 0)", "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtCantidad.Focus();
			}
		}
		else
		{
			MessageBox.Show("Ingrese una cantidad correcta (mayor a 0)", "Agregar Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			txtCantidad.Focus();
		}
	}

	private void cmbUnidad_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	public void asignarProductoDeOrdenCompra(int codigo_producto, Dictionary<string, object> valores)
	{
		Procede = 10;
		Proceso = 1;
		txtCodigo.Text = codigo_producto.ToString();
		txtCantidad.Text = valores["Cantidad"].ToString();
		txtPrecio.Text = valores["PrecioUnitario"].ToString();
		txtDscto1.Text = valores["dscto1"].ToString();
		txtDscto2.Text = valores["dscto2"].ToString();
		txtDscto3.Text = valores["dscto3"].ToString();
		txtPrecioNeto.Text = valores["PrecioTotal"].ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmProductoIngreso));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.txtUltimoPrecioCompra = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.checkProrrateo = new System.Windows.Forms.CheckBox();
		this.cmbUnidad = new System.Windows.Forms.ComboBox();
		this.txtReferencia = new System.Windows.Forms.TextBox();
		this.txtDscto3 = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtDscto2 = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtPrecioNeto = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtDscto1 = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtPrecio = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.txtCantidad = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtStock = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtControlStock = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtUnidad = new System.Windows.Forms.TextBox();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Controls.Add(this.pictureBox1);
		this.groupBox1.Controls.Add(this.txtUltimoPrecioCompra);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.checkProrrateo);
		this.groupBox1.Controls.Add(this.cmbUnidad);
		this.groupBox1.Controls.Add(this.txtReferencia);
		this.groupBox1.Controls.Add(this.txtDscto3);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.txtDscto2);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtPrecioNeto);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtDscto1);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.txtPrecio);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtCantidad);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtStock);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtControlStock);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.txtDescripcion);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(544, 137);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Seleccione Producto";
		this.pictureBox1.BackgroundImage = SIGEFA.Properties.Resources.question;
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox1.Location = new System.Drawing.Point(296, 216);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(26, 22);
		this.pictureBox1.TabIndex = 24;
		this.pictureBox1.TabStop = false;
		this.toolTip1.SetToolTip(this.pictureBox1, "MARQUE LA OPCIÓN PRORRATEO PARA CALCULAR EL PRECIO UNITARIO EN BASE A LA CANTIDAD Y EL PRECIO TOTAL");
		this.pictureBox1.Visible = false;
		this.txtUltimoPrecioCompra.Enabled = false;
		this.txtUltimoPrecioCompra.Location = new System.Drawing.Point(10, 216);
		this.txtUltimoPrecioCompra.Name = "txtUltimoPrecioCompra";
		this.txtUltimoPrecioCompra.Size = new System.Drawing.Size(162, 22);
		this.txtUltimoPrecioCompra.TabIndex = 23;
		this.txtUltimoPrecioCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtUltimoPrecioCompra.Visible = false;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(10, 197);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(163, 16);
		this.label12.TabIndex = 22;
		this.label12.Text = "Último Precio Compra:";
		this.label12.Visible = false;
		this.checkProrrateo.AutoSize = true;
		this.checkProrrateo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.checkProrrateo.Location = new System.Drawing.Point(187, 219);
		this.checkProrrateo.Name = "checkProrrateo";
		this.checkProrrateo.Size = new System.Drawing.Size(103, 17);
		this.checkProrrateo.TabIndex = 21;
		this.checkProrrateo.Text = "PRORRATEO";
		this.checkProrrateo.UseVisualStyleBackColor = true;
		this.checkProrrateo.Visible = false;
		this.checkProrrateo.CheckedChanged += new System.EventHandler(checkProrrateo_CheckedChanged);
		this.cmbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbUnidad.FormattingEnabled = true;
		this.cmbUnidad.Location = new System.Drawing.Point(15, 94);
		this.cmbUnidad.Name = "cmbUnidad";
		this.cmbUnidad.Size = new System.Drawing.Size(140, 24);
		this.cmbUnidad.TabIndex = 3;
		this.cmbUnidad.SelectedIndexChanged += new System.EventHandler(cmbUnidad_SelectedIndexChanged);
		this.cmbUnidad.SelectionChangeCommitted += new System.EventHandler(cmbUnidad_SelectionChangeCommitted);
		this.txtReferencia.BackColor = System.Drawing.Color.PeachPuff;
		this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtReferencia.Location = new System.Drawing.Point(12, 41);
		this.txtReferencia.Name = "txtReferencia";
		this.txtReferencia.Size = new System.Drawing.Size(100, 22);
		this.txtReferencia.TabIndex = 1;
		this.txtReferencia.TextChanged += new System.EventHandler(txtReferencia_TextChanged);
		this.txtReferencia.KeyDown += new System.Windows.Forms.KeyEventHandler(txtReferencia_KeyDown);
		this.txtReferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtReferencia_KeyPress);
		this.txtReferencia.Leave += new System.EventHandler(txtReferencia_Leave);
		this.txtDscto3.Location = new System.Drawing.Point(526, 219);
		this.txtDscto3.Name = "txtDscto3";
		this.txtDscto3.Size = new System.Drawing.Size(97, 22);
		this.txtDscto3.TabIndex = 10;
		this.txtDscto3.Visible = false;
		this.txtDscto3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDscto3_KeyPress);
		this.txtDscto3.Leave += new System.EventHandler(txtDscto3_Leave);
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(523, 203);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(85, 16);
		this.label11.TabIndex = 20;
		this.label11.Text = "% Dscto 3 :";
		this.label11.Visible = false;
		this.txtDscto2.Location = new System.Drawing.Point(418, 219);
		this.txtDscto2.Name = "txtDscto2";
		this.txtDscto2.Size = new System.Drawing.Size(102, 22);
		this.txtDscto2.TabIndex = 9;
		this.txtDscto2.Visible = false;
		this.txtDscto2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDscto2_KeyPress);
		this.txtDscto2.Leave += new System.EventHandler(txtDscto2_Leave);
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(418, 200);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(85, 16);
		this.label10.TabIndex = 18;
		this.label10.Text = "% Dscto 2 :";
		this.label10.Visible = false;
		this.txtPrecioNeto.Location = new System.Drawing.Point(629, 219);
		this.txtPrecioNeto.Name = "txtPrecioNeto";
		this.txtPrecioNeto.Size = new System.Drawing.Size(183, 22);
		this.txtPrecioNeto.TabIndex = 11;
		this.txtPrecioNeto.Visible = false;
		this.txtPrecioNeto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrecioNeto_KeyPress);
		this.txtPrecioNeto.KeyUp += new System.Windows.Forms.KeyEventHandler(txtPrecioNeto_KeyUp);
		this.txtPrecioNeto.Leave += new System.EventHandler(txtPrecioNeto_Leave);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(626, 200);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(97, 16);
		this.label9.TabIndex = 16;
		this.label9.Text = "Precio Total:";
		this.label9.Visible = false;
		this.txtDscto1.Location = new System.Drawing.Point(328, 219);
		this.txtDscto1.Name = "txtDscto1";
		this.txtDscto1.Size = new System.Drawing.Size(84, 22);
		this.txtDscto1.TabIndex = 8;
		this.txtDscto1.Visible = false;
		this.txtDscto1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtDscto_KeyPress);
		this.txtDscto1.Leave += new System.EventHandler(txtDscto_Leave);
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(327, 200);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(85, 16);
		this.label7.TabIndex = 14;
		this.label7.Text = "% Dscto 1 :";
		this.label7.Visible = false;
		this.txtPrecio.Location = new System.Drawing.Point(713, 222);
		this.txtPrecio.Name = "txtPrecio";
		this.txtPrecio.Size = new System.Drawing.Size(117, 22);
		this.txtPrecio.TabIndex = 7;
		this.txtPrecio.Visible = false;
		this.txtPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtPrecio_KeyPress);
		this.txtPrecio.Leave += new System.EventHandler(txtPrecio_Leave);
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(710, 203);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(119, 16);
		this.label8.TabIndex = 12;
		this.label8.Text = "Precio Unitario :";
		this.label8.Visible = false;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(12, 77);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(66, 16);
		this.label6.TabIndex = 10;
		this.label6.Text = "Unidad :";
		this.txtCantidad.Location = new System.Drawing.Point(416, 96);
		this.txtCantidad.MaxLength = 15;
		this.txtCantidad.Name = "txtCantidad";
		this.txtCantidad.Size = new System.Drawing.Size(106, 22);
		this.txtCantidad.TabIndex = 6;
		this.txtCantidad.TextChanged += new System.EventHandler(txtCantidad_TextChanged);
		this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCantidad_KeyPress);
		this.txtCantidad.Leave += new System.EventHandler(txtCantidad_Leave);
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(413, 77);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(78, 16);
		this.label5.TabIndex = 8;
		this.label5.Text = "Cantidad :";
		this.txtStock.Enabled = false;
		this.txtStock.Location = new System.Drawing.Point(164, 96);
		this.txtStock.Name = "txtStock";
		this.txtStock.Size = new System.Drawing.Size(137, 22);
		this.txtStock.TabIndex = 4;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(161, 77);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(134, 16);
		this.label4.TabIndex = 6;
		this.label4.Text = "Stock Disponible :";
		this.txtControlStock.Location = new System.Drawing.Point(307, 96);
		this.txtControlStock.Name = "txtControlStock";
		this.txtControlStock.Size = new System.Drawing.Size(100, 22);
		this.txtControlStock.TabIndex = 5;
		this.txtControlStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtControlStock_KeyPress);
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(307, 77);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(96, 16);
		this.label3.TabIndex = 4;
		this.label3.Text = "Serie / Lote :";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Enabled = false;
		this.txtDescripcion.Location = new System.Drawing.Point(118, 41);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.Size = new System.Drawing.Size(404, 22);
		this.txtDescripcion.TabIndex = 2;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(115, 22);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(99, 16);
		this.label2.TabIndex = 2;
		this.label2.Text = "Descripción :";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(12, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(92, 16);
		this.label1.TabIndex = 0;
		this.label1.Text = "Referencia :";
		this.txtUnidad.Enabled = false;
		this.txtUnidad.Location = new System.Drawing.Point(131, 154);
		this.txtUnidad.Name = "txtUnidad";
		this.txtUnidad.Size = new System.Drawing.Size(90, 20);
		this.txtUnidad.TabIndex = 14;
		this.txtUnidad.Visible = false;
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Location = new System.Drawing.Point(25, 154);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(100, 20);
		this.txtCodigo.TabIndex = 1;
		this.txtCodigo.Visible = false;
		this.txtCodigo.TextChanged += new System.EventHandler(txtCodigo_TextChanged);
		this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodigo_KeyDown);
		this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodigo_KeyPress);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(436, 155);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(120, 32);
		this.btnSalir.TabIndex = 13;
		this.btnSalir.Text = "CANCELAR";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Enabled = false;
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = (System.Drawing.Image)resources.GetObject("btnGuardar.Image");
		this.btnGuardar.Location = new System.Drawing.Point(250, 155);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(180, 32);
		this.btnGuardar.TabIndex = 12;
		this.btnGuardar.Text = "AGREGAR A LISTA";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(564, 196);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.txtCodigo);
		base.Controls.Add(this.txtUnidad);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmProductoIngreso";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Formulario de Adicion de Producto";
		base.Load += new System.EventHandler(frmDetalleIngreso_Load);
		base.Shown += new System.EventHandler(frmDetalleIngreso_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
