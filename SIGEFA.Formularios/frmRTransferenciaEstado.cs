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
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmRTransferenciaEstado : Office2007Form
{
	private clsReporteTransferencias ds = new clsReporteTransferencias();

	private clsAdmAlmacen admAlmacen = new clsAdmAlmacen();

	private clsAlmacen almacen = new clsAlmacen();

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

	private clsAdmCliente AdmCli = new clsAdmCliente();

	private clsCliente cli = new clsCliente();

	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private clsPedido pedido = new clsPedido();

	public int CodSerie;

	public int num;

	private clsAdmUsuario admUsuario = new clsAdmUsuario();

	public List<clsDetalleNotaSalida> detalleNS = new List<clsDetalleNotaSalida>();

	public List<clsDetalleNotaIngreso> detalleNI = new List<clsDetalleNotaIngreso>();

	public int CodTransaccion;

	public int CodDocumento;

	public int Proceso;

	public int CodTransDirecta;

	public int CodTransDirecta2 = 0;

	public int caso;

	public int enviados;

	public int CodCliente;

	public int Tipo;

	public string NombreCliente;

	public string CodPedido;

	public string NombreAlmacen;

	public string estadotransfer;

	private IContainer components = null;

	private Button btnsalir;

	private Label label1;

	private GroupBox groupBox1;

	public TextBox txtDocRef;

	public TextBox txtSerie;

	private Label label11;

	private TextBox txtcodserie;

	private TextBox txtNumero;

	private ComboBox cmbMoneda;

	private Label label17;

	private GroupBox groupBox5;

	private Label label7;

	private Label label2;

	private TextBox txtorgien;

	private TextBox txtdestino;

	private GroupBox groupBox2;

	public DataGridView dvgtransferencia;

	private GroupBox groupBox3;

	private TextBox txtcliente;

	private TextBox txtdni;

	private Label label5;

	private Label label3;

	private GroupBox groupBox4;

	private TextBox txttelefono;

	private TextBox txtnombre;

	private Label label4;

	private Label label6;

	private GroupBox groupBox6;

	private TextBox txtdireccion;

	private Label label8;

	private GroupBox groupBox7;

	private TextBox txtvendedor;

	private Label label10;

	private Button btnentregar;

	private TextBox txtCodTransDir;

	public TextBox txtCoddestino;

	public TextBox txtCodorigen;

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

	private DataGridViewTextBoxColumn cantpendiente;

	private DataGridViewTextBoxColumn cantentregar;

	private TextBox txtestado;

	private Label label12;

	private Button button1;

	private GroupBox groupBox8;

	private TextBox txtdespacho;

	private Label label13;

	private Label label9;

	private TextBox txtComentario;

	public frmRTransferenciaEstado()
	{
		InitializeComponent();
	}

	private void CargaTransferencia()
	{
		try
		{
			transfer = admTransferencia.CargaTransferencia(Convert.ToInt32(CodTransDirecta));
			CodTransDirecta2 = CodTransDirecta;
			if (transfer != null)
			{
				txtCodTransDir.Text = transfer.CodTransDir;
				txtcodserie.Text = transfer.Codserie.ToString();
				txtSerie.Text = transfer.Serie.ToString();
				txtNumero.Text = transfer.Numerodocumento.ToString();
				txtnombre.Text = transfer.Nombretrans.ToString();
				txttelefono.Text = transfer.Telefonotrans.ToString();
				txtdireccion.Text = transfer.Direcciontrans.ToString();
				txtComentario.Text = transfer.DescripcionRechazo;
				txtvendedor.Text = transfer.Autorizadopor;
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
				txtCodorigen.Text = transfer.CodAlmacenOrigen.ToString();
				txtCoddestino.Text = transfer.CodAlmacenDestino.ToString();
				almacen = admAlmacen.CargaAlmacen(transfer.CodAlmacenOrigen);
				txtorgien.Text = almacen.Descripcion;
				CargaPedido(Convert.ToInt32(transfer.Numpedido));
				txtdestino.Text = transfer.CodAlmacenDestino.ToString();
				almacen = admAlmacen.CargaAlmacen(transfer.CodAlmacenDestino);
				txtdestino.Text = almacen.Descripcion;
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			txtestado.Text = estadotransfer;
			int codalmaceno = Convert.ToInt32(frmLogin.iCodAlmacen);
			if (Convert.ToInt32(transfer.CodAlmacenOrigen).Equals(codalmaceno))
			{
				if (estadotransfer.Equals("FACTURADO") || estadotransfer.Equals("ENTREGADO PARCIAL"))
				{
					btnentregar.Enabled = true;
				}
				else
				{
					btnentregar.Enabled = false;
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void CargaPedido(int CodPedido)
	{
		pedido = AdmPedido.CargaPedido(Convert.ToInt32(CodPedido));
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

	private void button1_Click(object sender, EventArgs e)
	{
		imprimir(enviados, caso, frmLogin.iCodAlmacen, CodTransDirecta);
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
		txtdespacho.AutoCompleteCustomSource = lista;
	}

	private void dvgtransferencia_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		string nuevav = Convert.ToString(dvgtransferencia.CurrentRow.Cells[cantentregar.Name].Value);
		if (nuevav != "")
		{
			if (dvgtransferencia.RowCount > 0 && e.ColumnIndex == 26)
			{
				double cantidadnueva = Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[cantentregar.Name].Value);
				double cantidadpedido = Convert.ToDouble(dvgtransferencia.CurrentRow.Cells[25].Value);
				if (!(cantidadnueva <= cantidadpedido))
				{
					MessageBox.Show("Cantidad Inválida!! SOBREPASA LA CANTIDAD PENDIENTE DEL PRODUCTO!, SE TOMARÁ LA CANTIDAD ANTERIOR PARA EL PRODUCTO EDITADO", "Requerimiento de Transferencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					dvgtransferencia.CurrentRow.Cells[cantentregar.Name].Value = $"{0:#,##0.00}";
				}
			}
		}
		else
		{
			MessageBox.Show("Ingresar la cantidad que desea requerir");
		}
	}

	private void label9_Click(object sender, EventArgs e)
	{
	}

	private void imprimir(int enviados, int caso, int CodAlmacen, int CodTransDirecta)
	{
		try
		{
			CRTransferenciaDirectaVarios rpt = new CRTransferenciaDirectaVarios();
			frmTransferenciaDirecta frm = new frmTransferenciaDirecta();
			rpt.SetDataSource(ds.RptTransferenciaDirecta(enviados, caso, frmLogin.iCodAlmacen, CodTransDirecta).Tables[0]);
			frm.crvTransferenciaPendiente.ReportSource = rpt;
			frm.ShowDialog();
			rpt.PrintToPrinter(1, collated: false, 0, 0);
			CargaTransferencia();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		clsDetalleTransferencia deta = new clsDetalleTransferencia();
		deta.CodProducto = Convert.ToInt32(fila.Cells[codprod.Name].Value);
		deta.CodTransDir = Convert.ToInt32(CodTransDirecta2);
		deta.CodAlmacenOrigen = Convert.ToInt32(txtCodorigen.Text);
		deta.CodAlmacenDestino = Convert.ToInt32(txtCoddestino.Text);
		deta.UnidadIngresada = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.SerieLote = fila.Cells[serielote.Name].Value.ToString();
		deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		double entregarp = Convert.ToDouble(fila.Cells[cantentregar.Name].Value);
		deta.CantidadPendiente = Convert.ToDouble(fila.Cells[cantpendiente.Name].Value) - entregarp;
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
		deta.CantidadEntrega = Convert.ToDouble(fila.Cells[cantentregar.Name].Value);
		deta.Despachado = txtdespacho.Text;
		deta.CodUser = frmLogin.iCodUser;
		detalle.Add(deta);
	}

	private void btnentregar_Click(object sender, EventArgs e)
	{
		string nuevav = Convert.ToString(dvgtransferencia.CurrentRow.Cells[cantentregar.Name].Value);
		if (nuevav != "")
		{
			if (dvgtransferencia.RowCount > 0)
			{
				RecorreDetalle();
				{
					foreach (clsDetalleTransferencia det in detalle)
					{
						if (admTransferencia.updatedetalle2(det))
						{
							MessageBox.Show("Se registro Correctamente la Entrega del Producto");
							CargaTransferencia();
							imprimir(1, transfer.EstadoTrnas, frmLogin.iCodAlmacen, Convert.ToInt32(transfer.CodTransDir));
							switch (transfer.EstadoTrnas)
							{
							case 0:
								estadotransfer = "PENDIENTE";
								txtestado.Text = estadotransfer;
								btnentregar.Enabled = false;
								break;
							case 1:
								estadotransfer = "APROBADA/SEPARADA";
								txtestado.Text = estadotransfer;
								btnentregar.Enabled = false;
								break;
							case 2:
								estadotransfer = "FACTURADO";
								txtestado.Text = estadotransfer;
								break;
							case 3:
								estadotransfer = "ENTREGADO PARCIAL";
								txtestado.Text = estadotransfer;
								break;
							case 4:
								estadotransfer = "ENTREGADO TOTAL";
								txtestado.Text = estadotransfer;
								btnentregar.Enabled = false;
								break;
							case 5:
								estadotransfer = "ANULADO";
								txtestado.Text = estadotransfer;
								btnentregar.Enabled = false;
								break;
							}
						}
						else
						{
							MessageBox.Show("Hubo un Problema al registrar la entrega de producto");
						}
					}
					return;
				}
			}
			MessageBox.Show("Se necesita agregar datos a la tabla detalle para guardar!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			MessageBox.Show("Ingresar la cantidad que desea requerir");
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
				dvgtransferencia.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(), row[6].ToString(), row[24].ToString(), row[8].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), row[13].ToString(), row[14].ToString(), row[15].ToString(), row[16].ToString(), row[17].ToString(), row[19].ToString(), row[18].ToString(), row[19].ToString(), row[20].ToString(), row[21].ToString(), row[22].ToString(), row[23].ToString(), row[24].ToString(), row[25].ToString());
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		valorpromedio.Visible = false;
	}

	private void btnsalir_Click(object sender, EventArgs e)
	{
		cerrarformulario();
	}

	private void frmRTransferenciaEstado_Load(object sender, EventArgs e)
	{
		CargaTransferencia();
		autocompletar();
	}

	private void cerrarformulario()
	{
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
		this.btnsalir = new System.Windows.Forms.Button();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtestado = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.txtCodTransDir = new System.Windows.Forms.TextBox();
		this.cmbMoneda = new System.Windows.Forms.ComboBox();
		this.label17 = new System.Windows.Forms.Label();
		this.txtcodserie = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.txtorgien = new System.Windows.Forms.TextBox();
		this.txtdestino = new System.Windows.Forms.TextBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtCoddestino = new System.Windows.Forms.TextBox();
		this.txtCodorigen = new System.Windows.Forms.TextBox();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dvgtransferencia = new System.Windows.Forms.DataGridView();
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
		this.cantpendiente = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantentregar = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.txtcliente = new System.Windows.Forms.TextBox();
		this.txtdni = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.txttelefono = new System.Windows.Forms.TextBox();
		this.txtnombre = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.groupBox6 = new System.Windows.Forms.GroupBox();
		this.txtdireccion = new System.Windows.Forms.TextBox();
		this.label8 = new System.Windows.Forms.Label();
		this.groupBox7 = new System.Windows.Forms.GroupBox();
		this.txtvendedor = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.btnentregar = new System.Windows.Forms.Button();
		this.button1 = new System.Windows.Forms.Button();
		this.groupBox8 = new System.Windows.Forms.GroupBox();
		this.txtdespacho = new System.Windows.Forms.TextBox();
		this.label13 = new System.Windows.Forms.Label();
		this.label9 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.groupBox1.SuspendLayout();
		this.groupBox5.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dvgtransferencia).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox4.SuspendLayout();
		this.groupBox6.SuspendLayout();
		this.groupBox7.SuspendLayout();
		this.groupBox8.SuspendLayout();
		base.SuspendLayout();
		this.btnsalir.Location = new System.Drawing.Point(709, 525);
		this.btnsalir.Name = "btnsalir";
		this.btnsalir.Size = new System.Drawing.Size(86, 35);
		this.btnsalir.TabIndex = 0;
		this.btnsalir.Text = "Salir";
		this.btnsalir.UseVisualStyleBackColor = true;
		this.btnsalir.Click += new System.EventHandler(btnsalir_Click);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(17, 21);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 13);
		this.label1.TabIndex = 1;
		this.label1.Text = "Doc. Ref.";
		this.groupBox1.Controls.Add(this.txtestado);
		this.groupBox1.Controls.Add(this.label12);
		this.groupBox1.Controls.Add(this.txtCodTransDir);
		this.groupBox1.Controls.Add(this.cmbMoneda);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Controls.Add(this.txtcodserie);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.label11);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(4, 1);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(794, 60);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Detalle Transferencia";
		this.txtestado.Enabled = false;
		this.txtestado.Location = new System.Drawing.Point(461, 17);
		this.txtestado.Name = "txtestado";
		this.txtestado.Size = new System.Drawing.Size(162, 20);
		this.txtestado.TabIndex = 167;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(386, 21);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(61, 13);
		this.label12.TabIndex = 166;
		this.label12.Text = "ESTADO:";
		this.txtCodTransDir.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodTransDir.Location = new System.Drawing.Point(345, 18);
		this.txtCodTransDir.Name = "txtCodTransDir";
		this.txtCodTransDir.ReadOnly = true;
		this.txtCodTransDir.Size = new System.Drawing.Size(21, 20);
		this.txtCodTransDir.TabIndex = 165;
		this.txtCodTransDir.Visible = false;
		this.cmbMoneda.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmbMoneda.Enabled = false;
		this.cmbMoneda.FormattingEnabled = true;
		this.cmbMoneda.Items.AddRange(new object[2] { "SOLES", "DOLARES" });
		this.cmbMoneda.Location = new System.Drawing.Point(687, 17);
		this.cmbMoneda.Name = "cmbMoneda";
		this.cmbMoneda.Size = new System.Drawing.Size(95, 21);
		this.cmbMoneda.TabIndex = 163;
		this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(629, 20);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(52, 13);
		this.label17.TabIndex = 164;
		this.label17.Text = "Moneda :";
		this.txtcodserie.Location = new System.Drawing.Point(323, 18);
		this.txtcodserie.Name = "txtcodserie";
		this.txtcodserie.Size = new System.Drawing.Size(16, 20);
		this.txtcodserie.TabIndex = 162;
		this.txtcodserie.Visible = false;
		this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumero.Enabled = false;
		this.txtNumero.Location = new System.Drawing.Point(252, 18);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(65, 20);
		this.txtNumero.TabIndex = 161;
		this.txtNumero.Tag = "";
		this.txtSerie.Location = new System.Drawing.Point(176, 18);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(61, 20);
		this.txtSerie.TabIndex = 87;
		this.label11.AutoSize = true;
		this.label11.Location = new System.Drawing.Point(128, 23);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(34, 13);
		this.label11.TabIndex = 88;
		this.label11.Text = "Serie.";
		this.txtDocRef.BackColor = System.Drawing.Color.PeachPuff;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.txtDocRef.Location = new System.Drawing.Point(76, 18);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(28, 20);
		this.txtDocRef.TabIndex = 83;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.groupBox5.Controls.Add(this.txtorgien);
		this.groupBox5.Controls.Add(this.txtdestino);
		this.groupBox5.Controls.Add(this.label7);
		this.groupBox5.Controls.Add(this.label2);
		this.groupBox5.Controls.Add(this.txtCoddestino);
		this.groupBox5.Controls.Add(this.txtCodorigen);
		this.groupBox5.Location = new System.Drawing.Point(4, 67);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(794, 47);
		this.groupBox5.TabIndex = 143;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "REQUERIMIENTO DE TRANSFERENCIA";
		this.txtorgien.Enabled = false;
		this.txtorgien.Location = new System.Drawing.Point(577, 15);
		this.txtorgien.Name = "txtorgien";
		this.txtorgien.Size = new System.Drawing.Size(198, 20);
		this.txtorgien.TabIndex = 144;
		this.txtdestino.Enabled = false;
		this.txtdestino.Location = new System.Drawing.Point(168, 19);
		this.txtdestino.Name = "txtdestino";
		this.txtdestino.Size = new System.Drawing.Size(198, 20);
		this.txtdestino.TabIndex = 6;
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(427, 17);
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
		this.txtCoddestino.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCoddestino.Location = new System.Drawing.Point(168, 19);
		this.txtCoddestino.Name = "txtCoddestino";
		this.txtCoddestino.ReadOnly = true;
		this.txtCoddestino.Size = new System.Drawing.Size(28, 20);
		this.txtCoddestino.TabIndex = 158;
		this.txtCoddestino.Tag = "10";
		this.txtCoddestino.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCodorigen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodorigen.Location = new System.Drawing.Point(577, 15);
		this.txtCodorigen.Name = "txtCodorigen";
		this.txtCodorigen.ReadOnly = true;
		this.txtCodorigen.Size = new System.Drawing.Size(28, 20);
		this.txtCodorigen.TabIndex = 159;
		this.txtCodorigen.Tag = "10";
		this.txtCodorigen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.groupBox2.Controls.Add(this.dvgtransferencia);
		this.groupBox2.Location = new System.Drawing.Point(4, 120);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(794, 179);
		this.groupBox2.TabIndex = 144;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "DETALLE DE REQUERIMIENTO DE PRODUCTO";
		this.dvgtransferencia.AllowUserToAddRows = false;
		this.dvgtransferencia.AllowUserToDeleteRows = false;
		this.dvgtransferencia.ColumnHeadersHeight = 26;
		this.dvgtransferencia.Columns.AddRange(this.coddetalle, this.codprod, this.codigo, this.descripcion, this.codunidad, this.unidad, this.serielote, this.cantidad, this.preciounit, this.importe, this.dscto1, this.dscto2, this.dscto3, this.montodscto, this.valorventa, this.igv, this.precioventa, this.precioreal, this.valoreal, this.coduser, this.fecharegistro, this.codProv, this.valorpromedio, this.precioigv, this.cantpedido, this.cantpendiente, this.cantentregar);
		this.dvgtransferencia.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dvgtransferencia.Location = new System.Drawing.Point(3, 16);
		this.dvgtransferencia.Name = "dvgtransferencia";
		this.dvgtransferencia.RowHeadersVisible = false;
		this.dvgtransferencia.Size = new System.Drawing.Size(788, 160);
		this.dvgtransferencia.TabIndex = 0;
		this.dvgtransferencia.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dvgtransferencia_CellEndEdit);
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
		this.cantpendiente.HeaderText = "Cant. Pendiente";
		this.cantpendiente.Name = "cantpendiente";
		this.cantentregar.HeaderText = "Cant. Entregar";
		this.cantentregar.MinimumWidth = 7;
		this.cantentregar.Name = "cantentregar";
		this.cantentregar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.cantentregar.Width = 110;
		this.groupBox3.Controls.Add(this.txtcliente);
		this.groupBox3.Controls.Add(this.txtdni);
		this.groupBox3.Controls.Add(this.label5);
		this.groupBox3.Controls.Add(this.label3);
		this.groupBox3.Location = new System.Drawing.Point(4, 305);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(791, 45);
		this.groupBox3.TabIndex = 146;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Datos del Cliente";
		this.txtcliente.Enabled = false;
		this.txtcliente.Location = new System.Drawing.Point(389, 16);
		this.txtcliente.Name = "txtcliente";
		this.txtcliente.Size = new System.Drawing.Size(391, 20);
		this.txtcliente.TabIndex = 6;
		this.txtdni.Enabled = false;
		this.txtdni.Location = new System.Drawing.Point(126, 17);
		this.txtdni.Name = "txtdni";
		this.txtdni.Size = new System.Drawing.Size(132, 20);
		this.txtdni.TabIndex = 5;
		this.label5.AutoSize = true;
		this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label5.Location = new System.Drawing.Point(6, 19);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(87, 13);
		this.label5.TabIndex = 4;
		this.label5.Text = "RUC/DNI Nº: ";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(321, 19);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(67, 13);
		this.label3.TabIndex = 3;
		this.label3.Text = "CLIENTE :";
		this.groupBox4.Controls.Add(this.txttelefono);
		this.groupBox4.Controls.Add(this.txtnombre);
		this.groupBox4.Controls.Add(this.label4);
		this.groupBox4.Controls.Add(this.label6);
		this.groupBox4.Location = new System.Drawing.Point(4, 356);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(791, 45);
		this.groupBox4.TabIndex = 147;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Datos Para Comunicacion";
		this.txttelefono.Enabled = false;
		this.txttelefono.Location = new System.Drawing.Point(604, 12);
		this.txttelefono.Name = "txttelefono";
		this.txttelefono.Size = new System.Drawing.Size(168, 20);
		this.txttelefono.TabIndex = 6;
		this.txtnombre.Enabled = false;
		this.txtnombre.Location = new System.Drawing.Point(78, 16);
		this.txtnombre.Name = "txtnombre";
		this.txtnombre.Size = new System.Drawing.Size(411, 20);
		this.txtnombre.TabIndex = 5;
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(6, 19);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(68, 13);
		this.label4.TabIndex = 4;
		this.label4.Text = "NOMBRE: ";
		this.label6.AutoSize = true;
		this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label6.Location = new System.Drawing.Point(518, 16);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(80, 13);
		this.label6.TabIndex = 3;
		this.label6.Text = "TELEFONO :";
		this.groupBox6.Controls.Add(this.txtdireccion);
		this.groupBox6.Controls.Add(this.label8);
		this.groupBox6.Location = new System.Drawing.Point(4, 407);
		this.groupBox6.Name = "groupBox6";
		this.groupBox6.Size = new System.Drawing.Size(527, 45);
		this.groupBox6.TabIndex = 148;
		this.groupBox6.TabStop = false;
		this.groupBox6.Text = "Datos Delivery";
		this.txtdireccion.Enabled = false;
		this.txtdireccion.Location = new System.Drawing.Point(92, 15);
		this.txtdireccion.Name = "txtdireccion";
		this.txtdireccion.Size = new System.Drawing.Size(410, 20);
		this.txtdireccion.TabIndex = 5;
		this.label8.AutoSize = true;
		this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label8.Location = new System.Drawing.Point(6, 20);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(83, 13);
		this.label8.TabIndex = 4;
		this.label8.Text = "DIRECCION: ";
		this.groupBox7.Controls.Add(this.txtvendedor);
		this.groupBox7.Controls.Add(this.label10);
		this.groupBox7.Location = new System.Drawing.Point(4, 458);
		this.groupBox7.Name = "groupBox7";
		this.groupBox7.Size = new System.Drawing.Size(527, 45);
		this.groupBox7.TabIndex = 154;
		this.groupBox7.TabStop = false;
		this.groupBox7.Text = "Autorizado Por:";
		this.txtvendedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
		this.txtvendedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
		this.txtvendedor.Enabled = false;
		this.txtvendedor.Location = new System.Drawing.Point(78, 16);
		this.txtvendedor.Name = "txtvendedor";
		this.txtvendedor.Size = new System.Drawing.Size(230, 20);
		this.txtvendedor.TabIndex = 7;
		this.label10.AutoSize = true;
		this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label10.Location = new System.Drawing.Point(6, 19);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(68, 13);
		this.label10.TabIndex = 5;
		this.label10.Text = "NOMBRE: ";
		this.btnentregar.Enabled = false;
		this.btnentregar.Location = new System.Drawing.Point(598, 525);
		this.btnentregar.Name = "btnentregar";
		this.btnentregar.Size = new System.Drawing.Size(87, 35);
		this.btnentregar.TabIndex = 155;
		this.btnentregar.Text = "Entregar";
		this.btnentregar.UseVisualStyleBackColor = true;
		this.btnentregar.Click += new System.EventHandler(btnentregar_Click);
		this.button1.Location = new System.Drawing.Point(488, 525);
		this.button1.Name = "button1";
		this.button1.Size = new System.Drawing.Size(87, 35);
		this.button1.TabIndex = 158;
		this.button1.Text = "IMPRIMIR";
		this.button1.UseVisualStyleBackColor = true;
		this.button1.Click += new System.EventHandler(button1_Click);
		this.groupBox8.Controls.Add(this.txtdespacho);
		this.groupBox8.Controls.Add(this.label13);
		this.groupBox8.Location = new System.Drawing.Point(4, 510);
		this.groupBox8.Name = "groupBox8";
		this.groupBox8.Size = new System.Drawing.Size(460, 51);
		this.groupBox8.TabIndex = 159;
		this.groupBox8.TabStop = false;
		this.groupBox8.Text = "Despachado Por:";
		this.txtdespacho.Location = new System.Drawing.Point(78, 22);
		this.txtdespacho.Name = "txtdespacho";
		this.txtdespacho.Size = new System.Drawing.Size(361, 20);
		this.txtdespacho.TabIndex = 7;
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label13.Location = new System.Drawing.Point(8, 26);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(68, 13);
		this.label13.TabIndex = 6;
		this.label13.Text = "NOMBRE: ";
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(537, 407);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(105, 13);
		this.label9.TabIndex = 156;
		this.label9.Text = "COMENTARIOS: ";
		this.label9.Click += new System.EventHandler(label9_Click);
		this.txtComentario.Enabled = false;
		this.txtComentario.Location = new System.Drawing.Point(556, 427);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(239, 76);
		this.txtComentario.TabIndex = 157;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(803, 572);
		base.Controls.Add(this.groupBox8);
		base.Controls.Add(this.button1);
		base.Controls.Add(this.txtComentario);
		base.Controls.Add(this.label9);
		base.Controls.Add(this.btnentregar);
		base.Controls.Add(this.groupBox7);
		base.Controls.Add(this.groupBox6);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.btnsalir);
		this.DoubleBuffered = true;
		base.Name = "frmRTransferenciaEstado";
		this.Text = "Transferencia Estado";
		base.Load += new System.EventHandler(frmRTransferenciaEstado_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dvgtransferencia).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox6.ResumeLayout(false);
		this.groupBox6.PerformLayout();
		this.groupBox7.ResumeLayout(false);
		this.groupBox7.PerformLayout();
		this.groupBox8.ResumeLayout(false);
		this.groupBox8.PerformLayout();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
