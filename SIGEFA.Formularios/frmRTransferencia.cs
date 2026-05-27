using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Transactions;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmRTransferencia : Office2007Form
{
	private clsAdmAlmacen AdmAlm = new clsAdmAlmacen();

	private clsAlmacen alm = new clsAlmacen();

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private clsPedido pedido = new clsPedido();

	private clsAdmUsuario admUsuario = new clsAdmUsuario();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	public int CodCliente;

	public int CodDocumento;

	public int Tipo;

	public int Proceso = 0;

	public string NombreCliente;

	public string CodPedido;

	public string NombreAlmacen;

	public string estadonombre;

	public mdi_Menu menu;

	public int CodAlmacen;

	private List<DataGridViewRow> productotrans = new List<DataGridViewRow>();

	public List<clsProducto> ProdAlmacen;

	public decimal cantidadOriginalProducto;

	private clsReporteTransferencias ds = new clsReporteTransferencias();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTransaccion admTransaccion = new clsAdmTransaccion();

	private clsAdmTransferencia admTransferencia = new clsAdmTransferencia();

	private clsTransferencia transfer = new clsTransferencia();

	private List<clsDetalleTransferencia> detalle = new List<clsDetalleTransferencia>();

	private clsAdmTipoCambio AdmTc = new clsAdmTipoCambio();

	private clsTipoCambio tc = new clsTipoCambio();

	private clsAdmNotaSalida admNS = new clsAdmNotaSalida();

	private clsNotaSalida NS = new clsNotaSalida();

	private clsAdmNotaIngreso admNI = new clsAdmNotaIngreso();

	private clsNotaIngreso NI = new clsNotaIngreso();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private clsSerie ser = new clsSerie();

	private clsAdmSerie admSerie = new clsAdmSerie();

	public int CodSerie;

	public int num;

	public int codalmacenselec;

	public List<clsDetalleNotaSalida> detalleNS = new List<clsDetalleNotaSalida>();

	public List<clsDetalleNotaIngreso> detalleNI = new List<clsDetalleNotaIngreso>();

	public int CodTransaccion;

	public int CodDocumento1;

	public int Proceso2 = 1;

	public int cboalma = 0;

	public int cborigen;

	public int CodTransDirecta;

	public int caso;

	private decimal cantidadanterior;

	private decimal cantidadnueva = default(decimal);

	private bool bandera = true;

	private int codproducto_error = 0;

	private IContainer components = null;

	private DateTimePicker dtpFecha;

	private Label label13;

	private GroupBox groupBox5;

	private ComboBox cbalmacenes;

	private Label label7;

	private Label label2;

	private GroupBox groupBox1;

	private Button btndetalle;

	private GroupBox groupBox2;

	private Label label5;

	private Label label3;

	private TextBox txtdni;

	private TextBox txtcliente;

	private GroupBox groupBox3;

	private TextBox textBox3;

	private TextBox textBox4;

	private Label label4;

	private Label label6;

	private GroupBox groupBox4;

	private TextBox txtdireccion;

	private Label label8;

	private CheckBox checkBox1;

	private Label label9;

	private Button btnGuardaOV;

	private Button btnSalir;

	private GroupBox groupBox6;

	private TextBox txtvendedor;

	private Label label10;

	public DataGridView dvgtransferencia;

	public TextBox txtSerie;

	private TextBox txtNumero;

	private Label label11;

	public TextBox txtDocRef;

	private Label label1;

	private ComboBox cmbMoneda;

	private Label label17;

	private TextBox txtDocSal;

	private Label label12;

	private TextBox txtDocIng;

	private Label label14;

	private TextBox txtcodserie;

	private TextBox txtValorVenta;

	private Label label15;

	private TextBox txtBruto;

	private Label label16;

	private TextBox txtCodTransDir;

	private Button btnAprobar;

	private ComboBox cbalmacenes2;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codprod;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn serielote;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn preciounit;

	private DataGridViewTextBoxColumn importe;

	private DataGridViewTextBoxColumn dscto1;

	private DataGridViewTextBoxColumn dscto2;

	private DataGridViewTextBoxColumn dscto3;

	private DataGridViewTextBoxColumn montodscto;

	private DataGridViewTextBoxColumn valorventa;

	private DataGridViewTextBoxColumn igv;

	private DataGridViewTextBoxColumn precioventa;

	private DataGridViewTextBoxColumn precioreal;

	private DataGridViewTextBoxColumn valoreal;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	private DataGridViewTextBoxColumn codProv;

	private DataGridViewTextBoxColumn valorpromedio;

	private DataGridViewTextBoxColumn precioigv;

	private DataGridViewTextBoxColumn cantpedido;

	private DataGridViewTextBoxColumn stcokal;

	private DataGridViewTextBoxColumn cant2;

	private Button btneditar;

	private void frmRTransferencia_Load(object sender, EventArgs e)
	{
		autocompletar();
		doc = admtd.BuscaTipoDocumento("TD");
		CodTransaccion = 15;
		txtDocRef.Text = doc.Sigla;
		CodDocumento = doc.CodTipoDocumento;
		cmbMoneda.SelectedIndex = 0;
		dtpFecha.MaxDate = DateTime.Today.Date;
		if (Proceso2 == 1)
		{
			CargaPedido();
			CargaAlmacen1();
			CargaAlmacen();
			tc = AdmTc.CargaTipoCambio(dtpFecha.Value.Date, 2);
			tran = admTransaccion.MuestraTransaccionS("TD", 1);
			txtDocRef.Text = doc.Sigla;
			if (caso == 0)
			{
				txtDocSal.Visible = false;
				txtDocIng.Visible = false;
				txtDocSal.Enabled = false;
				txtDocIng.Enabled = false;
				btnAprobar.Enabled = false;
				btnGuardaOV.Visible = true;
			}
			btnAprobar.Visible = false;
		}
		else if (Proceso2 == 2)
		{
			CargaTransferencia();
			CargaPedido();
			btnGuardaOV.Enabled = false;
		}
		else if (Proceso2 == 3)
		{
			CargaPedido();
			CargaTransferencia();
			btndetalle.Visible = false;
			btnAprobar.Visible = true;
			btnGuardaOV.Enabled = false;
			tran = admTransaccion.MuestraTransaccionS("TD", 1);
			doc = admtd.BuscaTipoDocumento(transfer.SiglaDocumento);
			txtDocRef.Text = doc.Sigla;
			cargaAlmO(transfer.CodAlmacenOrigen);
			cargaAlmD(transfer.CodAlmacenDestino);
			if (estadonombre == "ANULADO")
			{
				btndetalle.Visible = false;
				btnAprobar.Visible = false;
				btnGuardaOV.Visible = false;
				btneditar.Visible = false;
			}
			double cant2 = Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[7].Value);
			double cant3 = Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[26].Value);
			if (cant2 > cant3)
			{
				MessageBox.Show("Editar la cantidad del Requerimiento sobrepasa la cantidad de Pedido");
			}
		}
	}

	private void CargaTransferencia()
	{
		try
		{
			transfer = admTransferencia.CargaTransferencia(Convert.ToInt32(CodTransDirecta));
			if (transfer != null)
			{
				CargaDetalle();
				dtpFecha.Value = transfer.FechaEnvio;
				txtcodserie.Text = transfer.Codserie.ToString();
				txtSerie.Text = transfer.Serie.ToString();
				txtNumero.Text = transfer.Numerodocumento.ToString();
				textBox3.Text = transfer.Telefonotrans;
				textBox4.Text = transfer.Nombretrans;
				txtvendedor.Text = transfer.Autorizadopor;
				txtdireccion.Text = transfer.Direcciontrans;
				if (transfer.Moneda == 1)
				{
					cmbMoneda.SelectedIndex = 0;
				}
				else
				{
					cmbMoneda.SelectedIndex = 1;
				}
				if (txtDocRef.Enabled)
				{
					CodDocumento = transfer.CodTipoDocumento;
					txtDocRef.Text = transfer.SiglaDocumento;
				}
				txtBruto.Text = $"{transfer.MontoBruto:#,##0.0000}";
				txtValorVenta.Text = $"{transfer.Total - transfer.Igv:#,##0.0000}";
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaDetalle()
	{
		DataTable newData = new DataTable();
		dvgtransferencia.Rows.Clear();
		try
		{
			newData = admTransferencia.CargaDetallePedido(Convert.ToInt32(transfer.CodTransDir));
			foreach (DataRow row in newData.Rows)
			{
				dvgtransferencia.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[24].ToString(), row[8].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[19].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), 0, row[7].ToString());
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		valorpromedio.Visible = false;
	}

	private void cargaserie()
	{
		ser = admSerie.BuscaSeriexDocumento(doc.CodTipoDocumento, cborigen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			num = ser.Numeracion;
			if (CodSerie != 0)
			{
				if (ser.PreImpreso)
				{
					CodSerie = ser.CodSerie;
					txtSerie.Text = ser.Serie;
					txtcodserie.Text = ser.CodSerie.ToString();
					txtNumero.Enabled = true;
					txtNumero.Text = "";
				}
				else
				{
					CodSerie = ser.CodSerie;
					txtSerie.Text = ser.Serie;
					txtcodserie.Text = ser.CodSerie.ToString().PadLeft(3, '0');
					txtNumero.Text = ser.Numeracion.ToString().PadLeft(6, '0');
					txtNumero.Enabled = false;
				}
			}
			else
			{
				txtSerie.Focus();
			}
		}
		else
		{
			MessageBox.Show("No existe numeracion para transferencia");
		}
	}

	private void btndetalle_Click(object sender, EventArgs e)
	{
		if (Convert.ToInt32(cbalmacenes2.SelectedValue) > 0)
		{
			frmRTransferenciaDetalle form = new frmRTransferenciaDetalle();
			form.Proceso = 2;
			form.CodPedido = CodPedido;
			form.codalmacenselec = Convert.ToInt32(cbalmacenes2.SelectedValue);
			form.MdiParent = base.MdiParent;
			form.Show();
			Close();
		}
		else
		{
			MessageBox.Show("Selecciona el almacen");
			cbalmacenes2.Focus();
		}
	}

	public frmRTransferencia(mdi_Menu menu)
	{
		InitializeComponent();
		this.menu = menu;
	}

	public frmRTransferencia()
	{
		InitializeComponent();
	}

	public void autocompletar()
	{
		DataTable newData = new DataTable();
		AutoCompleteStringCollection lista = new AutoCompleteStringCollection();
		newData = admUsuario.CargaUsuarios();
		for (int i = 1; i < newData.Rows.Count; i++)
		{
			lista.Add(newData.Rows[i]["vendedor"].ToString());
		}
		txtvendedor.AutoCompleteCustomSource = lista;
	}

	public void datadgvdetalle(List<DataGridViewRow> rowSelected)
	{
		DataTable newData = new DataTable();
		cbalmacenes.Enabled = true;
		dvgtransferencia.Rows.Clear();
		productotrans = rowSelected;
		try
		{
			foreach (DataGridViewRow row in rowSelected)
			{
				dvgtransferencia.Rows.Add(row.Cells[coddetalle.Name].Value, row.Cells[codprod.Name].Value, row.Cells[codigo.Name].Value, row.Cells[descripcion.Name].Value, row.Cells[codunidad.Name].Value, row.Cells[unidad.Name].Value, "", Convert.ToDecimal(row.Cells[cantidad.Name].Value), row.Cells[preciounit.Name].Value, row.Cells[importe.Name].Value, row.Cells[dscto1.Name].Value, row.Cells[dscto2.Name].Value, row.Cells[dscto3.Name].Value, row.Cells[montodscto.Name].Value, row.Cells[valorventa.Name].Value, row.Cells[igv.Name].Value, row.Cells[precioventa.Name].Value, row.Cells[precioreal.Name].Value, row.Cells[valoreal.Name].Value, frmLogin.iCodUser, dtpFecha.Value.Date, 0, row.Cells[valorpromedio.Name].Value, row.Cells[precioigv.Name].Value);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cbalmacenes_SelectionChangeCommitted(object sender, EventArgs e)
	{
		clsTransferencia transfer2 = new clsTransferencia();
		if (Proceso2 == 3)
		{
			CodAlmacen = Convert.ToInt32(transfer.CodAlmacenOrigen);
			foreach (DataGridViewRow row1 in (IEnumerable)dvgtransferencia.Rows)
			{
				ProdAlmacen = AdmPro.ListaProdAlmacen(Convert.ToInt32(row1.Cells[codprod.Name].Value), CodAlmacen);
				foreach (clsProducto prod in ProdAlmacen)
				{
					if (prod.CodProducto == Convert.ToInt32(row1.Cells[1].Value))
					{
						string almacen1 = cbalmacenes.GetItemText(cbalmacenes.SelectedItem);
						dvgtransferencia.Columns[25].HeaderText = cbalmacenes.Text;
						row1.Cells[stcokal.Name].Value = $"{Convert.ToDouble(prod.StockDisponible):#,##0.00}";
						break;
					}
				}
			}
		}
		else
		{
			CodAlmacen = int.Parse(cbalmacenes.SelectedValue.ToString());
		}
		foreach (DataGridViewRow row2 in productotrans)
		{
			for (int y = 0; y < productotrans.Count; y++)
			{
				ProdAlmacen = AdmPro.ListaProdAlmacen(Convert.ToInt32(row2.Cells[1].Value.ToString()), CodAlmacen);
				if (ProdAlmacen != null)
				{
					if (ProdAlmacen.Count <= 0)
					{
						continue;
					}
					foreach (DataGridViewRow row3 in (IEnumerable)dvgtransferencia.Rows)
					{
						foreach (clsProducto prod2 in ProdAlmacen)
						{
							if (prod2.CodProducto == Convert.ToInt32(row3.Cells[1].Value))
							{
								string almacen2 = cbalmacenes.GetItemText(cbalmacenes.SelectedItem);
								dvgtransferencia.Columns[25].HeaderText = almacen2;
								row3.Cells[stcokal.Name].Value = $"{Convert.ToDouble(prod2.StockDisponible):#,##0.00}";
								break;
							}
						}
					}
				}
				else
				{
					MessageBox.Show("No se encontro el Producto: " + row2.Cells[1].Value.ToString().PadLeft(8, '0') + " en Almacen: " + CodAlmacen, "Error Al Encontrar Producto Almacen Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
	}

	private void checkBox1_Click(object sender, EventArgs e)
	{
		if (checkBox1.Checked)
		{
			txtdireccion.Enabled = true;
		}
		else
		{
			txtdireccion.Enabled = false;
		}
	}

	private void dvgtransferencia_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dvgtransferencia.Rows.Count >= 1)
		{
			cantidadOriginalProducto = Convert.ToDecimal(dvgtransferencia.CurrentRow.Cells[7].Value);
		}
	}

	private void Recalcular()
	{
		double bruto = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		double figv = 0.0;
		double pven = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dvgtransferencia.Rows)
		{
			bruto += Convert.ToDouble(row.Cells[importe.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
			figv += Convert.ToDouble(row.Cells[igv.Name].Value);
			pven += Convert.ToDouble(row.Cells[precioventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.00}";
		txtValorVenta.Text = $"{valor:#,##0.00}";
		label11.Text = txtBruto.Text;
	}

	private void dvgtransferencia_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dvgtransferencia.RowCount <= 0 || e.ColumnIndex != 26)
		{
			return;
		}
		cantidadnueva = Convert.ToDecimal(dvgtransferencia.CurrentRow.Cells[cant2.Name].Value);
		double cantidadpedido = Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[7].Value);
		clsProducto productoBuscado = AdmPro.CargaProducto(Convert.ToInt32(dvgtransferencia.CurrentRow.Cells[codprod.Name].Value), CodAlmacen);
		if (Convert.ToDouble(cantidadnueva) <= productoBuscado.StockActual)
		{
			if (Convert.ToDouble(cantidadnueva) <= cantidadpedido)
			{
				dvgtransferencia.CurrentRow.Cells[precioventa.Name].Value = $"{cantidadnueva * Convert.ToDecimal(dvgtransferencia.CurrentRow.Cells[preciounit.Name].Value):#,##0.00}";
				dvgtransferencia.CurrentRow.Cells[valorventa.Name].Value = $"{Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[precioventa.Name].Value) * (1.0 - frmLogin.Configuracion.IGV / 100.0):#,##0.00}";
				dvgtransferencia.CurrentRow.Cells[igv.Name].Value = $"{Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[precioventa.Name].Value) - Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[valorventa.Name].Value):#,##0.00}";
				dvgtransferencia.CurrentRow.Cells[importe.Name].Value = $"{Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[precioventa.Name].Value):#,##0.00}";
				Recalcular();
				return;
			}
			int codigoDetallePedido = Convert.ToInt32(dvgtransferencia.CurrentRow.Cells[coddetalle.Name].Value);
			decimal cantidadOriginal = default(decimal);
			foreach (clsDetallePedido detalle_ in pedido.Detalle)
			{
				if (detalle_.CodDetallePedido == codigoDetallePedido)
				{
					cantidadOriginal = Convert.ToDecimal(detalle_.Cantidad);
					break;
				}
			}
			dvgtransferencia.CurrentRow.Cells[cant2.Name].Value = $"{Convert.ToDouble(cantidadOriginal):#,##0.00}";
			dvgtransferencia.CurrentRow.Cells[precioventa.Name].Value = $"{cantidadOriginal * Convert.ToDecimal(dvgtransferencia.CurrentRow.Cells[preciounit.Name].Value):#,##0.00}";
			dvgtransferencia.CurrentRow.Cells[valorventa.Name].Value = $"{Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[precioventa.Name].Value) * (1.0 - frmLogin.Configuracion.IGV / 100.0):#,##0.00}";
			dvgtransferencia.CurrentRow.Cells[igv.Name].Value = $"{Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[precioventa.Name].Value) - Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[valorventa.Name].Value):#,##0.00}";
			dvgtransferencia.CurrentRow.Cells[importe.Name].Value = $"{Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[precioventa.Name].Value):#,##0.00}";
			Recalcular();
			MessageBox.Show("Cantidad Inválida!! SOBREPASA EL STOCK ACTUAL DEL PRODUCTO!, SE TOMARÁ LA CANTIDAD ANTERIOR PARA EL PRODUCTO EDITADO", "Requerimiento de Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			dvgtransferencia.CurrentRow.Cells[cant2.Name].Value = $"{0:#,##0.00}";
		}
		else
		{
			MessageBox.Show("Cantidad Inválida!! SOBREPASA LE STOCK DEL ALMACEN DE DESPACHO!, SE TOMARÁ LA CANTIDAD ANTERIOR PARA EL PRODUCTO EDITADO", "Requerimiento de Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			dvgtransferencia.CurrentRow.Cells[cant2.Name].Value = $"{Convert.ToDouble(cantidadOriginalProducto):#,##0.00}";
		}
	}

	private void dvgtransferencia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dvgtransferencia.RowCount > 0)
		{
			dvgtransferencia.CurrentRow.Cells[cant2.Name].ReadOnly = false;
			dvgtransferencia.CurrentRow.Cells[cant2.Name].Style.BackColor = Color.PeachPuff;
			dvgtransferencia.CurrentRow.Cells[cant2.Name].Selected = true;
			cantidadanterior = Convert.ToDecimal(dvgtransferencia.CurrentRow.Cells[cant2.Name].Value);
		}
	}

	private void cargaAlmD(int codalm)
	{
		if (Proceso2 == 3)
		{
			alm = AdmAlm.CargaAlmacen(Convert.ToInt32(transfer.CodAlmacenDestino));
			cbalmacenes2.Text = alm.Nombre;
			cbalmacenes2.SelectedValue = transfer.CodAlmacenDestino;
			cbalmacenes2.Enabled = false;
			cborigen = transfer.CodAlmacenDestino;
		}
	}

	private void cargaAlmO(int codalm)
	{
		if (Proceso2 == 3)
		{
			alm = AdmAlm.CargaAlmacen(Convert.ToInt32(transfer.CodAlmacenOrigen));
			cbalmacenes.Text = alm.Nombre;
			cbalmacenes.SelectedValue = transfer.CodAlmacenOrigen;
			cbalmacenes.Enabled = false;
			CodAlmacen = Convert.ToInt32(transfer.CodAlmacenOrigen);
			cbalmacenes_SelectionChangeCommitted(new object(), new EventArgs());
		}
	}

	private void CargaAlmacen1()
	{
		try
		{
			if (cboalma == 1)
			{
				alm = AdmAlm.CargaAlmacen(Convert.ToInt32(codalmacenselec));
				cbalmacenes2.Text = alm.Nombre;
				cbalmacenes2.SelectedValue = codalmacenselec.ToString();
				cborigen = codalmacenselec;
				cbalmacenes2.Enabled = false;
				cargaserie();
			}
			else if (Proceso2 != 3)
			{
				cbalmacenes2.DataSource = AdmAlm.MuestraAlmacenesEmp2(int.Parse(CodPedido));
				cbalmacenes2.DisplayMember = "nombre";
				cbalmacenes2.ValueMember = "codAlmacen";
				cbalmacenes2.SelectedValue = -1;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaAlmacen()
	{
		try
		{
			cbalmacenes.DataSource = AdmAlm.MuestraAlmacenesEmp(codalmacenselec);
			cbalmacenes.DisplayMember = "nombre";
			cbalmacenes.ValueMember = "codAlmacen";
			cbalmacenes.SelectedValue = -1;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void dvgtransferencia_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
	{
		if (Proceso != 1)
		{
			return;
		}
		double bruto = 0.0;
		double descuen = 0.0;
		double valor = 0.0;
		foreach (DataGridViewRow row in (IEnumerable)dvgtransferencia.Rows)
		{
			bruto += Convert.ToDouble(row.Cells[importe.Name].Value);
			descuen += Convert.ToDouble(row.Cells[montodscto.Name].Value);
			valor += Convert.ToDouble(row.Cells[valorventa.Name].Value);
		}
		txtBruto.Text = $"{bruto:#,##0.0000}";
		txtValorVenta.Text = $"{valor:#,##0.0000}";
	}

	private void CargaPedido()
	{
		pedido = AdmPedido.CargaPedido(Convert.ToInt32(CodPedido));
		pedido.Detalle = AdmPedido.cargaDetallePedido(Convert.ToInt32(CodPedido));
		CargaCliente(pedido.CodCliente);
	}

	private void CargaCliente(int idcliente)
	{
		CodCliente = pedido.CodCliente;
		try
		{
			cli = AdmCli.MuestraCliente(CodCliente);
			txtdni.Text = cli.RucDni;
			txtcliente.Text = cli.RazonSocial;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnGuardaOV_Click(object sender, EventArgs e)
	{
		try
		{
			string nuevav = Convert.ToString(dvgtransferencia.CurrentRow.Cells[cant2.Name].Value);
			if (nuevav != "")
			{
				if (checkBox1.Checked && txtdireccion.Text == "")
				{
					txtdireccion.Focus();
				}
				if (textBox4.Text == "")
				{
					textBox4.Focus();
				}
				else if (textBox3.Text == "")
				{
					textBox3.Focus();
				}
				else if (txtvendedor.Text == "")
				{
					txtvendedor.Focus();
				}
				else if (dvgtransferencia.RowCount > 0)
				{
					if (Proceso2 != 0)
					{
						if (txtcodserie.Text != "" && txtNumero.Text != "")
						{
							transfer.CodAlmacenDestino = cborigen;
							transfer.CodAlmacenOrigen = Convert.ToInt32(CodAlmacen);
							transfer.CodTipoDocumento = 14;
							if (cmbMoneda.SelectedIndex == 0)
							{
								transfer.Moneda = 1;
							}
							else
							{
								transfer.Moneda = 2;
							}
							transfer.FechaEnvio = dtpFecha.Value;
							transfer.FechaEntrega = dtpFecha.Value;
							transfer.FormaPago = 0;
							transfer.FechaPago = dtpFecha.Value.Date;
							transfer.CodListaPrecio = 0;
							transfer.Comentario = "";
							transfer.DescripcionRechazo = "";
							transfer.MontoBruto = Convert.ToDecimal((txtBruto.Text == "") ? "0" : txtBruto.Text);
							transfer.MontoDscto = 0.00m;
							transfer.Igv = 0.00m;
							transfer.Total = Convert.ToDecimal((txtValorVenta.Text == "") ? "0" : txtValorVenta.Text);
							transfer.CodUser = frmLogin.iCodUser;
							transfer.Estado = 1;
							transfer.Codserie = Convert.ToInt32(txtcodserie.Text);
							transfer.Serie = txtSerie.Text;
							transfer.Numerodocumento = txtNumero.Text;
							transfer.Nombretrans = textBox4.Text;
							transfer.Telefonotrans = textBox3.Text;
							transfer.Direcciontrans = txtdireccion.Text;
							transfer.Autorizadopor = txtvendedor.Text;
							transfer.Numpedido = Convert.ToInt32(pedido.CodPedido);
							transfer.DocTransBF = 0;
							transfer.EstadoTrnas = 0;
							if (Proceso2 == 1)
							{
								if (admTransferencia.insert2(transfer))
								{
									RecorreDetalle();
									if (detalle.Count > 0)
									{
										foreach (clsDetalleTransferencia det in detalle)
										{
											admTransferencia.insertdetalle2(det);
										}
									}
									((FrmTPenPedido)Application.OpenForms["FrmTPenPedido"])?.cargarlista();
									MessageBox.Show("Los datos se guardaron correctamente!", "Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
									if (transfer.CodTransDir != null)
									{
										CodTransDirecta = CodTransDirecta;
									}
								}
							}
							else if (Proceso2 == 2 && admTransferencia.update(transfer))
							{
								RecorreDetalle();
								using (List<clsDetalleTransferencia>.Enumerator enumerator2 = detalle.GetEnumerator())
								{
									if (enumerator2.MoveNext())
									{
										clsDetalleTransferencia det2 = enumerator2.Current;
										admTransferencia.updatedetalle(det2);
										CargaDetalle();
										return;
									}
								}
								MessageBox.Show("Los datos se actualizaron correctamente!", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
						else
						{
							MessageBox.Show("Seleccione serie para la transferencia");
							dvgtransferencia.Focus();
						}
					}
				}
				else
				{
					MessageBox.Show("Se necesita agregar datos a la tabla detalle para guardar!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtDocIng.Focus();
				}
			}
			else
			{
				MessageBox.Show("Ingresar la cantidad que desea requerir");
			}
			Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void RecorreDetalle()
	{
		detalle.Clear();
		if (dvgtransferencia.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dvgtransferencia.Rows)
		{
			añadedetalle(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		clsDetalleTransferencia deta = new clsDetalleTransferencia();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codprod.Name].Value);
		deta.CodTransDir = Convert.ToInt32(transfer.CodTransDir);
		deta.CodAlmacenOrigen = CodAlmacen;
		deta.CodAlmacenDestino = cborigen;
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDouble(fila.Cells[cant2.Name].Value);
		deta.CantidadPendiente = deta.Cantidad;
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		deta.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		deta.MontoDescuento = Convert.ToDouble(fila.Cells[montodscto.Name].Value);
		deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta.Valorpromedio = Convert.ToDecimal(fila.Cells[valoreal.Name].Value);
		deta.EstadoTrnas = 0;
		deta.CodUser = frmLogin.iCodUser;
		detalle.Add(deta);
	}

	private void btneditar_Click(object sender, EventArgs e)
	{
		Proceso2 = 2;
		btnGuardaOV.Visible = true;
		btneditar.Enabled = false;
	}

	private void label11_Click(object sender, EventArgs e)
	{
	}

	private void cbalmacenes2_SelectionChangeCommitted(object sender, EventArgs e)
	{
		cborigen = Convert.ToInt32(cbalmacenes2.SelectedValue.ToString());
		cargaserie();
	}

	private void btnAprobar_Click(object sender, EventArgs e)
	{
		aprobar();
	}

	private void aprobar()
	{
		try
		{
			if (txtDocSal.Text != "")
			{
				if (txtDocIng.Text != "")
				{
					if (dvgtransferencia.RowCount <= 0)
					{
						return;
					}
					NS.NumDoc = txtNumero.Text;
					NS.CodAlmacen = Convert.ToInt32(transfer.CodAlmacenOrigen);
					NS.CodCliente = 0;
					NS.CodNotaCredito = 0;
					NS.CodSucursal = frmLogin.iCodSucursal;
					NS.RazonSocialCliente = "";
					NS.CodAutorizado = 0;
					NS.FechaSalida = dtpFecha.Value.Date;
					NS.DocumentoReferencia = 0;
					NS.CodTipoTransaccion = tran.CodTransaccion;
					NS.CodTipoDocumento = doc.CodTipoDocumento;
					NS.CodSerie = Convert.ToInt32(txtcodserie.Text);
					NS.Serie = txtSerie.Text;
					if (cmbMoneda.SelectedIndex == 0)
					{
						NS.Moneda = 1;
					}
					else if (cmbMoneda.SelectedIndex == 1)
					{
						NS.Moneda = 2;
					}
					NS.FechaSalida = dtpFecha.Value.Date;
					NS.FormaPago = 0;
					NS.FechaPago = dtpFecha.Value.Date;
					NS.Comentario = "";
					NS.MontoBruto = Convert.ToDouble(txtBruto.Text);
					NS.MontoDscto = 0.0;
					NS.Igv = 0.0;
					NS.Total = Convert.ToDouble(txtValorVenta.Text);
					NS.CodUser = transfer.CodUser;
					NS.Estado = 1;
					NS.Codtransferencia = CodTransDirecta;
					using (TransactionScope Scope = new TransactionScope())
					{
						if (admNS.insert(NS))
						{
							RecorreDetalleNS();
							if (detalleNS.Count > 0)
							{
								foreach (clsDetalleNotaSalida det in detalleNS)
								{
									if (!admNS.insertdetalle(det))
									{
										bandera = false;
										codproducto_error = det.CodProducto;
										Transaction.Current.Rollback();
										Scope.Dispose();
										break;
									}
								}
								if (bandera)
								{
									Scope.Complete();
									Scope.Dispose();
								}
							}
						}
						else
						{
							Transaction.Current.Rollback();
							Scope.Dispose();
							MessageBox.Show("Hubo un error al guardar la transferencia", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					if (bandera)
					{
						NI.NumDoc = txtNumero.Text;
						NI.CodAlmacen = Convert.ToInt32(transfer.CodAlmacenDestino);
						NI.CodAutorizado = 0;
						NI.CodReferencia = 0;
						NI.CodTipoTransaccion = tran.CodTransaccion;
						NI.CodTipoDocumento = doc.CodTipoDocumento;
						NI.CodSerie = Convert.ToInt32(txtSerie.Text);
						NI.Serie = txtSerie.Text;
						if (cmbMoneda.SelectedIndex == 0)
						{
							NI.Moneda = 1;
						}
						else if (cmbMoneda.SelectedIndex == 1)
						{
							NI.Moneda = 1;
						}
						NI.FechaIngreso = dtpFecha.Value.Date;
						NI.FormaPago = 0;
						NI.FechaPago = dtpFecha.Value.Date;
						NI.Comentario = "";
						NS.MontoBruto = Convert.ToDouble(txtBruto.Text);
						NI.MontoDscto = 0.0;
						NI.Igv = 0.0;
						NI.Total = Convert.ToDouble(txtValorVenta.Text);
						NI.CodUser = transfer.CodUser;
						NI.Estado = 1;
						NI.Codtransferencia = CodTransDirecta;
						using (TransactionScope Scope2 = new TransactionScope())
						{
							if (admNI.insert(NI))
							{
								RecorreDetalleNI();
								if (detalleNI.Count > 0)
								{
									foreach (clsDetalleNotaIngreso det2 in detalleNI)
									{
										if (!admNI.insertdetalle(det2))
										{
											bandera = false;
											codproducto_error = det2.CodProducto;
											Transaction.Current.Rollback();
											Scope2.Dispose();
											break;
										}
									}
								}
								if (bandera)
								{
									Scope2.Complete();
									Scope2.Dispose();
								}
								MessageBox.Show("Se aprobo la transferencia, datos guardados correctamente!", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								Transaction.Current.Rollback();
								Scope2.Dispose();
								MessageBox.Show("Hubo un error al guardar la transferencia", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							}
						}
						if (bandera)
						{
							admTransferencia.Aprobar(CodTransDirecta);
							Proceso = 3;
							caso = 1;
							CodTransDirecta = Convert.ToInt32(transfer.CodTransDir);
							((FrmTPenPedido)Application.OpenForms["FrmTPenPedido"])?.cargarlista();
						}
						else
						{
							MessageBox.Show("Hubo un error al guardar la transferencia", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
					}
					else
					{
						MessageBox.Show("No hay stock suficiente del producto codigo: " + codproducto_error, "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				else
				{
					MessageBox.Show("Ingrese el Numero de Documento correctamente!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					txtDocIng.Focus();
				}
			}
			else
			{
				MessageBox.Show("Ingrese el Numero de Documento correctamente!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				txtDocSal.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void RecorreDetalleNS()
	{
		detalleNS.Clear();
		if (dvgtransferencia.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dvgtransferencia.Rows)
		{
			añadedetalleNS(row);
		}
	}

	private void añadedetalleNS(DataGridViewRow fila)
	{
		clsDetalleNotaSalida deta = new clsDetalleNotaSalida();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codprod.Name].Value);
		deta.CodNotaSalida = Convert.ToInt32(NS.CodNotaSalida);
		deta.CodAlmacen = transfer.CodAlmacenOrigen;
		deta.CodAlmacen = Convert.ToInt32(transfer.CodAlmacenOrigen);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDouble(fila.Cells[cant2.Name].Value);
		deta.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		deta.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		deta.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		deta.Igv = Convert.ToDouble(fila.Cells[igv.Name].Value);
		deta.Importe = Convert.ToDouble(fila.Cells[precioventa.Name].Value);
		deta.PrecioReal = Convert.ToDouble(fila.Cells[precioreal.Name].Value);
		deta.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta.ValorPromedio = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		detalleNS.Add(deta);
	}

	private void RecorreDetalleNI()
	{
		detalleNI.Clear();
		if (dvgtransferencia.Rows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in (IEnumerable)dvgtransferencia.Rows)
		{
			añadedetalleNI(row);
		}
	}

	private void añadedetalleNI(DataGridViewRow fila)
	{
		clsDetalleNotaIngreso deta1 = new clsDetalleNotaIngreso();
		deta1.CodProducto = Convert.ToInt32(fila.Cells[codprod.Name].Value);
		deta1.CodNotaIngreso = Convert.ToInt32(NI.CodNotaIngreso);
		deta1.CodAlmacen = transfer.CodAlmacenDestino;
		deta1.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta1.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta1.Cantidad = Convert.ToDouble(fila.Cells[cant2.Name].Value);
		deta1.PrecioUnitario = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta1.Subtotal = Convert.ToDouble(fila.Cells[importe.Name].Value);
		deta1.Descuento1 = Convert.ToDouble(fila.Cells[dscto1.Name].Value);
		deta1.Descuento2 = Convert.ToDouble(fila.Cells[dscto2.Name].Value);
		deta1.Descuento3 = Convert.ToDouble(fila.Cells[dscto3.Name].Value);
		deta1.MontoDescuento = 0.0;
		deta1.ValoReal = deta1.PrecioUnitario / 1.18;
		deta1.Igv = deta1.ValoReal * 0.18;
		deta1.PrecioReal = deta1.ValoReal * 1.18;
		deta1.CodUser = frmLogin.iCodUser;
		deta1.Importe = deta1.PrecioUnitario * deta1.Cantidad;
		deta1.Subtotal = deta1.Importe;
		deta1.PrecioReal = Convert.ToDouble(fila.Cells[preciounit.Name].Value);
		deta1.ValoReal = Convert.ToDouble(fila.Cells[valoreal.Name].Value);
		deta1.CodProveedor = 0;
		deta1.FechaIngreso = dtpFecha.Value;
		detalleNI.Add(deta1);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRTransferencia));
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label13 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.cbalmacenes2 = new System.Windows.Forms.ComboBox();
		this.cbalmacenes = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dvgtransferencia = new System.Windows.Forms.DataGridView();
		this.btndetalle = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.txtcliente = new System.Windows.Forms.TextBox();
		this.txtdni = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.textBox4 = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.txtdireccion = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.checkBox1 = new System.Windows.Forms.CheckBox();
		this.label9 = new System.Windows.Forms.Label();
		this.btnGuardaOV = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.txtvendedor = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtDocSal = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtDocIng = new System.Windows.Forms.TextBox();
		this.label14 = new System.Windows.Forms.Label();
		this.txtcodserie = new System.Windows.Forms.TextBox();
		this.txtValorVenta = new System.Windows.Forms.TextBox();
		this.label15 = new System.Windows.Forms.Label();
		this.txtBruto = new System.Windows.Forms.TextBox();
		this.label16 = new System.Windows.Forms.Label();
		this.txtCodTransDir = new System.Windows.Forms.TextBox();
		this.btnAprobar = new System.Windows.Forms.Button();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codprod = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.serielote = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciounit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dscto3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.montodscto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valoreal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codProv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorpromedio = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.precioigv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantpedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stcokal = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cant2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btneditar = new System.Windows.Forms.Button();
		this.groupBox5.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dvgtransferencia).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox6.SuspendLayout();
		base.SuspendLayout();
		this.dtpFecha.Enabled = false;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(108, 8);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(100, 20);
		this.dtpFecha.TabIndex = 136;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(12, 9);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(89, 13);
		this.label13.TabIndex = 135;
		this.label13.Text = "FECHA DOC. :";
		this.groupBox5.Controls.Add(this.cbalmacenes2);
		this.groupBox5.Controls.Add(this.cbalmacenes);
		this.groupBox5.Controls.Add(this.label7);
		this.groupBox5.Controls.Add(this.label2);
		this.groupBox5.Location = new System.Drawing.Point(15, 60);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(748, 52);
		this.groupBox5.TabIndex = 142;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "REQUERIMIENTO DE TRANSFERENCIA";
		this.cbalmacenes2.FormattingEnabled = true;
		this.cbalmacenes2.Location = new System.Drawing.Point(168, 18);
		this.cbalmacenes2.Name = "cbalmacenes2";
		this.cbalmacenes2.Size = new System.Drawing.Size(150, 21);
		this.cbalmacenes2.TabIndex = 166;
		this.cbalmacenes2.SelectionChangeCommitted += new System.EventHandler(cbalmacenes2_SelectionChangeCommitted);
		this.cbalmacenes.Enabled = false;
		this.cbalmacenes.FormattingEnabled = true;
		this.cbalmacenes.Location = new System.Drawing.Point(547, 14);
		this.cbalmacenes.Name = "cbalmacenes";
		this.cbalmacenes.Size = new System.Drawing.Size(184, 21);
		this.cbalmacenes.TabIndex = 6;
		this.cbalmacenes.SelectionChangeCommitted += new System.EventHandler(cbalmacenes_SelectionChangeCommitted);
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(397, 17);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(144, 13);
		this.label7.TabIndex = 5;
		this.label7.Text = "ALMACEN DESPACHO :";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(4, 22);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(158, 13);
		this.label2.TabIndex = 4;
		this.label2.Text = "ALMACEN SOLICITANTE :";
		this.groupBox1.Controls.Add(this.dvgtransferencia);
		this.groupBox1.Location = new System.Drawing.Point(12, 148);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(751, 179);
		this.groupBox1.TabIndex = 143;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "DETALLE DE REQUERIMIENTO DE PRODUCTO";
		this.dvgtransferencia.AllowUserToAddRows = false;
		this.dvgtransferencia.AllowUserToDeleteRows = false;
		this.dvgtransferencia.ColumnHeadersHeight = 26;
		this.dvgtransferencia.Columns.AddRange(this.coddetalle, this.codprod, this.codigo, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.precioreal, this.valoreal, this.coduser, this.fecharegistro, this.codProv, this.valorpromedio, this.precioigv, this.cantpedido, this.stcokal, this.cant2);
		this.dvgtransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dvgtransferencia.Location = new System.Drawing.Point(3, 16);
		this.dvgtransferencia.Name = "dvgtransferencia";
		this.dvgtransferencia.RowHeadersVisible = false;
		this.dvgtransferencia.Size = new System.Drawing.Size(745, 160);
		this.dvgtransferencia.TabIndex = 0;
		this.dvgtransferencia.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dvgtransferencia_CellClick);
		this.dvgtransferencia.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dvgtransferencia_CellDoubleClick);
		this.dvgtransferencia.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dvgtransferencia_CellEndEdit);
		this.dvgtransferencia.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(dvgtransferencia_RowsAdded);
		this.btndetalle.Location = new System.Drawing.Point(659, 113);
		this.btndetalle.Name = "btndetalle";
		this.btndetalle.Size = new System.Drawing.Size(99, 29);
		this.btndetalle.TabIndex = 144;
		this.btndetalle.Text = "Detalle";
		this.btndetalle.UseVisualStyleBackColor = true;
		this.btndetalle.Click += new System.EventHandler(btndetalle_Click);
		this.groupBox2.Controls.Add(this.txtcliente);
		this.groupBox2.Controls.Add(this.txtdni);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Location = new System.Drawing.Point(15, 330);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(745, 45);
		this.groupBox2.TabIndex = 145;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Datos del Cliente";
		this.txtcliente.Enabled = false;
		this.txtcliente.Location = new System.Drawing.Point(340, 18);
		this.txtcliente.Name = "txtcliente";
		this.txtcliente.Size = new System.Drawing.Size(391, 20);
		this.txtcliente.TabIndex = 6;
		this.txtdni.Enabled = false;
		this.txtdni.Location = new System.Drawing.Point(126, 18);
		this.txtdni.Name = "txtdni";
		this.txtdni.Size = new System.Drawing.Size(132, 20);
		this.txtdni.TabIndex = 5;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(6, 20);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(113, 13);
		this.label5.TabIndex = 4;
		this.label5.Text = "RUC/DNI Nº (F1) :";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(272, 21);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(67, 13);
		this.label3.TabIndex = 3;
		this.label3.Text = "CLIENTE :";
		this.groupBox3.Controls.Add(this.textBox3);
		this.groupBox3.Controls.Add(this.textBox4);
		this.groupBox3.Controls.Add(this.label4);
		this.groupBox3.Controls.Add(this.label6);
		this.groupBox3.Location = new System.Drawing.Point(16, 381);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(744, 45);
		this.groupBox3.TabIndex = 146;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Datos Para Comunicacion";
		this.textBox3.Location = new System.Drawing.Point(578, 12);
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(146, 20);
		this.textBox3.TabIndex = 6;
		this.textBox4.Location = new System.Drawing.Point(72, 18);
		this.textBox4.Name = "textBox4";
		this.textBox4.Size = new System.Drawing.Size(352, 20);
		this.textBox4.TabIndex = 5;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(6, 20);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(68, 13);
		this.label4.TabIndex = 4;
		this.label4.Text = "NOMBRE: ";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(492, 16);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(80, 13);
		this.label6.TabIndex = 3;
		this.label6.Text = "TELEFONO :";
		this.groupBox4.Controls.Add(this.txtdireccion);
		this.groupBox4.Controls.Add(this.label8);
		this.groupBox4.Location = new System.Drawing.Point(15, 457);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(743, 45);
		this.groupBox4.TabIndex = 147;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Datos Delivery";
		this.txtdireccion.Enabled = false;
		this.txtdireccion.Location = new System.Drawing.Point(86, 17);
		this.txtdireccion.Name = "txtdireccion";
		this.txtdireccion.Size = new System.Drawing.Size(576, 20);
		this.txtdireccion.TabIndex = 5;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(6, 20);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(83, 13);
		this.label8.TabIndex = 4;
		this.label8.Text = "DIRECCION: ";
		this.checkBox1.AutoSize = true;
		this.checkBox1.Location = new System.Drawing.Point(80, 431);
		this.checkBox1.Name = "checkBox1";
		this.checkBox1.Size = new System.Drawing.Size(15, 14);
		this.checkBox1.TabIndex = 149;
		this.checkBox1.UseVisualStyleBackColor = true;
		this.checkBox1.Click += new System.EventHandler(checkBox1_Click);
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(21, 432);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(53, 13);
		this.label9.TabIndex = 148;
		this.label9.Text = "Delivery";
		this.btnGuardaOV.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnGuardaOV.FlatAppearance.BorderSize = 2;
		this.btnGuardaOV.Image = (System.Drawing.Image)resources.GetObject("btnGuardaOV.Image");
		this.btnGuardaOV.Location = new System.Drawing.Point(544, 565);
		this.btnGuardaOV.Name = "btnGuardaOV";
		this.btnGuardaOV.Size = new System.Drawing.Size(99, 43);
		this.btnGuardaOV.TabIndex = 150;
		this.btnGuardaOV.Text = "GUARDA RT";
		this.btnGuardaOV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardaOV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardaOV.UseVisualStyleBackColor = true;
		this.btnGuardaOV.Click += new System.EventHandler(btnGuardaOV_Click);
		this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnSalir.FlatAppearance.BorderSize = 2;
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(658, 568);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(100, 37);
		this.btnSalir.TabIndex = 151;
		this.btnSalir.Text = "SALIR";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.groupBox6.Controls.Add(this.txtvendedor);
		this.groupBox6.Controls.Add(this.label10);
		this.groupBox6.Location = new System.Drawing.Point(16, 509);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(742, 45);
		this.groupBox6.TabIndex = 153;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "Autorizado Por:";
		this.txtvendedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.txtvendedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
		this.txtvendedor.Location = new System.Drawing.Point(72, 16);
		this.txtvendedor.Name = "txtvendedor";
		this.txtvendedor.Size = new System.Drawing.Size(343, 20);
		this.txtvendedor.TabIndex = 7;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(6, 19);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(68, 13);
		this.label10.TabIndex = 5;
		this.label10.Text = "NOMBRE: ";
		this.txtSerie.Location = new System.Drawing.Point(384, 6);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(61, 20);
		this.txtSerie.TabIndex = 84;
		this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumero.Enabled = false;
		this.txtNumero.Location = new System.Drawing.Point(453, 6);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(65, 20);
		this.txtNumero.TabIndex = 85;
		this.txtNumero.Tag = "";
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(336, 11);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(34, 13);
		this.label11.TabIndex = 86;
		this.label11.Text = "Serie.";
		this.label11.Click += new System.EventHandler(label11_Click);
		this.txtDocRef.BackColor = System.Drawing.Color.PeachPuff;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.txtDocRef.Location = new System.Drawing.Point(293, 7);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(28, 20);
		this.txtDocRef.TabIndex = 82;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(234, 11);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 13);
		this.label1.TabIndex = 83;
		this.label1.Tag = "10";
		this.label1.Text = "Doc. Ref.";
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Items.AddRange(new object[2] { "SOLES", "DOLARES" });
		this.cmbMoneda.Location = new System.Drawing.Point(677, 8);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(81, 21);
		this.cmbMoneda.TabIndex = 154;
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(619, 11);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(52, 13);
		this.label17.TabIndex = 155;
		this.label17.Text = "Moneda :";
		this.txtDocSal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocSal.Enabled = false;
		this.txtDocSal.Location = new System.Drawing.Point(263, 29);
		this.txtDocSal.Name = "txtDocSal";
		this.txtDocSal.Size = new System.Drawing.Size(89, 20);
		this.txtDocSal.TabIndex = 156;
		this.txtDocSal.Text = ".";
		this.txtDocSal.Visible = false;
		this.label12.AutoSize = true;
		this.label12.Location = new System.Drawing.Point(168, 33);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(90, 13);
		this.label12.TabIndex = 157;
		this.label12.Text = "Num. Doc. Salida";
		this.label12.Visible = false;
		this.txtDocIng.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocIng.Enabled = false;
		this.txtDocIng.Location = new System.Drawing.Point(554, 31);
		this.txtDocIng.Name = "txtDocIng";
		this.txtDocIng.Size = new System.Drawing.Size(89, 20);
		this.txtDocIng.TabIndex = 158;
		this.txtDocIng.Text = ".";
		this.txtDocIng.Visible = false;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(455, 33);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(96, 13);
		this.label14.TabIndex = 159;
		this.label14.Text = "Num. Doc. Ingreso";
		this.label14.Visible = false;
		this.txtcodserie.Location = new System.Drawing.Point(524, 6);
		this.txtcodserie.Name = "txtcodserie";
		this.txtcodserie.Size = new System.Drawing.Size(16, 20);
		this.txtcodserie.TabIndex = 160;
		this.txtcodserie.Visible = false;
		this.txtValorVenta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtValorVenta.Location = new System.Drawing.Point(415, 433);
		this.txtValorVenta.Name = "txtValorVenta";
		this.txtValorVenta.ReadOnly = true;
		this.txtValorVenta.Size = new System.Drawing.Size(111, 20);
		this.txtValorVenta.TabIndex = 162;
		this.txtValorVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtValorVenta.Visible = false;
		this.label15.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(349, 437);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(54, 13);
		this.label15.TabIndex = 161;
		this.label15.Text = "V. Venta :";
		this.label15.Visible = false;
		this.txtBruto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.txtBruto.Location = new System.Drawing.Point(635, 433);
		this.txtBruto.Name = "txtBruto";
		this.txtBruto.ReadOnly = true;
		this.txtBruto.Size = new System.Drawing.Size(111, 20);
		this.txtBruto.TabIndex = 164;
		this.txtBruto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtBruto.Visible = false;
		this.label16.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(585, 437);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(38, 13);
		this.label16.TabIndex = 163;
		this.label16.Text = "Bruto :";
		this.label16.Visible = false;
		this.txtCodTransDir.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodTransDir.Location = new System.Drawing.Point(358, 29);
		this.txtCodTransDir.Name = "txtCodTransDir";
		this.txtCodTransDir.ReadOnly = true;
		this.txtCodTransDir.Size = new System.Drawing.Size(28, 20);
		this.txtCodTransDir.TabIndex = 77;
		this.txtCodTransDir.Visible = false;
		this.btnAprobar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAprobar.Image = SIGEFA.Properties.Resources.Database_Cloud_32;
		this.btnAprobar.Location = new System.Drawing.Point(319, 564);
		this.btnAprobar.Name = "btnAprobar";
		this.btnAprobar.Size = new System.Drawing.Size(104, 44);
		this.btnAprobar.TabIndex = 165;
		this.btnAprobar.Text = "Aprobar";
		this.btnAprobar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAprobar.UseVisualStyleBackColor = true;
		this.btnAprobar.Visible = false;
		this.btnAprobar.Click += new System.EventHandler(btnAprobar_Click);
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.Visible = false;
		this.codprod.HeaderText = "Codproducto";
		this.codprod.MinimumWidth = 7;
		this.codprod.Name = "codprod";
		this.codprod.ReadOnly = true;
		this.codprod.Visible = false;
		this.codigo.HeaderText = "Codigo";
		this.codigo.MinimumWidth = 7;
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codigo.Width = 68;
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.MinimumWidth = 7;
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 300;
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.Visible = false;
		this.unidad.HeaderText = "Unidad";
		this.unidad.MinimumWidth = 7;
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 90;
		this.serielote.HeaderText = "Serie/Lote";
		this.serielote.Name = "serielote";
		this.serielote.Visible = false;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.MinimumWidth = 7;
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantidad.Width = 60;
		this.preciounit.HeaderText = "P. Unit.";
		this.preciounit.Name = "preciounit";
		this.preciounit.Visible = false;
		this.importe.HeaderText = "Importe";
		this.importe.Name = "importe";
		this.importe.Visible = false;
		this.dscto1.HeaderText = "% Dscto1";
		this.dscto1.Name = "dscto1";
		this.dscto1.Visible = false;
		this.dscto2.HeaderText = "% Dscto2";
		this.dscto2.Name = "dscto2";
		this.dscto2.Visible = false;
		this.dscto3.HeaderText = "% Dscto3";
		this.dscto3.Name = "dscto3";
		this.dscto3.Visible = false;
		this.montodscto.HeaderText = "Monto Dscto";
		this.montodscto.Name = "montodscto";
		this.montodscto.Visible = false;
		this.valorventa.HeaderText = "V. Venta";
		this.valorventa.Name = "valorventa";
		this.valorventa.Visible = false;
		this.igv.HeaderText = "IGV";
		this.igv.Name = "igv";
		this.igv.Visible = false;
		this.precioventa.HeaderText = "P. Venta";
		this.precioventa.Name = "precioventa";
		this.precioventa.Visible = false;
		this.precioreal.HeaderText = "P. real";
		this.precioreal.Name = "precioreal";
		this.precioreal.Visible = false;
		this.valoreal.HeaderText = "V. real";
		this.valoreal.Name = "valoreal";
		this.valoreal.Visible = false;
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.Visible = false;
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.Visible = false;
		this.codProv.HeaderText = "codProv";
		this.codProv.Name = "codProv";
		this.codProv.Visible = false;
		this.valorpromedio.HeaderText = "Valor Promedio";
		this.valorpromedio.Name = "valorpromedio";
		this.valorpromedio.Visible = false;
		this.precioigv.HeaderText = "Precio IGV";
		this.precioigv.Name = "precioigv";
		this.precioigv.Visible = false;
		this.cantpedido.HeaderText = "CantPedido";
		this.cantpedido.Name = "cantpedido";
		this.cantpedido.Visible = false;
		this.stcokal.HeaderText = "Stock Almacen";
		this.stcokal.MinimumWidth = 7;
		this.stcokal.Name = "stcokal";
		this.stcokal.ReadOnly = true;
		this.stcokal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.stcokal.Width = 110;
		this.cant2.HeaderText = "Cant. Requerimiento";
		this.cant2.MinimumWidth = 7;
		this.cant2.Name = "cant2";
		this.cant2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cant2.Width = 110;
		this.btneditar.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btneditar.FlatAppearance.BorderSize = 2;
		this.btneditar.Image = SIGEFA.Properties.Resources.editgrid32;
		this.btneditar.Location = new System.Drawing.Point(429, 565);
		this.btneditar.Name = "btneditar";
		this.btneditar.Size = new System.Drawing.Size(99, 43);
		this.btneditar.TabIndex = 166;
		this.btneditar.Text = "EDITAR";
		this.btneditar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btneditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btneditar.UseVisualStyleBackColor = true;
		this.btneditar.Click += new System.EventHandler(btneditar_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(771, 613);
		base.Controls.Add(this.btneditar);
		base.Controls.Add(this.btnAprobar);
		base.Controls.Add(this.txtCodTransDir);
		base.Controls.Add(this.txtBruto);
		base.Controls.Add(this.label16);
		base.Controls.Add(this.txtValorVenta);
		base.Controls.Add(this.label15);
		base.Controls.Add(this.txtcodserie);
		base.Controls.Add(this.txtDocIng);
		base.Controls.Add(this.label14);
		base.Controls.Add(this.txtDocSal);
		base.Controls.Add(this.label12);
		base.Controls.Add(this.cmbMoneda);
		base.Controls.Add(this.label17);
		base.Controls.Add(this.txtSerie);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.txtNumero);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.label11);
		base.Controls.Add(this.txtDocRef);
		base.Controls.Add(this.label1);
		base.Controls.Add(this.btnGuardaOV);
		base.Controls.Add(this.checkBox1);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.btndetalle);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.dtpFecha);
		base.Controls.Add(this.label13);
		this.DoubleBuffered = true;
		base.Name = "frmRTransferencia";
		this.Text = "REQUERIMIENTO DE TRANSFERENCIA     ";
		base.Load += new System.EventHandler(frmRTransferencia_Load);
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dvgtransferencia).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
