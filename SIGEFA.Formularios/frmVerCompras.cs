using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Transactions;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using Telerik.WinControls.Export;
using Telerik.WinControls.UI;
using Telerik.WinControls.UI.Export;

namespace SIGEFA.Formularios;

public class frmVerCompras : RadForm
{
	public static BindingSource data = new BindingSource();

	private clsAdmFactura admFac = new clsAdmFactura();

	private clsFactura fac = new clsFactura();

	private clsNotaSalida nota = new clsNotaSalida();

	private clsAdmNotaIngreso AdmNota = new clsAdmNotaIngreso();

	private clsAdmNotaSalida AdmNotaSalida = new clsAdmNotaSalida();

	private clsTransaccion trans = new clsTransaccion();

	private clsSerie ser = new clsSerie();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsAdmTransaccion admTrans = new clsAdmTransaccion();

	private DataTable dt_AnulaCompra = new DataTable();

	private List<clsDetalleNotaSalida> lstNotaSalida = new List<clsDetalleNotaSalida>();

	private string filtro = string.Empty;

	private IContainer components = null;

	private GroupBox groupBox1;

	private Button btnConsultar;

	private Label label6;

	private Label label5;

	private DateTimePicker dtpDesde;

	private DateTimePicker dtpHasta;

	private Button btnSalir;

	private ImageList imageList1;

	private Button btnAnular;

	private GroupBox groupBox2;

	private Label txtNombreProducto;

	private Label label3;

	private Button btnBuscarProducto;

	private Label label2;

	private TextBox txtCodprod;

	private Button btnReporte;

	private RadGridView radGridView1;

	public ComboBox cmbfacturas;

	private GroupBox groupBox4;

	private Label label7;

	private Label label4;

	private Label label1;

	private Label sumaDolares;

	private Label sumaConvertido;

	private Label sumaSoles;

	private GroupBox groupBox3;

	private Label sumaDolaresFiltrado;

	private Label sumaConvertidoFiltrado;

	private Label sumaSolesFiltrado;

	private Label label11;

	private Label label12;

	private Label label13;

	private RadioButton rbFechaRegistro;

	private RadioButton rbFecha;

	public frmVerCompras()
	{
		InitializeComponent();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void frmVerCompras_Load(object sender, EventArgs e)
	{
		CargaLista();
		cmbfacturas.DropDownStyle = ComboBoxStyle.DropDownList;
		cmbfacturas.SelectedIndex = 2;
	}

	private void CambiaColor()
	{
		foreach (GridViewRowInfo row in radGridView1.Rows)
		{
			DateTime fecha_vence = Convert.ToDateTime(row.Cells["fechapago"].Value);
			string can = Convert.ToString(row.Cells["cancelado"].Value);
			if (!(fecha_vence <= Convert.ToDateTime(DateTime.Now.ToString())) || can == "Pendiente de pago")
			{
			}
			if (!(can == "Cancelada"))
			{
			}
		}
	}

	private void calculoSumatoriaActivos()
	{
		double auxSumaSoles = 0.0;
		double auxSumaDolares = 0.0;
		bool aux = data.IsReadOnly;
		foreach (GridViewRowInfo fila in radGridView1.Rows)
		{
			if (fila.Cells["Estado"].Value.ToString().Equals("Activo", StringComparison.OrdinalIgnoreCase))
			{
				if (fila.Cells["moneda"].Value.ToString().Equals("Soles", StringComparison.OrdinalIgnoreCase))
				{
					auxSumaSoles += Convert.ToDouble(fila.Cells["total"].Value);
				}
				else if (fila.Cells["moneda"].Value.ToString().Equals("Dolares americanos", StringComparison.OrdinalIgnoreCase))
				{
					auxSumaDolares += Convert.ToDouble(fila.Cells["total"].Value);
				}
			}
		}
		sumaSoles.Text = auxSumaSoles.ToString("## ### ##0.00");
		sumaDolares.Text = auxSumaDolares.ToString("## ### ##0.00");
		sumaConvertido.Text = convertirSumaDolaresASoles(auxSumaDolares, auxSumaSoles).ToString("## ### ##0.00");
	}

	private void calculoSumatoriaFiltrado()
	{
		double auxSumaSoles = 0.0;
		double auxSumaDolares = 0.0;
		foreach (GridViewRowInfo fila in radGridView1.ChildRows)
		{
			if (fila.Cells["Estado"].Value.ToString().Equals("Activo", StringComparison.OrdinalIgnoreCase))
			{
				if (fila.Cells["moneda"].Value.ToString().Equals("Soles", StringComparison.OrdinalIgnoreCase))
				{
					auxSumaSoles += Convert.ToDouble(fila.Cells["total"].Value);
				}
				else if (fila.Cells["moneda"].Value.ToString().Equals("Dolares americanos", StringComparison.OrdinalIgnoreCase))
				{
					auxSumaDolares += Convert.ToDouble(fila.Cells["total"].Value);
				}
			}
		}
		sumaSolesFiltrado.Text = auxSumaSoles.ToString("## ### ##0.00");
		sumaDolaresFiltrado.Text = auxSumaDolares.ToString("## ### ##0.00");
		sumaConvertidoFiltrado.Text = convertirSumaDolaresASoles(auxSumaDolares, auxSumaSoles).ToString("## ### ##0.00");
	}

	private double convertirSumaDolaresASoles(double monto_dolares, double monto_soles)
	{
		try
		{
			double tc_venta = mdi_Menu.tc_hoy;
			return monto_soles + monto_dolares * tc_venta;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return 0.0;
		}
	}

	private void CargaListaCombo()
	{
		radGridView1.DataSource = data;
		data.DataSource = admFac.MuestraFacturaCombo(dtpDesde.Value.Date, dtpHasta.Value.Date, frmLogin.iCodAlmacen, cmbfacturas.SelectedIndex);
		CambiaColor();
		data.Filter = string.Empty;
		filtro = string.Empty;
	}

	private void dgvFacturas_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		try
		{
			if (radGridView1.CurrentRow != null && fac.CodFactura > 0)
			{
				DialogResult dglResult = MessageBox.Show("Esta seguro que desea anular la Factura seleccionada: " + fac.NumFac, "Facturación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dglResult == DialogResult.No)
				{
					return;
				}
				DialogResult dr = DialogResult.None;
				frmAutorizacion frm = new frmAutorizacion();
				dr = frm.ShowDialog();
				if (dr != DialogResult.OK)
				{
					return;
				}
				TimeSpan timeout = TimeSpan.FromMinutes(10.0);
				bool bandera = false;
				using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
				{
					IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted,
					Timeout = timeout
				}))
				{
					int codNotaSalida = 0;
					nota = AdmNota.buscaNotaIngreso(Convert.ToInt32(fac.CodFactura));
					if (nota == null)
					{
						MessageBox.Show("Error al consultar Venta", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						return;
					}
					codNotaSalida = Convert.ToInt32(nota.CodNotaSalida);
					trans = admTrans.MuestraTransaccion(12);
					nota.CodTipoTransaccion = trans.CodTransaccion;
					doc = Admdoc.BuscaTipoDocumento("DIAC");
					ser = Admser.BuscaSeriexDocumento(doc.CodTipoDocumento, frmLogin.iCodAlmacen);
					nota.Serie = ser.Serie;
					nota.NumDoc = nota.NumDoc.ToString();
					nota.DescripcionTransaccion = trans.Descripcion;
					nota.CodTipoDocumento = doc.CodTipoDocumento;
					nota.CodSerie = ser.CodSerie;
					nota.DocumentoReferencia = Convert.ToInt32(nota.CodNotaSalida);
					if (!AdmNotaSalida.insert(nota))
					{
						MessageBox.Show("No se pudo registrar la salida..!\nReintentelo", "Compras", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						return;
					}
					dt_AnulaCompra = AdmNota.CargaDetalle(Convert.ToInt32(codNotaSalida));
					LeeProductos();
					foreach (clsDetalleNotaSalida det in lstNotaSalida)
					{
						if (!AdmNotaSalida.insertdetalle(det))
						{
							MessageBox.Show("No se puede devolver el siguiente producto:\nCodigo: " + $"{det.CodProducto:D9}" + "\tCantidad: " + det.Cantidad, "Compras", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
					}
					string codusuario = frm.user;
					if (admFac.anular_1(fac.CodFactura, Convert.ToInt32(codusuario)))
					{
						MessageBox.Show("La Factura ha sido anulada correctamente", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						bandera = true;
					}
					else
					{
						MessageBox.Show("La Factura no se puede anular", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						bandera = false;
					}
					if (bandera)
					{
						scope.Complete();
					}
				}
				if (bandera)
				{
					CargaLista();
				}
			}
			else
			{
				MessageBox.Show("Seleccione una factura a ANULAR", "Facturación", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error frmVerCompras", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}

	private void LeeProductos()
	{
		try
		{
			lstNotaSalida.Clear();
			foreach (DataRow row3 in dt_AnulaCompra.Rows)
			{
				clsDetalleNotaSalida DetIng = new clsDetalleNotaSalida();
				DetIng.CodProducto = Convert.ToInt32(row3[1]);
				DetIng.CodNotaSalida = Convert.ToInt32(nota.CodNotaSalida);
				DetIng.CodAlmacen = Convert.ToInt32(nota.CodAlmacen);
				DetIng.UnidadIngresada = Convert.ToInt32(row3[5]);
				DetIng.Cantidad = Convert.ToDouble(row3[8]);
				DetIng.PrecioUnitario = Convert.ToDouble(row3[9]);
				DetIng.Descuento1 = Convert.ToDouble(row3[11]);
				DetIng.Descuento2 = Convert.ToDouble(row3[12]);
				DetIng.Descuento3 = Convert.ToDouble(row3[13]);
				DetIng.MontoDescuento = Convert.ToDouble(row3[14]);
				DetIng.Importe = Convert.ToDouble(row3[9]) * DetIng.Cantidad;
				DetIng.Subtotal = DetIng.PrecioUnitario * DetIng.Cantidad;
				DetIng.Igv = DetIng.Importe - DetIng.Subtotal;
				DetIng.PrecioReal = Convert.ToDouble(row3[20]);
				DetIng.ValoReal = Convert.ToDouble(row3[19]);
				DetIng.CodUser = Convert.ToInt32(row3[22]);
				lstNotaSalida.Add(DetIng);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnConsultar_Click(object sender, EventArgs e)
	{
		CargaLista();
	}

	private void btnBuscarProducto_Click(object sender, EventArgs e)
	{
		if (txtCodprod.Text != "")
		{
			CargaListaxProducto();
		}
		else
		{
			txtNombreProducto.Text = "---";
		}
	}

	private void CargaLista()
	{
		radGridView1.DataSource = data;
		int tipoFecha = (rbFecha.Checked ? 2 : (rbFechaRegistro.Checked ? 1 : 0));
		data.DataSource = admFac.MuestraFactura(tipoFecha, dtpDesde.Value.Date, dtpHasta.Value.Date, frmLogin.iCodAlmacen);
		CambiaColor();
		if (filtro != string.Empty)
		{
			data.Filter = filtro;
		}
		else
		{
			data.Filter = string.Empty;
			filtro = string.Empty;
		}
		calculoSumatoriaActivos();
	}

	private void CargaListaxProducto()
	{
		radGridView1.DataSource = data;
		data.DataSource = admFac.MuestraFacturaxProducto(dtpDesde.Value.Date, dtpHasta.Value.Date, frmLogin.iCodAlmacen, Convert.ToInt32(txtCodprod.Text));
		CambiaColor();
		if (filtro != string.Empty)
		{
			data.Filter = filtro;
		}
		else
		{
			data.Filter = string.Empty;
			filtro = string.Empty;
		}
		calculoSumatoriaActivos();
	}

	private void txtCodprod_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 2;
			frm.CodLista = 1;
			frm.tc = mdi_Menu.tc_hoy;
			frm.alma = frmLogin.iCodAlmacen;
			DialogResult result = frm.ShowDialog();
			if (result == DialogResult.OK && frm.GetCodigoProducto().ToString() != "")
			{
				txtCodprod.Text = "";
				txtCodprod.Text = frm.GetCodigoProducto2().ToString();
				txtNombreProducto.Text = frm.GetNombreProducto();
			}
		}
		if (e.KeyCode == Keys.Return)
		{
			btnBuscarProducto_Click(null, null);
		}
	}

	private void dgvFacturas_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void radGridView1_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (radGridView1.Rows.Count >= 1 && e.RowIndex != -1)
		{
			frmNotaIngreso form = new frmNotaIngreso();
			form.MdiParent = base.MdiParent;
			form.CodNota = fac.CodFactura.ToString();
			form.Proceso = 2;
			form.Show();
		}
	}

	private void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
	{
		try
		{
			if (radGridView1.Rows.Count >= 1 && e.RowIndex != -1)
			{
				fac.CodFactura = Convert.ToInt32(e.Row.Cells["codNotaI"].Value);
				fac.NumFac = e.Row.Cells["Documento"].Value.ToString();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		try
		{
			if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\reporteCompras.xlsx"))
			{
				try
				{
					File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\reporteCompras.xlsx");
				}
				catch (Exception)
				{
					MessageBox.Show("Ocurrió un error al eliminar archivo de compras, se recomienda cerrar el archivo en\n los equipos que se estén usando");
				}
			}
			GridViewSpreadStreamExport spreadStreamExport = new GridViewSpreadStreamExport(radGridView1);
			spreadStreamExport.ExportVisualSettings = true;
			spreadStreamExport.PagingExportOption = PagingExportOption.AllPages;
			spreadStreamExport.RunExport(AppDomain.CurrentDomain.BaseDirectory + "\\reporteCompras.xlsx", new SpreadStreamExportRenderer());
			FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "\\reporteCompras.xlsx");
			if (fi.Exists)
			{
				Process.Start(AppDomain.CurrentDomain.BaseDirectory + "\\reporteCompras.xlsx");
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Ocurrió un error al eliminar archivo de compras, se recomienda cerrar el archivo en\n los equipos que se estén usando");
		}
	}

	private void cmbfacturas_SelectedValueChanged(object sender, EventArgs e)
	{
		try
		{
			if (cmbfacturas.SelectedIndex == 2)
			{
				data.Filter = string.Empty;
				filtro = string.Empty;
			}
			else
			{
				filtro = $"[Estado] like '*{cmbfacturas.Text}*'";
				data.Filter = filtro;
			}
			calculoSumatoriaActivos();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void radGridView1_FilterExpressionChanged(object sender, FilterExpressionChangedEventArgs e)
	{
		Console.WriteLine("\n" + e.FilterExpression + " -- " + e.ToString());
		foreach (GridViewRowInfo item in radGridView1.Rows)
		{
		}
		calculoSumatoriaFiltrado();
	}

	private void radGridView1_FilterChanged(object sender, GridViewCollectionChangedEventArgs e)
	{
		try
		{
			calculoSumatoriaFiltrado();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
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
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn12 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn13 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn14 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmVerCompras));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.radGridView1 = new Telerik.WinControls.UI.RadGridView();
		this.label6 = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.dtpDesde = new System.Windows.Forms.DateTimePicker();
		this.dtpHasta = new System.Windows.Forms.DateTimePicker();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.rbFechaRegistro = new System.Windows.Forms.RadioButton();
		this.rbFecha = new System.Windows.Forms.RadioButton();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.sumaDolaresFiltrado = new System.Windows.Forms.Label();
		this.sumaConvertidoFiltrado = new System.Windows.Forms.Label();
		this.sumaSolesFiltrado = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.label12 = new System.Windows.Forms.Label();
		this.label13 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.sumaDolares = new System.Windows.Forms.Label();
		this.sumaConvertido = new System.Windows.Forms.Label();
		this.sumaSoles = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbfacturas = new System.Windows.Forms.ComboBox();
		this.btnReporte = new System.Windows.Forms.Button();
		this.txtNombreProducto = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.btnBuscarProducto = new System.Windows.Forms.Button();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCodprod = new System.Windows.Forms.TextBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnConsultar = new System.Windows.Forms.Button();
		this.btnAnular = new System.Windows.Forms.Button();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.radGridView1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radGridView1.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.radGridView1);
		this.groupBox1.Location = new System.Drawing.Point(-2, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1418, 423);
		this.groupBox1.TabIndex = 1;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Vigentes";
		this.radGridView1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.radGridView1.AutoScroll = true;
		this.radGridView1.AutoSizeRows = true;
		this.radGridView1.BackColor = System.Drawing.Color.FromArgb(191, 219, 255);
		this.radGridView1.Cursor = System.Windows.Forms.Cursors.Default;
		this.radGridView1.Font = new System.Drawing.Font("Segoe UI", 8.25f);
		this.radGridView1.ForeColor = System.Drawing.Color.Black;
		this.radGridView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.radGridView1.Location = new System.Drawing.Point(3, 21);
		this.radGridView1.MasterTemplate.AllowAddNewRow = false;
		this.radGridView1.MasterTemplate.AllowColumnReorder = false;
		this.radGridView1.MasterTemplate.AllowRowReorder = true;
		this.radGridView1.MasterTemplate.AutoExpandGroups = true;
		this.radGridView1.MasterTemplate.AutoGenerateColumns = false;
		gridViewTextBoxColumn1.EnableExpressionEditor = false;
		gridViewTextBoxColumn1.FieldName = "codfactura";
		gridViewTextBoxColumn1.HeaderText = "COD. FACTURA";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "codfactura";
		gridViewTextBoxColumn2.EnableExpressionEditor = false;
		gridViewTextBoxColumn2.FieldName = "documento";
		gridViewTextBoxColumn2.HeaderText = "DOCUMENTO";
		gridViewTextBoxColumn2.Multiline = true;
		gridViewTextBoxColumn2.Name = "Documento";
		gridViewTextBoxColumn2.Width = 120;
		gridViewTextBoxColumn2.WrapText = true;
		gridViewTextBoxColumn3.EnableExpressionEditor = false;
		gridViewTextBoxColumn3.FieldName = "ruc";
		gridViewTextBoxColumn3.HeaderText = "RUC";
		gridViewTextBoxColumn3.Name = "RUC";
		gridViewTextBoxColumn3.Width = 90;
		gridViewTextBoxColumn4.AllowReorder = false;
		gridViewTextBoxColumn4.EnableExpressionEditor = false;
		gridViewTextBoxColumn4.FieldName = "razonsocial";
		gridViewTextBoxColumn4.HeaderText = "RAZON SOCIAL";
		gridViewTextBoxColumn4.Multiline = true;
		gridViewTextBoxColumn4.Name = "proveedor";
		gridViewTextBoxColumn4.Width = 200;
		gridViewTextBoxColumn4.WrapText = true;
		gridViewTextBoxColumn5.EnableExpressionEditor = false;
		gridViewTextBoxColumn5.FieldName = "total";
		gridViewTextBoxColumn5.HeaderText = "MONTO FACTURA";
		gridViewTextBoxColumn5.Name = "total";
		gridViewTextBoxColumn5.Width = 130;
		gridViewTextBoxColumn6.EnableExpressionEditor = false;
		gridViewTextBoxColumn6.FieldName = "moneda";
		gridViewTextBoxColumn6.HeaderText = "MONEDA";
		gridViewTextBoxColumn6.Name = "moneda";
		gridViewTextBoxColumn6.Width = 60;
		gridViewTextBoxColumn7.EnableExpressionEditor = false;
		gridViewTextBoxColumn7.FieldName = "monto_soles";
		gridViewTextBoxColumn7.HeaderText = "MONTO EN S/";
		gridViewTextBoxColumn7.IsVisible = false;
		gridViewTextBoxColumn7.Name = "monto_soles";
		gridViewTextBoxColumn7.Width = 82;
		gridViewTextBoxColumn8.EnableExpressionEditor = false;
		gridViewTextBoxColumn8.FieldName = "codProveedor";
		gridViewTextBoxColumn8.HeaderText = "COD. PROVEEDOR";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "codProveedor";
		gridViewTextBoxColumn9.EnableExpressionEditor = false;
		gridViewTextBoxColumn9.FieldName = "fecharegistro";
		gridViewTextBoxColumn9.HeaderText = "FECHA REGISTRO";
		gridViewTextBoxColumn9.Name = "fecharegistro";
		gridViewTextBoxColumn9.Width = 123;
		gridViewTextBoxColumn10.EnableExpressionEditor = false;
		gridViewTextBoxColumn10.FieldName = "fecha";
		gridViewTextBoxColumn10.HeaderText = "F.COMPRA";
		gridViewTextBoxColumn10.Name = "fecha";
		gridViewTextBoxColumn10.Width = 90;
		gridViewTextBoxColumn11.EnableExpressionEditor = false;
		gridViewTextBoxColumn11.FieldName = "fechapago";
		gridViewTextBoxColumn11.HeaderText = "F.VENCIMIENTO";
		gridViewTextBoxColumn11.Name = "fechapago";
		gridViewTextBoxColumn11.Width = 90;
		gridViewTextBoxColumn12.EnableExpressionEditor = false;
		gridViewTextBoxColumn12.FieldName = "responsable";
		gridViewTextBoxColumn12.HeaderText = "USUARIO";
		gridViewTextBoxColumn12.Multiline = true;
		gridViewTextBoxColumn12.Name = "responsable";
		gridViewTextBoxColumn12.Width = 100;
		gridViewTextBoxColumn12.WrapText = true;
		gridViewTextBoxColumn13.FieldName = "user_anulador";
		gridViewTextBoxColumn13.HeaderText = "USUARIO ANULADOR";
		gridViewTextBoxColumn13.Name = "user_anulador";
		gridViewTextBoxColumn13.Width = 120;
		gridViewTextBoxColumn14.EnableExpressionEditor = false;
		gridViewTextBoxColumn14.FieldName = "Estado";
		gridViewTextBoxColumn14.HeaderText = "ESTADO";
		gridViewTextBoxColumn14.Name = "Estado";
		gridViewTextBoxColumn14.Width = 80;
		gridViewTextBoxColumn15.EnableExpressionEditor = false;
		gridViewTextBoxColumn15.FieldName = "codNotaI";
		gridViewTextBoxColumn15.HeaderText = "COD. NOTA ";
		gridViewTextBoxColumn15.IsVisible = false;
		gridViewTextBoxColumn15.Name = "codNotaI";
		gridViewTextBoxColumn16.EnableExpressionEditor = false;
		gridViewTextBoxColumn16.FieldName = "cancelado";
		gridViewTextBoxColumn16.HeaderText = "CANCELADO";
		gridViewTextBoxColumn16.Name = "cancelado";
		gridViewTextBoxColumn16.Width = 123;
		gridViewTextBoxColumn17.EnableExpressionEditor = false;
		gridViewTextBoxColumn17.FieldName = "comentario";
		gridViewTextBoxColumn17.HeaderText = "COMENTARIO";
		gridViewTextBoxColumn17.Multiline = true;
		gridViewTextBoxColumn17.Name = "comentario";
		gridViewTextBoxColumn17.Width = 145;
		gridViewTextBoxColumn17.WrapText = true;
		this.radGridView1.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewTextBoxColumn8, gridViewTextBoxColumn9, gridViewTextBoxColumn10, gridViewTextBoxColumn11, gridViewTextBoxColumn12, gridViewTextBoxColumn13, gridViewTextBoxColumn14, gridViewTextBoxColumn15, gridViewTextBoxColumn16, gridViewTextBoxColumn17);
		this.radGridView1.MasterTemplate.EnableFiltering = true;
		this.radGridView1.MasterTemplate.EnableGrouping = false;
		this.radGridView1.MasterTemplate.ShowChildViewCaptions = true;
		this.radGridView1.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.radGridView1.Name = "radGridView1";
		this.radGridView1.ReadOnly = true;
		this.radGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.radGridView1.ShowChildViewCaptions = true;
		this.radGridView1.Size = new System.Drawing.Size(1377, 396);
		this.radGridView1.TabIndex = 1;
		this.radGridView1.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(radGridView1_CellClick);
		this.radGridView1.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(radGridView1_CellDoubleClick);
		this.radGridView1.FilterExpressionChanged += new Telerik.WinControls.UI.GridViewFilterExpressionChangedEventHandler(radGridView1_FilterExpressionChanged);
		this.radGridView1.FilterChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(radGridView1_FilterChanged);
		this.label6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(155, 26);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(57, 16);
		this.label6.TabIndex = 28;
		this.label6.Text = "Hasta :";
		this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(3, 26);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(62, 16);
		this.label5.TabIndex = 27;
		this.label5.Text = "Desde :";
		this.dtpDesde.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpDesde.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpDesde.Location = new System.Drawing.Point(6, 46);
		this.dtpDesde.Name = "dtpDesde";
		this.dtpDesde.Size = new System.Drawing.Size(138, 22);
		this.dtpDesde.TabIndex = 26;
		this.dtpHasta.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.dtpHasta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpHasta.Location = new System.Drawing.Point(158, 45);
		this.dtpHasta.Name = "dtpHasta";
		this.dtpHasta.Size = new System.Drawing.Size(138, 22);
		this.dtpHasta.TabIndex = 25;
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "exit.png");
		this.imageList1.Images.SetKeyName(1, "pedido.png");
		this.imageList1.Images.SetKeyName(2, "carrito.png");
		this.imageList1.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList1.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList1.Images.SetKeyName(5, "document_delete.png");
		this.groupBox2.Controls.Add(this.rbFechaRegistro);
		this.groupBox2.Controls.Add(this.rbFecha);
		this.groupBox2.Controls.Add(this.groupBox3);
		this.groupBox2.Controls.Add(this.groupBox4);
		this.groupBox2.Controls.Add(this.cmbfacturas);
		this.groupBox2.Controls.Add(this.btnReporte);
		this.groupBox2.Controls.Add(this.txtNombreProducto);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.btnBuscarProducto);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.txtCodprod);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.dtpHasta);
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Controls.Add(this.dtpDesde);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.btnConsultar);
		this.groupBox2.Controls.Add(this.btnAnular);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 416);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1378, 123);
		this.groupBox2.TabIndex = 32;
		this.groupBox2.TabStop = false;
		this.rbFechaRegistro.AutoSize = true;
		this.rbFechaRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbFechaRegistro.Location = new System.Drawing.Point(128, 7);
		this.rbFechaRegistro.Name = "rbFechaRegistro";
		this.rbFechaRegistro.Size = new System.Drawing.Size(122, 19);
		this.rbFechaRegistro.TabIndex = 56;
		this.rbFechaRegistro.Text = "Fecha Registro";
		this.rbFechaRegistro.UseVisualStyleBackColor = true;
		this.rbFecha.AutoSize = true;
		this.rbFecha.Checked = true;
		this.rbFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.rbFecha.Location = new System.Drawing.Point(6, 7);
		this.rbFecha.Name = "rbFecha";
		this.rbFecha.Size = new System.Drawing.Size(118, 19);
		this.rbFecha.TabIndex = 55;
		this.rbFecha.TabStop = true;
		this.rbFecha.Text = "Fecha Compra";
		this.rbFecha.UseVisualStyleBackColor = true;
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox3.Controls.Add(this.sumaDolaresFiltrado);
		this.groupBox3.Controls.Add(this.sumaConvertidoFiltrado);
		this.groupBox3.Controls.Add(this.sumaSolesFiltrado);
		this.groupBox3.Controls.Add(this.label11);
		this.groupBox3.Controls.Add(this.label12);
		this.groupBox3.Controls.Add(this.label13);
		this.groupBox3.ForeColor = System.Drawing.Color.Blue;
		this.groupBox3.Location = new System.Drawing.Point(518, 13);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(286, 89);
		this.groupBox3.TabIndex = 48;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Sumatoria Filtrado";
		this.sumaDolaresFiltrado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.sumaDolaresFiltrado.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.sumaDolaresFiltrado.ForeColor = System.Drawing.Color.Green;
		this.sumaDolaresFiltrado.Location = new System.Drawing.Point(170, 39);
		this.sumaDolaresFiltrado.Name = "sumaDolaresFiltrado";
		this.sumaDolaresFiltrado.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.sumaDolaresFiltrado.Size = new System.Drawing.Size(105, 15);
		this.sumaDolaresFiltrado.TabIndex = 5;
		this.sumaDolaresFiltrado.Text = "0.00";
		this.sumaDolaresFiltrado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.sumaConvertidoFiltrado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.sumaConvertidoFiltrado.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.sumaConvertidoFiltrado.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sumaConvertidoFiltrado.Location = new System.Drawing.Point(170, 61);
		this.sumaConvertidoFiltrado.Name = "sumaConvertidoFiltrado";
		this.sumaConvertidoFiltrado.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.sumaConvertidoFiltrado.Size = new System.Drawing.Size(105, 15);
		this.sumaConvertidoFiltrado.TabIndex = 4;
		this.sumaConvertidoFiltrado.Text = "0.00";
		this.sumaConvertidoFiltrado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.sumaSolesFiltrado.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.sumaSolesFiltrado.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.sumaSolesFiltrado.ForeColor = System.Drawing.Color.Goldenrod;
		this.sumaSolesFiltrado.Location = new System.Drawing.Point(170, 18);
		this.sumaSolesFiltrado.Name = "sumaSolesFiltrado";
		this.sumaSolesFiltrado.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.sumaSolesFiltrado.Size = new System.Drawing.Size(105, 15);
		this.sumaSolesFiltrado.TabIndex = 3;
		this.sumaSolesFiltrado.Text = "0.00";
		this.sumaSolesFiltrado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.label11.ForeColor = System.Drawing.Color.Green;
		this.label11.Location = new System.Drawing.Point(72, 39);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(92, 15);
		this.label11.TabIndex = 2;
		this.label11.Text = "Total Dolares: $";
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label12.Location = new System.Drawing.Point(3, 61);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(161, 15);
		this.label12.TabIndex = 1;
		this.label12.Text = "Total Convertido a Soles: S/.";
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.label13.ForeColor = System.Drawing.Color.Goldenrod;
		this.label13.Location = new System.Drawing.Point(77, 18);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(87, 15);
		this.label13.TabIndex = 0;
		this.label13.Text = "Total Soles: S/.";
		this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox4.Controls.Add(this.sumaDolares);
		this.groupBox4.Controls.Add(this.sumaConvertido);
		this.groupBox4.Controls.Add(this.sumaSoles);
		this.groupBox4.Controls.Add(this.label7);
		this.groupBox4.Controls.Add(this.label4);
		this.groupBox4.Controls.Add(this.label1);
		this.groupBox4.ForeColor = System.Drawing.Color.Blue;
		this.groupBox4.Location = new System.Drawing.Point(849, 13);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(286, 89);
		this.groupBox4.TabIndex = 47;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Sumatoria Activos";
		this.sumaDolares.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.sumaDolares.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.sumaDolares.ForeColor = System.Drawing.Color.Green;
		this.sumaDolares.Location = new System.Drawing.Point(170, 39);
		this.sumaDolares.Name = "sumaDolares";
		this.sumaDolares.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.sumaDolares.Size = new System.Drawing.Size(105, 15);
		this.sumaDolares.TabIndex = 5;
		this.sumaDolares.Text = "0.00";
		this.sumaDolares.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.sumaConvertido.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.sumaConvertido.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.sumaConvertido.ForeColor = System.Drawing.SystemColors.ControlText;
		this.sumaConvertido.Location = new System.Drawing.Point(170, 61);
		this.sumaConvertido.Name = "sumaConvertido";
		this.sumaConvertido.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.sumaConvertido.Size = new System.Drawing.Size(105, 15);
		this.sumaConvertido.TabIndex = 4;
		this.sumaConvertido.Text = "0.00";
		this.sumaConvertido.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.sumaSoles.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.sumaSoles.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.sumaSoles.ForeColor = System.Drawing.Color.Goldenrod;
		this.sumaSoles.Location = new System.Drawing.Point(170, 18);
		this.sumaSoles.Name = "sumaSoles";
		this.sumaSoles.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.sumaSoles.Size = new System.Drawing.Size(105, 15);
		this.sumaSoles.TabIndex = 3;
		this.sumaSoles.Text = "0.00";
		this.sumaSoles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.label7.ForeColor = System.Drawing.Color.Green;
		this.label7.Location = new System.Drawing.Point(72, 39);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(92, 15);
		this.label7.TabIndex = 2;
		this.label7.Text = "Total Dolares: $";
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
		this.label4.Location = new System.Drawing.Point(3, 61);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(161, 15);
		this.label4.TabIndex = 1;
		this.label4.Text = "Total Convertido a Soles: S/.";
		this.label1.AutoSize = true;
		this.label1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.label1.ForeColor = System.Drawing.Color.Goldenrod;
		this.label1.Location = new System.Drawing.Point(77, 18);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(87, 15);
		this.label1.TabIndex = 0;
		this.label1.Text = "Total Soles: S/.";
		this.cmbfacturas.BackColor = System.Drawing.SystemColors.ControlLight;
		this.cmbfacturas.Cursor = System.Windows.Forms.Cursors.Default;
		this.cmbfacturas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.cmbfacturas.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbfacturas.ForeColor = System.Drawing.SystemColors.Desktop;
		this.cmbfacturas.FormattingEnabled = true;
		this.cmbfacturas.Items.AddRange(new object[3] { "Activo", "Anulada", "Todos" });
		this.cmbfacturas.Location = new System.Drawing.Point(331, 51);
		this.cmbfacturas.Name = "cmbfacturas";
		this.cmbfacturas.Size = new System.Drawing.Size(124, 21);
		this.cmbfacturas.TabIndex = 45;
		this.cmbfacturas.SelectedValueChanged += new System.EventHandler(cmbfacturas_SelectedValueChanged);
		this.btnReporte.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnReporte.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.btnReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnReporte.Image = SIGEFA.Properties.Resources.microsoft_excel_32;
		this.btnReporte.Location = new System.Drawing.Point(1179, 21);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(100, 37);
		this.btnReporte.TabIndex = 44;
		this.btnReporte.Text = "Excel";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.txtNombreProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtNombreProducto.AutoSize = true;
		this.txtNombreProducto.Location = new System.Drawing.Point(231, 91);
		this.txtNombreProducto.Name = "txtNombreProducto";
		this.txtNombreProducto.Size = new System.Drawing.Size(19, 13);
		this.txtNombreProducto.TabIndex = 43;
		this.txtNombreProducto.Text = "---";
		this.label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(190, 91);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(37, 13);
		this.label3.TabIndex = 42;
		this.label3.Text = "Prod.:";
		this.btnBuscarProducto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnBuscarProducto.Image = SIGEFA.Properties.Resources.buscar;
		this.btnBuscarProducto.Location = new System.Drawing.Point(150, 81);
		this.btnBuscarProducto.Name = "btnBuscarProducto";
		this.btnBuscarProducto.Size = new System.Drawing.Size(34, 33);
		this.btnBuscarProducto.TabIndex = 41;
		this.btnBuscarProducto.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnBuscarProducto.UseVisualStyleBackColor = true;
		this.btnBuscarProducto.Click += new System.EventHandler(btnBuscarProducto_Click);
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(3, 75);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(119, 13);
		this.label2.TabIndex = 40;
		this.label2.Text = "Busqueda x Producto:";
		this.txtCodprod.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtCodprod.Location = new System.Drawing.Point(6, 91);
		this.txtCodprod.Name = "txtCodprod";
		this.txtCodprod.Size = new System.Drawing.Size(138, 20);
		this.txtCodprod.TabIndex = 39;
		this.txtCodprod.KeyDown += new System.Windows.Forms.KeyEventHandler(txtCodprod_KeyDown);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnSalir.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.btnSalir.Image = (System.Drawing.Image)resources.GetObject("btnSalir.Image");
		this.btnSalir.Location = new System.Drawing.Point(1285, 20);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(81, 38);
		this.btnSalir.TabIndex = 30;
		this.btnSalir.Text = "SALIR";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnConsultar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.btnConsultar.BackColor = System.Drawing.Color.White;
		this.btnConsultar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.btnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnConsultar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnConsultar.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.btnConsultar.Image = (System.Drawing.Image)resources.GetObject("btnConsultar.Image");
		this.btnConsultar.Location = new System.Drawing.Point(331, 13);
		this.btnConsultar.Name = "btnConsultar";
		this.btnConsultar.Size = new System.Drawing.Size(123, 32);
		this.btnConsultar.TabIndex = 29;
		this.btnConsultar.Text = "BUSCAR";
		this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnConsultar.UseVisualStyleBackColor = false;
		this.btnConsultar.Click += new System.EventHandler(btnConsultar_Click);
		this.btnAnular.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnAnular.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
		this.btnAnular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btnAnular.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnAnular.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
		this.btnAnular.Image = (System.Drawing.Image)resources.GetObject("btnAnular.Image");
		this.btnAnular.Location = new System.Drawing.Point(1197, 64);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(157, 38);
		this.btnAnular.TabIndex = 31;
		this.btnAnular.Text = "ANULAR FACTURA";
		this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAnular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1378, 539);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "frmVerCompras";
		base.RootElement.ApplyShapeToControl = true;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Facturas";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmVerCompras_Load);
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.radGridView1.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radGridView1).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
	}
}
