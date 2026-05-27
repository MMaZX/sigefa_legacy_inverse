using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmOrdenCompraCotizacion : RadForm
{
	public int Proceso = 0;

	private clsAdmCotizacion AdmCoti = new clsAdmCotizacion();

	private clsAdmOrdenCompraCotizacion AdmOrdenC = new clsAdmOrdenCompraCotizacion();

	public List<clsDetalleOrdenCompraCotizacion> detalle = new List<clsDetalleOrdenCompraCotizacion>();

	public List<clsDetalleCotizacion> detalleelimados = new List<clsDetalleCotizacion>();

	public string CodCotizacion;

	public int CodOrdenCompraVar;

	private clsCotizacion cotizacion = new clsCotizacion();

	private clsOrdenCompraCotizacion OrdenCompra = new clsOrdenCompraCotizacion();

	private clsAdmUsuario admUsuario = new clsAdmUsuario();

	public clsUsuario vendedor;

	private clsAdmMoneda AdmMon = new clsAdmMoneda();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private bool Genera = true;

	private object valorInicial = null;

	private IContainer components = null;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private RadGroupBox gbDatos;

	private RadGroupBox gbCabecera;

	private RadGroupBox gbDatosCliente;

	private RadGroupBox gbVendedor;

	private RadGroupBox gbCondiciones;

	private RadGroupBox gbTotales;

	private RadTextBox txtordenCompra;

	private RadTextBox txtcomentario;

	private RadTextBox txtdireccion;

	private RadTextBox txtnombrecliente;

	private RadTextBox txtcodcliente;

	private RadLabel radLabel3;

	private RadLabel radLabel2;

	private RadLabel radLabel1;

	private RadTextBox txtnombrevendedor;

	private RadTextBox txtcodvendedor;

	private RadLabel radLabel4;

	private RadTextBox txttipocambio;

	private RadLabel radLabel7;

	private RadMultiColumnComboBox cmbformapago;

	private RadLabel radLabel6;

	private RadLabel radLabel5;

	private RadMultiColumnComboBox cmbMoneda;

	private RadGroupBox radGroupBox1;

	private RadButton btneditar;

	private RadButton btnguardar;

	private RadButton btnsalir;

	private RadLabel radLabel13;

	private RadLabel radLabel12;

	private RadLabel radLabel11;

	private RadLabel radLabel10;

	private RadTextBox txtdescuento;

	private RadTextBox txttotal;

	private RadTextBox txtigv;

	private RadTextBox txtvalorventa;

	public DataGridView dgvDetalle;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn total;

	private DataGridViewTextBoxColumn stockDisp;

	private DataGridViewTextBoxColumn GananciaPorcentaje;

	private DataGridViewTextBoxColumn GananciaMonto;

	private DataGridViewTextBoxColumn CostoTotal;

	private DataGridViewTextBoxColumn dsctoMax;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn CodMarca;

	private DataGridViewTextBoxColumn cotiza;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private RadLabel radLabel14;

	private RadDateTimePicker dtfechacotizacion;

	private RadDateTimePicker dtfecharegistro;

	private RadLabel radLabel9;

	private RadLabel radLabel8;

	private RadButton btneliminar;

	private RadGroupBox radGroupBox2;

	public DataGridView dgvEliminados;

	private Label lblmensaje;

	private DataGridViewComboBoxColumn Motivo;

	private DataGridViewTextBoxColumn codDetalleCotizacion;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

	public frmOrdenCompraCotizacion()
	{
		InitializeComponent();
	}

	private void frmOrdenCompraCotizacion_Load(object sender, EventArgs e)
	{
		try
		{
			dtfecharegistro.MaxDate = DateTime.Today.Date;
			CargaMoneda();
			CargaFormaPagos();
			if (Proceso == 1)
			{
				CargaCotizacion();
				activar(valor: false);
				dgvdetalleEditable(editable: true);
				txtordenCompra.Focus();
			}
			else if (Proceso == 2)
			{
				CargaOrdenCompraCotizacion();
				activar(valor: false);
				Controles();
			}
			else if (Proceso != 3)
			{
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaMoneda()
	{
		cmbMoneda.DataSource = AdmMon.CargaMonedasHabiles();
		cmbMoneda.DisplayMember = "descripcion";
		cmbMoneda.ValueMember = "codMoneda";
		cmbMoneda.SelectedValue = 1;
	}

	private void CargaFormaPagos()
	{
		cmbformapago.DataSource = AdmPago.CargaFormaPagos(1);
		cmbformapago.DisplayMember = "descripcion";
		cmbformapago.ValueMember = "codFormaPago";
		cmbformapago.SelectedIndex = 0;
	}

	private void CargaCotizacion()
	{
		try
		{
			cotizacion = AdmCoti.CargaCotizacion(Convert.ToInt32(CodCotizacion), frmLogin.iCodAlmacen);
			if (cotizacion != null)
			{
				if (txtcodcliente.Enabled)
				{
					txtcodcliente.Text = cotizacion.CodCliente.ToString();
					txtnombrecliente.Text = cotizacion.Nombre;
					txtdireccion.Text = cotizacion.Direccion;
				}
				dtfecharegistro.Value = DateTime.Now.Date;
				dtfechacotizacion.Value = cotizacion.FechaCotizacion;
				cmbMoneda.SelectedValue = cotizacion.Moneda;
				txttipocambio.Text = cotizacion.TipoCambio.ToString();
				cmbformapago.SelectedValue = cotizacion.FormaPago;
				CargaVendedor(cotizacion.CodUser);
				txtcomentario.Text = cotizacion.Comentario;
				txtdescuento.Text = $"{cotizacion.MontoDscto:#,##0.00}";
				txtvalorventa.Text = $"{cotizacion.Total - cotizacion.Igv:#,##0.00}";
				txtigv.Text = $"{cotizacion.Igv:#,##0.00}";
				txttotal.Text = $"{cotizacion.Total:#,##0.00}";
				CargaDetalleCotizacion();
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

	private void CargaOrdenCompraCotizacion()
	{
		try
		{
			OrdenCompra = AdmOrdenC.CargaOrdenCompraCotizacion(Convert.ToInt32(CodOrdenCompraVar), frmLogin.iCodAlmacen);
			if (OrdenCompra != null)
			{
				txtordenCompra.Text = OrdenCompra.codOrdenCompra.ToString();
				if (txtcodcliente.Enabled)
				{
					txtcodcliente.Text = OrdenCompra.codCliente.ToString();
					txtnombrecliente.Text = OrdenCompra.RazonSocialCliente;
					txtdireccion.Text = OrdenCompra.direccion;
				}
				dtfechacotizacion.Value = OrdenCompra.fechacotizacion;
				dtfecharegistro.Value = OrdenCompra.fecharegistro;
				cmbMoneda.SelectedValue = OrdenCompra.moneda;
				txttipocambio.Text = OrdenCompra.tipocambio.ToString();
				cmbformapago.SelectedValue = OrdenCompra.formapago;
				CargaVendedor(OrdenCompra.codUsuario);
				txtcomentario.Text = OrdenCompra.comentario;
				txtdescuento.Text = $"{OrdenCompra.montodscto:#,##0.00}";
				txtvalorventa.Text = $"{OrdenCompra.subtotal:#,##0.00}";
				txtigv.Text = $"{OrdenCompra.igv:#,##0.00}";
				txttotal.Text = $"{OrdenCompra.total:#,##0.00}";
				Controles();
				CargaDetalleOrdenCompra();
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

	private void CargaVendedor(int codvendedor)
	{
		vendedor = admUsuario.MuestraUsuarioSinAdmin(codvendedor);
		if (vendedor != null)
		{
			txtnombrevendedor.Text = vendedor.Nombre + " " + vendedor.Apellido;
			txtcodvendedor.Text = codvendedor.ToString();
		}
	}

	private void CargaDetalleCotizacion()
	{
		DataTable newData = new DataTable();
		dgvDetalle.Rows.Clear();
		try
		{
			newData = AdmCoti.CargaDetalle(Convert.ToInt32(cotizacion.CodCotizacion), frmLogin.iCodAlmacen);
			foreach (DataRow row in newData.Rows)
			{
				dgvDetalle.Rows.Add(row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[15].ToString(), row[16].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[26].ToString(), row[27].ToString(), row[28].ToString(), row[29].ToString(), row[30].ToString());
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaDetalleOrdenCompra()
	{
		DataTable newData = new DataTable();
		dgvDetalle.Rows.Clear();
		try
		{
			newData = AdmOrdenC.CargaDetalleOrdenCompra(Convert.ToInt32(OrdenCompra.codOrdenCompra), frmLogin.iCodAlmacen);
			foreach (DataRow row in newData.Rows)
			{
				dgvDetalle.Rows.Add(Convert.ToBoolean(row[0]), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString());
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnguardar_Click(object sender, EventArgs e)
	{
		try
		{
			Validaciones();
			if (!Genera)
			{
				return;
			}
			if (Proceso != 0)
			{
				RecorreDetalleEliminados();
				if (detalleelimados.Count > 0)
				{
					foreach (clsDetalleCotizacion det in detalleelimados)
					{
						if (det.motivo != "")
						{
							AdmOrdenC.updatecotizacion(det);
							continue;
						}
						MessageBox.Show("Ingresar Motivo a todos los productos eliminados", "Falta Motivo", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						return;
					}
				}
				OrdenCompra.codCotizacion = Convert.ToInt32(cotizacion.CodCotizacion);
				OrdenCompra.codSucursal = frmLogin.iCodSucursal;
				OrdenCompra.codAlmacen = frmLogin.iCodAlmacen;
				OrdenCompra.codCliente = Convert.ToInt32(txtcodcliente.Text);
				OrdenCompra.moneda = Convert.ToInt32(cmbMoneda.SelectedValue);
				OrdenCompra.codTipoDocumento = 16;
				OrdenCompra.tipocambio = Convert.ToDecimal(txttipocambio.Text);
				OrdenCompra.fecharegistro = dtfecharegistro.Value.Date;
				OrdenCompra.fechacotizacion = dtfechacotizacion.Value.Date;
				OrdenCompra.comentario = txtcomentario.Text;
				OrdenCompra.montodscto = Convert.ToDecimal(txtdescuento.Text);
				OrdenCompra.igv = Convert.ToDecimal(txtigv.Text);
				OrdenCompra.subtotal = Convert.ToDecimal(txtvalorventa.Text);
				OrdenCompra.total = Convert.ToDecimal(txttotal.Text);
				OrdenCompra.codUsuario = Convert.ToInt32(txtcodvendedor.Text);
				OrdenCompra.formapago = Convert.ToInt32(cmbformapago.SelectedValue);
				OrdenCompra.margenganciamonto = 0m;
				OrdenCompra.margengananciaporcentaje = 0m;
				OrdenCompra.estado = true;
				OrdenCompra.estadoproceso = 1;
				OrdenCompra.numerooccliente = txtordenCompra.Text;
				if (Proceso != 1 || !AdmOrdenC.insert(OrdenCompra))
				{
					return;
				}
				RecorreDetalle();
				if (detalle.Count > 0)
				{
					foreach (clsDetalleOrdenCompraCotizacion det2 in detalle)
					{
						AdmOrdenC.insertdetalle(det2);
					}
				}
				MessageBox.Show("Los datos se guardaron correctamente", "Cotizacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
			else
			{
				MessageBox.Show("Seleccionar un Vendedor", "Cotizaciones", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
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

	private void RecorreDetalleEliminados()
	{
		detalleelimados.Clear();
		if (dgvEliminados.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvEliminados.Rows)
		{
			añadedetalleeliminados(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		try
		{
			clsDetalleOrdenCompraCotizacion deta = new clsDetalleOrdenCompraCotizacion();
			deta.codProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
			deta.referencia = fila.Cells[referencia.Name].Value.ToString();
			deta.codOrdenCompra = Convert.ToInt32(OrdenCompra.codOrdenCompra);
			deta.codAlmacen = frmLogin.iCodAlmacen;
			deta.codUnidadMedida = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
			deta.cantidad = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			deta.preciounitario = Convert.ToDecimal(fila.Cells[preciounit.Name].Value);
			deta.subtotal = Convert.ToDecimal(fila.Cells[valorventa.Name].Value);
			deta.montodscto = Convert.ToDecimal(fila.Cells[montodscto.Name].Value);
			deta.igv = Convert.ToDecimal(fila.Cells[igv.Name].Value);
			deta.total = Convert.ToDecimal(fila.Cells[total.Name].Value);
			deta.precioreal = Convert.ToDecimal(fila.Cells[precioreal.Name].Value);
			deta.codUser = frmLogin.iCodUser;
			deta.cantidadpendiente = Convert.ToDecimal(fila.Cells[cantidad.Name].Value);
			deta.stockactual = Convert.ToDecimal(fila.Cells[stockDisp.Name].Value);
			deta.codmarca = Convert.ToInt32(fila.Cells[CodMarca.Name].Value);
			deta.margenganciamonto = Convert.ToDecimal(fila.Cells[GananciaMonto.Name].Value);
			deta.margengananciaporcentaje = Convert.ToDecimal(fila.Cells[GananciaPorcentaje.Name].Value);
			deta.costototal = Convert.ToDecimal(fila.Cells[CostoTotal.Name].Value);
			deta.productocotizacion = ((Convert.ToInt32(fila.Cells[cotiza.Name].Value) != 0) ? true : false);
			deta.estado = true;
			deta.pendiente = true;
			detalle.Add(deta);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void añadedetalleeliminados(DataGridViewRow fila)
	{
		try
		{
			clsDetalleCotizacion deta = new clsDetalleCotizacion();
			deta.CodDetalleCotizacion = Convert.ToInt32(fila.Cells[codDetalleCotizacion.Name].Value);
			deta.motivo = ((fila.Cells[Motivo.Name].Value == null) ? "" : fila.Cells[Motivo.Name].Value.ToString());
			detalleelimados.Add(deta);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void Validaciones()
	{
		if (string.IsNullOrEmpty(txtcodcliente.Text))
		{
			MessageBox.Show("Agregar Cliente", "Orden Compra Cotizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Genera = false;
		}
		else if (string.IsNullOrEmpty(txtordenCompra.Text))
		{
			MessageBox.Show("Agregar Cliente", "Orden Compra Cotizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			Genera = false;
		}
		else
		{
			Genera = true;
		}
	}

	private void activar(bool valor)
	{
		txtcodcliente.Enabled = valor;
		txtnombrecliente.Enabled = valor;
		txtcodvendedor.Enabled = valor;
		txtnombrevendedor.Enabled = valor;
		txtdireccion.Enabled = valor;
		txtvalorventa.Enabled = valor;
		txtigv.Enabled = valor;
		txttotal.Enabled = valor;
		txtdescuento.Enabled = valor;
		cmbMoneda.Enabled = valor;
		cmbformapago.Enabled = valor;
		dtfecharegistro.Enabled = valor;
		dtfechacotizacion.Enabled = valor;
		txttipocambio.Enabled = valor;
		txtcomentario.Enabled = valor;
	}

	private void Controles()
	{
		btnguardar.Enabled = false;
		btneditar.Enabled = true;
		btneliminar.Enabled = false;
		dgvDetalle.ReadOnly = true;
	}

	private void dgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex < 0)
		{
			return;
		}
		try
		{
			if (!(dgvDetalle.Columns[e.ColumnIndex].Name == "cantidad"))
			{
				return;
			}
			object valor = dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			if (double.TryParse(valor.ToString(), out var nuevaCantidad))
			{
				if (nuevaCantidad < 0.0)
				{
					MessageBox.Show("La nueva cantidad debe ser mayor a cero", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					if (valorInicial != null)
					{
						dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
						valorInicial = null;
					}
					return;
				}
				int codProd = Convert.ToInt32(dgvDetalle.Rows[e.RowIndex].Cells[codproducto.Name].Value);
				int codUnidad = Convert.ToInt32(dgvDetalle.Rows[e.RowIndex].Cells[codunidad.Name].Value);
				clsProducto product = AdmPro.ListaTotalprod2(codProd, frmLogin.iCodAlmacen, codUnidad);
				double totalprod = product.StockMaximo;
				if (nuevaCantidad == Convert.ToDouble((valorInicial == null) ? ((object)0) : valorInicial))
				{
					return;
				}
				if (nuevaCantidad > totalprod)
				{
					MessageBox.Show("Stock no disponible", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					if (valorInicial != null)
					{
						dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
						valorInicial = null;
					}
					return;
				}
				if (nuevaCantidad < Convert.ToDouble((valorInicial == null) ? ((object)0) : valorInicial))
				{
					foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
					{
						try
						{
							if (row.Index == dgvDetalle.CurrentCell.RowIndex)
							{
								DataGridViewRow fila2 = new DataGridViewRow();
								fila2.CreateCells(dgvEliminados);
								fila2.Cells[1].Value = row.Cells[0].Value;
								fila2.Cells[2].Value = row.Cells[1].Value;
								fila2.Cells[3].Value = row.Cells[2].Value;
								fila2.Cells[4].Value = row.Cells[3].Value;
								fila2.Cells[5].Value = row.Cells[5].Value;
								fila2.Cells[6].Value = row.Cells[6].Value;
								fila2.Cells[7].Value = row.Cells[8].Value;
								fila2.Cells[8].Value = row.Cells[12].Value;
								dgvEliminados.Rows.Add(fila2);
							}
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString(), "OrdenCompraCotización", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
				}
				double cantidad = 0.0;
				double total = 0.0;
				double gananciamonto = 0.0;
				double preciocompra = 0.0;
				double gananciaporcentaje = 0.0;
				double totalcosto = 0.0;
				double precioUnitario = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[preciounit.Name].Value);
				preciocompra = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[dsctoMax.Name].Value);
				total = Math.Round(Convert.ToDouble(nuevaCantidad) * precioUnitario, 2);
				cantidad = Convert.ToDouble(nuevaCantidad);
				double bruto = total;
				totalcosto = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[CostoTotal.Name].Value);
				double Ganancia = Math.Round((Convert.ToDouble(precioUnitario) - Convert.ToDouble(totalcosto)) / 1.18, 2);
				gananciamonto = Math.Round(Convert.ToDouble(Ganancia) * cantidad, 2);
				gananciaporcentaje = Math.Round(Ganancia / (preciocompra / 1.18) * 100.0, 2);
				double factorigv = Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
				double valorventa = Math.Round(total / factorigv, 2);
				double igv = Math.Round(total - valorventa, 2);
				DataGridViewRow fila3 = dgvDetalle.Rows[e.RowIndex];
				fila3.Cells[this.valorventa.Name].Value = valorventa;
				fila3.Cells[this.igv.Name].Value = igv;
				fila3.Cells[this.total.Name].Value = total;
				fila3.Cells[GananciaPorcentaje.Name].Value = gananciaporcentaje;
				fila3.Cells[GananciaMonto.Name].Value = gananciamonto;
				calculatotales();
			}
			else
			{
				MessageBox.Show("El valor tiene que ser un numero", "Error de edicion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (valorInicial != null)
				{
					dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = valorInicial;
					valorInicial = null;
				}
			}
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message, "Error al Editar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvDetalle_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			try
			{
				valorInicial = dgvDetalle.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error al Editar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void dgvdetalleEditable(bool editable)
	{
		foreach (DataGridViewColumn col in dgvDetalle.Columns)
		{
			col.ReadOnly = true;
			if (editable)
			{
				if (col.Name == cantidad.Name)
				{
					col.ReadOnly = false;
				}
				else
				{
					col.ReadOnly = true;
				}
			}
		}
	}

	public void calculatotales()
	{
		if (Proceso == 0)
		{
			return;
		}
		double totalventa = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		double gananciaporcentaje = 0.0;
		double gananciamonto = 0.0;
		double preciocompra = 0.0;
		double totalgasto = 0.0;
		double totalprecioventa = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			totalventa += Convert.ToDouble(row.Cells[total.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
			gananciamonto += Convert.ToDouble(row.Cells[GananciaMonto.Name].Value);
			totalgasto += Convert.ToDouble(row.Cells[CostoTotal.Name].Value);
			totalprecioventa += Convert.ToDouble(row.Cells[preciounit.Name].Value);
			preciocompra += Convert.ToDouble(row.Cells[dsctoMax.Name].Value);
		}
		double Ganancia = (totalprecioventa - totalgasto) / 1.18;
		gananciaporcentaje = ((totalventa == 0.0) ? 0.0 : Math.Round(Ganancia / (preciocompra / 1.18) * 100.0, 2));
		txtdescuento.Text = $"{descuen:#,##0.00}";
		txtvalorventa.Text = $"{valor:#,##0.00}";
		txtigv.Text = $"{valor * 0.18:#,##0.00}";
		txttotal.Text = $"{totalventa:#,##0.00}";
	}

	private void btneliminar_Click(object sender, EventArgs e)
	{
		if (!((dgvDetalle.Rows.Count > 0) & (dgvDetalle.SelectedRows.Count > 0)))
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
		{
			try
			{
				if (row.Index == dgvDetalle.CurrentCell.RowIndex)
				{
					DataGridViewRow fila = new DataGridViewRow();
					fila.CreateCells(dgvEliminados);
					fila.Cells[1].Value = row.Cells[0].Value;
					fila.Cells[2].Value = row.Cells[1].Value;
					fila.Cells[3].Value = row.Cells[2].Value;
					fila.Cells[4].Value = row.Cells[3].Value;
					fila.Cells[5].Value = row.Cells[5].Value;
					fila.Cells[6].Value = row.Cells[6].Value;
					fila.Cells[7].Value = row.Cells[8].Value;
					fila.Cells[8].Value = row.Cells[12].Value;
					dgvEliminados.Rows.Add(fila);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "OrdenCompraCotización", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		dgvDetalle.Rows.Remove(dgvDetalle.SelectedRows[0]);
		calculatotales();
	}

	private void btneditar_Click(object sender, EventArgs e)
	{
		Proceso = 3;
		btneliminar.Enabled = true;
		btnguardar.Enabled = true;
		dgvDetalle.ReadOnly = false;
	}

	private void dgvDetalle_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
		if (dgvDetalle.Columns[e.ColumnIndex].Name == "stockDisp" || dgvDetalle.Columns[e.ColumnIndex].Name == "cantidad")
		{
			double stockdisponible = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[stockDisp.Name].Value ?? ((object)0));
			double cantidadagregada = Convert.ToDouble(dgvDetalle.Rows[e.RowIndex].Cells[cantidad.Name].Value ?? ((object)0));
			if (stockdisponible < cantidadagregada)
			{
				e.CellStyle.BackColor = Color.Red;
				lblmensaje.Visible = true;
			}
			else
			{
				lblmensaje.Visible = false;
			}
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition2 = new Telerik.WinControls.UI.TableViewDefinition();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmOrdenCompraCotizacion));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.gbDatos = new Telerik.WinControls.UI.RadGroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockDisp = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GananciaPorcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GananciaMonto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CostoTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dsctoMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.CodMarca = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cotiza = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.gbCabecera = new Telerik.WinControls.UI.RadGroupBox();
		this.radLabel14 = new Telerik.WinControls.UI.RadLabel();
		this.txtordenCompra = new Telerik.WinControls.UI.RadTextBox();
		this.gbDatosCliente = new Telerik.WinControls.UI.RadGroupBox();
		this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.txtcomentario = new Telerik.WinControls.UI.RadTextBox();
		this.txtdireccion = new Telerik.WinControls.UI.RadTextBox();
		this.txtnombrecliente = new Telerik.WinControls.UI.RadTextBox();
		this.txtcodcliente = new Telerik.WinControls.UI.RadTextBox();
		this.gbVendedor = new Telerik.WinControls.UI.RadGroupBox();
		this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
		this.txtnombrevendedor = new Telerik.WinControls.UI.RadTextBox();
		this.txtcodvendedor = new Telerik.WinControls.UI.RadTextBox();
		this.gbCondiciones = new Telerik.WinControls.UI.RadGroupBox();
		this.dtfechacotizacion = new Telerik.WinControls.UI.RadDateTimePicker();
		this.dtfecharegistro = new Telerik.WinControls.UI.RadDateTimePicker();
		this.radLabel9 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel8 = new Telerik.WinControls.UI.RadLabel();
		this.txttipocambio = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel7 = new Telerik.WinControls.UI.RadLabel();
		this.cmbformapago = new Telerik.WinControls.UI.RadMultiColumnComboBox();
		this.radLabel6 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel5 = new Telerik.WinControls.UI.RadLabel();
		this.cmbMoneda = new Telerik.WinControls.UI.RadMultiColumnComboBox();
		this.gbTotales = new Telerik.WinControls.UI.RadGroupBox();
		this.radLabel13 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel12 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel11 = new Telerik.WinControls.UI.RadLabel();
		this.radLabel10 = new Telerik.WinControls.UI.RadLabel();
		this.txtdescuento = new Telerik.WinControls.UI.RadTextBox();
		this.txttotal = new Telerik.WinControls.UI.RadTextBox();
		this.txtigv = new Telerik.WinControls.UI.RadTextBox();
		this.txtvalorventa = new Telerik.WinControls.UI.RadTextBox();
		this.radGroupBox1 = new Telerik.WinControls.UI.RadGroupBox();
		this.lblmensaje = new System.Windows.Forms.Label();
		this.btneliminar = new Telerik.WinControls.UI.RadButton();
		this.btneditar = new Telerik.WinControls.UI.RadButton();
		this.btnguardar = new Telerik.WinControls.UI.RadButton();
		this.btnsalir = new Telerik.WinControls.UI.RadButton();
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.radGroupBox2 = new Telerik.WinControls.UI.RadGroupBox();
		this.dgvEliminados = new System.Windows.Forms.DataGridView();
		this.Motivo = new System.Windows.Forms.DataGridViewComboBoxColumn();
		this.codDetalleCotizacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)this.gbDatos).BeginInit();
		this.gbDatos.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gbCabecera).BeginInit();
		this.gbCabecera.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel14).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtordenCompra).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gbDatosCliente).BeginInit();
		this.gbDatosCliente.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtcomentario).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtdireccion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtnombrecliente).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtcodcliente).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gbVendedor).BeginInit();
		this.gbVendedor.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtnombrevendedor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtcodvendedor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gbCondiciones).BeginInit();
		this.gbCondiciones.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dtfechacotizacion).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dtfecharegistro).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel9).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel8).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txttipocambio).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel7).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbformapago).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbformapago.EditorControl).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbformapago.EditorControl.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel6).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel5).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda.EditorControl).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda.EditorControl.MasterTemplate).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.gbTotales).BeginInit();
		this.gbTotales.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel13).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel12).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel11).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel10).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtdescuento).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txttotal).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtigv).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtvalorventa).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox1).BeginInit();
		this.radGroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.btneliminar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btneditar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox2).BeginInit();
		this.radGroupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvEliminados).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.gbDatos.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.gbDatos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbDatos.Controls.Add(this.dgvDetalle);
		this.gbDatos.HeaderText = "Datos";
		this.gbDatos.Location = new System.Drawing.Point(12, 3);
		this.gbDatos.Name = "gbDatos";
		this.gbDatos.Size = new System.Drawing.Size(761, 320);
		this.gbDatos.TabIndex = 0;
		this.gbDatos.Text = "Datos";
		this.gbDatos.ThemeName = "TelerikMetroTouch";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToDeleteRows = false;
		this.dgvDetalle.AllowUserToResizeColumns = false;
		this.dgvDetalle.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.HighlightText;
		this.dgvDetalle.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvDetalle.BackgroundColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.MenuHighlight;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvDetalle.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.cantidad, this.precioreal, this.preciounit, this.montodscto, this.valorventa, this.igv, this.total, this.stockDisp, this.GananciaPorcentaje, this.GananciaMonto, this.CostoTotal, this.dsctoMax, this.coduser, this.fecharegistro, this.CodMarca, this.cotiza);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.GridColor = System.Drawing.SystemColors.ControlLightLight;
		this.dgvDetalle.Location = new System.Drawing.Point(2, 18);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(757, 300);
		this.dgvDetalle.TabIndex = 2;
		this.dgvDetalle.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(dgvDetalle_CellBeginEdit);
		this.dgvDetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellEndEdit);
		this.dgvDetalle.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(dgvDetalle_CellFormatting);
		this.coddetalle.DataPropertyName = "codDetalleCotizacion";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.referencia.DefaultCellStyle = dataGridViewCellStyle3;
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.referencia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.referencia.Width = 80;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 120;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 71;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle4.Format = "N2";
		dataGridViewCellStyle4.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle4;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 72;
		this.precioreal.DataPropertyName = "precioreal";
		dataGridViewCellStyle5.Format = "N4";
		this.precioreal.DefaultCellStyle = dataGridViewCellStyle5;
		this.precioreal.HeaderText = "P. Venta";
		this.precioreal.Name = "precioreal";
		this.precioreal.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.precioreal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle6.Format = "N4";
		this.preciounit.DefaultCellStyle = dataGridViewCellStyle6;
		this.preciounit.HeaderText = "Pv. Final";
		this.preciounit.Name = "preciounit";
		this.preciounit.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.preciounit.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.preciounit.Width = 71;
		this.montodscto.DataPropertyName = "montodscto";
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle7.Format = "N4";
		dataGridViewCellStyle7.NullValue = null;
		this.montodscto.DefaultCellStyle = dataGridViewCellStyle7;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.montodscto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.DataPropertyName = "valorventa";
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle8.Format = "N4";
		dataGridViewCellStyle8.NullValue = null;
		this.valorventa.DefaultCellStyle = dataGridViewCellStyle8;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.valorventa.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.valorventa.Width = 71;
		this.igv.DataPropertyName = "igv";
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle9.Format = "N4";
		this.igv.DefaultCellStyle = dataGridViewCellStyle9;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.igv.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.Width = 71;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle10.Format = "N4";
		this.total.DefaultCellStyle = dataGridViewCellStyle10;
		this.total.HeaderText = "Total";
		this.total.Name = "total";
		this.total.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.total.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.total.Width = 71;
		this.stockDisp.DataPropertyName = "stockdisponible";
		this.stockDisp.HeaderText = "StockDisponible";
		this.stockDisp.Name = "stockDisp";
		this.stockDisp.Width = 72;
		this.GananciaPorcentaje.DataPropertyName = "GananciaPorcentaje";
		this.GananciaPorcentaje.HeaderText = "GananciaPorcentaje";
		this.GananciaPorcentaje.Name = "GananciaPorcentaje";
		this.GananciaPorcentaje.Visible = false;
		this.GananciaPorcentaje.Width = 120;
		this.GananciaMonto.DataPropertyName = "GananciaMonto";
		this.GananciaMonto.HeaderText = "GananciaMonto";
		this.GananciaMonto.Name = "GananciaMonto";
		this.GananciaMonto.Visible = false;
		this.GananciaMonto.Width = 110;
		this.CostoTotal.HeaderText = "CostoTotal";
		this.CostoTotal.Name = "CostoTotal";
		this.CostoTotal.Visible = false;
		this.dsctoMax.DataPropertyName = "maxPorcDescto";
		this.dsctoMax.HeaderText = "%DsctoMax";
		this.dsctoMax.Name = "dsctoMax";
		this.dsctoMax.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.Visible = false;
		this.CodMarca.HeaderText = "CodMarca";
		this.CodMarca.Name = "CodMarca";
		this.CodMarca.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.CodMarca.Visible = false;
		this.cotiza.DataPropertyName = "cotiza";
		this.cotiza.HeaderText = "cotiza";
		this.cotiza.Name = "cotiza";
		this.cotiza.Visible = false;
		this.gbCabecera.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.gbCabecera.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.gbCabecera.Controls.Add(this.radLabel14);
		this.gbCabecera.Controls.Add(this.txtordenCompra);
		this.gbCabecera.HeaderText = "Cabecera";
		this.gbCabecera.Location = new System.Drawing.Point(784, 3);
		this.gbCabecera.Name = "gbCabecera";
		this.gbCabecera.Size = new System.Drawing.Size(497, 57);
		this.gbCabecera.TabIndex = 1;
		this.gbCabecera.Text = "Cabecera";
		this.gbCabecera.ThemeName = "TelerikMetroTouch";
		this.radLabel14.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel14.Location = new System.Drawing.Point(51, 18);
		this.radLabel14.Name = "radLabel14";
		this.radLabel14.Size = new System.Drawing.Size(144, 18);
		this.radLabel14.TabIndex = 3;
		this.radLabel14.Text = "Orden Compra N° Cliente:";
		this.txtordenCompra.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtordenCompra.Font = new System.Drawing.Font("Segoe UI", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtordenCompra.Location = new System.Drawing.Point(201, 14);
		this.txtordenCompra.Name = "txtordenCompra";
		this.txtordenCompra.Size = new System.Drawing.Size(152, 25);
		this.txtordenCompra.TabIndex = 1;
		this.txtordenCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtordenCompra.ThemeName = "ControlDefault";
		this.gbDatosCliente.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.gbDatosCliente.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.gbDatosCliente.Controls.Add(this.radLabel3);
		this.gbDatosCliente.Controls.Add(this.radLabel2);
		this.gbDatosCliente.Controls.Add(this.radLabel1);
		this.gbDatosCliente.Controls.Add(this.txtcomentario);
		this.gbDatosCliente.Controls.Add(this.txtdireccion);
		this.gbDatosCliente.Controls.Add(this.txtnombrecliente);
		this.gbDatosCliente.Controls.Add(this.txtcodcliente);
		this.gbDatosCliente.HeaderText = "Datos Cliente";
		this.gbDatosCliente.Location = new System.Drawing.Point(784, 77);
		this.gbDatosCliente.Name = "gbDatosCliente";
		this.gbDatosCliente.Size = new System.Drawing.Size(497, 129);
		this.gbDatosCliente.TabIndex = 2;
		this.gbDatosCliente.Text = "Datos Cliente";
		this.gbDatosCliente.ThemeName = "TelerikMetroTouch";
		this.radLabel3.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel3.Location = new System.Drawing.Point(14, 94);
		this.radLabel3.Name = "radLabel3";
		this.radLabel3.Size = new System.Drawing.Size(75, 18);
		this.radLabel3.TabIndex = 4;
		this.radLabel3.Text = "Comentario :";
		this.radLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel2.Location = new System.Drawing.Point(14, 59);
		this.radLabel2.Name = "radLabel2";
		this.radLabel2.Size = new System.Drawing.Size(62, 18);
		this.radLabel2.TabIndex = 3;
		this.radLabel2.Text = "Direccion :";
		this.radLabel1.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel1.Location = new System.Drawing.Point(14, 33);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(49, 18);
		this.radLabel1.TabIndex = 2;
		this.radLabel1.Text = "Cliente :";
		this.txtcomentario.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcomentario.Location = new System.Drawing.Point(105, 92);
		this.txtcomentario.Name = "txtcomentario";
		this.txtcomentario.Size = new System.Drawing.Size(367, 21);
		this.txtcomentario.TabIndex = 1;
		this.txtdireccion.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtdireccion.Location = new System.Drawing.Point(105, 57);
		this.txtdireccion.Name = "txtdireccion";
		this.txtdireccion.Size = new System.Drawing.Size(367, 21);
		this.txtdireccion.TabIndex = 1;
		this.txtnombrecliente.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtnombrecliente.Location = new System.Drawing.Point(189, 31);
		this.txtnombrecliente.Name = "txtnombrecliente";
		this.txtnombrecliente.Size = new System.Drawing.Size(283, 21);
		this.txtnombrecliente.TabIndex = 1;
		this.txtcodcliente.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcodcliente.Location = new System.Drawing.Point(105, 31);
		this.txtcodcliente.Name = "txtcodcliente";
		this.txtcodcliente.Size = new System.Drawing.Size(78, 21);
		this.txtcodcliente.TabIndex = 1;
		this.gbVendedor.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.gbVendedor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.gbVendedor.Controls.Add(this.radLabel4);
		this.gbVendedor.Controls.Add(this.txtnombrevendedor);
		this.gbVendedor.Controls.Add(this.txtcodvendedor);
		this.gbVendedor.HeaderText = "";
		this.gbVendedor.Location = new System.Drawing.Point(784, 212);
		this.gbVendedor.Name = "gbVendedor";
		this.gbVendedor.Size = new System.Drawing.Size(497, 52);
		this.gbVendedor.TabIndex = 3;
		this.gbVendedor.ThemeName = "TelerikMetroTouch";
		this.radLabel4.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel4.Location = new System.Drawing.Point(14, 14);
		this.radLabel4.Name = "radLabel4";
		this.radLabel4.Size = new System.Drawing.Size(64, 18);
		this.radLabel4.TabIndex = 4;
		this.radLabel4.Text = "Vendedor :";
		this.txtnombrevendedor.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtnombrevendedor.Location = new System.Drawing.Point(181, 16);
		this.txtnombrevendedor.Name = "txtnombrevendedor";
		this.txtnombrevendedor.Size = new System.Drawing.Size(271, 21);
		this.txtnombrevendedor.TabIndex = 2;
		this.txtcodvendedor.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtcodvendedor.Location = new System.Drawing.Point(105, 16);
		this.txtcodvendedor.Name = "txtcodvendedor";
		this.txtcodvendedor.Size = new System.Drawing.Size(70, 21);
		this.txtcodvendedor.TabIndex = 2;
		this.gbCondiciones.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.gbCondiciones.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.gbCondiciones.Controls.Add(this.dtfechacotizacion);
		this.gbCondiciones.Controls.Add(this.dtfecharegistro);
		this.gbCondiciones.Controls.Add(this.radLabel9);
		this.gbCondiciones.Controls.Add(this.radLabel8);
		this.gbCondiciones.Controls.Add(this.txttipocambio);
		this.gbCondiciones.Controls.Add(this.radLabel7);
		this.gbCondiciones.Controls.Add(this.cmbformapago);
		this.gbCondiciones.Controls.Add(this.radLabel6);
		this.gbCondiciones.Controls.Add(this.radLabel5);
		this.gbCondiciones.Controls.Add(this.cmbMoneda);
		this.gbCondiciones.HeaderText = "Condiciones";
		this.gbCondiciones.Location = new System.Drawing.Point(784, 279);
		this.gbCondiciones.Name = "gbCondiciones";
		this.gbCondiciones.Size = new System.Drawing.Size(497, 116);
		this.gbCondiciones.TabIndex = 4;
		this.gbCondiciones.Text = "Condiciones";
		this.gbCondiciones.ThemeName = "TelerikMetroTouch";
		this.dtfechacotizacion.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.dtfechacotizacion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtfechacotizacion.Location = new System.Drawing.Point(119, 50);
		this.dtfechacotizacion.Name = "dtfechacotizacion";
		this.dtfechacotizacion.Size = new System.Drawing.Size(145, 21);
		this.dtfechacotizacion.TabIndex = 14;
		this.dtfechacotizacion.TabStop = false;
		this.dtfechacotizacion.Value = new System.DateTime(0L);
		this.dtfecharegistro.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.dtfecharegistro.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtfecharegistro.Location = new System.Drawing.Point(119, 80);
		this.dtfecharegistro.Name = "dtfecharegistro";
		this.dtfecharegistro.Size = new System.Drawing.Size(145, 21);
		this.dtfecharegistro.TabIndex = 13;
		this.dtfecharegistro.TabStop = false;
		this.dtfecharegistro.Value = new System.DateTime(0L);
		this.radLabel9.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel9.Location = new System.Drawing.Point(14, 52);
		this.radLabel9.Name = "radLabel9";
		this.radLabel9.Size = new System.Drawing.Size(101, 18);
		this.radLabel9.TabIndex = 12;
		this.radLabel9.Text = "Fecha Cotizacion :";
		this.radLabel8.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel8.Location = new System.Drawing.Point(14, 82);
		this.radLabel8.Name = "radLabel8";
		this.radLabel8.Size = new System.Drawing.Size(90, 18);
		this.radLabel8.TabIndex = 11;
		this.radLabel8.Text = "Fecha Registro :";
		this.txttipocambio.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txttipocambio.Location = new System.Drawing.Point(375, 80);
		this.txttipocambio.Name = "txttipocambio";
		this.txttipocambio.Size = new System.Drawing.Size(109, 21);
		this.txttipocambio.TabIndex = 2;
		this.radLabel7.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel7.Location = new System.Drawing.Point(289, 82);
		this.radLabel7.Name = "radLabel7";
		this.radLabel7.Size = new System.Drawing.Size(80, 18);
		this.radLabel7.TabIndex = 6;
		this.radLabel7.Text = "Tipo Cambio :";
		this.cmbformapago.EditorControl.BackColor = System.Drawing.SystemColors.Window;
		this.cmbformapago.EditorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbformapago.EditorControl.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmbformapago.EditorControl.Location = new System.Drawing.Point(0, 0);
		this.cmbformapago.EditorControl.MasterTemplate.AllowAddNewRow = false;
		this.cmbformapago.EditorControl.MasterTemplate.AllowCellContextMenu = false;
		this.cmbformapago.EditorControl.MasterTemplate.AllowColumnChooser = false;
		this.cmbformapago.EditorControl.MasterTemplate.EnableGrouping = false;
		this.cmbformapago.EditorControl.MasterTemplate.ShowFilteringRow = false;
		this.cmbformapago.EditorControl.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.cmbformapago.EditorControl.Name = "NestedRadGridView";
		this.cmbformapago.EditorControl.ReadOnly = true;
		this.cmbformapago.EditorControl.ShowGroupPanel = false;
		this.cmbformapago.EditorControl.Size = new System.Drawing.Size(240, 150);
		this.cmbformapago.EditorControl.TabIndex = 0;
		this.cmbformapago.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbformapago.Location = new System.Drawing.Point(316, 44);
		this.cmbformapago.Name = "cmbformapago";
		this.cmbformapago.Size = new System.Drawing.Size(168, 21);
		this.cmbformapago.TabIndex = 1;
		this.cmbformapago.TabStop = false;
		this.radLabel6.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel6.Location = new System.Drawing.Point(365, 20);
		this.radLabel6.Name = "radLabel6";
		this.radLabel6.Size = new System.Drawing.Size(76, 18);
		this.radLabel6.TabIndex = 6;
		this.radLabel6.Text = "Forma Pago :";
		this.radLabel5.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel5.Location = new System.Drawing.Point(14, 26);
		this.radLabel5.Name = "radLabel5";
		this.radLabel5.Size = new System.Drawing.Size(56, 18);
		this.radLabel5.TabIndex = 5;
		this.radLabel5.Text = "Moneda :";
		this.cmbMoneda.EditorControl.BackColor = System.Drawing.SystemColors.Window;
		this.cmbMoneda.EditorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.EditorControl.ForeColor = System.Drawing.SystemColors.ControlText;
		this.cmbMoneda.EditorControl.Location = new System.Drawing.Point(0, 0);
		this.cmbMoneda.EditorControl.MasterTemplate.AllowAddNewRow = false;
		this.cmbMoneda.EditorControl.MasterTemplate.AllowCellContextMenu = false;
		this.cmbMoneda.EditorControl.MasterTemplate.AllowColumnChooser = false;
		this.cmbMoneda.EditorControl.MasterTemplate.EnableGrouping = false;
		this.cmbMoneda.EditorControl.MasterTemplate.ShowFilteringRow = false;
		this.cmbMoneda.EditorControl.MasterTemplate.ViewDefinition = tableViewDefinition2;
		this.cmbMoneda.EditorControl.Name = "NestedRadGridView";
		this.cmbMoneda.EditorControl.ReadOnly = true;
		this.cmbMoneda.EditorControl.ShowGroupPanel = false;
		this.cmbMoneda.EditorControl.Size = new System.Drawing.Size(240, 150);
		this.cmbMoneda.EditorControl.TabIndex = 0;
		this.cmbMoneda.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbMoneda.Location = new System.Drawing.Point(119, 20);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(145, 21);
		this.cmbMoneda.TabIndex = 0;
		this.cmbMoneda.TabStop = false;
		this.gbTotales.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.gbTotales.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.gbTotales.Controls.Add(this.radLabel13);
		this.gbTotales.Controls.Add(this.radLabel12);
		this.gbTotales.Controls.Add(this.radLabel11);
		this.gbTotales.Controls.Add(this.radLabel10);
		this.gbTotales.Controls.Add(this.txtdescuento);
		this.gbTotales.Controls.Add(this.txttotal);
		this.gbTotales.Controls.Add(this.txtigv);
		this.gbTotales.Controls.Add(this.txtvalorventa);
		this.gbTotales.HeaderText = "Totales";
		this.gbTotales.Location = new System.Drawing.Point(784, 415);
		this.gbTotales.Name = "gbTotales";
		this.gbTotales.Size = new System.Drawing.Size(497, 130);
		this.gbTotales.TabIndex = 5;
		this.gbTotales.Text = "Totales";
		this.gbTotales.ThemeName = "TelerikMetroTouch";
		this.radLabel13.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel13.Location = new System.Drawing.Point(301, 56);
		this.radLabel13.Name = "radLabel13";
		this.radLabel13.Size = new System.Drawing.Size(68, 18);
		this.radLabel13.TabIndex = 6;
		this.radLabel13.Text = "Descuento :";
		this.radLabel12.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel12.Location = new System.Drawing.Point(301, 21);
		this.radLabel12.Name = "radLabel12";
		this.radLabel12.Size = new System.Drawing.Size(40, 18);
		this.radLabel12.TabIndex = 6;
		this.radLabel12.Text = "Total :";
		this.radLabel11.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel11.Location = new System.Drawing.Point(14, 56);
		this.radLabel11.Name = "radLabel11";
		this.radLabel11.Size = new System.Drawing.Size(29, 18);
		this.radLabel11.TabIndex = 6;
		this.radLabel11.Text = "Igv :";
		this.radLabel10.Font = new System.Drawing.Font("Segoe UI", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.radLabel10.Location = new System.Drawing.Point(14, 25);
		this.radLabel10.Name = "radLabel10";
		this.radLabel10.Size = new System.Drawing.Size(54, 18);
		this.radLabel10.TabIndex = 6;
		this.radLabel10.Text = "V.Venta :";
		this.txtdescuento.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtdescuento.Location = new System.Drawing.Point(384, 56);
		this.txtdescuento.Name = "txtdescuento";
		this.txtdescuento.Size = new System.Drawing.Size(100, 21);
		this.txtdescuento.TabIndex = 2;
		this.txttotal.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txttotal.Location = new System.Drawing.Point(384, 15);
		this.txttotal.Name = "txttotal";
		this.txttotal.Size = new System.Drawing.Size(100, 21);
		this.txttotal.TabIndex = 3;
		this.txtigv.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtigv.Location = new System.Drawing.Point(105, 50);
		this.txtigv.Name = "txtigv";
		this.txtigv.Size = new System.Drawing.Size(100, 21);
		this.txtigv.TabIndex = 2;
		this.txtvalorventa.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtvalorventa.Location = new System.Drawing.Point(105, 15);
		this.txtvalorventa.Name = "txtvalorventa";
		this.txtvalorventa.Size = new System.Drawing.Size(100, 21);
		this.txtvalorventa.TabIndex = 2;
		this.radGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.radGroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.radGroupBox1.Controls.Add(this.lblmensaje);
		this.radGroupBox1.Controls.Add(this.btneliminar);
		this.radGroupBox1.Controls.Add(this.btneditar);
		this.radGroupBox1.Controls.Add(this.btnguardar);
		this.radGroupBox1.Controls.Add(this.btnsalir);
		this.radGroupBox1.HeaderText = "Acciones";
		this.radGroupBox1.Location = new System.Drawing.Point(12, 562);
		this.radGroupBox1.Name = "radGroupBox1";
		this.radGroupBox1.Size = new System.Drawing.Size(1269, 50);
		this.radGroupBox1.TabIndex = 2;
		this.radGroupBox1.Text = "Acciones";
		this.radGroupBox1.ThemeName = "TelerikMetroTouch";
		this.lblmensaje.AutoSize = true;
		this.lblmensaje.BackColor = System.Drawing.Color.Red;
		this.lblmensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblmensaje.ForeColor = System.Drawing.SystemColors.Control;
		this.lblmensaje.Location = new System.Drawing.Point(230, 18);
		this.lblmensaje.Name = "lblmensaje";
		this.lblmensaje.Size = new System.Drawing.Size(184, 13);
		this.lblmensaje.TabIndex = 50;
		this.lblmensaje.Text = "Productos sin Stock Disponible";
		this.lblmensaje.Visible = false;
		this.btneliminar.Anchor = System.Windows.Forms.AnchorStyles.Left;
		this.btneliminar.Image = (System.Drawing.Image)resources.GetObject("btneliminar.Image");
		this.btneliminar.Location = new System.Drawing.Point(77, 9);
		this.btneliminar.Name = "btneliminar";
		this.btneliminar.Size = new System.Drawing.Size(119, 32);
		this.btneliminar.TabIndex = 2;
		this.btneliminar.Text = "Eliminar";
		this.btneliminar.ThemeName = "TelerikMetroTouch";
		this.btneliminar.Click += new System.EventHandler(btneliminar_Click);
		this.btneditar.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btneditar.Enabled = false;
		this.btneditar.Image = (System.Drawing.Image)resources.GetObject("btneditar.Image");
		this.btneditar.Location = new System.Drawing.Point(926, 8);
		this.btneditar.Name = "btneditar";
		this.btneditar.Size = new System.Drawing.Size(110, 32);
		this.btneditar.TabIndex = 2;
		this.btneditar.Text = "Editar";
		this.btneditar.ThemeName = "TelerikMetroTouch";
		this.btneditar.Click += new System.EventHandler(btneditar_Click);
		this.btnguardar.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnguardar.Image = SIGEFA.Properties.Resources.save1;
		this.btnguardar.Location = new System.Drawing.Point(1043, 9);
		this.btnguardar.Name = "btnguardar";
		this.btnguardar.Size = new System.Drawing.Size(119, 32);
		this.btnguardar.TabIndex = 1;
		this.btnguardar.Text = "Guardar";
		this.btnguardar.ThemeName = "TelerikMetroTouch";
		this.btnguardar.Click += new System.EventHandler(btnguardar_Click);
		this.btnsalir.Anchor = System.Windows.Forms.AnchorStyles.Right;
		this.btnsalir.Image = (System.Drawing.Image)resources.GetObject("btnsalir.Image");
		this.btnsalir.Location = new System.Drawing.Point(1173, 9);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(91, 32);
		this.btnsalir.TabIndex = 0;
		this.btnsalir.Text = "Salir";
		this.btnsalir.ThemeName = "TelerikMetroTouch";
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.radGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
		this.radGroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.radGroupBox2.Controls.Add(this.dgvEliminados);
		this.radGroupBox2.HeaderText = "Eliminados";
		this.radGroupBox2.Location = new System.Drawing.Point(10, 331);
		this.radGroupBox2.Name = "radGroupBox2";
		this.radGroupBox2.Size = new System.Drawing.Size(761, 225);
		this.radGroupBox2.TabIndex = 3;
		this.radGroupBox2.Text = "Eliminados";
		this.radGroupBox2.ThemeName = "TelerikMetroTouch";
		this.dgvEliminados.AllowUserToAddRows = false;
		this.dgvEliminados.AllowUserToDeleteRows = false;
		this.dgvEliminados.AllowUserToResizeColumns = false;
		this.dgvEliminados.AllowUserToResizeRows = false;
		dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.HighlightText;
		this.dgvEliminados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
		this.dgvEliminados.BackgroundColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.MenuHighlight;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvEliminados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
		this.dgvEliminados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvEliminados.Columns.AddRange(this.Motivo, this.codDetalleCotizacion, this.dataGridViewTextBoxColumn2, this.dataGridViewTextBoxColumn3, this.dataGridViewTextBoxColumn4, this.dataGridViewTextBoxColumn6, this.dataGridViewTextBoxColumn7, this.dataGridViewTextBoxColumn9, this.dataGridViewTextBoxColumn13);
		this.dgvEliminados.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvEliminados.GridColor = System.Drawing.SystemColors.ControlLightLight;
		this.dgvEliminados.Location = new System.Drawing.Point(2, 18);
		this.dgvEliminados.Name = "dgvEliminados";
		this.dgvEliminados.RowHeadersVisible = false;
		this.dgvEliminados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvEliminados.Size = new System.Drawing.Size(757, 205);
		this.dgvEliminados.TabIndex = 2;
		this.Motivo.HeaderText = "Motivo";
		this.Motivo.Items.AddRange("ALTERNATIVA", "LÍNEA / MARCA", "PRECIO", "ROTURA STOCK", "RQ INVÁLIDO", "STANDBY");
		this.Motivo.Name = "Motivo";
		this.codDetalleCotizacion.DataPropertyName = "codDetalleCotizacion";
		this.codDetalleCotizacion.HeaderText = "CodDetalle";
		this.codDetalleCotizacion.Name = "codDetalleCotizacion";
		this.codDetalleCotizacion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codDetalleCotizacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codDetalleCotizacion.Visible = false;
		this.dataGridViewTextBoxColumn2.DataPropertyName = "codProducto";
		this.dataGridViewTextBoxColumn2.HeaderText = "CodProducto";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn2.Visible = false;
		this.dataGridViewTextBoxColumn3.DataPropertyName = "referencia";
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
		this.dataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle13;
		this.dataGridViewTextBoxColumn3.HeaderText = "Referencia";
		this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
		this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn3.Width = 80;
		this.dataGridViewTextBoxColumn4.DataPropertyName = "producto";
		this.dataGridViewTextBoxColumn4.HeaderText = "Descripcion";
		this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
		this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn4.Width = 120;
		this.dataGridViewTextBoxColumn6.DataPropertyName = "unidad";
		this.dataGridViewTextBoxColumn6.HeaderText = "Unidad";
		this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
		this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn6.Width = 71;
		this.dataGridViewTextBoxColumn7.DataPropertyName = "cantidad";
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle14.Format = "N2";
		dataGridViewCellStyle14.NullValue = null;
		this.dataGridViewTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle14;
		this.dataGridViewTextBoxColumn7.HeaderText = "Cantidad";
		this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
		this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn7.Width = 72;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle15.Format = "N4";
		this.dataGridViewTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle15;
		this.dataGridViewTextBoxColumn9.HeaderText = "Pv. Final";
		this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
		this.dataGridViewTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn9.Width = 71;
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle16.Format = "N4";
		this.dataGridViewTextBoxColumn13.DefaultCellStyle = dataGridViewCellStyle16;
		this.dataGridViewTextBoxColumn13.HeaderText = "Total";
		this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
		this.dataGridViewTextBoxColumn13.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn13.Width = 71;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1315, 647);
		base.Controls.Add(this.radGroupBox2);
		base.Controls.Add(this.radGroupBox1);
		base.Controls.Add(this.gbTotales);
		base.Controls.Add(this.gbCondiciones);
		base.Controls.Add(this.gbVendedor);
		base.Controls.Add(this.gbDatosCliente);
		base.Controls.Add(this.gbCabecera);
		base.Controls.Add(this.gbDatos);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmOrdenCompraCotizacion";
		base.RootElement.ApplyShapeToControl = true;
		this.Text = "frmOrdenCompraCotizacion";
		base.ThemeName = "TelerikMetroBlue";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmOrdenCompraCotizacion_Load);
		((System.ComponentModel.ISupportInitialize)this.gbDatos).EndInit();
		this.gbDatos.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gbCabecera).EndInit();
		this.gbCabecera.ResumeLayout(false);
		this.gbCabecera.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel14).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtordenCompra).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gbDatosCliente).EndInit();
		this.gbDatosCliente.ResumeLayout(false);
		this.gbDatosCliente.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtcomentario).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtdireccion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtnombrecliente).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtcodcliente).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gbVendedor).EndInit();
		this.gbVendedor.ResumeLayout(false);
		this.gbVendedor.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtnombrevendedor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtcodvendedor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gbCondiciones).EndInit();
		this.gbCondiciones.ResumeLayout(false);
		this.gbCondiciones.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dtfechacotizacion).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dtfecharegistro).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel9).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel8).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txttipocambio).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel7).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbformapago.EditorControl.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbformapago.EditorControl).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbformapago).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel6).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel5).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda.EditorControl.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda.EditorControl).EndInit();
		((System.ComponentModel.ISupportInitialize)this.cmbMoneda).EndInit();
		((System.ComponentModel.ISupportInitialize)this.gbTotales).EndInit();
		this.gbTotales.ResumeLayout(false);
		this.gbTotales.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.radLabel13).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel12).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel11).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel10).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtdescuento).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txttotal).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtigv).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtvalorventa).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox1).EndInit();
		this.radGroupBox1.ResumeLayout(false);
		this.radGroupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.btneliminar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btneditar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnguardar).EndInit();
		((System.ComponentModel.ISupportInitialize)this.btnsalir).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radGroupBox2).EndInit();
		this.radGroupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvEliminados).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
