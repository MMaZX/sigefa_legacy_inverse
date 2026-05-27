using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DataGridViewAutoFilter;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmProductosLista : Office2007Form
{
	public List<clsProducto> prodvendidos;

	private clsAdmProducto AdmProd = new clsAdmProducto();

	public bool consultorext;

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmTipoArticulo AdmTip = new clsAdmTipoArticulo();

	public clsProducto pro = new clsProducto();

	public int CodLista = 0;

	public int codproveedor = 0;

	public bool bvalorventa = false;

	public int Proceso = 0;

	public int Procede = 0;

	public int Moneda = 0;

	public double tc = 0.0;

	public int codtrans = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public List<int> seleccion = new List<int>();

	public List<clsDetalleNotaIngreso> productoscargados = new List<clsDetalleNotaIngreso>();

	public List<clsDetalleFacturaVenta> productosfactura = new List<clsDetalleFacturaVenta>();

	public List<clsDetalleCotizacion> productoscotizacion = new List<clsDetalleCotizacion>();

	public List<clsDetalleNotaSalida> productosNotaSalida = new List<clsDetalleNotaSalida>();

	public int codalmacen = 0;

	public int codigoPro;

	public int alma = 0;

	public string referenciaPro;

	public string descripcionPro;

	public int Tipo = 0;

	public int CodVendedor;

	public bool ventasinafectarstock = false;

	public int codlin = 0;

	public int codfami = 0;

	public int codpro = 0;

	public int codigoproducto = 0;

	private int filaClickeada = -1;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private DataGridView dgvProductos;

	private ImageList imageList1;

	private Button button6;

	private ComboBox cbTipoArticulo;

	private Button btnAceptar;

	private DateTimePicker dtpFechaPago;

	private TextBox txtFiltroDescripcion;

	private TextBox txtFiltroUbicacion;

	private TextBox txtFiltroCodUniv;

	private TextBox txtFiltroMarca;

	private TextBox txtFiltroModelo;

	private Label label8;

	private Label label7;

	private Label label6;

	private Label label5;

	private Label label4;

	private Label label10;

	private TextBox txtFiltroCodigo;

	private Label label9;

	private Label lblCantidadProductos;

	private ButtonX btnnuevoproducto;

	private Label lblsolocotizacion;

	private DataGridViewAutoFilterTextBoxColumn codigo;

	private DataGridViewAutoFilterTextBoxColumn referencia;

	private DataGridViewAutoFilterTextBoxColumn codUniversal;

	private DataGridViewAutoFilterTextBoxColumn ubicacion;

	private DataGridViewAutoFilterTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn Almacen;

	private DataGridViewAutoFilterTextBoxColumn Modelo;

	private DataGridViewAutoFilterTextBoxColumn marca;

	private DataGridViewTextBoxColumn preciooferta;

	private DataGridViewTextBoxColumn stockdisponible;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn preciodolares;

	private DataGridViewTextBoxColumn preciosoles;

	private DataGridViewTextBoxColumn codlinea;

	private DataGridViewTextBoxColumn codfamilia;

	private DataGridViewTextBoxColumn cotizacion;

	private DataGridViewTextBoxColumn cod_almacen;

	private CheckBox chktodos;

	public frmProductosLista()
	{
		InitializeComponent();
	}

	public int GetCodigoProducto()
	{
		try
		{
			return Convert.ToInt32(dgvProductos.CurrentRow.Cells[referencia.Name].Value);
		}
		catch (Exception)
		{
			return 0;
		}
	}

	public int GetCodigoCotizacion()
	{
		try
		{
			return Convert.ToInt32(dgvProductos.CurrentRow.Cells[cotizacion.Name].Value);
		}
		catch (Exception)
		{
			return 0;
		}
	}

	public int GetCodigoAlmacen()
	{
		try
		{
			return Convert.ToInt32(dgvProductos.CurrentRow.Cells[cod_almacen.Name].Value);
		}
		catch (Exception)
		{
			return 0;
		}
	}

	public int GetCodigoProducto2()
	{
		try
		{
			return Convert.ToInt32(dgvProductos.CurrentRow.Cells[codigo.Name].Value);
		}
		catch (Exception)
		{
			return 0;
		}
	}

	public string GetNombreProducto()
	{
		try
		{
			return Convert.ToString(dgvProductos.CurrentRow.Cells[descripcion.Name].Value);
		}
		catch (Exception)
		{
			return "";
		}
	}

	private void frmProductosLista_Load(object sender, EventArgs e)
	{
		dgvProductos.DefaultCellStyle.Font = new Font("Verdana", 10f);
		dgvProductos.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
		dgvProductos.AutoGenerateColumns = false;
		dgvProductos.RowHeadersVisible = false;
		CargaTipoArticulos();
		CargaLista(Procede);
		lblCantidadProductos.Text = lblCantidadProductos.Text + " " + dgvProductos.RowCount;
		label2.Text = "Descripcion";
		label3.Text = "descripcion";
	}

	private void dgvProductos_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void CargaLista(int proce)
	{
		if (proce == 6 || proce == 8 || proce == 10)
		{
			dgvProductos.DataSource = data;
			data.DataSource = AdmPro.RelacionIngresoPorProveedor(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen, codproveedor);
			DepurarLista();
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvProductos.ClearSelection();
			stockdisponible.Visible = false;
			preciooferta.Visible = false;
			precioventa.Visible = false;
			preciodolares.Visible = false;
			preciosoles.Visible = false;
			Procede = 3;
			return;
		}
		switch (proce)
		{
		case 7:
			dgvProductos.DataSource = data;
			data.DataSource = AdmPro.RelacionIngreso(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen);
			DepurarLista();
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvProductos.ClearSelection();
			stockdisponible.Visible = false;
			preciooferta.Visible = false;
			precioventa.Visible = false;
			break;
		case 4:
			chktodos.Visible = true;
			dgvProductos.DataSource = data;
			data.DataSource = AdmPro.RelacionCotizacion(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen, CodLista, Convert.ToInt32(chktodos.Checked));
			DepurarLista3();
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvProductos.ClearSelection();
			preciooferta.Visible = false;
			precioventa.Visible = false;
			break;
		default:
			if (proce != 41)
			{
				switch (proce)
				{
				case 1:
					dgvProductos.DataSource = data;
					if (codalmacen != 0)
					{
						data.DataSource = AdmPro.RelacionSalida(Convert.ToInt32(cbTipoArticulo.SelectedValue), codalmacen, CodLista);
					}
					else
					{
						data.DataSource = AdmPro.RelacionSalida(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen, CodLista);
					}
					DepurarLista4();
					data.Filter = string.Empty;
					filtro = string.Empty;
					dgvProductos.ClearSelection();
					preciooferta.Visible = false;
					precioventa.Visible = false;
					break;
				case 42:
					cbTipoArticulo.SelectedValue = 1;
					dgvProductos.DataSource = data;
					if (codalmacen != 0)
					{
						data.DataSource = AdmPro.RelacionVendedor(Convert.ToInt32(cbTipoArticulo.SelectedValue), codalmacen, 0, CodVendedor);
					}
					else
					{
						data.DataSource = AdmPro.RelacionVendedor(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen, 0, CodVendedor);
					}
					if (data.Count == 0)
					{
						MessageBox.Show("El vendedor no tiene asignada una entrega");
						Close();
					}
					prodvendidos = AdmProd.ListaProdConsultor(CodVendedor);
					if (prodvendidos.Count > 0)
					{
						foreach (DataGridViewRow row2 in (IEnumerable)dgvProductos.Rows)
						{
							foreach (clsProducto prod2 in prodvendidos)
							{
								if (prod2.CodProducto == Convert.ToInt32(row2.Cells[codigo.Name].Value))
								{
									row2.Cells[stockdisponible.Name].Value = $"{Convert.ToDouble(row2.Cells[stockdisponible.Name].Value) - Convert.ToDouble(prod2.StockActual):#,##0.00}";
									break;
								}
							}
						}
					}
					DepurarLista2();
					data.Filter = string.Empty;
					filtro = string.Empty;
					dgvProductos.ClearSelection();
					preciooferta.Visible = false;
					precioventa.Visible = false;
					break;
				default:
					if (proce != 22)
					{
						if (Procede == 11 || Procede == 12 || Procede == 13 || Procede == 14 || Procede == 20)
						{
							dgvProductos.DataSource = data;
							data.DataSource = AdmPro.RelacionIngreso(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen);
							data.Filter = string.Empty;
							filtro = string.Empty;
							dgvProductos.ClearSelection();
							precioventa.Visible = false;
							preciooferta.Visible = false;
							stockdisponible.Visible = false;
							preciodolares.Visible = false;
							preciosoles.Visible = false;
						}
						else if (Procede == 15)
						{
							dgvProductos.DataSource = data;
							data.DataSource = AdmPro.RelacionProductos(frmLogin.iCodAlmacen);
							data.Filter = string.Empty;
							filtro = string.Empty;
							dgvProductos.ClearSelection();
							precioventa.Visible = false;
							preciooferta.Visible = false;
							stockdisponible.Visible = false;
							preciodolares.Visible = false;
							preciosoles.Visible = false;
						}
						else if (Procede == 90 || Procede == 91 || Procede == 92)
						{
							dgvProductos.DataSource = data;
							data.DataSource = AdmPro.RelacionIngreso(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen);
							data.Filter = string.Empty;
							filtro = string.Empty;
							dgvProductos.ClearSelection();
							precioventa.Visible = false;
							preciooferta.Visible = false;
							stockdisponible.Visible = false;
							preciodolares.Visible = false;
							preciosoles.Visible = false;
						}
						else if (Procede == 16)
						{
							dgvProductos.DataSource = data;
							if (codalmacen != 0)
							{
								data.DataSource = AdmPro.ListadoProductosParaRequerimientoAlmacen(Convert.ToInt32(cbTipoArticulo.SelectedValue), codalmacen, CodLista);
								break;
							}
							MessageBox.Show("No se ah definido almacen para listar productos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							Close();
						}
						break;
					}
					goto case 2;
				case 2:
				case 3:
					dgvProductos.DataSource = data;
					if (codalmacen != 0)
					{
						data.DataSource = AdmPro.RelacionSalida(Convert.ToInt32(cbTipoArticulo.SelectedValue), codalmacen, CodLista);
					}
					else
					{
						int CodListaEnviado = ((Convert.ToInt32(cbTipoArticulo.SelectedValue) != 2) ? 1 : 0);
						if (ventasinafectarstock && CodListaEnviado == 1)
						{
							data.DataSource = AdmPro.RelacionSalidaSinAfectarStock(Convert.ToInt32(cbTipoArticulo.SelectedValue), alma, CodListaEnviado);
						}
						else
						{
							data.DataSource = AdmPro.RelacionSalida(Convert.ToInt32(cbTipoArticulo.SelectedValue), alma, CodListaEnviado);
						}
					}
					if (consultorext)
					{
						if (data.Count == 0)
						{
							MessageBox.Show("El vendedor no tiene asignada una entrega");
							Close();
						}
						prodvendidos = AdmProd.ListaProdConsultor(CodVendedor);
						if (prodvendidos.Count > 0)
						{
							foreach (DataGridViewRow row in (IEnumerable)dgvProductos.Rows)
							{
								foreach (clsProducto prod in prodvendidos)
								{
									if (prod.CodProducto == Convert.ToInt32(row.Cells[codigo.Name].Value))
									{
										row.Cells[stockdisponible.Name].Value = $"{Convert.ToDouble(row.Cells[stockdisponible.Name].Value) - Convert.ToDouble(prod.StockActual):#,##0.00}";
										break;
									}
								}
							}
						}
					}
					DepurarLista2();
					data.Filter = string.Empty;
					filtro = string.Empty;
					dgvProductos.ClearSelection();
					preciooferta.Visible = false;
					precioventa.Visible = false;
					break;
				}
				break;
			}
			goto case 5;
		case 5:
		case 9:
			dgvProductos.DataSource = data;
			if (codalmacen != 0)
			{
				data.DataSource = AdmPro.RelacionSalida(Convert.ToInt32(cbTipoArticulo.SelectedValue), codalmacen, CodLista);
			}
			else
			{
				data.DataSource = AdmPro.RelacionSalida(Convert.ToInt32(cbTipoArticulo.SelectedValue), frmLogin.iCodAlmacen, CodLista);
			}
			DepurarLista2();
			data.Filter = string.Empty;
			filtro = string.Empty;
			dgvProductos.ClearSelection();
			preciooferta.Visible = false;
			precioventa.Visible = false;
			break;
		}
	}

	private void DepurarLista()
	{
		foreach (clsDetalleNotaIngreso deta in productoscargados)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvProductos.Rows)
			{
				if (Convert.ToInt32(row.Cells[codigo.Name].Value) == deta.CodProducto)
				{
					dgvProductos.Rows.Remove(row);
				}
			}
		}
	}

	private void DepurarLista2()
	{
		foreach (clsDetalleFacturaVenta deta in productosfactura)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvProductos.Rows)
			{
				if (Convert.ToInt32(row.Cells[codigo.Name].Value) == deta.CodProducto)
				{
					dgvProductos.Rows.Remove(row);
				}
			}
		}
	}

	private void DepurarLista3()
	{
		foreach (clsDetalleCotizacion deta in productoscotizacion)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvProductos.Rows)
			{
				if (Convert.ToInt32(row.Cells[codigo.Name].Value) == deta.CodProducto)
				{
					dgvProductos.Rows.Remove(row);
				}
			}
		}
	}

	private void DepurarLista4()
	{
		foreach (clsDetalleNotaSalida deta in productosNotaSalida)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvProductos.Rows)
			{
				if (Convert.ToInt32(row.Cells[codigo.Name].Value) == deta.CodProducto)
				{
					dgvProductos.Rows.Remove(row);
				}
			}
		}
	}

	private void CargaTipoArticulos()
	{
		cbTipoArticulo.DataSource = AdmTip.MuestraTipoArticulos();
		cbTipoArticulo.DisplayMember = "descripcion";
		cbTipoArticulo.ValueMember = "codTipoArticulo";
		cbTipoArticulo.SelectedValue = 1;
	}

	private void frmProductosLista_Shown(object sender, EventArgs e)
	{
		dgvProductos.ClearSelection();
		txtFiltroDescripcion.Focus();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
	}

	private void dgvProductos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvProductos.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvProductos.Columns[e.ColumnIndex].DataPropertyName;
		txtFiltro.Focus();
	}

	private void button6_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void dgvProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		recorrelista();
		if (Procede == 6 || Procede == 7 || Procede == 10)
		{
			base.DialogResult = DialogResult.OK;
		}
		else if (Procede == 1 || Procede == 2 || Procede == 3 || Procede == 4 || Procede == 41 || Procede == 42 || Procede == 20 || Procede == 22)
		{
			base.DialogResult = DialogResult.OK;
		}
		else if (Procede == 5 || Procede == 11 || Procede == 12 || Procede == 9)
		{
			base.DialogResult = DialogResult.OK;
		}
		else if (Procede == 13)
		{
			codigoPro = pro.CodProducto;
			referenciaPro = pro.Referencia;
			descripcionPro = pro.Descripcion;
		}
		else if (Procede == 14)
		{
			codigoPro = pro.CodProducto;
			referenciaPro = pro.Referencia;
			descripcionPro = pro.Descripcion;
		}
		else if (Procede == 15)
		{
			Close();
			base.DialogResult = DialogResult.OK;
		}
		else if (Procede == 90 || Procede == 91 || Procede == 92)
		{
			base.DialogResult = DialogResult.OK;
		}
		if (Procede != 16)
		{
			return;
		}
		if (filaClickeada != -1)
		{
			double stock = Convert.ToDouble(dgvProductos.Rows[filaClickeada].Cells[stockdisponible.Name].Value ?? ((object)0));
			if (stock == 0.0)
			{
				MessageBox.Show("No se permite Agregar productos con stock en cero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			base.DialogResult = DialogResult.Yes;
		}
		else
		{
			MessageBox.Show("No se encontrado la fila seleccionada intente nuevamente");
			base.DialogResult = DialogResult.Cancel;
		}
		Close();
	}

	private void btnAceptar_Click(object sender, EventArgs e)
	{
		recorrelista();
		if (Procede == 6 || Procede == 7 || Procede == 8)
		{
			foreach (int cod in seleccion)
			{
				if (Application.OpenForms["frmDetalleIngreso"] != null)
				{
					Application.OpenForms["frmDetalleIngreso"].Close();
				}
				frmDetalleIngreso form = new frmDetalleIngreso();
				form.Proceso = Proceso;
				form.Seleccion = 2;
				form.Procede = Procede;
				form.bvalorventa = bvalorventa;
				form.txtCodigo.Text = cod.ToString();
				if (form.repetido == 1)
				{
					form.Close();
					Close();
				}
				else
				{
					form.txtCantidad.Focus();
					form.ShowDialog();
				}
			}
		}
		else if (Procede == 1 || Procede == 2 || Procede == 3 || Procede == 4 || Procede == 22)
		{
			foreach (int cod2 in seleccion)
			{
				if (Application.OpenForms["frmDetalleSalida"] != null)
				{
					Application.OpenForms["frmDetalleSalida"].Close();
				}
				frmDetalleSalida form2 = new frmDetalleSalida();
				form2.Seleccion = 2;
				form2.Proceso = Proceso;
				form2.Codlista = CodLista;
				form2.Procede = Procede;
				form2.Moneda = Moneda;
				form2.tc = tc;
				form2.alma = alma;
				form2.txtCodigo.Text = cod2.ToString();
				form2.txtPrecio.ReadOnly = true;
				form2.codTran = codtrans;
				form2.ShowDialog();
			}
		}
		else if (Procede == 5 || Procede == 11 || Procede == 12 || Procede == 9 || Procede == 10)
		{
			foreach (int cod3 in seleccion)
			{
				if (Application.OpenForms["frmDetalleGuia"] != null)
				{
					Application.OpenForms["frmDetalleGuia"].Close();
				}
				frmDetalleGuia form3 = new frmDetalleGuia();
				form3.txtCantidad.Focus();
				form3.Seleccion = 2;
				form3.Proceso = Proceso;
				form3.Procede = Procede;
				if (Procede == 10)
				{
					form3.chBonificacion.Visible = true;
				}
				form3.txtCodigo.Text = cod3.ToString();
				if (form3.repetido == 1)
				{
					form3.Close();
					Close();
				}
				else
				{
					form3.txtCantidad.Focus();
					form3.ShowDialog();
				}
			}
		}
		else if (Procede == 13)
		{
			codigoPro = pro.CodProducto;
			referenciaPro = pro.Referencia;
			descripcionPro = pro.Descripcion;
		}
		else if (Procede == 14)
		{
			codigoPro = pro.CodProducto;
			referenciaPro = pro.Referencia;
			descripcionPro = pro.Descripcion;
		}
		else if (Procede != 16)
		{
		}
		Close();
	}

	private void recorrelista()
	{
		seleccion.Clear();
		if (dgvProductos.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in dgvProductos.SelectedRows)
		{
			seleccion.Add(Convert.ToInt32(row.Cells[codigo.Name].Value));
		}
	}

	private void cbTipoArticulo_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaLista(Procede);
	}

	private void frmProductosLista_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void txtFiltroCodigo_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtFiltroModelo_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltroModelo.Text.Length >= 1)
			{
				string Filtro = ((txtFiltroCodigo.Text.Trim().Length > 0) ? "[referencia] like '{0}' and [modelo] like '%{1}%' and [nmarca] like '%{2}%' and [codUniversal] like '%{3}%' and [ubicacion] like '%{4}%' and [descripcion] like '%{5}%'" : "[referencia] like '%{0}%' and [modelo] like '%{1}%' and [nmarca] like '%{2}%' and [codUniversal] like '%{3}%' and [ubicacion] like '%{4}%' and [descripcion] like '%{5}%'");
				data.Filter = string.Format(Filtro, txtFiltroCodigo.Text.Trim(), txtFiltroModelo.Text.Trim(), txtFiltroMarca.Text.Trim(), txtFiltroCodUniv.Text.Trim(), txtFiltroUbicacion.Text.Trim(), txtFiltroDescripcion.Text.Trim());
			}
			else
			{
				data.Filter = string.Empty;
			}
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			if (ee.KeyChar != '(')
			{
				dgvProductos.ClearSelection();
			}
		}
		catch (Exception)
		{
		}
	}

	private void txtFiltroMarca_TextChanged(object sender, EventArgs e)
	{
	}

	private void txtFiltroCodUniv_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltroCodUniv.Text.Length >= 1)
			{
				string Filtro = ((txtFiltroCodigo.Text.Trim().Length > 0) ? "[referencia] like '{0}' and [modelo] like '%{1}%' and [nmarca] like '%{2}%' and [codUniversal] like '%{3}%' and [ubicacion] like '%{4}%' and [descripcion] like '%{5}%'" : "[referencia] like '%{0}%' and [modelo] like '%{1}%' and [nmarca] like '%{2}%' and [codUniversal] like '%{3}%' and [ubicacion] like '%{4}%' and [descripcion] like '%{5}%'");
				data.Filter = string.Format(Filtro, txtFiltroCodigo.Text.Trim(), txtFiltroModelo.Text.Trim(), txtFiltroMarca.Text.Trim(), txtFiltroCodUniv.Text.Trim(), txtFiltroUbicacion.Text.Trim(), txtFiltroDescripcion.Text.Trim());
			}
			else
			{
				data.Filter = string.Empty;
			}
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			if (ee.KeyChar != '(')
			{
				dgvProductos.ClearSelection();
			}
		}
		catch (Exception)
		{
		}
	}

	private void txtFiltroUbicacion_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltroUbicacion.Text.Length >= 1)
			{
				string Filtro = ((txtFiltroCodigo.Text.Trim().Length > 0) ? "[referencia] like '{0}' and [modelo] like '%{1}%' and [nmarca] like '%{2}%' and [codUniversal] like '%{3}%' and [ubicacion] like '%{4}%' and [descripcion] like '%{5}%'" : "[referencia] like '%{0}%' and [modelo] like '%{1}%' and [nmarca] like '%{2}%' and [codUniversal] like '%{3}%' and [ubicacion] like '%{4}%' and [descripcion] like '%{5}%'");
				data.Filter = string.Format(Filtro, txtFiltroCodigo.Text.Trim(), txtFiltroModelo.Text.Trim(), txtFiltroMarca.Text.Trim(), txtFiltroCodUniv.Text.Trim(), txtFiltroUbicacion.Text.Trim(), txtFiltroDescripcion.Text.Trim());
			}
			else
			{
				data.Filter = string.Empty;
			}
			KeyPressEventArgs ee = new KeyPressEventArgs('\r');
			if (ee.KeyChar != '(')
			{
				dgvProductos.ClearSelection();
			}
		}
		catch (Exception)
		{
		}
	}

	private void txtFiltroDescripcion_TextChanged(object sender, EventArgs e)
	{
	}

	private void cbTipoArticulo_SelectedIndexChanged(object sender, EventArgs e)
	{
		switch (cbTipoArticulo.SelectedIndex)
		{
		case 0:
			txtFiltroModelo.Enabled = true;
			txtFiltroMarca.Enabled = true;
			txtFiltroCodUniv.Enabled = true;
			txtFiltroUbicacion.Enabled = true;
			break;
		case 1:
			txtFiltroModelo.Enabled = false;
			txtFiltroMarca.Enabled = false;
			txtFiltroCodUniv.Enabled = false;
			txtFiltroUbicacion.Enabled = false;
			break;
		}
	}

	private void txtFiltroCodigo_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			dgvProductos.Focus();
		}
	}

	private void txtFiltroCodigo_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (txtFiltroCodigo.Text.Length >= 3)
			{
				List<string> queries = new List<string>();
				if (txtFiltroCodigo.Text != "")
				{
					string filterCod = txtFiltroCodigo.Text;
					string[] cad = filterCod.Split(' ');
					int cont = 1;
					if (cad.Count() > 1)
					{
						string[] array = cad;
						foreach (string c in array)
						{
							if (cont == 1)
							{
								queries.Add($"referencia LIKE '%{c}'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"referencia LIKE '%{c}'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"referencia LIKE '%{filterCod}'";
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
		}
	}

	private void txtFiltroDescripcion_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (txtFiltroDescripcion.Text.Length >= 3)
			{
				List<string> queries = new List<string>();
				if (txtFiltroDescripcion.Text != "")
				{
					string filterCod = txtFiltroDescripcion.Text;
					string[] cad = filterCod.Split(' ');
					int cont = 1;
					if (cad.Count() > 1)
					{
						string[] array = cad;
						foreach (string c in array)
						{
							if (cont == 1)
							{
								queries.Add($"descripcion LIKE '%{c}%'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"descripcion LIKE '%{c}%'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"descripcion LIKE '%{filterCod}%'";
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
		}
	}

	private void txtFiltroMarca_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Cursor = Cursors.WaitCursor;
			if (txtFiltroMarca.Text.Length >= 3)
			{
				List<string> queries = new List<string>();
				if (txtFiltroMarca.Text != "")
				{
					string filterCod = txtFiltroMarca.Text;
					string[] cad = filterCod.Split(' ');
					int cont = 1;
					if (cad.Count() > 1)
					{
						string[] array = cad;
						foreach (string c in array)
						{
							if (cont == 1)
							{
								queries.Add($"marca LIKE '%{c}%'");
								string queryFilter = string.Join(" ", queries);
								data.Filter = queryFilter;
							}
							else
							{
								queries.Add($"marca LIKE '%{c}%'");
								string queryFilter2 = string.Join(" AND ", queries);
								data.Filter = queryFilter2;
							}
							cont++;
						}
					}
					if (cad.Count() == 1)
					{
						data.Filter = $"marca LIKE '%{filterCod}%'";
					}
				}
			}
			else
			{
				data.Filter = string.Empty;
			}
			Cursor = Cursors.Default;
		}
		catch (Exception)
		{
		}
	}

	private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void dgvProductos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
	{
		double coti = 0.0;
		if (e.RowIndex < 0)
		{
			return;
		}
		double stock = Convert.ToDouble(dgvProductos.Rows[e.RowIndex].Cells[stockdisponible.Name].Value ?? ((object)0));
		if (stock == 0.0 && coti == 0.0)
		{
			dgvProductos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
		}
		if (Procede == 4)
		{
			coti = Convert.ToDouble(dgvProductos.Rows[e.RowIndex].Cells[cotizacion.Name].Value ?? ((object)0));
			if (coti == 1.0)
			{
				dgvProductos.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Gray;
				dgvProductos.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.White;
				lblsolocotizacion.Visible = true;
			}
		}
	}

	private void btnnuevoproducto_Click(object sender, EventArgs e)
	{
		try
		{
			frmRegistroProducto frm = new frmRegistroProducto();
			frm.Proceso = 1;
			frm.Coti = 1;
			frm.ShowDialog();
			CargaLista(Procede);
		}
		catch (Exception)
		{
		}
	}

	private void lblsolocotizacion_Click(object sender, EventArgs e)
	{
	}

	private void chktodos_CheckedChanged(object sender, EventArgs e)
	{
		try
		{
			if (chktodos.Checked & (frmLogin.iNivelUser != 1))
			{
				DialogResult dr = DialogResult.None;
				frmAutorizacion frm = new frmAutorizacion();
				dr = frm.ShowDialog();
				if (dr == DialogResult.OK)
				{
					CargaLista(Procede);
				}
				else
				{
					chktodos.Checked = false;
				}
			}
			else
			{
				CargaLista(Procede);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			if (dgvProductos.Rows.Count >= 1 && e.RowIndex != -1 && dgvProductos.CurrentRow.Index == e.RowIndex)
			{
				DataGridViewRow Row = dgvProductos.Rows[e.RowIndex];
				filaClickeada = e.RowIndex;
				pro.CodProducto = Convert.ToInt32(Row.Cells[codigo.Name].Value);
				codigoproducto = Convert.ToInt32(Row.Cells[codigo.Name].Value);
				pro.Referencia = Row.Cells[referencia.Name].Value.ToString();
				pro.Descripcion = Row.Cells[descripcion.Name].Value.ToString();
				_ = pro.codli;
				if (pro.codli != 0)
				{
					pro.codli = Convert.ToInt32(Row.Cells[codlinea.Name].Value);
				}
				_ = pro.codfami;
				if (pro.codfami != 0)
				{
					pro.codfami = Convert.ToInt32(Row.Cells[codfamilia.Name].Value);
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtFiltro_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Down)
		{
			dgvProductos.Focus();
		}
	}

	private void dgvProductos_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		recorrelista();
		if (Procede == 6 || Procede == 7 || Procede == 8)
		{
			foreach (int cod in seleccion)
			{
				if (Application.OpenForms["frmDetalleIngreso"] != null)
				{
					Application.OpenForms["frmDetalleIngreso"].Close();
				}
				frmDetalleIngreso form = new frmDetalleIngreso();
				form.Proceso = Proceso;
				form.Seleccion = 2;
				form.Procede = Procede;
				form.bvalorventa = bvalorventa;
				form.txtCodigo.Text = cod.ToString();
				if (form.repetido == 1)
				{
					form.Close();
					Close();
				}
				else
				{
					form.txtCantidad.Focus();
					form.ShowDialog();
				}
			}
		}
		else if (Procede == 1 || Procede == 2 || Procede == 3 || Procede == 4 || Procede == 20 || Procede == 22)
		{
			base.DialogResult = DialogResult.OK;
		}
		else if (Procede == 5 || Procede == 11 || Procede == 12 || Procede == 9 || Procede == 10)
		{
			foreach (int cod2 in seleccion)
			{
				if (Application.OpenForms["frmDetalleGuia"] != null)
				{
					Application.OpenForms["frmDetalleGuia"].Close();
				}
				frmDetalleGuia form2 = new frmDetalleGuia();
				form2.txtCantidad.Focus();
				form2.Seleccion = 2;
				form2.Proceso = Proceso;
				form2.Procede = Procede;
				if (Procede == 10)
				{
					form2.chBonificacion.Visible = true;
				}
				form2.txtCodigo.Text = cod2.ToString();
				if (form2.repetido == 1)
				{
					form2.Close();
					Close();
				}
				else
				{
					form2.txtCantidad.Focus();
					form2.ShowDialog();
				}
			}
		}
		else if (Procede == 13)
		{
			codigoPro = pro.CodProducto;
			referenciaPro = pro.Referencia;
			descripcionPro = pro.Descripcion;
		}
		else if (Procede == 14)
		{
			codigoPro = pro.CodProducto;
			referenciaPro = pro.Referencia;
			descripcionPro = pro.Descripcion;
		}
		else if (Procede == 15)
		{
			int f = dgvProductos.CurrentRow.Index;
			pro.CodProducto = Convert.ToInt32(dgvProductos.Rows[f].Cells[codigo.Name].Value);
		}
		Close();
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmProductosLista));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lblsolocotizacion = new System.Windows.Forms.Label();
		this.btnnuevoproducto = new DevComponents.DotNetBar.ButtonX();
		this.label10 = new System.Windows.Forms.Label();
		this.txtFiltroCodigo = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.txtFiltroDescripcion = new System.Windows.Forms.TextBox();
		this.txtFiltroUbicacion = new System.Windows.Forms.TextBox();
		this.txtFiltroCodUniv = new System.Windows.Forms.TextBox();
		this.txtFiltroMarca = new System.Windows.Forms.TextBox();
		this.txtFiltroModelo = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.cbTipoArticulo = new System.Windows.Forms.ComboBox();
		this.dgvProductos = new System.Windows.Forms.DataGridView();
		this.codigo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.referencia = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.codUniversal = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.ubicacion = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.descripcion = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.Almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Modelo = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.marca = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
		this.preciooferta = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockdisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciodolares = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciosoles = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codlinea = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codfamilia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cotizacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cod_almacen = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.dtpFechaPago = new System.Windows.Forms.DateTimePicker();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.button6 = new System.Windows.Forms.Button();
		this.btnAceptar = new System.Windows.Forms.Button();
		this.lblCantidadProductos = new System.Windows.Forms.Label();
		this.chktodos = new System.Windows.Forms.CheckBox();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.BackColor = System.Drawing.Color.White;
		this.groupBox1.Controls.Add(this.chktodos);
		this.groupBox1.Controls.Add(this.lblsolocotizacion);
		this.groupBox1.Controls.Add(this.btnnuevoproducto);
		this.groupBox1.Controls.Add(this.label10);
		this.groupBox1.Controls.Add(this.txtFiltroCodigo);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtFiltroDescripcion);
		this.groupBox1.Controls.Add(this.txtFiltroUbicacion);
		this.groupBox1.Controls.Add(this.txtFiltroCodUniv);
		this.groupBox1.Controls.Add(this.txtFiltroMarca);
		this.groupBox1.Controls.Add(this.txtFiltroModelo);
		this.groupBox1.Controls.Add(this.label8);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Controls.Add(this.label6);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.label4);
		this.groupBox1.Controls.Add(this.cbTipoArticulo);
		this.groupBox1.Controls.Add(this.dgvProductos);
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(914, 398);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Lista de Productos";
		this.lblsolocotizacion.AutoSize = true;
		this.lblsolocotizacion.BackColor = System.Drawing.Color.Gray;
		this.lblsolocotizacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblsolocotizacion.ForeColor = System.Drawing.SystemColors.Control;
		this.lblsolocotizacion.Location = new System.Drawing.Point(6, 376);
		this.lblsolocotizacion.Name = "lblsolocotizacion";
		this.lblsolocotizacion.Size = new System.Drawing.Size(183, 13);
		this.lblsolocotizacion.TabIndex = 51;
		this.lblsolocotizacion.Text = "Productos solo para Cotización";
		this.lblsolocotizacion.Visible = false;
		this.lblsolocotizacion.Click += new System.EventHandler(lblsolocotizacion_Click);
		this.btnnuevoproducto.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
		this.btnnuevoproducto.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
		this.btnnuevoproducto.Location = new System.Drawing.Point(336, 365);
		this.btnnuevoproducto.Name = "btnnuevoproducto";
		this.btnnuevoproducto.Size = new System.Drawing.Size(45, 24);
		this.btnnuevoproducto.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
		this.btnnuevoproducto.TabIndex = 20;
		this.btnnuevoproducto.Text = "Nuevo";
		this.btnnuevoproducto.Visible = false;
		this.btnnuevoproducto.Click += new System.EventHandler(btnnuevoproducto_Click);
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(531, 26);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(131, 18);
		this.label10.TabIndex = 19;
		this.label10.Text = "Tipo de Artículo:";
		this.txtFiltroCodigo.Location = new System.Drawing.Point(115, 26);
		this.txtFiltroCodigo.Name = "txtFiltroCodigo";
		this.txtFiltroCodigo.Size = new System.Drawing.Size(165, 24);
		this.txtFiltroCodigo.TabIndex = 18;
		this.txtFiltroCodigo.TextChanged += new System.EventHandler(txtFiltroCodigo_TextChanged);
		this.txtFiltroCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltroCodigo_KeyDown);
		this.txtFiltroCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltroCodigo_KeyUp);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(42, 29);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(67, 18);
		this.label9.TabIndex = 17;
		this.label9.Text = "Código:";
		this.txtFiltroDescripcion.Location = new System.Drawing.Point(115, 106);
		this.txtFiltroDescripcion.Name = "txtFiltroDescripcion";
		this.txtFiltroDescripcion.Size = new System.Drawing.Size(787, 24);
		this.txtFiltroDescripcion.TabIndex = 16;
		this.txtFiltroDescripcion.TextChanged += new System.EventHandler(txtFiltroDescripcion_TextChanged);
		this.txtFiltroDescripcion.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltroDescripcion_KeyUp);
		this.txtFiltroUbicacion.Enabled = false;
		this.txtFiltroUbicacion.Location = new System.Drawing.Point(654, 66);
		this.txtFiltroUbicacion.Name = "txtFiltroUbicacion";
		this.txtFiltroUbicacion.Size = new System.Drawing.Size(81, 24);
		this.txtFiltroUbicacion.TabIndex = 15;
		this.txtFiltroUbicacion.Visible = false;
		this.txtFiltroUbicacion.TextChanged += new System.EventHandler(txtFiltroUbicacion_TextChanged);
		this.txtFiltroCodUniv.Enabled = false;
		this.txtFiltroCodUniv.Location = new System.Drawing.Point(115, 66);
		this.txtFiltroCodUniv.Name = "txtFiltroCodUniv";
		this.txtFiltroCodUniv.Size = new System.Drawing.Size(165, 24);
		this.txtFiltroCodUniv.TabIndex = 14;
		this.txtFiltroCodUniv.TextChanged += new System.EventHandler(txtFiltroCodUniv_TextChanged);
		this.txtFiltroMarca.Location = new System.Drawing.Point(349, 66);
		this.txtFiltroMarca.Name = "txtFiltroMarca";
		this.txtFiltroMarca.Size = new System.Drawing.Size(203, 24);
		this.txtFiltroMarca.TabIndex = 13;
		this.txtFiltroMarca.TextChanged += new System.EventHandler(txtFiltroMarca_TextChanged);
		this.txtFiltroMarca.KeyUp += new System.Windows.Forms.KeyEventHandler(txtFiltroMarca_KeyUp);
		this.txtFiltroModelo.Location = new System.Drawing.Point(358, 26);
		this.txtFiltroModelo.Name = "txtFiltroModelo";
		this.txtFiltroModelo.Size = new System.Drawing.Size(116, 24);
		this.txtFiltroModelo.TabIndex = 12;
		this.txtFiltroModelo.Visible = false;
		this.txtFiltroModelo.TextChanged += new System.EventHandler(txtFiltroModelo_TextChanged);
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(8, 109);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(103, 18);
		this.label8.TabIndex = 11;
		this.label8.Text = "Descripción:";
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label7.Location = new System.Drawing.Point(560, 69);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(88, 18);
		this.label7.TabIndex = 10;
		this.label7.Text = "Ubicación:";
		this.label7.Visible = false;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(28, 54);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(83, 36);
		this.label6.TabIndex = 9;
		this.label6.Text = "Codigo \r\nUniversal:";
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(283, 69);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(60, 18);
		this.label5.TabIndex = 8;
		this.label5.Text = "Marca:";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(283, 29);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(69, 18);
		this.label4.TabIndex = 7;
		this.label4.Text = "Modelo:";
		this.label4.Visible = false;
		this.cbTipoArticulo.FormattingEnabled = true;
		this.cbTipoArticulo.Location = new System.Drawing.Point(668, 23);
		this.cbTipoArticulo.Name = "cbTipoArticulo";
		this.cbTipoArticulo.Size = new System.Drawing.Size(234, 26);
		this.cbTipoArticulo.TabIndex = 2;
		this.cbTipoArticulo.Tag = "1";
		this.cbTipoArticulo.SelectedIndexChanged += new System.EventHandler(cbTipoArticulo_SelectedIndexChanged);
		this.cbTipoArticulo.SelectionChangeCommitted += new System.EventHandler(cbTipoArticulo_SelectionChangeCommitted);
		this.dgvProductos.AllowUserToAddRows = false;
		this.dgvProductos.AllowUserToDeleteRows = false;
		this.dgvProductos.AllowUserToResizeColumns = false;
		this.dgvProductos.AllowUserToResizeRows = false;
		this.dgvProductos.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
		this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvProductos.Columns.AddRange(this.codigo, this.referencia, this.codUniversal, this.ubicacion, this.descripcion, this.Almacen, this.Modelo, this.marca, this.preciooferta, this.stockdisponible, this.precioventa, this.preciodolares, this.preciosoles, this.codlinea, this.codfamilia, this.cotizacion, this.cod_almacen);
		this.dgvProductos.Location = new System.Drawing.Point(1, 148);
		this.dgvProductos.Name = "dgvProductos";
		this.dgvProductos.ReadOnly = true;
		this.dgvProductos.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
		this.dgvProductos.RowHeadersVisible = false;
		this.dgvProductos.RowHeadersWidth = 40;
		this.dgvProductos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvProductos.Size = new System.Drawing.Size(901, 214);
		this.dgvProductos.StandardTab = true;
		this.dgvProductos.TabIndex = 3;
		this.dgvProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProductos_CellClick);
		this.dgvProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProductos_CellContentClick);
		this.dgvProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProductos_CellDoubleClick);
		this.dgvProductos.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvProductos_ColumnHeaderMouseClick);
		this.dgvProductos.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(dgvProductos_RowPostPaint);
		this.dgvProductos.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvProductos_RowStateChanged);
		this.dgvProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(dgvProductos_KeyDown);
		this.codigo.DataPropertyName = "codProducto";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Código";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codUniversal.DataPropertyName = "codUniversal";
		this.codUniversal.HeaderText = "Código Universal";
		this.codUniversal.Name = "codUniversal";
		this.codUniversal.ReadOnly = true;
		this.codUniversal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.ubicacion.DataPropertyName = "ubicacion";
		this.ubicacion.HeaderText = "Ubicación";
		this.ubicacion.Name = "ubicacion";
		this.ubicacion.ReadOnly = true;
		this.ubicacion.Visible = false;
		this.descripcion.DataPropertyName = "descripcion";
		this.descripcion.HeaderText = "Descripción";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.Width = 400;
		this.Almacen.DataPropertyName = "Almacen";
		this.Almacen.HeaderText = "Almacen";
		this.Almacen.Name = "Almacen";
		this.Almacen.ReadOnly = true;
		this.Almacen.Width = 200;
		this.Modelo.DataPropertyName = "modelo";
		this.Modelo.HeaderText = "Modelo";
		this.Modelo.Name = "Modelo";
		this.Modelo.ReadOnly = true;
		this.Modelo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Modelo.Visible = false;
		this.marca.DataPropertyName = "nmarca";
		this.marca.HeaderText = "Marca";
		this.marca.Name = "marca";
		this.marca.ReadOnly = true;
		this.marca.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciooferta.DataPropertyName = "preciooferta";
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N2";
		dataGridViewCellStyle16.NullValue = null;
		this.preciooferta.DefaultCellStyle = dataGridViewCellStyle16;
		this.preciooferta.HeaderText = "Precio Oferta";
		this.preciooferta.Name = "preciooferta";
		this.preciooferta.ReadOnly = true;
		this.preciooferta.Visible = false;
		this.stockdisponible.DataPropertyName = "stockdisponible";
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle17.Format = "N2";
		dataGridViewCellStyle17.NullValue = null;
		this.stockdisponible.DefaultCellStyle = dataGridViewCellStyle17;
		this.stockdisponible.HeaderText = "Stock";
		this.stockdisponible.Name = "stockdisponible";
		this.stockdisponible.ReadOnly = true;
		this.stockdisponible.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.stockdisponible.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.precioventa.DataPropertyName = "precioventa";
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle18.Format = "N2";
		this.precioventa.DefaultCellStyle = dataGridViewCellStyle18;
		this.precioventa.HeaderText = "Precio";
		this.precioventa.Name = "precioventa";
		this.precioventa.ReadOnly = true;
		this.precioventa.Visible = false;
		this.preciodolares.DataPropertyName = "preciodolares";
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.preciodolares.DefaultCellStyle = dataGridViewCellStyle19;
		this.preciodolares.HeaderText = "Precio";
		this.preciodolares.Name = "preciodolares";
		this.preciodolares.ReadOnly = true;
		this.preciosoles.DataPropertyName = "preciosoles";
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.preciosoles.DefaultCellStyle = dataGridViewCellStyle20;
		this.preciosoles.HeaderText = "Soles";
		this.preciosoles.Name = "preciosoles";
		this.preciosoles.ReadOnly = true;
		this.preciosoles.Visible = false;
		this.codlinea.DataPropertyName = "codlinea";
		this.codlinea.HeaderText = "codlinea";
		this.codlinea.Name = "codlinea";
		this.codlinea.ReadOnly = true;
		this.codfamilia.DataPropertyName = "codfamilia";
		this.codfamilia.HeaderText = "codfamilia";
		this.codfamilia.Name = "codfamilia";
		this.codfamilia.ReadOnly = true;
		this.cotizacion.DataPropertyName = "cotizacion";
		this.cotizacion.HeaderText = "cotizacion";
		this.cotizacion.Name = "cotizacion";
		this.cotizacion.ReadOnly = true;
		this.cotizacion.Visible = false;
		this.cod_almacen.DataPropertyName = "cod_almacen";
		this.cod_almacen.HeaderText = "CodAlmacen";
		this.cod_almacen.Name = "cod_almacen";
		this.cod_almacen.ReadOnly = true;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(399, 415);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(81, 413);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "X";
		this.label2.Visible = false;
		this.txtFiltro.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFiltro.Location = new System.Drawing.Point(172, 415);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(221, 20);
		this.txtFiltro.TabIndex = 1;
		this.txtFiltro.Visible = false;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.txtFiltro.KeyDown += new System.Windows.Forms.KeyEventHandler(txtFiltro_KeyDown);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(10, 415);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.label1.Visible = false;
		this.dtpFechaPago.Enabled = false;
		this.dtpFechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaPago.Location = new System.Drawing.Point(439, 415);
		this.dtpFechaPago.Name = "dtpFechaPago";
		this.dtpFechaPago.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaPago.TabIndex = 22;
		this.dtpFechaPago.Tag = "16";
		this.dtpFechaPago.Visible = false;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.button6.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.button6.ImageIndex = 5;
		this.button6.ImageList = this.imageList1;
		this.button6.Location = new System.Drawing.Point(846, 411);
		this.button6.Name = "button6";
		this.button6.Size = new System.Drawing.Size(68, 32);
		this.button6.TabIndex = 5;
		this.button6.Text = "Salir";
		this.button6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.button6.UseVisualStyleBackColor = true;
		this.button6.Click += new System.EventHandler(button6_Click);
		this.btnAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.btnAceptar.ImageIndex = 6;
		this.btnAceptar.ImageList = this.imageList1;
		this.btnAceptar.Location = new System.Drawing.Point(763, 411);
		this.btnAceptar.Name = "btnAceptar";
		this.btnAceptar.Size = new System.Drawing.Size(77, 32);
		this.btnAceptar.TabIndex = 4;
		this.btnAceptar.Text = "Aceptar";
		this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAceptar.UseVisualStyleBackColor = true;
		this.btnAceptar.Visible = false;
		this.btnAceptar.Click += new System.EventHandler(btnAceptar_Click);
		this.lblCantidadProductos.AutoSize = true;
		this.lblCantidadProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblCantidadProductos.Location = new System.Drawing.Point(527, 413);
		this.lblCantidadProductos.Name = "lblCantidadProductos";
		this.lblCantidadProductos.Size = new System.Drawing.Size(198, 20);
		this.lblCantidadProductos.TabIndex = 23;
		this.lblCantidadProductos.Text = "Productos Disponibles: ";
		this.chktodos.AutoSize = true;
		this.chktodos.Location = new System.Drawing.Point(751, 65);
		this.chktodos.Name = "chktodos";
		this.chktodos.Size = new System.Drawing.Size(147, 22);
		this.chktodos.TabIndex = 52;
		this.chktodos.Text = "Todos Almacenes";
		this.chktodos.UseVisualStyleBackColor = true;
		this.chktodos.Visible = false;
		this.chktodos.CheckedChanged += new System.EventHandler(chktodos_CheckedChanged);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.White;
		base.CancelButton = this.button6;
		base.ClientSize = new System.Drawing.Size(924, 445);
		base.Controls.Add(this.lblCantidadProductos);
		base.Controls.Add(this.dtpFechaPago);
		base.Controls.Add(this.btnAceptar);
		base.Controls.Add(this.button6);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.txtFiltro);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.label2);
		base.Controls.Add(this.label3);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmProductosLista";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Productos";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(frmProductosLista_FormClosing);
		base.Load += new System.EventHandler(frmProductosLista_Load);
		base.Shown += new System.EventHandler(frmProductosLista_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
