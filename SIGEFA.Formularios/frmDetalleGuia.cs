using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmDetalleGuia : Office2007Form
{
	public static List<int> seleccion = new List<int>();

	public int Proceso = 0;

	public int repetido = 0;

	public int Seleccion = 0;

	public int Procede = 0;

	public int CodProducto = 0;

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsProducto pro = new clsProducto();

	private clsProducto prod = new clsProducto();

	public clsDetalleRequerimiento detalle;

	private clsUnidadEquivalente uniequi = new clsUnidadEquivalente();

	private clsUnidadMedida unidadMed = new clsUnidadMedida();

	private clsAdmUnidad Unid = new clsAdmUnidad();

	private decimal factorconvert = default(decimal);

	private clsValidar ok = new clsValidar();

	public DataTable data = new DataTable();

	public int Codlista = 0;

	public int codproveedor = 0;

	public int codalmacen = 0;

	private clsAdmOrdenCompra AdmOrd = new clsAdmOrdenCompra();

	public decimal stock = default(decimal);

	public frmPropuestaDeOrdenCompra ventana = null;

	public bool vieneDeReqAlmacen = false;

	public bool vieneDePropuesta = false;

	internal frmReqAlmacen ventanaReqAlm = null;

	private IContainer components = null;

	private Button btnSalir;

	private ImageList imageList1;

	private GroupBox groupBox1;

	public TextBox txtReferencia;

	private Label label6;

	private Label label5;

	private TextBox txtStock;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	public TextBox txtCodigo;

	public TextBox txtCantidad;

	public TextBox txtControlStock;

	public TextBox txtUnidad;

	public TextBox txtCodUnidad;

	public Button btnGuardar;

	public TextBox txtDescripcion;

	public ComboBox cmbUnidad;

	public CheckBox chBonificacion;

	public frmDetalleGuia()
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

	private void CargaProducto()
	{
		try
		{
			if (txtCodigo.Text != "")
			{
				pro = AdmPro.CargaProductoDetalle1(Convert.ToInt32(txtCodigo.Text), (codalmacen == 0) ? frmLogin.iCodAlmacen : codalmacen, 2, Codlista);
				if (pro != null)
				{
					CodProducto = pro.CodProducto;
					txtReferencia.Text = pro.Referencia;
					txtDescripcion.Text = pro.Descripcion;
					txtUnidad.Text = pro.UnidadDescrip;
					CargaUnidades(cmbUnidad);
					cmbUnidad.SelectedIndex = 0;
					cmbUnidad_SelectionChangeCommitted(new object(), new EventArgs());
					stock = pro.StockDisponible;
					txtStock.Text = pro.StockDisponible.ToString();
				}
				else
				{
					MessageBox.Show("El producto se encuentra con estado Inactivo", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void txtCodigo_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtCodigo.Text != "")
			{
				pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 2, Codlista, 0);
				if (Procede == 11)
				{
					frmRequerimiento form = (frmRequerimiento)Application.OpenForms["frmRequerimiento"];
					if (form.codProd.Contains(pro.CodProducto) && Proceso == 1)
					{
						MessageBox.Show("El Producto ya existe");
						repetido = 1;
					}
					else
					{
						CodProducto = pro.CodProducto;
						txtReferencia.Text = pro.Referencia;
						txtDescripcion.Text = pro.Descripcion;
						txtUnidad.Text = pro.UnidadDescrip;
						CargaUnidades(cmbUnidad);
						cmbUnidad.SelectedValue = pro.CodUnidadMedida;
						txtStock.Text = pro.StockDisponible.ToString();
						unidadMed = Unid.CargaUnidad(Convert.ToInt32(cmbUnidad.SelectedValue));
						txtUnidad.Text = unidadMed.Descripcion;
						txtControlStock.Text = "";
						txtCantidad.Text = "";
						btnGuardar.Enabled = true;
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
					}
				}
				else if (Procede == 3)
				{
					CargaProducto();
				}
				else if (Procede == 9)
				{
					frmTranferenciaDirecta form2 = (frmTranferenciaDirecta)Application.OpenForms["frmTranferenciaDirecta"];
					if (form2.codProd.Contains(pro.CodProducto) && Proceso == 1)
					{
						MessageBox.Show("El Producto ya existe");
						repetido = 1;
					}
					else
					{
						CodProducto = pro.CodProducto;
						txtReferencia.Text = pro.Referencia;
						txtDescripcion.Text = pro.Descripcion;
						txtUnidad.Text = pro.UnidadDescrip;
						CargaUnidades(cmbUnidad);
						cmbUnidad.SelectedValue = pro.CodUnidadMedida;
						stock = pro.StockDisponible;
						txtStock.Text = pro.StockDisponible.ToString();
						txtControlStock.Text = "";
						txtCantidad.Text = "";
						btnGuardar.Enabled = true;
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
					}
				}
				else
				{
					if (Procede == 2)
					{
						pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 2, 0, 0);
					}
					else
					{
						pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 2, Codlista, 0);
					}
					CodProducto = pro.CodProducto;
					txtReferencia.Text = pro.Referencia;
					txtDescripcion.Text = pro.Descripcion;
					txtUnidad.Text = pro.UnidadDescrip;
					CargaUnidades(cmbUnidad);
					cmbUnidad.SelectedValue = pro.CodUnidadMedida;
					stock = pro.StockDisponible;
					txtStock.Text = pro.StockDisponible.ToString();
					txtControlStock.Text = "";
					txtCantidad.Text = "";
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
				}
			}
			if (!(txtCodigo.Text != "") || Procede != 11)
			{
				return;
			}
			if (Procede == 11)
			{
				frmRequerimiento form3 = (frmRequerimiento)Application.OpenForms["frmRequerimiento"];
				if (!form3.codProd.Contains(pro.CodProducto))
				{
				}
			}
			else if (Procede == 9)
			{
				frmTranferenciaDirecta form4 = (frmTranferenciaDirecta)Application.OpenForms["frmTranferenciaDirecta"];
				if (!form4.codProd.Contains(pro.CodProducto))
				{
				}
			}
			else
			{
				pro = AdmPro.CargaProductoDetalle(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, 2, Codlista, 0);
				txtStock.Text = pro.StockDisponible.ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
		if (e.KeyChar == '\r')
		{
			if (txtCantidad.Text != "")
			{
				if (Convert.ToDouble(txtCantidad.Text) > Convert.ToDouble(txtStock.Text))
				{
					btnGuardar.Enabled = false;
				}
				else
				{
					btnGuardar.Enabled = true;
					ProcessTabKey(forward: true);
				}
			}
			else
			{
				MessageBox.Show("Cantidad no disponible, verifique el stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				txtCantidad.Focus();
			}
		}
		if (Procede == 9 && !(txtCantidad.Text != ""))
		{
		}
	}

	private void txtCantidad_Leave(object sender, EventArgs e)
	{
		if (!vieneDePropuesta)
		{
			if (Convert.ToDouble(pro.StockDisponible) == 0.0)
			{
				MessageBox.Show("Producto no tiene stock para la transferencia", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				txtCantidad.Text = "";
				txtCantidad.Focus();
			}
			if (pro != null && txtCantidad.Text != "" && Convert.ToDouble(txtCantidad.Text) > Convert.ToDouble(txtStock.Text))
			{
				MessageBox.Show("Cantidad debe ser menor a Stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				txtCantidad.Text = "";
				txtCantidad.Focus();
			}
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (Procede == 5)
		{
			frmGuiaRemision form = (frmGuiaRemision)Application.OpenForms["frmGuiaRemision"];
			if (txtCantidad.Text == "")
			{
				MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtCantidad.Focus();
			}
			else if (form.dgvDetalle.Rows.Count < 1000)
			{
				if (Proceso == 1)
				{
				}
				double Precio = 0.0;
				int estado = 0;
				bool result = form.validaciones(CodProducto, Convert.ToDouble(txtCantidad.Text));
				if (chBonificacion.Checked)
				{
					Precio = 0.0;
					estado = 3;
				}
				else
				{
					Precio = pro.PrecioVenta;
					estado = 0;
				}
				if (result)
				{
					form.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, txtCodUnidad.Text, cmbUnidad.Text, txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), "", "", "", "", "", "", "", "", "", "", "");
				}
				limpiarformulario();
				if (Seleccion == 2)
				{
					Close();
				}
			}
			else
			{
				MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		if (Procede == 11)
		{
			frmRequerimiento form2 = (frmRequerimiento)Application.OpenForms["frmRequerimiento"];
			if (txtCantidad.Text == "")
			{
				MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtCantidad.Focus();
			}
			else if (form2.dgvDetalle.Rows.Count < 3000)
			{
				if (form2.proce == 1)
				{
					form2.dgvDetalle.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, Convert.ToDouble(txtCantidad.Text), "", "");
					form2.codProd.Add(pro.CodProducto);
					limpiarformulario();
					if (Seleccion == 2)
					{
						Close();
					}
				}
				else if (form2.proce == 2)
				{
					form2.dgvDetalle.CurrentRow.SetValues(detalle.CodDetalleRequerimiento, pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, Convert.ToDouble(txtCantidad.Text), detalle.CodUser, detalle.FechaRegistro);
					form2.codProd.Add(pro.CodProducto);
					limpiarformulario();
					Close();
				}
				else if (form2.proce == 3)
				{
					data = (DataTable)form2.dgvDetalle.DataSource;
					data.Rows.Add(0, pro.CodProducto, pro.Referencia, pro.Descripcion, cmbUnidad.SelectedValue, txtUnidad.Text, Convert.ToDouble(txtCantidad.Text), 0, DateTime.Now);
					form2.dgvDetalle.DataSource = data;
					limpiarformulario();
					form2.codProd.Add(pro.CodProducto);
					if (Seleccion == 2)
					{
						Close();
					}
				}
			}
			else
			{
				MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		if (Procede == 3)
		{
			if (txtCantidad.Text == "")
			{
				MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtCantidad.Focus();
				return;
			}
			if (Convert.ToDecimal(txtCantidad.Text) > Convert.ToDecimal(txtStock.Text))
			{
				MessageBox.Show("Cantidad no puede ser mayor al Stock disponible", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtCantidad.Focus();
				return;
			}
			if (vieneDeReqAlmacen)
			{
				List<GridViewRowInfo> encontrado = Enumerable.Where<GridViewRowInfo>(ventanaReqAlm.rgvDetalleRequerimiento.Rows.AsEnumerable(), (Func<GridViewRowInfo, bool>)((GridViewRowInfo x) => Convert.ToInt32(x.Cells["colCodProducto"].Value) == pro.CodProducto && Convert.ToInt32(x.Cells["colCodUnidad"].Value) == Convert.ToInt32(cmbUnidad.SelectedValue))).ToList();
				if (encontrado.Count > 0)
				{
					MessageBox.Show("Producto INGRESADO con la misma unidad.\n Referencia: " + pro.Referencia + " - Unidad: " + cmbUnidad.Text, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				ventanaReqAlm.rgvDetalleRequerimiento.Rows.Add("", pro.CodProducto, pro.Referencia, pro.Descripcion, Convert.ToInt32(cmbUnidad.SelectedValue), cmbUnidad.Text, Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtStock.Text), 0);
				base.DialogResult = DialogResult.Yes;
				Close();
				return;
			}
			F2TransferenciaEntreAlmacenes form3 = (F2TransferenciaEntreAlmacenes)Application.OpenForms["F2TransferenciaEntreAlmacenes"];
			decimal bruto = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(pro.ValorProm);
			decimal precioventa;
			decimal valorventa;
			if (pro.ConIgv)
			{
				precioventa = bruto;
				decimal factorigv = Convert.ToDecimal(frmLogin.Configuracion.IGV) / 100m + 1m;
				valorventa = precioventa / factorigv;
			}
			else
			{
				valorventa = bruto;
				precioventa = valorventa;
			}
			decimal precioreal = precioventa / Convert.ToDecimal(txtCantidad.Text);
			decimal valorreal = valorventa / Convert.ToDecimal(txtCantidad.Text);
			decimal igv = precioventa - valorventa;
			DataTable unidades = (DataTable)cmbUnidad.DataSource;
			List<object> CodUnd = (from x in unidades.AsEnumerable()
				where Convert.ToInt32(x.ItemArray[0]) == Convert.ToInt32(cmbUnidad.SelectedValue)
				select x.ItemArray[1]).ToList();
			List<object> DescUnd = (from x in unidades.AsEnumerable()
				where Convert.ToInt32(x.ItemArray[0]) == Convert.ToInt32(cmbUnidad.SelectedValue)
				select x.ItemArray[2]).ToList();
			if (form3.dgvDetalle.Rows.Count < 15000)
			{
				form3.dgvDetalle.Rows.Add("", pro.CodProducto, txtReferencia.Text, txtDescripcion.Text, Convert.ToInt32(CodUnd[0]), DescUnd[0].ToString(), txtControlStock.Text, Convert.ToDouble(txtCantidad.Text), pro.ValorProm, bruto, 0, 0, 0, 0, valorventa, igv, precioventa, precioreal, precioreal, "", "", 0, pro.ValorProm, uniequi.Factor);
				limpiarformulario();
				if (Seleccion == 2)
				{
					Close();
				}
				btnGuardar.Enabled = false;
				txtReferencia.Focus();
			}
			else
			{
				MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "Detalle Articulo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		if (Procede == 9)
		{
			frmTranferenciaDirecta form4 = (frmTranferenciaDirecta)Application.OpenForms["frmTranferenciaDirecta"];
			form4.Procede = 9;
			if (txtCantidad.Text == "")
			{
				MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtCantidad.Focus();
			}
			else if (Convert.ToDecimal(txtCantidad.Text) > pro.StockDisponible)
			{
				MessageBox.Show("Cantidad no disponible, verifique el stock", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				txtCantidad.Focus();
			}
			else if (form4.dgvDetalle.Rows.Count < 10000)
			{
				prod = AdmPro.MuestraProductosTransferencia(pro.CodProducto, frmLogin.iCodAlmacen);
				if (Proceso == 1)
				{
					form4.dgvDetalle.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, txtCodUnidad.Text, txtUnidad.Text, Convert.ToDouble(txtCantidad.Text), prod.ValorProm, prod.ValorPromsoles, prod.PrecioProm, prod.StockActual);
					limpiarformulario();
					if (Seleccion == 2)
					{
						Close();
					}
				}
				else if (Proceso == 2)
				{
					form4.dgvDetalle.CurrentRow.SetValues("0", Convert.ToInt32(txtCodigo.Text), txtReferencia.Text, txtDescripcion.Text, Convert.ToInt32(txtCodUnidad.Text), txtUnidad.Text, Convert.ToDouble(txtCantidad.Text), prod.ValorProm, prod.ValorPromsoles, prod.PrecioProm, prod.StockActual);
					limpiarformulario();
					Close();
				}
			}
			else
			{
				MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		if (Procede == 10)
		{
			frmNotaIngreso form5 = (frmNotaIngreso)Application.OpenForms["frmNotaIngreso"];
			if (txtCantidad.Text == "" || Convert.ToInt32(txtCantidad.Text) == 0)
			{
				MessageBox.Show("Verificar Cantidad", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				txtCantidad.Focus();
			}
			else
			{
				decimal Precio2;
				decimal Bonificacion;
				if (chBonificacion.Checked)
				{
					Precio2 = default(decimal);
					Bonificacion = 1m;
				}
				else
				{
					Precio2 = AdmPro.CargaPrecioProducto(Convert.ToInt32(txtCodigo.Text), frmLogin.iCodAlmacen, Convert.ToInt32(form5.cmbMoneda.SelectedValue));
					Bonificacion = default(decimal);
				}
				decimal bruto2 = Convert.ToDecimal(txtCantidad.Text) * Precio2;
				decimal precioventa2;
				decimal valorventa2;
				if (pro.ConIgv)
				{
					precioventa2 = bruto2;
					decimal factorigv2 = Convert.ToDecimal(frmLogin.Configuracion.IGV / 100.0 + 1.0);
					valorventa2 = precioventa2 / factorigv2;
				}
				else
				{
					valorventa2 = Precio2;
					precioventa2 = valorventa2;
				}
				decimal precioreal2 = precioventa2 / Convert.ToDecimal(txtCantidad.Text);
				decimal valorreal2 = valorventa2 / Convert.ToDecimal(txtCantidad.Text);
				decimal igv2 = precioventa2 - valorventa2;
				DataTable unidades2 = (DataTable)cmbUnidad.DataSource;
				List<object> CodUnd2 = (from x in unidades2.AsEnumerable()
					where Convert.ToInt32(x.ItemArray[0]) == Convert.ToInt32(cmbUnidad.SelectedValue)
					select x.ItemArray[1]).ToList();
				List<object> DescUnd2 = (from x in unidades2.AsEnumerable()
					where Convert.ToInt32(x.ItemArray[0]) == Convert.ToInt32(cmbUnidad.SelectedValue)
					select x.ItemArray[2]).ToList();
				if (Proceso == 1)
				{
					data = (DataTable)form5.dgvDetalle2.DataSource;
					if (form5.dgvDetalle2.Rows.Count <= 0)
					{
						form5.dgvDetalle2.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, DescUnd2[0].ToString(), Convert.ToInt32(CodUnd2[0]), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtStock.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(Precio2), bruto2, 0, 0, 0, 0, valorventa2, igv2, precioventa2, precioreal2, valorreal2, 0, Convert.ToDouble(txtCantidad.Text), 0, Convert.ToDouble(txtCantidad.Text), 0, Bonificacion);
					}
					else if (data == null)
					{
						form5.dgvDetalle2.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, DescUnd2[0].ToString(), Convert.ToInt32(CodUnd2[0]), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtStock.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(Precio2), bruto2, 0, 0, 0, 0, valorventa2, igv2, precioventa2, precioreal2, valorreal2, 0, Convert.ToDouble(txtCantidad.Text), 0, Convert.ToDouble(txtCantidad.Text), 0, Bonificacion);
					}
					else
					{
						data.Rows.Add("0", pro.CodProducto, pro.Referencia, pro.Descripcion, DescUnd2[0].ToString(), Convert.ToInt32(CodUnd2[0]), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(txtStock.Text), Convert.ToDouble(txtCantidad.Text), Convert.ToDouble(Precio2), bruto2, 0, 0, 0, 0, valorventa2, igv2, precioventa2, precioreal2, valorreal2, 0, Convert.ToDouble(txtCantidad.Text), 0, Convert.ToDouble(txtCantidad.Text), 0, Bonificacion);
						form5.dgvDetalle2.DataSource = data;
					}
					limpiarformulario();
					if (Seleccion == 2)
					{
						Close();
					}
				}
				else
				{
					MessageBox.Show("Se alcanzo el limite de items permitidos en el formato", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
			}
		}
		if (Procede != 1 || !vieneDePropuesta)
		{
			return;
		}
		unidadMed = Unid.CargaUnidad(Convert.ToInt32((cmbUnidad.SelectedValue != null) ? cmbUnidad.SelectedValue : ((object)0)));
		base.DialogResult = DialogResult.Yes;
		frmPropuestaDeOrdenCompra form6 = new frmPropuestaDeOrdenCompra();
		form6 = ((ventana == null) ? ((frmPropuestaDeOrdenCompra)Application.OpenForms["frmPropuestaDeOrdenCompra"]) : ventana);
		int ctdad = form6.tableDGV.Rows.Count;
		if (ctdad == 0)
		{
			form6.conviertiendoDGVaData();
		}
		else
		{
			List<DataRow> ubi = (from x in form6.tableDGV.AsEnumerable()
				where Convert.ToInt32(x.Field<object>("codProd")) == pro.CodProducto
				select x).ToList();
			if (ubi.Count > 0)
			{
				MessageBox.Show("Item ya esta AGREGADO", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				base.DialogResult = DialogResult.Cancel;
				return;
			}
		}
		double ult_pre = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(pro.CodProducto, unidadMed.CodUnidad, 0));
		string s_ult_pre = ult_pre.ToString("C", CultureInfo.CreateSpecificCulture("es-PE"));
		form6.tableDGV.Rows.Add(ctdad + 1, null, pro.CodProducto, pro.Referencia, pro.Descripcion, unidadMed.CodUnidad, unidadMed.Descripcion, txtStock.Text, null, null, txtCantidad.Text, ult_pre);
		Close();
	}

	private void CargaFilaDetalle()
	{
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
		if (txtReferencia.Text.Trim() == "")
		{
			txtReferencia.Focus();
		}
		if (Proceso == 2)
		{
			txtCantidad.Focus();
		}
		else if ((Proceso == 3 && txtDescripcion.Text.Trim() != "") || (Proceso == 1 && txtDescripcion.Text.Trim() != ""))
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
	}

	private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
	{
	}

	private void txtReferencia_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtReferencia_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmProductosLista"] != null)
			{
				Application.OpenForms["frmProductosLista"].Activate();
				return;
			}
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 16;
			frm.Proceso = Proceso;
			frm.codproveedor = codproveedor;
			frm.CodLista = Codlista;
			if (codalmacen != 0)
			{
				frm.codalmacen = codalmacen;
			}
			DialogResult rpta = frm.ShowDialog();
			if (rpta == DialogResult.Yes && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodigo.Text = Convert.ToString(frm.pro.CodProducto);
				txtCantidad.Focus();
				btnGuardar.Enabled = true;
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
		if (e.KeyChar != '\r' || !(txtReferencia.Text != ""))
		{
			return;
		}
		if (BuscaProducto())
		{
			if (Procede == 3)
			{
				CargaProducto();
			}
			ProcessTabKey(forward: true);
			btnGuardar.Enabled = true;
		}
		else
		{
			MessageBox.Show("Verifique datos del producto ", "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private bool BuscaProducto()
	{
		pro = AdmPro.CargaProductoDetalleR(txtReferencia.Text, (codalmacen == 0) ? frmLogin.iCodAlmacen : codalmacen, 2, Codlista);
		if (pro != null)
		{
			if (Procede == 11)
			{
				frmRequerimiento form = (frmRequerimiento)Application.OpenForms["frmRequerimiento"];
				if (form.codProd.Contains(pro.CodProducto) && Proceso == 1)
				{
					MessageBox.Show("El Producto ya existe");
					repetido = 1;
					return false;
				}
				CodProducto = pro.CodProducto;
				txtReferencia.Text = pro.Referencia;
				txtDescripcion.Text = pro.Descripcion;
				txtUnidad.Text = pro.UnidadDescrip;
				CargaUnidades(cmbUnidad);
				cmbUnidad.SelectedValue = pro.CodUnidadMedida;
				txtStock.Text = pro.StockDisponible.ToString();
				txtControlStock.Text = "";
				txtCantidad.Text = "";
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
				return true;
			}
			if (Procede == 9)
			{
				frmTranferenciaDirecta form2 = (frmTranferenciaDirecta)Application.OpenForms["frmTranferenciaDirecta"];
				if (form2.codProd.Contains(pro.CodProducto) && Proceso == 1)
				{
					MessageBox.Show("El Producto ya existe");
					repetido = 1;
					return false;
				}
				CodProducto = pro.CodProducto;
				txtReferencia.Text = pro.Referencia;
				txtDescripcion.Text = pro.Descripcion;
				txtUnidad.Text = pro.UnidadDescrip;
				CargaUnidades(cmbUnidad);
				cmbUnidad.SelectedValue = pro.CodUnidadMedida;
				txtStock.Text = pro.StockDisponible.ToString();
				txtControlStock.Text = "";
				txtCantidad.Text = "";
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
				return true;
			}
			CodProducto = pro.CodProducto;
			if (pro == null)
			{
				return false;
			}
			txtCodigo.Text = pro.CodProducto.ToString();
			txtReferencia.Text = pro.Referencia;
			txtDescripcion.Text = pro.Descripcion;
			txtUnidad.Text = pro.UnidadDescrip;
			CargaUnidades(cmbUnidad);
			cmbUnidad.SelectedValue = pro.CodUnidadMedida;
			if (pro.CodUnidadMedida > 0)
			{
				cmbUnidad.SelectedIndex = 0;
			}
			txtStock.Text = pro.StockDisponible.ToString();
			txtControlStock.Text = "";
			txtCantidad.Text = "";
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
			return true;
		}
		CodProducto = 0;
		txtDescripcion.Text = "";
		txtUnidad.Text = "";
		cmbUnidad.SelectedIndex = -1;
		txtStock.Text = "";
		txtControlStock.Text = "";
		txtCantidad.Text = "";
		return false;
	}

	private void CargaUnidades(ComboBox combo)
	{
		combo.Enabled = true;
		combo.Visible = true;
		if (Procede == 12)
		{
			combo.DataSource = AdmPro.MuestraUnidadesEquivalentesCompra(CodProducto, frmLogin.iCodAlmacen);
			combo.DisplayMember = "descripcion";
			combo.ValueMember = "codUnidadMedida";
		}
		else if ((Procede == 1 && vieneDePropuesta) || vieneDeReqAlmacen)
		{
			combo.DataSource = AdmPro.MuestraUnidadesEquivalentes(CodProducto, frmLogin.iCodAlmacen);
			combo.DisplayMember = "descripcion";
			combo.ValueMember = "codUnidadMedida";
		}
		else
		{
			combo.DataSource = AdmPro.MuestraUnidadesEquivalentesVenta1(CodProducto, frmLogin.iCodAlmacen);
			combo.DisplayMember = "descripcion";
			combo.ValueMember = "codUnidadEquivalente";
		}
		if (combo.Items.Count > 0)
		{
			txtStock.Visible = true;
			label4.Visible = true;
		}
	}

	private void cmbUnidad_SelectedValueChanged(object sender, EventArgs e)
	{
	}

	private void cmbUnidad_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void groupBox1_Enter(object sender, EventArgs e)
	{
	}

	private void cmbUnidad_SelectionChangeCommitted(object sender, EventArgs e)
	{
		try
		{
			uniequi = AdmPro.PrecioVenta(Convert.ToInt32(cmbUnidad.SelectedValue), frmLogin.iCodAlmacen);
			if (uniequi == null)
			{
				return;
			}
			if ((vieneDePropuesta || vieneDeReqAlmacen) && cmbUnidad.SelectedValue != null)
			{
				decimal stockdisponiblesegununidad = default(decimal);
				if (Convert.ToInt32(cmbUnidad.SelectedValue) != pro.CodUnidadMedida)
				{
					clsUnidadEquivalente undequi = AdmPro.CargaUnidadEquivalente(Convert.ToInt32(cmbUnidad.SelectedValue), pro.CodProducto, 2);
					double factorUE = 0.0;
					if (undequi != null)
					{
						factorUE = Convert.ToDouble(undequi.Factor);
						stockdisponiblesegununidad = pro.StockDisponible / Convert.ToDecimal((factorUE == 0.0) ? 1.0 : factorUE);
					}
					else
					{
						stockdisponiblesegununidad = pro.StockDisponible;
					}
				}
				else
				{
					stockdisponiblesegununidad = pro.StockDisponible;
				}
				txtStock.Text = stockdisponiblesegununidad.ToString("###0.0000");
			}
			else
			{
				txtStock.Text = $"{uniequi.Stock:###0.0000}";
			}
			txtCodUnidad.Text = uniequi.CodUnidad.ToString();
			txtCantidad.Focus();
			btnGuardar.Enabled = true;
			txtCantidad.Enabled = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void frmDetalleGuia_Load(object sender, EventArgs e)
	{
		CargaUnidades(cmbUnidad);
		cmbUnidad.SelectedValue = -1;
		if (vieneDePropuesta || vieneDeReqAlmacen)
		{
			chBonificacion.Visible = false;
			txtUnidad.Visible = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmDetalleGuia));
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.chBonificacion = new System.Windows.Forms.CheckBox();
		this.cmbUnidad = new System.Windows.Forms.ComboBox();
		this.txtReferencia = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtCantidad = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtStock = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtUnidad = new System.Windows.Forms.TextBox();
		this.txtControlStock = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.txtCodUnidad = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.chBonificacion);
		this.groupBox1.Controls.Add(this.cmbUnidad);
		this.groupBox1.Controls.Add(this.txtReferencia);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.txtCantidad);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.txtStock);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.txtDescripcion);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(471, 115);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Ingresar Artículo";
		this.groupBox1.Enter += new System.EventHandler(groupBox1_Enter);
		this.chBonificacion.AutoSize = true;
		this.chBonificacion.Location = new System.Drawing.Point(15, 98);
		this.chBonificacion.Name = "chBonificacion";
		this.chBonificacion.Size = new System.Drawing.Size(84, 17);
		this.chBonificacion.TabIndex = 11;
		this.chBonificacion.Text = "Bonificacion";
		this.chBonificacion.UseVisualStyleBackColor = true;
		this.cmbUnidad.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbUnidad.FormattingEnabled = true;
		this.cmbUnidad.Location = new System.Drawing.Point(14, 73);
		this.cmbUnidad.Name = "cmbUnidad";
		this.cmbUnidad.Size = new System.Drawing.Size(202, 21);
		this.cmbUnidad.TabIndex = 19;
		this.cmbUnidad.Visible = false;
		this.cmbUnidad.SelectedIndexChanged += new System.EventHandler(cmbUnidad_SelectedIndexChanged);
		this.cmbUnidad.SelectionChangeCommitted += new System.EventHandler(cmbUnidad_SelectionChangeCommitted);
		this.cmbUnidad.SelectedValueChanged += new System.EventHandler(cmbUnidad_SelectedValueChanged);
		this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtReferencia.Location = new System.Drawing.Point(12, 32);
		this.txtReferencia.Name = "txtReferencia";
		this.txtReferencia.Size = new System.Drawing.Size(100, 20);
		this.txtReferencia.TabIndex = 1;
		this.txtReferencia.KeyDown += new System.Windows.Forms.KeyEventHandler(txtReferencia_KeyDown);
		this.txtReferencia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtReferencia_KeyPress);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(12, 57);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(47, 13);
		this.label6.TabIndex = 10;
		this.label6.Text = "Unidad :";
		this.txtCantidad.Location = new System.Drawing.Point(240, 74);
		this.txtCantidad.MaxLength = 15;
		this.txtCantidad.Name = "txtCantidad";
		this.txtCantidad.Size = new System.Drawing.Size(115, 20);
		this.txtCantidad.TabIndex = 4;
		this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCantidad_KeyPress);
		this.txtCantidad.Leave += new System.EventHandler(txtCantidad_Leave);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(237, 58);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(55, 13);
		this.label5.TabIndex = 8;
		this.label5.Text = "Cantidad :";
		this.txtStock.Enabled = false;
		this.txtStock.Location = new System.Drawing.Point(364, 32);
		this.txtStock.Name = "txtStock";
		this.txtStock.ReadOnly = true;
		this.txtStock.Size = new System.Drawing.Size(100, 20);
		this.txtStock.TabIndex = 2;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(361, 16);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(93, 13);
		this.label4.TabIndex = 6;
		this.label4.Text = "Stock Disponible :";
		this.txtDescripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDescripcion.Enabled = false;
		this.txtDescripcion.Location = new System.Drawing.Point(118, 32);
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.ReadOnly = true;
		this.txtDescripcion.Size = new System.Drawing.Size(237, 20);
		this.txtDescripcion.TabIndex = 5;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(115, 16);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(69, 13);
		this.label2.TabIndex = 2;
		this.label2.Text = "Descripcion :";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(9, 16);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Referencia :";
		this.txtUnidad.Enabled = false;
		this.txtUnidad.Location = new System.Drawing.Point(5, 133);
		this.txtUnidad.Name = "txtUnidad";
		this.txtUnidad.ReadOnly = true;
		this.txtUnidad.Size = new System.Drawing.Size(90, 20);
		this.txtUnidad.TabIndex = 3;
		this.txtControlStock.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtControlStock.Location = new System.Drawing.Point(222, 133);
		this.txtControlStock.Name = "txtControlStock";
		this.txtControlStock.Size = new System.Drawing.Size(80, 20);
		this.txtControlStock.TabIndex = 17;
		this.txtControlStock.Visible = false;
		this.txtControlStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtControlStock_KeyPress);
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(222, 117);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(69, 13);
		this.label3.TabIndex = 4;
		this.label3.Text = "Serie / Lote :";
		this.label3.Visible = false;
		this.txtCodigo.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Location = new System.Drawing.Point(171, 133);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(45, 20);
		this.txtCodigo.TabIndex = 16;
		this.txtCodigo.Visible = false;
		this.txtCodigo.TextChanged += new System.EventHandler(txtCodigo_TextChanged);
		this.txtCodigo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtCodigo_KeyPress);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(397, 125);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 7;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(314, 125);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 6;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.txtCodUnidad.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtCodUnidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodUnidad.Location = new System.Drawing.Point(125, 134);
		this.txtCodUnidad.Name = "txtCodUnidad";
		this.txtCodUnidad.Size = new System.Drawing.Size(40, 20);
		this.txtCodUnidad.TabIndex = 20;
		this.txtCodUnidad.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(471, 165);
		base.Controls.Add(this.txtCodUnidad);
		base.Controls.Add(this.txtUnidad);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.txtCodigo);
		base.Controls.Add(this.label3);
		base.Controls.Add(this.txtControlStock);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmDetalleGuia";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Detalle Guia";
		base.Load += new System.EventHandler(frmDetalleGuia_Load);
		base.Shown += new System.EventHandler(frmDetalleSalida_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
