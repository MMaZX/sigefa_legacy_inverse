using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;
using SIGEFA.Reportes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmReqAlmacen : Form
{
	private const string pr_estado = "NUEVO";

	private clsAdmAlmacen AdmAlm = new clsAdmAlmacen();

	private clsAdmUsuario admUsuario = new clsAdmUsuario();

	private clsAdmProducto AdmPro = new clsAdmProducto();

	public int TipoReq = 1;

	public int Proceso = 0;

	private clsTipoDocumento doc = null;

	private clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();

	private clsAdmTransferencia admTransferencia = new clsAdmTransferencia();

	private clsSerie ser = null;

	private clsAdmSerie admSerie = new clsAdmSerie();

	private clsAdmPropuestaDePedido admPropuestaDePedido = new clsAdmPropuestaDePedido();

	public int codRequerimientoAlmacen = 0;

	private clsRequerimientoAlmacen req_alm = null;

	private clsAdmRequerimientoAlmacen admreqalm = new clsAdmRequerimientoAlmacen();

	private List<clsDetalleRequerimientoAlmacen> detalleOld = new List<clsDetalleRequerimientoAlmacen>();

	public DataTable dataCtdadGenerarTrans = null;

	private List<clsDetalleTransferencia> detalle = new List<clsDetalleTransferencia>();

	private clsTransferencia transfer = new clsTransferencia();

	internal string CodPedido = null;

	internal List<DataGridViewRow> filasSelecccionadasDetalle = new List<DataGridViewRow>();

	internal FrmTPenPedido ventanaListaReqVentas = null;

	public List<clsDetalleNotaSalida> detalleNS = new List<clsDetalleNotaSalida>();

	public List<clsDetalleNotaIngreso> detalleNI = new List<clsDetalleNotaIngreso>();

	private clsTransaccion tran = new clsTransaccion();

	private clsAdmTransaccion AdmTran = new clsAdmTransaccion();

	private bool bandera = true;

	private int codproducto_error = 0;

	private clsAdmNotaSalida admNS = new clsAdmNotaSalida();

	private clsNotaSalida NS = new clsNotaSalida();

	private clsAdmNotaIngreso admNI = new clsAdmNotaIngreso();

	private clsNotaIngreso NI = new clsNotaIngreso();

	private clsPedido pedido = new clsPedido();

	private clsAdmPedido AdmPedido = new clsAdmPedido();

	private bool vieneDeAprobar = false;

	public bool bandEditar = false;

	private object valor_inicial = null;

	private clsValidar valida = new clsValidar();

	internal bool ReqAnulado = false;

	internal bool verificarAnulacion = false;

	internal clsUsuario usuario_click = null;

	private IContainer components = null;

	private DateTimePicker dtpFecha;

	private Label label13;

	private GroupBox groupBox5;

	private ComboBox cmbAlmacenesDespacho;

	private Label label7;

	private Label label2;

	private GroupBox groupBox1;

	private Button btndetalle;

	private Button btnGuarda;

	private Button btnSalir;

	public TextBox txtSerie;

	private TextBox txtNumero;

	private Label label11;

	public TextBox txtDocRef;

	private Label label1;

	private Button btnAprobar;

	private ComboBox cmbAlmacenesSolicitantes;

	private Button btnanular;

	private GroupBox groupBox2;

	private Label label3;

	private TextBox txtEstado;

	private TextBox txtComentarioDespacho;

	private Label label4;

	private TextBox txtComentario;

	private Label label9;

	private Button BtnGenerarTD;

	public RadGridView rgvDetalleRequerimiento;

	private Button btnImprimirTicket;

	private Button btnImprimirPDF;

	private DataGridView dgvTransGeneradas;

	private DataGridViewTextBoxColumn colDocTrans;

	private DataGridViewTextBoxColumn colEstado;

	private DataGridViewTextBoxColumn colCodTransDir;

	private DataGridViewTextBoxColumn colCaso;

	private GroupBox gbContactoAEntregar;

	private TextBox txtTelefonoContacto;

	private Label label5;

	private TextBox txtNombreContacto;

	private Label label6;

	private CheckBox chkDelivery;

	private GroupBox gbDelivery;

	private TextBox txtdireccion;

	private Label label12;

	private ComboBox cmbusuariodesp;

	private Label lblusuariodesp;

	private Label lblusuariosolic;

	private TextBox txtusuariosolic;

	private TextBox txtusuarioaprob;

	private Label lblusuarioaprob;

	private Label lblTipoReq;

	private Button btnGuardarEdicion;

	private Button btnCerrarReq;

	private TextBox txtFacturaVenta;

	private Label lblFacturaVenta;

	public frmReqAlmacen()
	{
		InitializeComponent();
	}

	private void frmReqAlmacen_Load(object sender, EventArgs e)
	{
		if (CodPedido != null)
		{
			pedido = AdmPedido.CargaPedido(Convert.ToInt32(CodPedido));
		}
		rgvDetalleRequerimiento.Columns["colStockAlmacenSolicitante"].IsVisible = false;
		dgvTransGeneradas.AutoGenerateColumns = false;
		doc = admtd.BuscaTipoDocumento("RQAL");
		txtDocRef.Text = doc.Sigla;
		foreach (GridViewDataColumn col in rgvDetalleRequerimiento.Columns)
		{
			col.ReadOnly = true;
			if (col.Name == "colCtdadRequerimiento")
			{
				col.ReadOnly = false;
			}
		}
		gbContactoAEntregar.Visible = TipoReq == 2;
		if (Proceso == 0)
		{
			dtpFecha.Value = DateTime.Today.Date;
			rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].IsVisible = false;
			cargaAlmacenSolicitante();
			cmbusuariodesp.Enabled = false;
			cargaAlmacenDespacho();
			txtEstado.Text = "NUEVO";
			TextBox textBox = txtComentarioDespacho;
			bool visible = (label4.Visible = false);
			textBox.Visible = visible;
			DataGridView dataGridView = dgvTransGeneradas;
			Button button = btnImprimirPDF;
			bool flag2 = (btnImprimirTicket.Visible = false);
			visible = (button.Visible = flag2);
			dataGridView.Visible = visible;
			cambioDeTituloDeColumnas(TipoReq);
			if (TipoReq == 2)
			{
				rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].IsVisible = true;
				rgvDetalleRequerimiento.Columns["colStockAlmacenSolicitante"].IsVisible = true;
				rgvDetalleRequerimiento.ReadOnly = false;
				CheckBox checkBox = chkDelivery;
				visible = (gbDelivery.Visible = true);
				checkBox.Visible = visible;
				Label label = lblusuariosolic;
				TextBox textBox2 = txtusuariosolic;
				Label label2 = lblusuariodesp;
				bool flag6 = (cmbusuariodesp.Visible = true);
				flag2 = (label2.Visible = flag6);
				visible = (textBox2.Visible = flag2);
				label.Visible = visible;
				if (pedido.CodUser > 0)
				{
					clsUsuario vendedor = admUsuario.MuestraUsuarioSinAdmin(pedido.CodUser);
					if (vendedor != null)
					{
						txtusuariosolic.Text = vendedor.Nombre + " " + vendedor.Apellido;
					}
				}
			}
			if (TipoReq == 1)
			{
				cmbAlmacenesSolicitantes.Enabled = false;
			}
			lblTipoReq.Text = "";
		}
		if (Proceso == 1)
		{
			req_alm = admreqalm.CargaRequerimiento(codRequerimientoAlmacen);
			TipoReq = req_alm.Tipo;
			setDatosRequerimientoAlmacen();
			cambioDeTituloDeColumnas(req_alm.Tipo);
			rgvDetalleRequerimiento.DataSource = admreqalm.ListaDetalleRequerimiento(req_alm.Codigo);
			detalleOld = admreqalm.CargaDetalleRequerimientoAlmacen(req_alm.Codigo);
			recargaStockRGV();
			if (frmLogin.iCodAlmacen == req_alm.CodAlmacenDespacho)
			{
				rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].IsVisible = true;
			}
			ponerControlesEnReadOnly(band: true);
			btnAprobar.Visible = true;
			btnGuarda.Visible = false;
			btndetalle.Visible = false;
			if (req_alm.IEstado != 7)
			{
				btnAprobar.Visible = false;
				rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].ReadOnly = true;
				if (req_alm.IEstado != 12)
				{
					rgvDetalleRequerimiento.Columns["colCtdadPendiente"].IsVisible = true;
					BtnGenerarTD.Visible = false;
					DataTable noatend = admreqalm.generarDatosParaFormularioIntermedioTransferencia(req_alm.Codigo);
					if (noatend != null && noatend.Rows.Count > 0)
					{
						BtnGenerarTD.Visible = true;
					}
				}
				else
				{
					btnanular.Visible = false;
				}
			}
			if (req_alm.IEstado == 10)
			{
			}
			DataTable AlmAux = AdmAlm.AlmacenXUbicacion(frmLogin.iCodAlmacen);
			List<int> idsAlmacenes = (from x in AlmAux.AsEnumerable()
				select Convert.ToInt32(x.Field<object>("codalmacen"))).ToList();
			int codAlmacenEnviar = (idsAlmacenes.Contains(req_alm.CodAlmacenSolicitante) ? req_alm.CodAlmacenSolicitante : (idsAlmacenes.Contains(req_alm.CodAlmacenDespacho) ? req_alm.CodAlmacenDespacho : frmLogin.iCodAlmacen));
			dgvTransGeneradas.DataSource = admreqalm.listadoTransferenciasGeneradas(req_alm.Codigo, codAlmacenEnviar);
			if (TipoReq == 2)
			{
				rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].ReadOnly = true;
				rgvDetalleRequerimiento.Columns["colStockAlmacenSolicitante"].IsVisible = true;
				Button button2 = btnGuarda;
				bool visible = (btnanular.Visible = false);
				button2.Visible = visible;
			}
			if (req_alm.Tipo == 1)
			{
				GroupBox groupBox = gbContactoAEntregar;
				GroupBox groupBox2 = gbDelivery;
				bool flag2 = (chkDelivery.Visible = false);
				bool visible = (groupBox2.Visible = flag2);
				groupBox.Visible = visible;
				if (req_alm.IEstado == 7 || req_alm.IEstado == 8)
				{
					verificarAnulacion = true;
				}
			}
			if (bandEditar)
			{
				rgvDetalleRequerimiento.ReadOnly = false;
				rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].ReadOnly = false;
			}
		}
		if (Proceso == 2)
		{
			req_alm = admreqalm.CargaRequerimiento(codRequerimientoAlmacen);
			TipoReq = req_alm.Tipo;
			setDatosRequerimientoAlmacen();
			cambioDeTituloDeColumnas(req_alm.Tipo);
			rgvDetalleRequerimiento.DataSource = admreqalm.ListaDetalleRequerimiento(req_alm.Codigo);
			recargaStockRGV();
			if (frmLogin.iCodAlmacen == req_alm.CodAlmacenDespacho)
			{
				rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].IsVisible = true;
			}
			else
			{
				label4.Visible = false;
			}
			ponerControlesEnReadOnly(band: true);
			txtComentarioDespacho.ReadOnly = true;
			btnAprobar.Visible = false;
			btnGuarda.Visible = false;
			btndetalle.Visible = false;
			if (req_alm.IEstado != 7)
			{
				btnAprobar.Visible = false;
				if (req_alm.IEstado != 12)
				{
					rgvDetalleRequerimiento.Columns["colCtdadPendiente"].IsVisible = true;
					BtnGenerarTD.Visible = false;
				}
				else
				{
					btnanular.Visible = false;
				}
				if (req_alm.IEstado == 12)
				{
					btndetalle.Visible = false;
				}
				if (req_alm.IEstado == 9)
				{
					rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].ReadOnly = false;
				}
			}
			DataTable AlmAux2 = AdmAlm.AlmacenXUbicacion(frmLogin.iCodAlmacen);
			List<int> idsAlmacenes2 = (from x in AlmAux2.AsEnumerable()
				select Convert.ToInt32(x.Field<object>("codalmacen"))).ToList();
			int codAlmacenEnviar2 = (idsAlmacenes2.Contains(req_alm.CodAlmacenSolicitante) ? req_alm.CodAlmacenSolicitante : (idsAlmacenes2.Contains(req_alm.CodAlmacenDespacho) ? req_alm.CodAlmacenDespacho : frmLogin.iCodAlmacen));
			dgvTransGeneradas.DataSource = admreqalm.listadoTransferenciasGeneradas(req_alm.Codigo, codAlmacenEnviar2);
			if (req_alm.Tipo == 1)
			{
				GroupBox groupBox3 = gbContactoAEntregar;
				GroupBox groupBox4 = gbDelivery;
				bool flag2 = (chkDelivery.Visible = false);
				bool visible = (groupBox4.Visible = flag2);
				groupBox3.Visible = visible;
				Label label3 = lblusuariosolic;
				visible = (txtusuariosolic.Visible = true);
				label3.Visible = visible;
			}
		}
		if (verificarAnulacion)
		{
			btnanular.Visible = true;
		}
	}

	public void cambioDeTituloDeColumnas(int tipo)
	{
		if (rgvDetalleRequerimiento.ColumnCount > 0)
		{
			if (tipo == 1)
			{
				rgvDetalleRequerimiento.Columns["colCantidad"].HeaderText += " (Requerimiento)";
				rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].HeaderText += " (Confirmacion)";
			}
			if (tipo == 2)
			{
				rgvDetalleRequerimiento.Columns["colCantidad"].HeaderText += " (Comprobante)";
				rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].HeaderText += " (Requerimiento)";
			}
		}
	}

	public void cargaUsuariosDespacho()
	{
		if (cmbAlmacenesDespacho.SelectedValue != null)
		{
			DataTable aux = admUsuario.ListaUsuariosDespacho(0, Convert.ToInt32(cmbAlmacenesDespacho.SelectedValue));
			cmbusuariodesp.DataSource = aux;
			cmbusuariodesp.DisplayMember = "vendedor";
			cmbusuariodesp.ValueMember = "codUsuario";
			cmbusuariodesp.SelectedIndex = -1;
			cmbusuariodesp.AutoCompleteCustomSource = CargaAutoComplete(aux);
			cmbusuariodesp.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
			cmbusuariodesp.AutoCompleteSource = AutoCompleteSource.CustomSource;
		}
	}

	public static AutoCompleteStringCollection CargaAutoComplete(DataTable dt, string nameFila = "vendedor")
	{
		AutoCompleteStringCollection stringCol = new AutoCompleteStringCollection();
		foreach (DataRow row in dt.Rows)
		{
			stringCol.Add(Convert.ToString(row[nameFila]));
		}
		return stringCol;
	}

	private void setDatosRequerimientoAlmacen()
	{
		dtpFecha.Value = req_alm.FechaRequerimiento;
		cargarCombosConAlmacenes();
		txtComentario.Text = req_alm.ComentarioSolicitante;
		txtComentarioDespacho.Text = req_alm.ComentarioDespacho;
		txtEstado.Text = req_alm.SEstado;
		txtNumero.Text = req_alm.NumDocumento;
		txtSerie.Text = req_alm.NumSerie;
		txtNombreContacto.Text = req_alm.NombreContacto;
		txtTelefonoContacto.Text = req_alm.TelefonoContacto;
		txtdireccion.Text = req_alm.DireccionDelivery;
		ComboBox comboBox = cmbusuariodesp;
		GroupBox groupBox = gbContactoAEntregar;
		CheckBox checkBox = chkDelivery;
		GroupBox groupBox2 = gbDelivery;
		bool flag = (chkDelivery.Checked = req_alm.Delivery == 1);
		bool flag3 = (groupBox2.Visible = flag);
		bool flag5 = (checkBox.Visible = flag3);
		bool flag7 = (groupBox.Visible = flag5);
		comboBox.Enabled = !flag7;
		Button button = btnImprimirPDF;
		flag7 = (btnImprimirTicket.Visible = ((req_alm.Tipo != 2) ? (req_alm.Tipo == 1) : (req_alm.IEstado != 7 && req_alm.IEstado != 12)));
		button.Visible = flag7;
		chkDelivery.Enabled = req_alm.Delivery != 1;
		cmbusuariodesp.Text = req_alm.AutorizadoPor;
		txtusuariosolic.Text = req_alm.UserRegistro;
		txtusuarioaprob.Text = req_alm.UserAprobador;
		if (req_alm.Tipo == 1)
		{
			lblTipoReq.Text = "REQUERIMIENTO PARA REPOSICION DE STOCK";
		}
		else if (req_alm.Tipo == 2)
		{
			lblTipoReq.Text = "REQUERIMIENTO PARA VENTA";
			if (req_alm.CodFacturaVenta > 0)
			{
				Label label = lblFacturaVenta;
				TextBox textBox = txtFacturaVenta;
				flag5 = (txtFacturaVenta.Enabled = true);
				flag7 = (textBox.Visible = flag5);
				label.Visible = flag7;
				txtFacturaVenta.Text = req_alm.TituloFacturaVenta;
			}
		}
		else
		{
			lblTipoReq.Text = "NO RECONOCIDO";
		}
	}

	private void cargarCombosConAlmacenes()
	{
		DataTable aux = AdmAlm.ListaAlmacen2();
		DataTable dtalmacenes = new DataTable();
		DataTable dtalmacenes2 = new DataTable();
		if (aux != null)
		{
			dtalmacenes.Columns.Add("codAlmacen");
			dtalmacenes.Columns.Add("descripcion");
			dtalmacenes2.Columns.Add("codAlmacen");
			dtalmacenes2.Columns.Add("descripcion");
			foreach (DataRow fila in aux.Rows)
			{
				dtalmacenes.Rows.Add(fila.Field<object>("codAlmacen"), fila.Field<object>("nombre"));
				dtalmacenes2.Rows.Add(fila.Field<object>("codAlmacen"), fila.Field<object>("nombre"));
			}
		}
		else
		{
			dtalmacenes = (dtalmacenes2 = aux);
		}
		cmbAlmacenesSolicitantes.DataSource = dtalmacenes;
		cmbAlmacenesSolicitantes.DisplayMember = "descripcion";
		cmbAlmacenesSolicitantes.ValueMember = "codAlmacen";
		cmbAlmacenesSolicitantes.SelectedValue = req_alm.CodAlmacenSolicitante;
		cmbAlmacenesDespacho.DataSource = dtalmacenes2;
		cmbAlmacenesDespacho.DisplayMember = "descripcion";
		cmbAlmacenesDespacho.ValueMember = "codAlmacen";
		cmbAlmacenesDespacho.SelectedValue = req_alm.CodAlmacenDespacho;
		cargaUsuariosDespacho();
	}

	private void ponerControlesEnReadOnly(bool band)
	{
		cmbAlmacenesDespacho.Enabled = !band;
		cmbAlmacenesSolicitantes.Enabled = !band;
		txtComentario.ReadOnly = band;
		dtpFecha.Enabled = !band;
	}

	private void cargaAlmacenDespacho()
	{
		if (cmbAlmacenesSolicitantes.SelectedValue != null)
		{
			DataTable dtAlmacenes = admPropuestaDePedido.listarAlmacenesParaPropReqAlmacen(Convert.ToInt32(cmbAlmacenesSolicitantes.SelectedValue));
			List<DataRow> almacen = (from x in dtAlmacenes.AsEnumerable()
				where x.Field<int>("codAlmacen") == Convert.ToInt32(cmbAlmacenesSolicitantes.SelectedValue)
				select (x)).ToList();
			if (dtAlmacenes.Rows.Count > 1 && almacen.Count == 1)
			{
				dtAlmacenes.Rows.Remove(almacen[0]);
			}
			else
			{
				MessageBox.Show("Ocurrio un error al seleccionar almacen de despacho. Intente seleccionando otro almacen", "Error Almacen Despacho", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			cmbAlmacenesDespacho.DataSource = dtAlmacenes;
			cmbAlmacenesDespacho.DisplayMember = "descripcion";
			cmbAlmacenesDespacho.ValueMember = "codAlmacen";
			if (TipoReq == 1)
			{
				cmbAlmacenesDespacho.SelectedIndex = 0;
			}
			else
			{
				cmbAlmacenesDespacho.SelectedIndex = -1;
			}
		}
	}

	private void cargaAlmacenSolicitante()
	{
		DataTable dtalmacenes;
		if (TipoReq == 2)
		{
			clsAdmAlmacen admalm = new clsAdmAlmacen();
			DataTable almacenesOV = admalm.MuestraAlmacenesEmp2(int.Parse(CodPedido));
			DataTable almacenesConRA = admalm.MuestraAlmacenesConRA(Convert.ToInt32(CodPedido));
			int a = 0;
			List<int> listaCodAlmacenesConRA = (from x in almacenesConRA.AsEnumerable()
				select Convert.ToInt32(x.Field<object>("codAlmacenSolicitante").ToString())).ToList();
			List<DataRow> almacenesDispParaRA = (from x in almacenesOV.AsEnumerable()
				where !listaCodAlmacenesConRA.Contains(Convert.ToInt32(x.Field<object>("codAlmacen").ToString()))
				select x).ToList();
			dtalmacenes = new DataTable();
			dtalmacenes.Columns.Add("codAlmacen");
			dtalmacenes.Columns.Add("descripcion");
			foreach (DataRow fila in almacenesDispParaRA)
			{
				dtalmacenes.Rows.Add(fila.Field<object>("codAlmacen"), fila.Field<object>("nombre"));
			}
		}
		else
		{
			dtalmacenes = admPropuestaDePedido.listarAlmacenesParaPropReqAlmacen(frmLogin.iCodAlmacen);
		}
		cmbAlmacenesSolicitantes.DataSource = dtalmacenes;
		cmbAlmacenesSolicitantes.DisplayMember = "descripcion";
		cmbAlmacenesSolicitantes.ValueMember = "codAlmacen";
		if (TipoReq == 1)
		{
			cmbAlmacenesSolicitantes.SelectedValue = frmLogin.iCodAlmacen;
		}
		else
		{
			cmbAlmacenesSolicitantes.SelectedItem = null;
		}
	}

	private void btndetalle_Click(object sender, EventArgs e)
	{
		try
		{
			if (TipoReq == 1)
			{
				if (Convert.ToInt32(cmbAlmacenesDespacho.SelectedValue) > 0)
				{
					if (Application.OpenForms["frmDetalleGuia"] != null)
					{
						Application.OpenForms["frmDetalleGuia"].Activate();
					}
					else
					{
						frmDetalleGuia form = new frmDetalleGuia();
						form.Procede = 3;
						form.codalmacen = Convert.ToInt32(cmbAlmacenesDespacho.SelectedValue);
						form.Codlista = 1;
						form.Text = "Detalle Requerimiento de " + cmbAlmacenesDespacho.Text;
						form.vieneDeReqAlmacen = true;
						form.ventanaReqAlm = this;
						DialogResult rpta = form.ShowDialog();
						if (rpta == DialogResult.Yes)
						{
							btndetalle.PerformClick();
							btnGuarda.Enabled = true;
							recargaStockRGV();
						}
					}
				}
				else
				{
					MessageBox.Show("Selecciona un almacen de despacho", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					cmbAlmacenesDespacho.Focus();
				}
			}
			if (TipoReq != 2)
			{
				return;
			}
			if (Convert.ToInt32(cmbAlmacenesSolicitantes.SelectedValue) > 0)
			{
				frmRTransferenciaDetalle form2 = new frmRTransferenciaDetalle();
				form2.Proceso = 2;
				form2.CodPedido = CodPedido;
				form2.codalmacenselec = Convert.ToInt32(cmbAlmacenesSolicitantes.SelectedValue);
				form2.ventanaRA = this;
				DialogResult rpta2 = form2.ShowDialog();
				if (rpta2 == DialogResult.Yes)
				{
					if (filasSelecccionadasDetalle.Count > 0)
					{
						asignarElementosSeleccioandos(filasSelecccionadasDetalle);
						recargaStockRGV();
						cmbAlmacenesDespacho.Focus();
						btnAprobar.Visible = false;
						btnanular.Visible = false;
					}
					else
					{
						MessageBox.Show("Ocurrio un error al obtener los elementos seleccionados", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
			}
			else
			{
				MessageBox.Show("Selecciona un almacen solicitante", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				cmbAlmacenesSolicitantes.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	public void asignarElementosSeleccioandos(List<DataGridViewRow> rowSelected)
	{
		try
		{
			DataTable data = (DataTable)rgvDetalleRequerimiento.DataSource;
			foreach (DataGridViewRow row in rowSelected)
			{
				if (data != null)
				{
					List<DataRow> busqueda = (from x in data.AsEnumerable()
						where x.Field<object>("codProducto").ToString() == row.Cells["codprod"].Value.ToString() && x.Field<object>("codUnidad").ToString() == row.Cells["codunidad"].Value.ToString()
						select x).ToList();
					if (busqueda.Count > 0)
					{
						DataRow filaEncontrada = busqueda[0];
						int indice = data.Rows.IndexOf(filaEncontrada);
						filaEncontrada.SetField("cantidad", row.Cells["cantidad"].Value);
						filaEncontrada.SetField("ctdadRequerimiento", (object)0);
						filaEncontrada.SetField<object>("ctdadPendiente", null);
						data.AcceptChanges();
					}
					else
					{
						data.Rows.Add((req_alm != null) ? req_alm.Codigo.ToString() : "", null, row.Cells["codprod"].Value, row.Cells["codigo"].Value, row.Cells["descripcion"].Value, row.Cells["codunidad"].Value, row.Cells["unidad"].Value, row.Cells["cantidad"].Value, 0, null);
					}
				}
				else
				{
					List<GridViewRowInfo> encontrado = Enumerable.Where<GridViewRowInfo>(rgvDetalleRequerimiento.Rows.AsEnumerable(), (Func<GridViewRowInfo, bool>)((GridViewRowInfo x) => x.Cells["colCodProducto"].Value.ToString() == row.Cells["codprod"].Value.ToString() && x.Cells["colCodUnidad"].Value.ToString() == row.Cells["codunidad"].Value.ToString())).ToList();
					if (encontrado.Count <= 0)
					{
						rgvDetalleRequerimiento.Rows.Add(null, row.Cells["codprod"].Value, row.Cells["codigo"].Value, row.Cells["descripcion"].Value, row.Cells["codunidad"].Value, row.Cells["unidad"].Value, row.Cells["cantidad"].Value, null, null, 0, null, (req_alm != null) ? req_alm.Codigo.ToString() : "");
					}
				}
			}
			rgvDetalleRequerimiento.Columns["colCtdadRequerimiento"].ReadOnly = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void recargaStockRGV()
	{
		foreach (GridViewRowInfo fila in rgvDetalleRequerimiento.Rows)
		{
			int codAlmacenDespacho = ((Proceso == 0) ? Convert.ToInt32(cmbAlmacenesDespacho.SelectedValue) : req_alm.CodAlmacenDespacho);
			int codAlmacenSolicitante = ((Proceso == 0) ? Convert.ToInt32(cmbAlmacenesSolicitantes.SelectedValue) : req_alm.CodAlmacenSolicitante);
			clsProducto pro_desp = AdmPro.CargaProductoDetalle(Convert.ToInt32(fila.Cells["colCodProducto"].Value), codAlmacenDespacho, 2, 0, 0);
			clsProducto pro_solic = AdmPro.CargaProductoDetalle(Convert.ToInt32(fila.Cells["colCodProducto"].Value), codAlmacenSolicitante, 2, 0, 0);
			int unidad = Convert.ToInt32(fila.Cells["colCodUnidad"].Value);
			decimal stockdisponiblesegununidad;
			if (unidad != pro_desp.CodUnidadMedida)
			{
				clsUnidadEquivalente undequi = AdmPro.CargaUnidadEquivalente(unidad, pro_desp.CodProducto, 2);
				double factorUE = 0.0;
				if (undequi != null)
				{
					factorUE = Convert.ToDouble(undequi.Factor);
					stockdisponiblesegununidad = pro_desp.StockDisponible / Convert.ToDecimal((factorUE == 0.0) ? 1.0 : factorUE);
					stockdisponiblesegununidad = Math.Round(stockdisponiblesegununidad, 3);
				}
				else
				{
					stockdisponiblesegununidad = pro_desp.StockDisponible;
				}
			}
			else
			{
				stockdisponiblesegununidad = pro_desp.StockDisponible;
			}
			fila.Cells["colStockAlmacenDespacho"].Value = stockdisponiblesegununidad;
			decimal stockdisponiblesegununidad2;
			if (unidad != pro_desp.CodUnidadMedida)
			{
				clsUnidadEquivalente undequi2 = AdmPro.CargaUnidadEquivalente(unidad, pro_solic.CodProducto, 2);
				double factorUE2 = 0.0;
				if (undequi2 != null)
				{
					factorUE2 = Convert.ToDouble(undequi2.Factor);
					stockdisponiblesegununidad2 = pro_solic.StockDisponible / Convert.ToDecimal((factorUE2 == 0.0) ? 1.0 : factorUE2);
					stockdisponiblesegununidad2 = Math.Round(stockdisponiblesegununidad2, 3);
				}
				else
				{
					stockdisponiblesegununidad2 = pro_solic.StockDisponible;
				}
			}
			else
			{
				stockdisponiblesegununidad2 = pro_solic.StockDisponible;
			}
			fila.Cells["colStockAlmacenSolicitante"].Value = stockdisponiblesegununidad2;
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		object val = cmbusuariodesp.SelectedValue;
		if (Proceso == 2 || Proceso == 1)
		{
			Close();
			return;
		}
		object algo = cmbAlmacenesSolicitantes.SelectedValue;
		DialogResult rpta = MessageBox.Show("Esta Seguro de Salir?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (rpta == DialogResult.Yes)
		{
			Close();
		}
		if (ventanaListaReqVentas != null)
		{
			ventanaListaReqVentas.cargarlista();
		}
	}

	private void cmbalmacenesdespacho_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (rgvDetalleRequerimiento.Rows.Count > 0)
		{
			recargaStockRGV();
		}
		if (TipoReq == 2)
		{
			cmbAlmacenesDespacho.Enabled = true;
			cmbusuariodesp.Enabled = true;
			cargaUsuariosDespacho();
		}
	}

	private void btnGuarda_Click(object sender, EventArgs e)
	{
		try
		{
			if (rgvDetalleRequerimiento.Rows.Count == 0)
			{
				MessageBox.Show("Necesita agregar productos para guardar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (Proceso != 0 || TipoReq == 2)
			{
				string rpta1 = verificarCtdadRequerimiento();
				if (rpta1 != "")
				{
					MessageBox.Show(rpta1, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
			}
			if (TipoReq == 2)
			{
				if (cmbusuariodesp.SelectedValue == null)
				{
					MessageBox.Show("Debe definir un usuario despachador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				if (!validarContacto() || !validarDelivery())
				{
					return;
				}
			}
			if (Proceso == 0)
			{
				if (txtComentario.Text.Trim() == "")
				{
					DialogResult rpta2 = MessageBox.Show("Continuar Guardar sin comentario?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
					if (rpta2 == DialogResult.No)
					{
						txtComentario.Focus();
						return;
					}
				}
				ser = admSerie.CargaSerieEmpresa(Convert.ToInt32(cmbAlmacenesSolicitantes.SelectedValue), doc.CodTipoDocumento);
				if (ser != null)
				{
					req_alm = getDatosRequerimientoAlmacen();
					if (admreqalm.insert(req_alm, req_alm.ListadoDetalle))
					{
						MessageBox.Show("Requerimiento de Almacen Guardado Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					if (TipoReq == 1)
					{
						recargarPagina();
					}
					else
					{
						ventanaListaReqVentas.cargarlista();
						codRequerimientoAlmacen = req_alm.Codigo;
						base.DialogResult = DialogResult.Yes;
						Close();
					}
				}
				else
				{
					MessageBox.Show("No se a registrado serie para este documento, en el almacen solicitante", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			if (Proceso != 1)
			{
				return;
			}
			if (TipoReq == 1)
			{
				if (txtComentarioDespacho.Text.Trim() == "")
				{
					MessageBox.Show("Imposible guardar sin comentario de despacho", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				return;
			}
			int a = 1;
			req_alm = getDatosRequerimientoAlmacen();
			if (admreqalm.update(req_alm, req_alm.ListadoDetalle, detalleOld))
			{
				MessageBox.Show("Requerimiento de Almacen Actualizado Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Button button = btnAprobar;
				bool visible = (btnanular.Visible = true);
				button.Visible = visible;
				recargarPagina(Proceso, bandEditar: true);
			}
			else
			{
				MessageBox.Show("Ocurrio un error al actualizar el requerimiento de almacen", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Encontrado", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private bool validarContacto()
	{
		bool band = true;
		string cadena = "";
		if (txtNombreContacto.Text == "")
		{
			band = false;
			cadena = "Debe ingresar el nombre de contacto";
		}
		else if (txtTelefonoContacto.Text.Length != 9)
		{
			band = false;
			cadena = "Debe indicar un numero de contacto";
		}
		if (!band)
		{
			MessageBox.Show(cadena, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return band;
	}

	private bool validarDelivery()
	{
		bool band = true;
		string cadena = "";
		if (chkDelivery.Checked && txtdireccion.Text.Trim() == "")
		{
			band = false;
			cadena = "Debe definir una direccion de delivery";
			txtdireccion.Focus();
		}
		if (!band)
		{
			MessageBox.Show(cadena, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return band;
	}

	private void recargarPagina(int modo = 2, bool bandEditar = false)
	{
		frmReqAlmacen form = new frmReqAlmacen();
		form.MdiParent = base.MdiParent;
		form.codRequerimientoAlmacen = req_alm.Codigo;
		form.Proceso = modo;
		form.bandEditar = bandEditar;
		form.ventanaListaReqVentas = ventanaListaReqVentas;
		form.WindowState = FormWindowState.Maximized;
		form.Show();
		Close();
	}

	private clsRequerimientoAlmacen getDatosRequerimientoAlmacen()
	{
		clsRequerimientoAlmacen nuevo = new clsRequerimientoAlmacen();
		if (Proceso == 0)
		{
			nuevo.CodTipoDocumento = doc.CodTipoDocumento;
			nuevo.NumDocumento = ser.Numeracion.ToString().PadLeft(8, '0');
			nuevo.CodSerie = ser.CodSerie;
			nuevo.NumSerie = ser.Serie;
			nuevo.CodAlmacenRegistro = frmLogin.iCodAlmacen;
			nuevo.CodUserRegistro = frmLogin.iCodUser;
			nuevo.FechaRegistro = DateTime.Now;
			nuevo.CodAlmacenSolicitante = Convert.ToInt32(cmbAlmacenesSolicitantes.SelectedValue);
			nuevo.CodAlmacenDespacho = Convert.ToInt32(cmbAlmacenesDespacho.SelectedValue);
			nuevo.FechaRequerimiento = dtpFecha.Value;
			nuevo.IEstado = 7;
			nuevo.ComentarioSolicitante = txtComentario.Text;
			nuevo.Tipo = TipoReq;
			nuevo.NombreContacto = txtNombreContacto.Text;
			nuevo.TelefonoContacto = txtTelefonoContacto.Text;
			nuevo.AutorizadoPor = cmbusuariodesp.Text.ToString();
			nuevo.Delivery = (chkDelivery.Checked ? 1 : 0);
			nuevo.DireccionDelivery = txtdireccion.Text;
		}
		if (Proceso == 1)
		{
			nuevo = req_alm;
			nuevo.FechaModifico = DateTime.Now;
			nuevo.CodUserModifico = frmLogin.iCodUser;
			nuevo.ComentarioSolicitante = txtComentario.Text;
			nuevo.ComentarioDespacho = txtComentarioDespacho.Text;
			nuevo.AutorizadoPor = cmbusuariodesp.Text.ToString();
			nuevo.Delivery = (chkDelivery.Checked ? 1 : 0);
			nuevo.DireccionDelivery = txtdireccion.Text;
			nuevo.NombreContacto = txtNombreContacto.Text;
			nuevo.TelefonoContacto = txtTelefonoContacto.Text;
		}
		if (TipoReq == 2)
		{
			nuevo.CodPedidoVenta = CodPedido;
			nuevo.CodUserRegistro = pedido.CodUser;
		}
		else
		{
			nuevo.CodPedidoVenta = null;
		}
		nuevo.ListadoDetalle = convertirRGVaListado();
		return nuevo;
	}

	private List<clsDetalleRequerimientoAlmacen> convertirRGVaListado()
	{
		List<clsDetalleRequerimientoAlmacen> listado = new List<clsDetalleRequerimientoAlmacen>();
		foreach (GridViewRowInfo fila in rgvDetalleRequerimiento.Rows)
		{
			clsDetalleRequerimientoAlmacen nuevo = new clsDetalleRequerimientoAlmacen();
			nuevo.Codigo = Convert.ToInt32((fila.Cells["colCodDetalle"].Value == "" || fila.Cells["colCodDetalle"].Value == DBNull.Value) ? ((object)0) : fila.Cells["colCodDetalle"].Value);
			nuevo.CodProducto = Convert.ToInt32(fila.Cells["colCodProducto"].Value);
			nuevo.CodUnidad = Convert.ToInt32(fila.Cells["colCodUnidad"].Value);
			decimal cantidad = (nuevo.CantidadPedida = Convert.ToDecimal(fila.Cells["colCantidad"].Value));
			nuevo.Cantidad = cantidad;
			double aux_conv = Convert.ToDouble(fila.Cells["colCtdadRequerimiento"].Value ?? ((object)0));
			nuevo.CantidadConfirmada = Convert.ToDecimal(aux_conv);
			bool band = true;
			decimal ctdadPendiente;
			if (Proceso == 0)
			{
				ctdadPendiente = default(decimal);
			}
			else if (Proceso == 1)
			{
				ctdadPendiente = Convert.ToDecimal(fila.Cells["colCtdadRequerimiento"].Value ?? ((object)0));
				if (TipoReq == 2 && !vieneDeAprobar)
				{
					band = false;
				}
			}
			else
			{
				ctdadPendiente = Convert.ToDecimal(fila.Cells["colCtdadPendiente"].Value ?? ((object)0));
				band = false;
			}
			nuevo.CantidadPendiente = ctdadPendiente;
			if (band)
			{
				nuevo.CantidadPendienteAprobada = ctdadPendiente;
			}
			listado.Add(nuevo);
		}
		return listado;
	}

	private void btnanular_Click(object sender, EventArgs e)
	{
		if (TipoReq == 1 && req_alm.IEstado != 7 && req_alm.IEstado != 8)
		{
			MessageBox.Show("No puede anular este requerimiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		DialogResult rpta = MessageBox.Show("Esta seguro de Anular este Requerimiento?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (rpta == DialogResult.No)
		{
			return;
		}
		if (txtComentario.Text.Trim() == "")
		{
			rpta = MessageBox.Show("Esta seguro de anular sin un comentario?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rpta == DialogResult.No)
			{
				return;
			}
		}
		try
		{
			if (req_alm.IEstado != 11)
			{
				if (TipoReq == 1)
				{
					GroupBox groupBox = gbContactoAEntregar;
					GroupBox groupBox2 = gbDelivery;
					bool flag = (chkDelivery.Visible = false);
					bool visible = (groupBox2.Visible = flag);
					groupBox.Visible = visible;
					dgvTransGeneradas.DataSource = admreqalm.listadoTransferenciasGeneradas(req_alm.Codigo, frmLogin.iCodAlmacen);
					admreqalm.anular(req_alm.Codigo, frmLogin.iCodUser);
					DataTable listadoCodTrans = admreqalm.cargaTransferenciasPendientes(req_alm.Codigo);
					clsAdmTransferencia admTransferencia = new clsAdmTransferencia();
					if (listadoCodTrans != null)
					{
						foreach (DataRow fila in listadoCodTrans.Rows)
						{
							int codTransDir = Convert.ToInt32(fila.Field<object>(0));
							admTransferencia.rechazado(codTransDir, "Transferencia Anulada por Requerimiento: " + req_alm.NumSerie + "-" + req_alm.NumDocumento);
						}
					}
					req_alm.ListadoDetalle = admreqalm.CargaDetalleRequerimientoAlmacen(req_alm.Codigo);
					foreach (clsDetalleRequerimientoAlmacen item in req_alm.ListadoDetalle)
					{
						admreqalm.retornarStock(req_alm.CodAlmacenDespacho, item.CodProducto, item.CodUnidad, item.CantidadPendienteAprobada, modificarStockActual: false, item.Codigo);
					}
					MessageBox.Show("Requerimiento de Almacen Anulado", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					req_alm = admreqalm.CargaRequerimiento(codRequerimientoAlmacen);
					setDatosRequerimientoAlmacen();
				}
				if (TipoReq == 2 && ventanaListaReqVentas != null)
				{
					ventanaListaReqVentas.cargarlista();
				}
				BtnGenerarTD.Visible = false;
				btnAprobar.Visible = false;
				btnanular.Visible = false;
			}
			else
			{
				MessageBox.Show("no definido");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void cmbalmacenessolicitantes_SelectionChangeCommitted(object sender, EventArgs e)
	{
		cargaAlmacenDespacho();
		if (TipoReq == 2)
		{
			cmbAlmacenesSolicitantes.Enabled = false;
		}
	}

	private void txtComentarioDespacho_TextChanged(object sender, EventArgs e)
	{
		if (req_alm.IEstado == 7)
		{
			btnGuarda.Visible = txtComentarioDespacho.Text.Trim() != "";
		}
	}

	private void BtnGenerarTD_Click(object sender, EventArgs e)
	{
		try
		{
			frmInterReqAlmTransf form = new frmInterReqAlmTransf();
			form.data = admreqalm.generarDatosParaFormularioIntermedioTransferencia(req_alm.Codigo);
			form.ventana_ra = this;
			form.ShowDialog();
			if (form.DialogResult != DialogResult.OK)
			{
				return;
			}
			clsSerie ser2 = admSerie.BuscaSeriexDocumento(14, req_alm.CodAlmacenDespacho);
			transfer.codReqAlm = req_alm.Codigo;
			transfer.CodAlmacenOrigen = req_alm.CodAlmacenDespacho;
			transfer.CodAlmacenDestino = req_alm.CodAlmacenSolicitante;
			transfer.CodTipoDocumento = 14;
			transfer.FechaEnvio = DateTime.Now;
			transfer.FechaEntrega = DateTime.Now;
			transfer.FormaPago = 0;
			transfer.FechaPago = DateTime.Now.Date;
			transfer.CodListaPrecio = 0;
			string comentario = ((req_alm.ComentarioSolicitante == "") ? "" : ("Comentario de Almacen Destino:\n" + req_alm.ComentarioSolicitante)) + "\n" + ((req_alm.ComentarioDespacho == "") ? "" : ("\nComentario de Almacen Origen:\n" + req_alm.ComentarioDespacho));
			transfer.Comentario = comentario;
			transfer.DescripcionRechazo = "";
			transfer.CodUser = frmLogin.iCodUser;
			transfer.Estado = 1;
			transfer.Codserie = ser2.CodSerie;
			transfer.Serie = ser2.Serie;
			transfer.Numerodocumento = ser2.Numeracion.ToString().PadLeft(6, '0');
			transfer.Moneda = 1;
			obtenerDetalleParaTransferencia();
			if (detalle.Count <= 0 || !admTransferencia.insert(transfer))
			{
				return;
			}
			admreqalm.registrarTransferencia(req_alm.Codigo, Convert.ToInt32(transfer.CodTransDir), frmLogin.iCodUser);
			foreach (clsDetalleTransferencia det in detalle)
			{
				det.CodTransDir = Convert.ToInt32(transfer.CodTransDir);
				admTransferencia.insertdetalle(det);
			}
			admreqalm.actualizaCantidadPendienteReqAlmacen(req_alm.Codigo);
			admreqalm.actualizaEstadoReqAlmacen(req_alm.Codigo, 10);
			MessageBox.Show("Transferencia Directa Generada Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			recargarPagina(1);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void apruebaTransferencia(clsTransferencia transfer)
	{
		try
		{
			clsTipoDocumento doc = new clsTipoDocumento();
			clsAdmTipoDocumento admtd = new clsAdmTipoDocumento();
			tran = AdmTran.MuestraTransaccion(15);
			doc = admtd.BuscaTipoDocumento("TD");
			NS.NumDoc = transfer.Numerodocumento;
			NS.CodAlmacen = transfer.CodAlmacenOrigen;
			NS.CodCliente = 0;
			NS.CodNotaCredito = 0;
			NS.CodSucursal = transfer.CodAlmacenOrigen;
			NS.RazonSocialCliente = "";
			NS.CodAutorizado = 0;
			NS.FechaSalida = DateTime.Now.Date;
			NS.DocumentoReferencia = 0;
			NS.CodTipoTransaccion = tran.CodTransaccion;
			NS.CodTipoDocumento = doc.CodTipoDocumento;
			NS.CodSerie = transfer.Codserie;
			NS.Serie = transfer.Serie;
			NS.Moneda = 1;
			NS.FechaSalida = DateTime.Now.Date;
			NS.FormaPago = 0;
			NS.FechaPago = DateTime.Now.Date;
			NS.Comentario = "";
			NS.MontoBruto = Convert.ToDouble(transfer.MontoBruto);
			NS.MontoDscto = 0.0;
			NS.Igv = 0.0;
			NS.Total = Convert.ToDouble(transfer.Total);
			NS.CodUser = transfer.CodUser;
			NS.Estado = 1;
			NS.Codtransferencia = Convert.ToInt32(transfer.CodTransDir);
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
					MessageBox.Show("Hubo un error al guardar la transferencia ", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			if (bandera)
			{
				NI.NumDoc = NS.NumDoc;
				NI.CodAlmacen = transfer.CodAlmacenDestino;
				NI.CodAutorizado = 0;
				NI.CodReferencia = 0;
				NI.CodTipoTransaccion = tran.CodTransaccion;
				NI.CodTipoDocumento = doc.CodTipoDocumento;
				NI.CodSerie = NS.CodSerie;
				NI.Serie = NS.Serie;
				NI.Moneda = 1;
				NI.FechaIngreso = DateTime.Now.Date;
				NI.FormaPago = 0;
				NI.FechaPago = DateTime.Now.Date;
				NI.Comentario = "";
				NS.MontoBruto = Convert.ToDouble(transfer.MontoBruto);
				NI.MontoDscto = 0.0;
				NI.Igv = 0.0;
				NI.Total = Convert.ToDouble(transfer.Total);
				NI.CodUser = transfer.CodUser;
				NI.Estado = 1;
				NI.Codtransferencia = Convert.ToInt32(transfer.CodTransDir);
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
					}
					else
					{
						Transaction.Current.Rollback();
						Scope2.Dispose();
						MessageBox.Show("Hubo un error al guardar la transferencia ", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
				}
				if (bandera)
				{
					admTransferencia.Aprobar(Convert.ToInt32(transfer.CodTransDir));
				}
				else
				{
					MessageBox.Show("Hubo un error al guardar la transferencia ", "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
			else
			{
				MessageBox.Show("No hay stock suficiente del producto codigo: " + codproducto_error, "Transferencia Directa", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void RecorreDetalleNI()
	{
		detalleNI.Clear();
		if (detalle.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalle)
		{
			añadedetalleNI(row);
		}
	}

	private void añadedetalleNI(clsDetalleTransferencia fila)
	{
		clsDetalleNotaIngreso deta1 = new clsDetalleNotaIngreso();
		deta1.CodProducto = fila.CodProducto;
		deta1.CodNotaIngreso = Convert.ToInt32(NI.CodNotaIngreso);
		deta1.CodAlmacen = transfer.CodAlmacenDestino;
		deta1.UnidadIngresada = fila.UnidadIngresada;
		deta1.SerieLote = "0";
		deta1.Cantidad = fila.Cantidad;
		deta1.PrecioUnitario = fila.PrecioUnitario;
		deta1.Subtotal = fila.Subtotal;
		deta1.Descuento1 = fila.Descuento1;
		deta1.Descuento2 = fila.Descuento2;
		deta1.Descuento3 = fila.Descuento3;
		deta1.MontoDescuento = 0.0;
		deta1.ValoReal = deta1.PrecioUnitario / 1.18;
		deta1.Igv = deta1.ValoReal * 0.18;
		deta1.PrecioReal = deta1.ValoReal * 1.18;
		deta1.CodUser = frmLogin.iCodUser;
		deta1.Importe = deta1.PrecioUnitario * deta1.Cantidad;
		deta1.Subtotal = deta1.Importe;
		deta1.PrecioReal = fila.PrecioUnitario;
		deta1.ValoReal = fila.ValoReal;
		deta1.CodProveedor = 0;
		deta1.FechaIngreso = dtpFecha.Value;
		detalleNI.Add(deta1);
	}

	private void RecorreDetalleNS()
	{
		detalleNS.Clear();
		if (detalle.Count <= 0)
		{
			return;
		}
		foreach (clsDetalleTransferencia row in detalle)
		{
			añadedetalleNS(row);
		}
	}

	private void añadedetalleNS(clsDetalleTransferencia fila)
	{
		clsDetalleNotaSalida deta = new clsDetalleNotaSalida();
		deta.CodProducto = fila.CodProducto;
		deta.CodNotaSalida = Convert.ToInt32(NS.CodNotaSalida);
		deta.CodAlmacen = transfer.CodAlmacenOrigen;
		deta.UnidadIngresada = fila.UnidadIngresada;
		deta.SerieLote = "0";
		deta.Cantidad = fila.Cantidad;
		deta.PrecioUnitario = fila.PrecioUnitario;
		deta.Subtotal = fila.Subtotal;
		deta.Descuento1 = fila.Descuento1;
		deta.Descuento2 = fila.Descuento2;
		deta.Descuento3 = fila.Descuento3;
		deta.Igv = fila.Igv;
		deta.Importe = fila.PrecioVenta;
		deta.PrecioReal = fila.PrecioReal;
		deta.ValoReal = fila.ValoReal;
		deta.ValorPromedio = Convert.ToDouble(fila.Valorpromedio);
		deta.CodUser = frmLogin.iCodUser;
		detalleNS.Add(deta);
	}

	private void disminuirCantidadesPendientes()
	{
		foreach (clsDetalleTransferencia item in detalle)
		{
			List<GridViewRowInfo> encontrado = Enumerable.Where<GridViewRowInfo>(rgvDetalleRequerimiento.Rows.AsEnumerable(), (Func<GridViewRowInfo, bool>)((GridViewRowInfo x) => item.CodProducto == Convert.ToInt32(x.Cells["colCodProducto"].Value) && item.CodUnidad == Convert.ToInt32(x.Cells["colCodUnidad"].Value))).ToList();
			if (encontrado.Count > 0)
			{
				encontrado[0].Cells["colCtdadPendiente"].Value = Convert.ToDouble(encontrado[0].Cells["colCtdadPendiente"].Value) - item.Cantidad;
			}
		}
	}

	private void obtenerDetalleParaTransferencia()
	{
		detalle.Clear();
		if (TipoReq == 1 && dataCtdadGenerarTrans != null)
		{
			foreach (DataRow row in dataCtdadGenerarTrans.Rows)
			{
				if (Convert.ToDouble(row.Field<object>("cantidad") ?? ((object)0)) > 0.0)
				{
					clsDetalleTransferencia deta = new clsDetalleTransferencia();
					deta = obtenerDetalleGrillaTransferencia(row);
					detalle.Add(deta);
					transfer.MontoBruto += Convert.ToDecimal(deta.Importe);
					transfer.MontoDscto += Convert.ToDecimal(deta.MontoDescuento);
					transfer.Igv += Convert.ToDecimal(deta.Igv);
					transfer.Total += Convert.ToDecimal(deta.Subtotal);
				}
			}
		}
		if (TipoReq != 2)
		{
			return;
		}
		foreach (GridViewRowInfo filaRGV in rgvDetalleRequerimiento.Rows)
		{
			if (Convert.ToDouble(filaRGV.Cells["colCtdadRequerimiento"].Value ?? ((object)0)) > 0.0)
			{
				clsDetalleTransferencia deta2 = new clsDetalleTransferencia();
				double _cantidad = Convert.ToDouble(filaRGV.Cells["colCtdadRequerimiento"].Value ?? ((object)0));
				filaRGV.Cells["colCtdadPendiente"].Value = Convert.ToDecimal(filaRGV.Cells["colCtdadPendiente"].Value) - Convert.ToDecimal(_cantidad);
				deta2.codDetalleReqAlm = Convert.ToInt32(filaRGV.Cells["colCodDetalle"].Value);
				deta2.CodProducto = Convert.ToInt32(filaRGV.Cells["colCodProducto"].Value);
				deta2.CodAlmacenOrigen = req_alm.CodAlmacenDespacho;
				deta2.CodAlmacenDestino = req_alm.CodAlmacenSolicitante;
				deta2.UnidadIngresada = Convert.ToInt32(filaRGV.Cells["colCodUnidad"].Value);
				deta2.SerieLote = "";
				deta2.Cantidad = _cantidad;
				deta2.CantidadPendiente = _cantidad;
				double ult_pre = (deta2.PrecioUnitario = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(deta2.CodProducto, deta2.UnidadIngresada, 0)));
				deta2.Subtotal = ult_pre * deta2.Cantidad;
				deta2.Descuento1 = 0.0;
				deta2.Descuento2 = 0.0;
				deta2.Descuento3 = 0.0;
				deta2.MontoDescuento = 0.0;
				bool flag = true;
				deta2.PrecioVenta = deta2.Subtotal;
				double factorigv = Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
				deta2.ValorVenta = deta2.PrecioVenta / factorigv;
				deta2.PrecioReal = deta2.PrecioVenta / deta2.Cantidad;
				deta2.ValoReal = deta2.ValorVenta / deta2.Cantidad;
				deta2.Igv = deta2.PrecioVenta - deta2.ValorVenta;
				deta2.Importe = deta2.Subtotal;
				deta2.Valorpromedio = Convert.ToDecimal(deta2.PrecioUnitario);
				deta2.CodUser = frmLogin.iCodUser;
				detalle.Add(deta2);
				transfer.MontoBruto += Convert.ToDecimal(deta2.Importe);
				transfer.MontoDscto += Convert.ToDecimal(deta2.MontoDescuento);
				transfer.Igv += Convert.ToDecimal(deta2.Igv);
				transfer.Total += Convert.ToDecimal(deta2.Subtotal);
			}
		}
	}

	private clsDetalleTransferencia obtenerDetalleGrillaTransferencia(DataRow fila)
	{
		clsDetalleTransferencia deta = new clsDetalleTransferencia();
		List<GridViewRowInfo> encontrado = Enumerable.Where<GridViewRowInfo>(rgvDetalleRequerimiento.Rows.AsEnumerable(), (Func<GridViewRowInfo, bool>)((GridViewRowInfo x) => Convert.ToInt32(fila.Field<object>("codDetalle")) == Convert.ToInt32(x.Cells["colCodDetalle"].Value))).ToList();
		if (encontrado.Count > 0)
		{
			GridViewRowInfo filaRGV = encontrado[0];
			double _cantidad = Convert.ToDouble(fila.Field<object>("cantidad"));
			filaRGV.Cells["colCtdadPendiente"].Value = Convert.ToDecimal(filaRGV.Cells["colCtdadPendiente"].Value) - Convert.ToDecimal(_cantidad);
			deta.codDetalleReqAlm = Convert.ToInt32(filaRGV.Cells["colCodDetalle"].Value);
			deta.CodProducto = Convert.ToInt32(filaRGV.Cells["colCodProducto"].Value);
			deta.CodAlmacenOrigen = req_alm.CodAlmacenDespacho;
			deta.CodAlmacenDestino = req_alm.CodAlmacenSolicitante;
			deta.UnidadIngresada = Convert.ToInt32(filaRGV.Cells["colCodUnidad"].Value);
			deta.SerieLote = "";
			deta.Cantidad = _cantidad;
			deta.CantidadPendiente = _cantidad;
			double ult_pre = (deta.PrecioUnitario = Convert.ToDouble(AdmPro.UltimoPrecioCompraProducto(deta.CodProducto, deta.UnidadIngresada, 0)));
			deta.Subtotal = ult_pre * deta.Cantidad;
			deta.Descuento1 = 0.0;
			deta.Descuento2 = 0.0;
			deta.Descuento3 = 0.0;
			deta.MontoDescuento = 0.0;
			bool flag = true;
			deta.PrecioVenta = deta.Subtotal;
			double factorigv = Convert.ToDouble(frmLogin.Configuracion.IGV / 100.0 + 1.0);
			deta.ValorVenta = deta.PrecioVenta / factorigv;
			deta.PrecioReal = deta.PrecioVenta / deta.Cantidad;
			deta.ValoReal = deta.ValorVenta / deta.Cantidad;
			deta.Igv = deta.PrecioVenta - deta.ValorVenta;
			deta.Importe = deta.Subtotal;
			deta.Valorpromedio = Convert.ToDecimal(deta.PrecioUnitario);
			deta.CodUser = frmLogin.iCodUser;
			return deta;
		}
		throw new Exception("Ocurrio un error al tratar de generar el detalle de la trasnferencia.\nIntente nuevamente.");
	}

	private void rgvDetalleRequerimiento_CellDoubleClick(object sender, GridViewCellEventArgs e)
	{
		if (rgvDetalleRequerimiento.Rows.Count > 1)
		{
			if (txtEstado.Text == "NUEVO" && e.Column.Name == "colRefProducto")
			{
				rgvDetalleRequerimiento.Rows.Remove(e.Row);
			}
			if (req_alm != null && req_alm.Tipo == 2 && req_alm.SEstado == "Pendiente" && e.Column.Name == "colRefProducto")
			{
				rgvDetalleRequerimiento.Rows.Remove(e.Row);
			}
		}
	}

	private void btnAprobar_Click(object sender, EventArgs e)
	{
		try
		{
			if (TipoReq == 2 && cmbusuariodesp.SelectedValue == null)
			{
				cmbusuariodesp.Focus();
				MessageBox.Show("Debe definir un usuario autorizador o despachador", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			string rpta = verificarCtdadRequerimiento();
			if (rpta != "")
			{
				MessageBox.Show(rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
			clsSerie ser2 = admSerie.BuscaSeriexDocumento(14, req_alm.CodAlmacenDespacho);
			if (TipoReq == 2 && ser2 == null)
			{
				MessageBox.Show("No existe serie creada para transferencia en el almacen despachador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (TipoReq == 1)
			{
				admreqalm.aprobar(req_alm.Codigo, frmLogin.iCodUser);
				req_alm = admreqalm.CargaRequerimiento(codRequerimientoAlmacen);
				req_alm.ComentarioDespacho = ((txtComentarioDespacho.Text.Trim() != "") ? (((req_alm.ComentarioDespacho != "") ? (req_alm.ComentarioDespacho + "\n") : "") + txtComentarioDespacho.Text) : req_alm.ComentarioDespacho);
				req_alm.ListadoDetalle = convertirRGVaListado();
				admreqalm.update(req_alm, req_alm.ListadoDetalle, detalleOld);
				foreach (clsDetalleRequerimientoAlmacen item in req_alm.ListadoDetalle)
				{
					admreqalm.separarStock(req_alm.CodAlmacenDespacho, item.CodProducto, item.CodUnidad, item.CantidadConfirmada, item.Codigo);
				}
			}
			if (TipoReq == 2)
			{
				transfer = new clsTransferencia();
				transfer.codReqAlm = req_alm.Codigo;
				transfer.CodAlmacenOrigen = req_alm.CodAlmacenDespacho;
				transfer.CodAlmacenDestino = req_alm.CodAlmacenSolicitante;
				transfer.CodTipoDocumento = 14;
				transfer.FechaEnvio = DateTime.Now;
				transfer.FechaEntrega = DateTime.Now;
				transfer.FormaPago = 0;
				transfer.FechaPago = DateTime.Now.Date;
				transfer.CodListaPrecio = 0;
				string comentario = "Req Almacen para Ventas generado de O.V: " + CodPedido;
				transfer.Comentario = comentario;
				transfer.DescripcionRechazo = "";
				transfer.CodUser = frmLogin.iCodUser;
				transfer.Estado = 1;
				transfer.Codserie = ser2.CodSerie;
				transfer.Serie = ser2.Serie;
				transfer.Numerodocumento = ser2.Numeracion.ToString().PadLeft(6, '0');
				transfer.Moneda = 1;
				obtenerDetalleParaTransferencia();
				if (detalle.Count > 0)
				{
					admreqalm.aprobar(req_alm.Codigo, frmLogin.iCodUser);
					admreqalm.asignarAutorizador(req_alm.Codigo, Convert.ToInt32(cmbusuariodesp.SelectedValue));
					req_alm = admreqalm.CargaRequerimiento(codRequerimientoAlmacen);
					req_alm.ComentarioDespacho = ((txtComentarioDespacho.Text.Trim() != "") ? (((req_alm.ComentarioDespacho != "") ? (req_alm.ComentarioDespacho + "\n") : "") + txtComentarioDespacho.Text) : req_alm.ComentarioDespacho);
					vieneDeAprobar = true;
					req_alm.ListadoDetalle = convertirRGVaListado();
					admreqalm.update(req_alm, req_alm.ListadoDetalle, detalleOld);
					foreach (clsDetalleRequerimientoAlmacen item2 in req_alm.ListadoDetalle)
					{
						admreqalm.separarStock(req_alm.CodAlmacenDespacho, item2.CodProducto, item2.CodUnidad, item2.CantidadConfirmada, item2.Codigo);
					}
					if (admTransferencia.insert(transfer))
					{
						admreqalm.registrarTransferencia(req_alm.Codigo, Convert.ToInt32(transfer.CodTransDir), frmLogin.iCodUser);
						foreach (clsDetalleTransferencia det in detalle)
						{
							det.CodTransDir = Convert.ToInt32(transfer.CodTransDir);
							admTransferencia.insertdetalle(det);
						}
						apruebaTransferencia(transfer);
						admreqalm.actualizaCantidadPendienteReqAlmacen(req_alm.Codigo);
						admreqalm.actualizaEstadoReqAlmacen(req_alm.Codigo, 13);
					}
				}
			}
			MessageBox.Show("Requerimiento de Almacen Aprobado Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			req_alm = admreqalm.CargaRequerimiento(codRequerimientoAlmacen);
			setDatosRequerimientoAlmacen();
			rgvDetalleRequerimiento.DataSource = admreqalm.ListaDetalleRequerimiento(req_alm.Codigo);
			recargaStockRGV();
			btnAprobar.Visible = false;
			BtnGenerarTD.Visible = true;
			Label label = lblusuarioaprob;
			bool visible = (txtusuarioaprob.Visible = true);
			label.Visible = visible;
			txtusuarioaprob.Text = req_alm.UserAprobador;
			if (TipoReq == 2)
			{
				ventanaListaReqVentas.cargarlista();
				Button btnGenerarTD = BtnGenerarTD;
				Button button = btnGuarda;
				Button button2 = btnanular;
				bool flag2 = (btndetalle.Visible = false);
				bool flag4 = (button2.Visible = flag2);
				visible = (button.Visible = flag4);
				btnGenerarTD.Visible = visible;
				dgvTransGeneradas.DataSource = admreqalm.listadoTransferenciasGeneradas(req_alm.Codigo, frmLogin.iCodAlmacen);
			}
			if (TipoReq == 1)
			{
				recargarPagina(1);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void btnImprimirPDF_Click(object sender, EventArgs e)
	{
		string ruta = "C:\\tmp\\RequerimientosDeAlmacen";
		string nombreArchivo = "RQAL " + req_alm.NumSerie + "-" + req_alm.NumDocumento.ToString().PadLeft(8, '0');
		CRReqAlmacen rpt = new CRReqAlmacen();
		rpt.SetDataSource(admreqalm.ReporteImprimirReqAlm(Convert.ToInt32(req_alm.Codigo)).Tables[0]);
		Directory.CreateDirectory(ruta);
		rpt.ExportToDisk(ExportFormatType.PortableDocFormat, ruta + "\\" + nombreArchivo + ".pdf");
		Process p = new Process();
		p.StartInfo.FileName = ruta + "\\" + nombreArchivo + ".pdf";
		p.Start();
	}

	private void btnImprimirTicket_Click(object sender, EventArgs e)
	{
		try
		{
			clsTipoDocumento doc = admtd.BuscaTipoDocumento("RQAL");
			clsSerie ser = admSerie.BuscaSeriexDocumento(doc.CodTipoDocumento, frmLogin.iCodAlmacen);
			clsConsultasExternas ext = new clsConsultasExternas();
			CRReqAlmacenFormatoContinuocont rpt1 = new CRReqAlmacenFormatoContinuocont();
			rpt1.SetDataSource(admreqalm.ReporteImprimirReqAlm(Convert.ToInt32(req_alm.Codigo)).Tables[0]);
			PrintOptions rptoption = rpt1.PrintOptions;
			rptoption.PrinterName = ser.NombreImpresora;
			rptoption.PaperSize = (PaperSize)ext.GetIDPaperSize(ser.NombreImpresora, ser.PaperSize);
			rptoption.ApplyPageMargins(new PageMargins(50, 5, 0, 10));
			rpt1.PrintToPrinter(1, collated: false, 1, 1);
			rpt1.Close();
			rpt1.Dispose();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Se encontro el siguiente problema: " + ex.Message, "Venta", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
	}

	private void rgvDetalleRequerimiento_CellEndEdit(object sender, GridViewCellEventArgs e)
	{
		try
		{
			bool bandNoModificar = false;
			recargaStockRGV();
			List<double> cantidades = new List<double>();
			cantidades.Add(Convert.ToDouble(e.Row.Cells["colCantidad"].Value));
			cantidades.Add(Convert.ToDouble(e.Row.Cells["colStockAlmacenDespacho"].Value));
			cantidades.Sort();
			double menor = cantidades.First();
			if (menor <= Convert.ToDouble(e.Row.Cells[e.ColumnIndex].Value) && menor != Convert.ToDouble(e.Row.Cells[e.ColumnIndex].Value))
			{
				MessageBox.Show("La cantidad ingresada no puede ser mayor que " + menor, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				e.Row.Cells[e.ColumnIndex].Value = menor;
				bandNoModificar = true;
			}
			if (Proceso == 0 || req_alm.IEstado < 8 || !bandEditar)
			{
				return;
			}
			if (valor_inicial == null)
			{
				throw new Exception("Se encontro un valor null con respecto a la columna de cantidad de requerimiento");
			}
			if (bandNoModificar)
			{
				e.Row.Cells[e.ColumnIndex].Value = Convert.ToDouble(valor_inicial);
				return;
			}
			double ctdadReq = Convert.ToDouble(e.Row.Cells["colCantidad"].Value);
			double valorActual = Convert.ToDouble(e.Row.Cells[e.ColumnIndex].Value);
			int codProducto = Convert.ToInt32(e.Row.Cells["colCodProducto"].Value);
			double ctdadTransferencias = admreqalm.getCantidadProductoTransferenciasActivas(req_alm.Codigo, codProducto);
			if (ctdadTransferencias == double.NaN)
			{
				MessageBox.Show("ocurrio un error al traer la cantidad de las transferencias del requerimiento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				e.Row.Cells[e.ColumnIndex].Value = valor_inicial;
			}
			else if (valorActual >= ctdadTransferencias && valorActual <= ctdadReq)
			{
				btnGuardarEdicion.Visible = true;
			}
			else
			{
				MessageBox.Show("La cantidad no puede ser menor que " + ctdadTransferencias, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				e.Row.Cells[e.ColumnIndex].Value = valor_inicial;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dgvTransGeneradas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			if (req_alm.CodAlmacenSolicitante != frmLogin.iCodAlmacen && req_alm.CodAlmacenDespacho != frmLogin.iCodAlmacen)
			{
				MessageBox.Show("No tiene permitido abrir esta transferencia", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				return;
			}
			F2TransferenciaEntreAlmacenes form = new F2TransferenciaEntreAlmacenes();
			form.MdiParent = base.MdiParent;
			form.CodTransDirecta = Convert.ToInt32(dgvTransGeneradas.Rows[e.RowIndex].Cells[colCodTransDir.Name].Value);
			form.Proceso = 3;
			form.caso = Convert.ToInt32(dgvTransGeneradas.Rows[e.RowIndex].Cells[colCaso.Name].Value);
			form.Show();
		}
	}

	private string verificarCtdadRequerimiento()
	{
		string rpta = "";
		recargaStockRGV();
		foreach (GridViewRowInfo fila in rgvDetalleRequerimiento.Rows)
		{
			if (double.TryParse(fila.Cells["colCtdadRequerimiento"].Value.ToString(), out var ctdadreq))
			{
				if (ctdadreq != 0.0)
				{
					if (double.TryParse(fila.Cells["colStockAlmacenDespacho"].Value.ToString(), out var ctdadalm))
					{
						bool flag = true;
						if (ctdadreq > ctdadalm)
						{
							rpta = "La Columna: " + fila.Cells["colCtdadRequerimiento"].ColumnInfo.HeaderText + " no debe ser mayor a la Columna: " + fila.Cells["colStockAlmacenDespacho"].ColumnInfo.HeaderText;
						}
					}
					else
					{
						rpta = "La Columna: " + fila.Cells["colStockAlmacenDespacho"].ColumnInfo.HeaderText + " debe contener un numero.\n De no haberlo debe ingresar la cantidad Cero en la Columna " + fila.Cells["colCtdadRequerimiento"].ColumnInfo.HeaderText;
					}
				}
				else if (Proceso == 0 && TipoReq == 2)
				{
					rpta = "Debe ingresar la cantidad mayor a Cero en la Columna " + fila.Cells["colCtdadRequerimiento"].ColumnInfo.HeaderText + "\nDe no requerir el producto proceda a eliminarlo de la lista.";
				}
			}
			else
			{
				rpta = "La Columna: " + fila.Cells["colCtdadRequerimiento"].ColumnInfo.HeaderText + " debe contener un numero";
			}
		}
		return rpta;
	}

	private void chkDelivery_CheckedChanged(object sender, EventArgs e)
	{
		if (!(gbDelivery.Visible = chkDelivery.Checked))
		{
			txtdireccion.Text = "";
		}
	}

	private void label14_Click(object sender, EventArgs e)
	{
	}

	private void groupBox5_Enter(object sender, EventArgs e)
	{
	}

	private void frmReqAlmacen_Shown(object sender, EventArgs e)
	{
		if (Proceso != 1 && Proceso != 2)
		{
			return;
		}
		if (req_alm.Tipo == 2)
		{
			GroupBox groupBox = gbContactoAEntregar;
			CheckBox checkBox = chkDelivery;
			Label label = lblusuariosolic;
			TextBox textBox = txtusuariosolic;
			Label label2 = lblusuariodesp;
			ComboBox comboBox = cmbusuariodesp;
			bool flag = (btndetalle.Visible = true);
			bool flag3 = (comboBox.Visible = flag);
			bool flag5 = (label2.Visible = flag3);
			bool flag7 = (textBox.Visible = flag5);
			bool flag9 = (label.Visible = flag7);
			bool visible = (checkBox.Visible = flag9);
			groupBox.Visible = visible;
			gbDelivery.Visible = chkDelivery.Checked;
			if (req_alm.IEstado == 7)
			{
				btnGuarda.Visible = true;
			}
			if (req_alm.IEstado == 13)
			{
				btndetalle.Visible = false;
			}
		}
		if (req_alm.IEstado == 12)
		{
			btndetalle.Visible = false;
		}
	}

	private void btnGuardarEdicion_Click(object sender, EventArgs e)
	{
		try
		{
			usuario_click = null;
			frmAutorizacion frm = new frmAutorizacion();
			frm.tipoAccion = 2;
			int codPermiso = new clsAdmFormulario().getPermisoEditarReqReposStock();
			frm.permiso = codPermiso;
			frm.PermitirAdministradores = true;
			frm.tipoVentanaAAsignarUsuario = 5;
			frm.ventanaReqAlmacen = this;
			DialogResult dr = frm.ShowDialog();
			if (dr != DialogResult.OK || usuario_click == null)
			{
				return;
			}
			foreach (GridViewRowInfo fila in rgvDetalleRequerimiento.Rows)
			{
				int idDetalle = Convert.ToInt32(fila.Cells["colCodDetalle"].Value);
				double nuevaCtdadConfirmacion = Convert.ToDouble(fila.Cells["colCtdadRequerimiento"].Value);
				if (!admreqalm.ModificarCantidadConfirmada(idDetalle, nuevaCtdadConfirmacion))
				{
					throw new Exception("Error en guardado de modificacion de cantidad de requerimiento");
				}
			}
			admreqalm.actualizaCantidadPendienteReqAlmacen(req_alm.Codigo);
			admreqalm.actualizaCantidadPendienteAprobadaReqAlmacen(req_alm.Codigo);
			btnGuardarEdicion.Visible = false;
			recargarPagina(1, bandEditar: true);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void rgvDetalleRequerimiento_CellBeginEdit(object sender, GridViewCellCancelEventArgs e)
	{
		try
		{
			if (bandEditar && e.Column.Name == "colCtdadRequerimiento")
			{
				valor_inicial = e.Row.Cells["colCtdadRequerimiento"].Value;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void txtTelefonoContacto_KeyPress(object sender, KeyPressEventArgs e)
	{
		valida.telefono(e);
	}

	private void rgvDetalleRequerimiento_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (rgvDetalleRequerimiento.SelectedRows.Count > 0 && e.KeyChar == '.')
		{
			DialogResult rpta = MessageBox.Show("Esta seguro de eliminar estos elementos:", "Eliminar productos", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (rpta == DialogResult.Yes)
			{
				MessageBox.Show("eliminado");
			}
		}
	}

	private void btnCerrarReq_Click(object sender, EventArgs e)
	{
		DialogResult rpta = MessageBox.Show("Esta seguro de cerrar este requerimiento?", "Cerrar Requerimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		if (rpta != DialogResult.Yes || req_alm.IEstado == 7 || req_alm.IEstado == 11 || req_alm.IEstado == 12)
		{
			return;
		}
		admreqalm.cerrar(req_alm.Codigo, frmLogin.iCodUser);
		DataTable listadoCodTrans = admreqalm.cargaTransferenciasPendientes(req_alm.Codigo);
		clsAdmTransferencia admTransferencia = new clsAdmTransferencia();
		if (listadoCodTrans != null)
		{
			foreach (DataRow fila in listadoCodTrans.Rows)
			{
				int codTransDir = Convert.ToInt32(fila.Field<object>(0));
				admTransferencia.rechazado(codTransDir, "Transferencia Anulada por Requerimiento: " + req_alm.NumSerie + "-" + req_alm.NumDocumento);
			}
		}
		req_alm.ListadoDetalle = admreqalm.CargaDetalleRequerimientoAlmacen(req_alm.Codigo);
		foreach (clsDetalleRequerimientoAlmacen item in req_alm.ListadoDetalle)
		{
			admreqalm.retornarStock(req_alm.CodAlmacenDespacho, item.CodProducto, item.CodUnidad, item.CantidadPendienteAprobada, TipoReq == 2, item.Codigo);
		}
		MessageBox.Show("Requerimiento de Almacen Cerrado Con Exito", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		req_alm = admreqalm.CargaRequerimiento(codRequerimientoAlmacen);
		setDatosRequerimientoAlmacen();
		Button btnGenerarTD = BtnGenerarTD;
		Button button = btnAprobar;
		Button button2 = btnanular;
		bool flag = (btnCerrarReq.Visible = false);
		bool flag3 = (button2.Visible = flag);
		bool visible = (button.Visible = flag3);
		btnGenerarTD.Visible = visible;
		if (TipoReq == 2)
		{
			recargarPagina();
		}
		if (TipoReq == 1)
		{
			GroupBox groupBox = gbContactoAEntregar;
			GroupBox groupBox2 = gbDelivery;
			flag3 = (chkDelivery.Visible = false);
			visible = (groupBox2.Visible = flag3);
			groupBox.Visible = visible;
			dgvTransGeneradas.DataSource = admreqalm.listadoTransferenciasGeneradas(req_alm.Codigo, frmLogin.iCodAlmacen);
		}
	}

	private void txtFacturaVenta_DoubleClick(object sender, EventArgs e)
	{
		if (req_alm.CodFacturaVenta > 0)
		{
			frmVenta form = new frmVenta();
			form.MdiParent = base.MdiParent;
			form.CodVenta = req_alm.CodFacturaVenta.ToString();
			form.Proceso = 3;
			form.Show();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmReqAlmacen));
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn1 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn2 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewDecimalColumn gridViewDecimalColumn3 = new Telerik.WinControls.UI.GridViewDecimalColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
		Telerik.WinControls.UI.TableViewDefinition tableViewDefinition1 = new Telerik.WinControls.UI.TableViewDefinition();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.label13 = new System.Windows.Forms.Label();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.txtFacturaVenta = new System.Windows.Forms.TextBox();
		this.lblFacturaVenta = new System.Windows.Forms.Label();
		this.lblTipoReq = new System.Windows.Forms.Label();
		this.txtusuarioaprob = new System.Windows.Forms.TextBox();
		this.lblusuarioaprob = new System.Windows.Forms.Label();
		this.txtusuariosolic = new System.Windows.Forms.TextBox();
		this.cmbusuariodesp = new System.Windows.Forms.ComboBox();
		this.lblusuariodesp = new System.Windows.Forms.Label();
		this.lblusuariosolic = new System.Windows.Forms.Label();
		this.chkDelivery = new System.Windows.Forms.CheckBox();
		this.gbDelivery = new System.Windows.Forms.GroupBox();
		this.txtdireccion = new System.Windows.Forms.TextBox();
		this.label12 = new System.Windows.Forms.Label();
		this.gbContactoAEntregar = new System.Windows.Forms.GroupBox();
		this.txtTelefonoContacto = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.txtNombreContacto = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.dgvTransGeneradas = new System.Windows.Forms.DataGridView();
		this.colDocTrans = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCodTransDir = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.colCaso = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnImprimirTicket = new System.Windows.Forms.Button();
		this.btnImprimirPDF = new System.Windows.Forms.Button();
		this.txtComentarioDespacho = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.txtComentario = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.txtEstado = new System.Windows.Forms.TextBox();
		this.cmbAlmacenesSolicitantes = new System.Windows.Forms.ComboBox();
		this.cmbAlmacenesDespacho = new System.Windows.Forms.ComboBox();
		this.label7 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.label11 = new System.Windows.Forms.Label();
		this.btndetalle = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.rgvDetalleRequerimiento = new Telerik.WinControls.UI.RadGridView();
		this.btnGuarda = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnAprobar = new System.Windows.Forms.Button();
		this.btnanular = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnCerrarReq = new System.Windows.Forms.Button();
		this.btnGuardarEdicion = new System.Windows.Forms.Button();
		this.BtnGenerarTD = new System.Windows.Forms.Button();
		this.groupBox5.SuspendLayout();
		this.gbDelivery.SuspendLayout();
		this.gbContactoAEntregar.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTransGeneradas).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalleRequerimiento).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalleRequerimiento.MasterTemplate).BeginInit();
		this.groupBox2.SuspendLayout();
		base.SuspendLayout();
		this.dtpFecha.Enabled = false;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(655, 19);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(100, 20);
		this.dtpFecha.TabIndex = 136;
		this.dtpFecha.Value = new System.DateTime(2021, 12, 7, 18, 57, 3, 0);
		this.label13.AutoSize = true;
		this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.label13.Location = new System.Drawing.Point(609, 22);
		this.label13.Name = "label13";
		this.label13.Size = new System.Drawing.Size(40, 13);
		this.label13.TabIndex = 135;
		this.label13.Text = "Fecha:";
		this.groupBox5.Controls.Add(this.txtFacturaVenta);
		this.groupBox5.Controls.Add(this.lblFacturaVenta);
		this.groupBox5.Controls.Add(this.lblTipoReq);
		this.groupBox5.Controls.Add(this.txtusuarioaprob);
		this.groupBox5.Controls.Add(this.lblusuarioaprob);
		this.groupBox5.Controls.Add(this.txtusuariosolic);
		this.groupBox5.Controls.Add(this.cmbusuariodesp);
		this.groupBox5.Controls.Add(this.lblusuariodesp);
		this.groupBox5.Controls.Add(this.lblusuariosolic);
		this.groupBox5.Controls.Add(this.chkDelivery);
		this.groupBox5.Controls.Add(this.gbDelivery);
		this.groupBox5.Controls.Add(this.gbContactoAEntregar);
		this.groupBox5.Controls.Add(this.dgvTransGeneradas);
		this.groupBox5.Controls.Add(this.btnImprimirTicket);
		this.groupBox5.Controls.Add(this.btnImprimirPDF);
		this.groupBox5.Controls.Add(this.txtComentarioDespacho);
		this.groupBox5.Controls.Add(this.label4);
		this.groupBox5.Controls.Add(this.txtComentario);
		this.groupBox5.Controls.Add(this.label9);
		this.groupBox5.Controls.Add(this.label3);
		this.groupBox5.Controls.Add(this.txtEstado);
		this.groupBox5.Controls.Add(this.cmbAlmacenesSolicitantes);
		this.groupBox5.Controls.Add(this.cmbAlmacenesDespacho);
		this.groupBox5.Controls.Add(this.label7);
		this.groupBox5.Controls.Add(this.label2);
		this.groupBox5.Controls.Add(this.label1);
		this.groupBox5.Controls.Add(this.label13);
		this.groupBox5.Controls.Add(this.txtSerie);
		this.groupBox5.Controls.Add(this.dtpFecha);
		this.groupBox5.Controls.Add(this.txtDocRef);
		this.groupBox5.Controls.Add(this.txtNumero);
		this.groupBox5.Controls.Add(this.label11);
		this.groupBox5.Controls.Add(this.btndetalle);
		this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox5.Location = new System.Drawing.Point(0, 0);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(1334, 239);
		this.groupBox5.TabIndex = 142;
		this.groupBox5.TabStop = false;
		this.groupBox5.Text = "DATOS DEL REQUERIMIENTO";
		this.groupBox5.Enter += new System.EventHandler(groupBox5_Enter);
		this.txtFacturaVenta.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtFacturaVenta.Enabled = false;
		this.txtFacturaVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtFacturaVenta.Location = new System.Drawing.Point(204, 198);
		this.txtFacturaVenta.Name = "txtFacturaVenta";
		this.txtFacturaVenta.ReadOnly = true;
		this.txtFacturaVenta.Size = new System.Drawing.Size(192, 20);
		this.txtFacturaVenta.TabIndex = 190;
		this.txtFacturaVenta.Tag = "";
		this.txtFacturaVenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtFacturaVenta.Visible = false;
		this.txtFacturaVenta.DoubleClick += new System.EventHandler(txtFacturaVenta_DoubleClick);
		this.lblFacturaVenta.AutoSize = true;
		this.lblFacturaVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblFacturaVenta.Location = new System.Drawing.Point(17, 203);
		this.lblFacturaVenta.Name = "lblFacturaVenta";
		this.lblFacturaVenta.Size = new System.Drawing.Size(181, 13);
		this.lblFacturaVenta.TabIndex = 189;
		this.lblFacturaVenta.Text = "FACTURA VENTA RELACIONADA: ";
		this.lblFacturaVenta.Visible = false;
		this.lblTipoReq.Font = new System.Drawing.Font("Lucida Sans", 10f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblTipoReq.ForeColor = System.Drawing.Color.DodgerBlue;
		this.lblTipoReq.Location = new System.Drawing.Point(811, 16);
		this.lblTipoReq.Name = "lblTipoReq";
		this.lblTipoReq.Size = new System.Drawing.Size(203, 48);
		this.lblTipoReq.TabIndex = 188;
		this.lblTipoReq.Text = "REQUERIMIENTO PARA VENTA";
		this.lblTipoReq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
		this.txtusuarioaprob.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtusuarioaprob.Enabled = false;
		this.txtusuarioaprob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtusuarioaprob.Location = new System.Drawing.Point(155, 172);
		this.txtusuarioaprob.Name = "txtusuarioaprob";
		this.txtusuarioaprob.ReadOnly = true;
		this.txtusuarioaprob.Size = new System.Drawing.Size(241, 20);
		this.txtusuarioaprob.TabIndex = 187;
		this.txtusuarioaprob.Tag = "";
		this.txtusuarioaprob.Visible = false;
		this.lblusuarioaprob.AutoSize = true;
		this.lblusuarioaprob.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblusuarioaprob.Location = new System.Drawing.Point(17, 177);
		this.lblusuarioaprob.Name = "lblusuarioaprob";
		this.lblusuarioaprob.Size = new System.Drawing.Size(130, 13);
		this.lblusuarioaprob.TabIndex = 186;
		this.lblusuarioaprob.Text = "USUARIO APROBADOR:";
		this.lblusuarioaprob.Visible = false;
		this.txtusuariosolic.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtusuariosolic.Enabled = false;
		this.txtusuariosolic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtusuariosolic.Location = new System.Drawing.Point(161, 45);
		this.txtusuariosolic.Name = "txtusuariosolic";
		this.txtusuariosolic.ReadOnly = true;
		this.txtusuariosolic.Size = new System.Drawing.Size(241, 20);
		this.txtusuariosolic.TabIndex = 185;
		this.txtusuariosolic.Tag = "";
		this.txtusuariosolic.Visible = false;
		this.cmbusuariodesp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbusuariodesp.FormattingEnabled = true;
		this.cmbusuariodesp.Location = new System.Drawing.Point(554, 44);
		this.cmbusuariodesp.Name = "cmbusuariodesp";
		this.cmbusuariodesp.Size = new System.Drawing.Size(241, 21);
		this.cmbusuariodesp.TabIndex = 184;
		this.cmbusuariodesp.Visible = false;
		this.lblusuariodesp.AutoSize = true;
		this.lblusuariodesp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblusuariodesp.Location = new System.Drawing.Point(415, 47);
		this.lblusuariodesp.Name = "lblusuariodesp";
		this.lblusuariodesp.Size = new System.Drawing.Size(121, 13);
		this.lblusuariodesp.TabIndex = 183;
		this.lblusuariodesp.Text = "USUARIO DESPACHO:";
		this.lblusuariodesp.Visible = false;
		this.lblusuariosolic.AutoSize = true;
		this.lblusuariosolic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.lblusuariosolic.Location = new System.Drawing.Point(17, 52);
		this.lblusuariosolic.Name = "lblusuariosolic";
		this.lblusuariosolic.Size = new System.Drawing.Size(132, 13);
		this.lblusuariosolic.TabIndex = 182;
		this.lblusuariosolic.Text = "USUARIO SOLICITANTE:";
		this.lblusuariosolic.Visible = false;
		this.lblusuariosolic.Click += new System.EventHandler(label14_Click);
		this.chkDelivery.AutoSize = true;
		this.chkDelivery.Checked = true;
		this.chkDelivery.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkDelivery.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.chkDelivery.Location = new System.Drawing.Point(627, 157);
		this.chkDelivery.Name = "chkDelivery";
		this.chkDelivery.Size = new System.Drawing.Size(72, 17);
		this.chkDelivery.TabIndex = 180;
		this.chkDelivery.Text = "Delivery";
		this.chkDelivery.UseVisualStyleBackColor = true;
		this.chkDelivery.Visible = false;
		this.chkDelivery.CheckedChanged += new System.EventHandler(chkDelivery_CheckedChanged);
		this.gbDelivery.Controls.Add(this.txtdireccion);
		this.gbDelivery.Controls.Add(this.label12);
		this.gbDelivery.Location = new System.Drawing.Point(618, 180);
		this.gbDelivery.Name = "gbDelivery";
		this.gbDelivery.Size = new System.Drawing.Size(430, 45);
		this.gbDelivery.TabIndex = 178;
		this.gbDelivery.TabStop = false;
		this.gbDelivery.Text = "Datos Delivery";
		this.gbDelivery.Visible = false;
		this.txtdireccion.Location = new System.Drawing.Point(86, 17);
		this.txtdireccion.MaxLength = 255;
		this.txtdireccion.Name = "txtdireccion";
		this.txtdireccion.Size = new System.Drawing.Size(338, 20);
		this.txtdireccion.TabIndex = 5;
		this.label12.AutoSize = true;
		this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label12.Location = new System.Drawing.Point(6, 20);
		this.label12.Name = "label12";
		this.label12.Size = new System.Drawing.Size(55, 13);
		this.label12.TabIndex = 4;
		this.label12.Text = "Direccion:";
		this.gbContactoAEntregar.Controls.Add(this.txtTelefonoContacto);
		this.gbContactoAEntregar.Controls.Add(this.label5);
		this.gbContactoAEntregar.Controls.Add(this.txtNombreContacto);
		this.gbContactoAEntregar.Controls.Add(this.label6);
		this.gbContactoAEntregar.Location = new System.Drawing.Point(612, 77);
		this.gbContactoAEntregar.Name = "gbContactoAEntregar";
		this.gbContactoAEntregar.Size = new System.Drawing.Size(420, 74);
		this.gbContactoAEntregar.TabIndex = 177;
		this.gbContactoAEntregar.TabStop = false;
		this.gbContactoAEntregar.Text = "Contacto de Entrega";
		this.txtTelefonoContacto.Location = new System.Drawing.Point(87, 45);
		this.txtTelefonoContacto.MaxLength = 15;
		this.txtTelefonoContacto.Name = "txtTelefonoContacto";
		this.txtTelefonoContacto.Size = new System.Drawing.Size(328, 20);
		this.txtTelefonoContacto.TabIndex = 16;
		this.txtTelefonoContacto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtTelefonoContacto_KeyPress);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(12, 48);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(52, 13);
		this.label5.TabIndex = 15;
		this.label5.Text = "Telefono:";
		this.txtNombreContacto.Location = new System.Drawing.Point(87, 19);
		this.txtNombreContacto.MaxLength = 255;
		this.txtNombreContacto.Name = "txtNombreContacto";
		this.txtNombreContacto.Size = new System.Drawing.Size(328, 20);
		this.txtNombreContacto.TabIndex = 14;
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(12, 22);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(47, 13);
		this.label6.TabIndex = 13;
		this.label6.Text = "Nombre:";
		this.dgvTransGeneradas.AllowUserToAddRows = false;
		this.dgvTransGeneradas.AllowUserToResizeColumns = false;
		this.dgvTransGeneradas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvTransGeneradas.BackgroundColor = System.Drawing.Color.FromArgb(194, 217, 247);
		this.dgvTransGeneradas.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvTransGeneradas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvTransGeneradas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvTransGeneradas.Columns.AddRange(this.colDocTrans, this.colEstado, this.colCodTransDir, this.colCaso);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvTransGeneradas.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvTransGeneradas.Location = new System.Drawing.Point(1063, 9);
		this.dgvTransGeneradas.MultiSelect = false;
		this.dgvTransGeneradas.Name = "dgvTransGeneradas";
		this.dgvTransGeneradas.ReadOnly = true;
		this.dgvTransGeneradas.RowHeadersVisible = false;
		this.dgvTransGeneradas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvTransGeneradas.Size = new System.Drawing.Size(240, 168);
		this.dgvTransGeneradas.TabIndex = 176;
		this.dgvTransGeneradas.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvTransGeneradas_CellDoubleClick);
		this.colDocTrans.DataPropertyName = "docTrans";
		this.colDocTrans.HeaderText = "Documento";
		this.colDocTrans.Name = "colDocTrans";
		this.colDocTrans.ReadOnly = true;
		this.colEstado.DataPropertyName = "estado";
		this.colEstado.HeaderText = "Estado";
		this.colEstado.Name = "colEstado";
		this.colEstado.ReadOnly = true;
		this.colCodTransDir.DataPropertyName = "codTransDir";
		this.colCodTransDir.HeaderText = "CodTransDir";
		this.colCodTransDir.Name = "colCodTransDir";
		this.colCodTransDir.ReadOnly = true;
		this.colCodTransDir.Visible = false;
		this.colCaso.DataPropertyName = "caso";
		this.colCaso.HeaderText = "caso";
		this.colCaso.Name = "colCaso";
		this.colCaso.ReadOnly = true;
		this.colCaso.Visible = false;
		this.btnImprimirTicket.FlatAppearance.BorderColor = System.Drawing.Color.White;
		this.btnImprimirTicket.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
		this.btnImprimirTicket.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnImprimirTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimirTicket.Image = (System.Drawing.Image)resources.GetObject("btnImprimirTicket.Image");
		this.btnImprimirTicket.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImprimirTicket.Location = new System.Drawing.Point(1197, 183);
		this.btnImprimirTicket.Name = "btnImprimirTicket";
		this.btnImprimirTicket.Size = new System.Drawing.Size(106, 43);
		this.btnImprimirTicket.TabIndex = 175;
		this.btnImprimirTicket.Text = "IMPRIMIR";
		this.btnImprimirTicket.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimirTicket.UseVisualStyleBackColor = true;
		this.btnImprimirTicket.Click += new System.EventHandler(btnImprimirTicket_Click);
		this.btnImprimirPDF.FlatAppearance.BorderColor = System.Drawing.Color.White;
		this.btnImprimirPDF.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
		this.btnImprimirPDF.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.btnImprimirPDF.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnImprimirPDF.Image = (System.Drawing.Image)resources.GetObject("btnImprimirPDF.Image");
		this.btnImprimirPDF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnImprimirPDF.Location = new System.Drawing.Point(1054, 183);
		this.btnImprimirPDF.Name = "btnImprimirPDF";
		this.btnImprimirPDF.Size = new System.Drawing.Size(137, 43);
		this.btnImprimirPDF.TabIndex = 174;
		this.btnImprimirPDF.Text = "IMPRIMIR PDF";
		this.btnImprimirPDF.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnImprimirPDF.UseVisualStyleBackColor = true;
		this.btnImprimirPDF.Click += new System.EventHandler(btnImprimirPDF_Click);
		this.txtComentarioDespacho.Location = new System.Drawing.Point(343, 110);
		this.txtComentarioDespacho.Multiline = true;
		this.txtComentarioDespacho.Name = "txtComentarioDespacho";
		this.txtComentarioDespacho.Size = new System.Drawing.Size(243, 53);
		this.txtComentarioDespacho.TabIndex = 172;
		this.txtComentarioDespacho.TextChanged += new System.EventHandler(txtComentarioDespacho_TextChanged);
		this.label4.AutoSize = true;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(324, 90);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(63, 13);
		this.label4.TabIndex = 171;
		this.label4.Text = "Comentario:";
		this.txtComentario.Location = new System.Drawing.Point(37, 112);
		this.txtComentario.Multiline = true;
		this.txtComentario.Name = "txtComentario";
		this.txtComentario.Size = new System.Drawing.Size(249, 53);
		this.txtComentario.TabIndex = 170;
		this.label9.AutoSize = true;
		this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label9.Location = new System.Drawing.Point(18, 92);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(63, 13);
		this.label9.TabIndex = 169;
		this.label9.Text = "Comentario:";
		this.label3.AutoSize = true;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.label3.Location = new System.Drawing.Point(320, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(43, 13);
		this.label3.TabIndex = 168;
		this.label3.Text = "Estado:";
		this.txtEstado.BackColor = System.Drawing.SystemColors.Control;
		this.txtEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtEstado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtEstado.ForeColor = System.Drawing.Color.OrangeRed;
		this.txtEstado.Location = new System.Drawing.Point(369, 19);
		this.txtEstado.Name = "txtEstado";
		this.txtEstado.ReadOnly = true;
		this.txtEstado.Size = new System.Drawing.Size(213, 20);
		this.txtEstado.TabIndex = 167;
		this.txtEstado.Tag = "";
		this.txtEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmbAlmacenesSolicitantes.FormattingEnabled = true;
		this.cmbAlmacenesSolicitantes.Location = new System.Drawing.Point(161, 71);
		this.cmbAlmacenesSolicitantes.Name = "cmbAlmacenesSolicitantes";
		this.cmbAlmacenesSolicitantes.Size = new System.Drawing.Size(130, 21);
		this.cmbAlmacenesSolicitantes.TabIndex = 166;
		this.cmbAlmacenesSolicitantes.SelectionChangeCommitted += new System.EventHandler(cmbalmacenessolicitantes_SelectionChangeCommitted);
		this.cmbAlmacenesDespacho.FormattingEnabled = true;
		this.cmbAlmacenesDespacho.Location = new System.Drawing.Point(456, 72);
		this.cmbAlmacenesDespacho.Name = "cmbAlmacenesDespacho";
		this.cmbAlmacenesDespacho.Size = new System.Drawing.Size(130, 21);
		this.cmbAlmacenesDespacho.TabIndex = 6;
		this.cmbAlmacenesDespacho.SelectionChangeCommitted += new System.EventHandler(cmbalmacenesdespacho_SelectionChangeCommitted);
		this.label7.AutoSize = true;
		this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label7.Location = new System.Drawing.Point(324, 75);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(126, 13);
		this.label7.TabIndex = 5;
		this.label7.Text = "ALMACEN DESPACHO :";
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(18, 77);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(137, 13);
		this.label2.TabIndex = 4;
		this.label2.Text = "ALMACEN SOLICITANTE :";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(23, 24);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(53, 13);
		this.label1.TabIndex = 83;
		this.label1.Tag = "10";
		this.label1.Text = "Doc. Ref.";
		this.txtSerie.Location = new System.Drawing.Point(155, 19);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(61, 20);
		this.txtSerie.TabIndex = 84;
		this.txtSerie.Tag = "";
		this.txtSerie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.txtDocRef.BackColor = System.Drawing.Color.PeachPuff;
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f);
		this.txtDocRef.Location = new System.Drawing.Point(82, 20);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.ReadOnly = true;
		this.txtDocRef.Size = new System.Drawing.Size(51, 20);
		this.txtDocRef.TabIndex = 82;
		this.txtDocRef.Tag = "10";
		this.txtDocRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumero.Enabled = false;
		this.txtNumero.Location = new System.Drawing.Point(222, 19);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.Size = new System.Drawing.Size(65, 20);
		this.txtNumero.TabIndex = 85;
		this.txtNumero.Tag = "";
		this.txtNumero.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
		this.label11.AutoSize = true;
		this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label11.Location = new System.Drawing.Point(139, 22);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(12, 15);
		this.label11.TabIndex = 86;
		this.label11.Text = "-";
		this.btndetalle.BackColor = System.Drawing.Color.White;
		this.btndetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.btndetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btndetalle.Image = SIGEFA.Properties.Resources.agregar;
		this.btndetalle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btndetalle.Location = new System.Drawing.Point(456, 197);
		this.btndetalle.Name = "btndetalle";
		this.btndetalle.Size = new System.Drawing.Size(141, 29);
		this.btndetalle.TabIndex = 144;
		this.btndetalle.Text = "Agregar Producto";
		this.btndetalle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btndetalle.UseVisualStyleBackColor = false;
		this.btndetalle.Click += new System.EventHandler(btndetalle_Click);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.groupBox1.Controls.Add(this.rgvDetalleRequerimiento);
		this.groupBox1.Location = new System.Drawing.Point(0, 245);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(1334, 271);
		this.groupBox1.TabIndex = 143;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "DETALLE DE REQUERIMIENTO DE ALMACEN";
		this.rgvDetalleRequerimiento.AutoScroll = true;
		this.rgvDetalleRequerimiento.Dock = System.Windows.Forms.DockStyle.Fill;
		this.rgvDetalleRequerimiento.Location = new System.Drawing.Point(3, 16);
		this.rgvDetalleRequerimiento.MasterTemplate.AllowAddNewRow = false;
		this.rgvDetalleRequerimiento.MasterTemplate.AllowColumnReorder = false;
		this.rgvDetalleRequerimiento.MasterTemplate.AllowDeleteRow = false;
		this.rgvDetalleRequerimiento.MasterTemplate.AllowDragToGroup = false;
		this.rgvDetalleRequerimiento.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
		gridViewTextBoxColumn1.FieldName = "codDetalle";
		gridViewTextBoxColumn1.HeaderText = "codDetalle";
		gridViewTextBoxColumn1.IsVisible = false;
		gridViewTextBoxColumn1.Name = "colCodDetalle";
		gridViewTextBoxColumn2.FieldName = "codProducto";
		gridViewTextBoxColumn2.HeaderText = "codProducto";
		gridViewTextBoxColumn2.IsVisible = false;
		gridViewTextBoxColumn2.Name = "colCodProducto";
		gridViewTextBoxColumn3.FieldName = "refProducto";
		gridViewTextBoxColumn3.HeaderText = "Referencia";
		gridViewTextBoxColumn3.Name = "colRefProducto";
		gridViewTextBoxColumn3.Width = 121;
		gridViewTextBoxColumn4.FieldName = "descProducto";
		gridViewTextBoxColumn4.HeaderText = "Descripcion";
		gridViewTextBoxColumn4.Name = "colDescProducto";
		gridViewTextBoxColumn4.Width = 379;
		gridViewTextBoxColumn5.FieldName = "codUnidad";
		gridViewTextBoxColumn5.HeaderText = "codUnidad";
		gridViewTextBoxColumn5.IsVisible = false;
		gridViewTextBoxColumn5.Name = "colCodUnidad";
		gridViewTextBoxColumn6.FieldName = "descUnidad";
		gridViewTextBoxColumn6.HeaderText = "Unidad";
		gridViewTextBoxColumn6.Name = "colDescUnidad";
		gridViewTextBoxColumn6.Width = 227;
		gridViewTextBoxColumn7.AllowFiltering = false;
		gridViewTextBoxColumn7.FieldName = "cantidad";
		gridViewTextBoxColumn7.HeaderText = "Cantidad Solicitante";
		gridViewTextBoxColumn7.Name = "colCantidad";
		gridViewTextBoxColumn7.Width = 151;
		gridViewTextBoxColumn7.WrapText = true;
		gridViewDecimalColumn1.FieldName = "stockAlmacenSolicitante";
		gridViewDecimalColumn1.HeaderText = "Stock Almacen Solicitante";
		gridViewDecimalColumn1.Name = "colStockAlmacenSolicitante";
		gridViewDecimalColumn1.Width = 151;
		gridViewDecimalColumn1.WrapText = true;
		gridViewDecimalColumn2.DecimalPlaces = 3;
		gridViewDecimalColumn2.FieldName = "stockAlmacenDespacho";
		gridViewDecimalColumn2.HeaderText = "Stock Almacen Despacho";
		gridViewDecimalColumn2.Name = "colStockAlmacenDespacho";
		gridViewDecimalColumn2.Width = 151;
		gridViewDecimalColumn2.WrapText = true;
		gridViewDecimalColumn3.DecimalPlaces = 3;
		gridViewDecimalColumn3.FieldName = "ctdadRequerimiento";
		gridViewDecimalColumn3.HeaderText = "Cantidad a Despachar";
		gridViewDecimalColumn3.Name = "colCtdadRequerimiento";
		gridViewDecimalColumn3.Width = 153;
		gridViewDecimalColumn3.WrapText = true;
		gridViewTextBoxColumn8.AllowFiltering = false;
		gridViewTextBoxColumn8.FieldName = "ctdadPendiente";
		gridViewTextBoxColumn8.HeaderText = "Ctdad. Pendiente";
		gridViewTextBoxColumn8.IsVisible = false;
		gridViewTextBoxColumn8.Name = "colCtdadPendiente";
		gridViewTextBoxColumn8.ReadOnly = true;
		gridViewTextBoxColumn8.Width = 100;
		gridViewTextBoxColumn9.FieldName = "codReqAlm";
		gridViewTextBoxColumn9.HeaderText = "colCodReqAlm";
		gridViewTextBoxColumn9.IsVisible = false;
		gridViewTextBoxColumn9.Name = "colCodReqAlm";
		this.rgvDetalleRequerimiento.MasterTemplate.Columns.AddRange(gridViewTextBoxColumn1, gridViewTextBoxColumn2, gridViewTextBoxColumn3, gridViewTextBoxColumn4, gridViewTextBoxColumn5, gridViewTextBoxColumn6, gridViewTextBoxColumn7, gridViewDecimalColumn1, gridViewDecimalColumn2, gridViewDecimalColumn3, gridViewTextBoxColumn8, gridViewTextBoxColumn9);
		this.rgvDetalleRequerimiento.MasterTemplate.EnableFiltering = true;
		this.rgvDetalleRequerimiento.MasterTemplate.EnableGrouping = false;
		this.rgvDetalleRequerimiento.MasterTemplate.SelectionMode = Telerik.WinControls.UI.GridViewSelectionMode.CellSelect;
		this.rgvDetalleRequerimiento.MasterTemplate.ShowRowHeaderColumn = false;
		this.rgvDetalleRequerimiento.MasterTemplate.ViewDefinition = tableViewDefinition1;
		this.rgvDetalleRequerimiento.Name = "rgvDetalleRequerimiento";
		this.rgvDetalleRequerimiento.ReadOnly = true;
		this.rgvDetalleRequerimiento.Size = new System.Drawing.Size(1328, 252);
		this.rgvDetalleRequerimiento.TabIndex = 1;
		this.rgvDetalleRequerimiento.CellBeginEdit += new Telerik.WinControls.UI.GridViewCellCancelEventHandler(rgvDetalleRequerimiento_CellBeginEdit);
		this.rgvDetalleRequerimiento.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDetalleRequerimiento_CellEndEdit);
		this.rgvDetalleRequerimiento.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(rgvDetalleRequerimiento_CellDoubleClick);
		this.rgvDetalleRequerimiento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(rgvDetalleRequerimiento_KeyPress);
		this.btnGuarda.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnGuarda.FlatAppearance.BorderSize = 2;
		this.btnGuarda.Image = (System.Drawing.Image)resources.GetObject("btnGuarda.Image");
		this.btnGuarda.Location = new System.Drawing.Point(670, 17);
		this.btnGuarda.Name = "btnGuarda";
		this.btnGuarda.Size = new System.Drawing.Size(99, 43);
		this.btnGuarda.TabIndex = 150;
		this.btnGuarda.Text = "GUARDAR";
		this.btnGuarda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuarda.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuarda.UseVisualStyleBackColor = true;
		this.btnGuarda.Click += new System.EventHandler(btnGuarda_Click);
		this.btnSalir.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnSalir.FlatAppearance.BorderSize = 2;
		this.btnSalir.Image = SIGEFA.Properties.Resources.exit;
		this.btnSalir.Location = new System.Drawing.Point(775, 8);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(100, 59);
		this.btnSalir.TabIndex = 151;
		this.btnSalir.Text = "SALIR";
		this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnAprobar.Image = SIGEFA.Properties.Resources.Database_Cloud_32;
		this.btnAprobar.Location = new System.Drawing.Point(296, 15);
		this.btnAprobar.Name = "btnAprobar";
		this.btnAprobar.Size = new System.Drawing.Size(104, 44);
		this.btnAprobar.TabIndex = 165;
		this.btnAprobar.Text = "APROBAR";
		this.btnAprobar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnAprobar.UseVisualStyleBackColor = true;
		this.btnAprobar.Visible = false;
		this.btnAprobar.Click += new System.EventHandler(btnAprobar_Click);
		this.btnanular.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnanular.FlatAppearance.BorderSize = 2;
		this.btnanular.Image = SIGEFA.Properties.Resources.x_button;
		this.btnanular.Location = new System.Drawing.Point(406, 15);
		this.btnanular.Name = "btnanular";
		this.btnanular.Size = new System.Drawing.Size(99, 43);
		this.btnanular.TabIndex = 166;
		this.btnanular.Text = "ANULAR";
		this.btnanular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnanular.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnanular.UseVisualStyleBackColor = true;
		this.btnanular.Visible = false;
		this.btnanular.Click += new System.EventHandler(btnanular_Click);
		this.groupBox2.Controls.Add(this.btnCerrarReq);
		this.groupBox2.Controls.Add(this.btnGuardarEdicion);
		this.groupBox2.Controls.Add(this.BtnGenerarTD);
		this.groupBox2.Controls.Add(this.btnGuarda);
		this.groupBox2.Controls.Add(this.btnSalir);
		this.groupBox2.Controls.Add(this.btnanular);
		this.groupBox2.Controls.Add(this.btnAprobar);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 522);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(1334, 74);
		this.groupBox2.TabIndex = 167;
		this.groupBox2.TabStop = false;
		this.btnCerrarReq.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnCerrarReq.FlatAppearance.BorderSize = 2;
		this.btnCerrarReq.Image = SIGEFA.Properties.Resources.x_button;
		this.btnCerrarReq.Location = new System.Drawing.Point(82, 15);
		this.btnCerrarReq.Name = "btnCerrarReq";
		this.btnCerrarReq.Size = new System.Drawing.Size(98, 43);
		this.btnCerrarReq.TabIndex = 168;
		this.btnCerrarReq.Text = "CERRAR REQ.";
		this.btnCerrarReq.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCerrarReq.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCerrarReq.UseVisualStyleBackColor = true;
		this.btnCerrarReq.Visible = false;
		this.btnCerrarReq.Click += new System.EventHandler(btnCerrarReq_Click);
		this.btnGuardarEdicion.FlatAppearance.BorderColor = System.Drawing.Color.Black;
		this.btnGuardarEdicion.FlatAppearance.BorderSize = 2;
		this.btnGuardarEdicion.Image = (System.Drawing.Image)resources.GetObject("btnGuardarEdicion.Image");
		this.btnGuardarEdicion.Location = new System.Drawing.Point(570, 15);
		this.btnGuardarEdicion.Name = "btnGuardarEdicion";
		this.btnGuardarEdicion.Size = new System.Drawing.Size(94, 43);
		this.btnGuardarEdicion.TabIndex = 167;
		this.btnGuardarEdicion.Text = "GUARDAR EDICION";
		this.btnGuardarEdicion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardarEdicion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardarEdicion.UseVisualStyleBackColor = true;
		this.btnGuardarEdicion.Visible = false;
		this.btnGuardarEdicion.Click += new System.EventHandler(btnGuardarEdicion_Click);
		this.BtnGenerarTD.Image = SIGEFA.Properties.Resources.agregar;
		this.BtnGenerarTD.Location = new System.Drawing.Point(186, 15);
		this.BtnGenerarTD.Name = "BtnGenerarTD";
		this.BtnGenerarTD.Size = new System.Drawing.Size(104, 44);
		this.BtnGenerarTD.TabIndex = 166;
		this.BtnGenerarTD.Text = "GENERAR TD";
		this.BtnGenerarTD.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.BtnGenerarTD.UseVisualStyleBackColor = true;
		this.BtnGenerarTD.Visible = false;
		this.BtnGenerarTD.Click += new System.EventHandler(BtnGenerarTD_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(194, 217, 247);
		base.ClientSize = new System.Drawing.Size(1334, 596);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox5);
		this.DoubleBuffered = true;
		this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		base.Name = "frmReqAlmacen";
		this.Text = "REQUERIMIENTO DE ALMACEN";
		base.Load += new System.EventHandler(frmReqAlmacen_Load);
		base.Shown += new System.EventHandler(frmReqAlmacen_Shown);
		this.groupBox5.ResumeLayout(false);
		this.groupBox5.PerformLayout();
		this.gbDelivery.ResumeLayout(false);
		this.gbDelivery.PerformLayout();
		this.gbContactoAEntregar.ResumeLayout(false);
		this.gbContactoAEntregar.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvTransGeneradas).EndInit();
		this.groupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.rgvDetalleRequerimiento.MasterTemplate).EndInit();
		((System.ComponentModel.ISupportInitialize)this.rgvDetalleRequerimiento).EndInit();
		this.groupBox2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
