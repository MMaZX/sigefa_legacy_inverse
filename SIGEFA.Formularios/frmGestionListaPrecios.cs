using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmGestionListaPrecios : Office2007Form
{
	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	private clsAdmListaPrecio AdmLista = new clsAdmListaPrecio();

	private clsListaPrecio origen = new clsListaPrecio();

	private clsConsultasExternas ext = new clsConsultasExternas();

	public clsListaPrecio lista = new clsListaPrecio();

	public int Proceso = 0;

	private clsValidar ok = new clsValidar();

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	public int CodListaOrigen = 0;

	private IContainer components = null;

	private GroupBox groupBox1;

	private DataGridView dgvListaPrecios;

	private Label label3;

	private Label label2;

	private TextBox txtFiltro;

	private Label label1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnReporte;

	private Button btnEliminar;

	private TextBox txtCodigo;

	private Label label5;

	private Label label4;

	private GroupBox groupBox2;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private ImageList imageList1;

	private CheckBox cbPrecioProm;

	private Label label6;

	private CheckBox cbMargenProv;

	private Label label7;

	private TextBox txtMargen;

	private Label label9;

	private ComboBox cmbRedondeo;

	private CheckBox cbActualiza;

	private CustomValidator customValidator1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator4;

	private CheckBox cbRedondear;

	private Button btnCalularPrecios;

	private TextBox txtVariacion;

	private Label label8;

	private Label label14;

	private CustomValidator customValidator5;

	private CustomValidator customValidator2;

	private Label lbFormaPago;

	private LinkLabel linkLabel1;

	private CustomValidator customValidator6;

	public ComboBox cmbFormaPago;

	private TextBox txtNombre;

	public TextBox txtListaOrigen;

	public Label label15;

	private Button btnAnular;

	private ImageList imageList2;

	private DataGridViewTextBoxColumn codigo;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn decimales;

	private DataGridViewTextBoxColumn anulado;

	public frmGestionListaPrecios()
	{
		InitializeComponent();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Registro Nuevo";
		Proceso = 1;
		ext.limpiar(groupBox2.Controls);
	}

	private void CambiarEstados(bool Estado)
	{
		groupBox1.Visible = Estado;
		groupBox2.Visible = !Estado;
		linkLabel1.Enabled = Estado;
		btnGuardar.Enabled = !Estado;
		btnNuevo.Enabled = Estado;
		btnEditar.Enabled = Estado;
		btnEliminar.Enabled = Estado;
		btnAnular.Enabled = Estado;
		btnReporte.Enabled = Estado;
		txtCodigo.Text = "";
		label15.Text = "";
		cmbRedondeo.SelectedIndex = -1;
		superValidator1.Validate();
	}

	private void txtFiltro_TextChanged(object sender, EventArgs e)
	{
		try
		{
			if (txtFiltro.Text.Length >= 2)
			{
				data.Filter = $"[{label3.Text.Trim()}] like '*{txtFiltro.Text.Trim()}*'";
			}
			else
			{
				data.Filter = string.Empty;
			}
		}
		catch (Exception)
		{
		}
	}

	private void frmListaPrecios_Load(object sender, EventArgs e)
	{
		CargaFormaPagos();
		if (Proceso == 3)
		{
			CargaListas();
			bloquearbotones();
		}
		else
		{
			CargaListas();
			label2.Text = "Codigo";
			label3.Text = "codigo";
		}
	}

	private void CargaFormaPagos()
	{
		cmbFormaPago.DataSource = AdmPago.CargaFormaPagos(1);
		cmbFormaPago.DisplayMember = "descripcion";
		cmbFormaPago.ValueMember = "codFormaPago";
		cmbFormaPago.SelectedIndex = 0;
	}

	private void bloquearbotones()
	{
		btnNuevo.Visible = false;
		btnEditar.Visible = false;
		btnEliminar.Visible = false;
		btnAnular.Visible = false;
		btnReporte.Visible = false;
		btnGuardar.Text = "Aceptar";
		btnGuardar.ImageIndex = 6;
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		CambiarEstados(Estado: false);
		groupBox2.Text = "Editar Registro";
		Proceso = 2;
		CargaLista();
	}

	private void CargaLista()
	{
		lista = AdmLista.CargaListaPrecio(lista.CodListaPrecio);
		if (lista != null)
		{
			txtCodigo.Text = lista.CodListaPrecio.ToString();
			txtNombre.Text = lista.Nombre;
			cbPrecioProm.Checked = lista.PrecioProm;
			if (!lista.PrecioProm)
			{
				txtListaOrigen.Text = lista.ListaOrigen.ToString();
				KeyPressEventArgs ee = new KeyPressEventArgs('\r');
				txtListaOrigen_KeyPress(txtListaOrigen, ee);
				txtVariacion.Text = lista.Variacion.ToString();
			}
			cbMargenProv.Checked = lista.MargenProv;
			if (!lista.MargenProv)
			{
				txtMargen.Text = lista.Margen.ToString();
			}
			cbRedondear.Checked = lista.Redondear;
			cmbFormaPago.SelectedValue = lista.CodFormaPago;
			if (lista.Redondear)
			{
				cmbRedondeo.SelectedIndex = lista.Decimales;
			}
			cbActualiza.Checked = lista.Update;
		}
		else
		{
			MessageBox.Show("No se encuentra la lista solicitada", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void CargaListas()
	{
		dgvListaPrecios.DataSource = data;
		data.DataSource = AdmLista.MuestraListas(frmLogin.iCodSucursal);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvListaPrecios.ClearSelection();
	}

	private void frmTransacciones_Shown(object sender, EventArgs e)
	{
		CargaListas();
		txtFiltro.Focus();
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		if (groupBox1.Visible)
		{
			Close();
			return;
		}
		Proceso = 0;
		CambiarEstados(Estado: true);
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (btnGuardar.Text == "Guardar")
		{
			if (!superValidator1.Validate() || Proceso == 0)
			{
				return;
			}
			lista.CodSucursal = frmLogin.iCodSucursal;
			lista.Nombre = txtNombre.Text;
			lista.PrecioProm = cbPrecioProm.Checked;
			if (!cbPrecioProm.Checked)
			{
				lista.ListaOrigen = CodListaOrigen;
				lista.Variacion = Convert.ToDouble(txtVariacion.Text);
			}
			else
			{
				lista.ListaOrigen = 0;
				lista.Variacion = 0.0;
			}
			lista.MargenProv = cbMargenProv.Checked;
			if (!cbMargenProv.Checked && cbMargenProv.Enabled)
			{
				lista.Margen = Convert.ToDouble(txtMargen.Text);
			}
			lista.Descuento1 = 0.0;
			lista.Descuento2 = 0.0;
			lista.Descuento3 = 0.0;
			lista.CodFormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
			lista.Redondear = cbRedondear.Checked;
			if (cbRedondear.Checked)
			{
				lista.Decimales = cmbRedondeo.SelectedIndex;
			}
			else
			{
				lista.Decimales = -1;
			}
			lista.Update = cbActualiza.Checked;
			if (Proceso == 1)
			{
				lista.CodUser = frmLogin.iCodUser;
				if (AdmLista.insert(lista))
				{
					if (AdmLista.GeneraLista(lista.CodListaPrecio, frmLogin.iCodAlmacen, lista.Decimales))
					{
						MessageBox.Show("Los datos se guardaron correctamente", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
					else
					{
						MessageBox.Show("No se generó correctamente la Lista de Precios", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					CambiarEstados(Estado: true);
					CargaListas();
					Proceso = 0;
				}
			}
			else if (Proceso == 2 && AdmLista.update(lista))
			{
				if (AdmLista.GeneraLista(lista.CodListaPrecio, frmLogin.iCodAlmacen, lista.Decimales))
				{
					MessageBox.Show("Los datos se actualizaron correctamente", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				else
				{
					MessageBox.Show("No se actualizó correctamente la Lista de Precios", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				CambiarEstados(Estado: true);
				CargaListas();
				Proceso = 0;
			}
		}
		else if (btnGuardar.Text == "Aceptar" && (Proceso == 3 || Proceso == 4))
		{
			Close();
		}
	}

	private void dgvTransacciones_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (base.Visible)
		{
			if (dgvListaPrecios.Rows.Count >= 1 && e.Row.Selected)
			{
				lista.CodListaPrecio = Convert.ToInt32(e.Row.Cells[codigo.Name].Value);
				lista.Decimales = Convert.ToInt32(e.Row.Cells[decimales.Name].Value);
				lista.Nombre = e.Row.Cells[descripcion.Name].Value.ToString();
				btnEditar.Enabled = true;
				btnEliminar.Enabled = true;
				btnAnular.Enabled = true;
			}
			else if (dgvListaPrecios.SelectedRows.Count == 0)
			{
				btnEditar.Enabled = false;
				btnEliminar.Enabled = false;
				btnAnular.Enabled = false;
			}
		}
	}

	private void dgvListaPrecios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			Close();
		}
	}

	private void dgvListaPrecios_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		label2.Text = dgvListaPrecios.Columns[e.ColumnIndex].HeaderText;
		label3.Text = dgvListaPrecios.Columns[e.ColumnIndex].DataPropertyName;
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
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

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvListaPrecios.CurrentRow.Index != -1 && lista.CodListaPrecio != 0)
		{
			DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Listas de Precios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult != DialogResult.No && AdmLista.delete(lista.CodListaPrecio))
			{
				MessageBox.Show("Los datos han sido eliminado correctamente", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListas();
			}
		}
	}

	private void dgvListaPrecios_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Proceso == 3)
		{
			btnGuardar.Enabled = true;
		}
		if (dgvListaPrecios.Rows.Count >= 1 && e.RowIndex != -1)
		{
			if (dgvListaPrecios.Rows[e.RowIndex].Cells[anulado.Name].Value.ToString() == "ACTIVO")
			{
				btnAnular.Text = "Anular";
				btnAnular.Enabled = true;
				btnAnular.ImageIndex = 4;
			}
			else
			{
				btnAnular.Text = "Activar";
				btnAnular.Enabled = true;
				btnAnular.ImageIndex = 6;
			}
		}
	}

	private void cbPrecioProm_CheckedChanged(object sender, EventArgs e)
	{
		if (cbPrecioProm.Checked)
		{
			txtVariacion.Enabled = false;
			txtListaOrigen.Enabled = false;
			cbMargenProv.Enabled = true;
			if (cbMargenProv.Checked)
			{
				txtMargen.Enabled = false;
				txtMargen.Text = "";
			}
			else
			{
				txtMargen.Enabled = true;
			}
			txtListaOrigen.Text = "";
			txtVariacion.Text = "";
		}
		else
		{
			cbMargenProv.Enabled = false;
			txtMargen.Enabled = false;
			txtListaOrigen.Enabled = true;
			txtVariacion.Enabled = true;
		}
	}

	private void btnReporte_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.tipo = 8;
		frm.ShowDialog();
	}

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Enabled)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void cbMargenProv_CheckedChanged(object sender, EventArgs e)
	{
		if (cbMargenProv.Checked)
		{
			txtMargen.Enabled = false;
			txtMargen.Text = "";
		}
		else
		{
			txtMargen.Enabled = true;
		}
	}

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Enabled)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
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

	private void checkBox1_CheckedChanged(object sender, EventArgs e)
	{
		if (cbRedondear.Checked)
		{
			cmbRedondeo.Enabled = true;
		}
		else
		{
			cmbRedondeo.Enabled = false;
		}
	}

	private void btnCalularPrecios_Click(object sender, EventArgs e)
	{
		if (lista != null)
		{
			if (AdmLista.GeneraLista(lista.CodListaPrecio, frmLogin.iCodAlmacen, lista.Decimales))
			{
				MessageBox.Show("Se generó la lista de precios correctamente", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			else
			{
				MessageBox.Show("No se generó correctamente la Lista de Precios", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
	}

	private void groupBox2_Enter(object sender, EventArgs e)
	{
	}

	private void customValidator5_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (e.ControlToValidate.Enabled)
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
		else
		{
			e.IsValid = true;
		}
	}

	private void txtListaOrigen_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.enteros(e);
		if (e.KeyChar == '\r' && txtListaOrigen.Text != "")
		{
			if (BuscaListaPrecio())
			{
				txtVariacion.Focus();
			}
			else
			{
				MessageBox.Show("El codigo buscado no existe", "Lista de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private bool BuscaListaPrecio()
	{
		origen = AdmLista.CargaListaPrecio(Convert.ToInt32(txtListaOrigen.Text));
		if (origen != null)
		{
			CodListaOrigen = origen.CodListaPrecio;
			txtListaOrigen.Text = origen.CodListaPrecio.ToString();
			label15.Text = origen.Nombre;
			label15.Visible = true;
			return true;
		}
		CodListaOrigen = 0;
		txtListaOrigen.Text = "";
		label15.Text = "";
		label15.Visible = false;
		return false;
	}

	private void txtListaOrigen_Leave(object sender, EventArgs e)
	{
		if (txtListaOrigen.Text != "")
		{
			if (BuscaListaPrecio())
			{
				txtVariacion.Focus();
			}
			else
			{
				MessageBox.Show("El codigo buscado no existe", "Lista de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void txtVariacion_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void txtMargen_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.decimalesNegativos(sender, e);
	}

	private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		if (dgvListaPrecios.Rows.Count >= 1 && dgvListaPrecios.SelectedRows.Count > 0)
		{
			frmListaPreciosProductos listapro = new frmListaPreciosProductos();
			if (Application.OpenForms["frmListaPreciosProductos"] != null)
			{
				Application.OpenForms["frmListaPreciosProductos"].Activate();
				return;
			}
			foreach (DataGridViewRow row in (IEnumerable)dgvListaPrecios.Rows)
			{
				listapro.decimales = lista.Decimales;
				listapro.txtNombre.Text = lista.Nombre;
				listapro.codlista = lista.CodListaPrecio;
			}
			listapro.ShowDialog();
		}
		else
		{
			MessageBox.Show("Seleccione Lista");
		}
	}

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0)
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

	private void txtListaOrigen_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmListaPrecios"] != null)
			{
				Application.OpenForms["frmListaPrecios"].Activate();
				return;
			}
			frmListaPrecios form = new frmListaPrecios();
			form.ShowDialog();
		}
	}

	private void btnAnular_Click(object sender, EventArgs e)
	{
		if (dgvListaPrecios.Rows.Count < 1 || dgvListaPrecios.CurrentRow == null)
		{
			return;
		}
		DataGridViewRow row = dgvListaPrecios.CurrentRow;
		if (btnAnular.Text == "Anular")
		{
			if (dgvListaPrecios.CurrentRow.Index != -1 && lista.CodListaPrecio != 0)
			{
				DialogResult dlgResult = MessageBox.Show("¿Esta seguro que desea anular la Lista de Precio seleccionada?", "Listas de Precios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult != DialogResult.No && AdmLista.anular(frmLogin.iCodSucursal, lista.CodListaPrecio))
				{
					MessageBox.Show("La lista de precio ha sido anulada correctamente", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					CargaListas();
				}
			}
		}
		else if (btnAnular.Text == "Activar" && dgvListaPrecios.Rows.Count >= 1 && dgvListaPrecios.CurrentRow.Index != -1 && lista.CodListaPrecio != 0)
		{
			DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea activar esta Lista de Precio", "Listas de Precios", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult2 != DialogResult.No && AdmLista.activar(frmLogin.iCodSucursal, lista.CodListaPrecio))
			{
				MessageBox.Show("La lista de precio ha sido activada correctamente", "Listas de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				CargaListas();
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
		this.components = new System.ComponentModel.Container();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmGestionListaPrecios));
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.dgvListaPrecios = new System.Windows.Forms.DataGridView();
		this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.decimales = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.anulado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.txtFiltro = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnAnular = new System.Windows.Forms.Button();
		this.imageList2 = new System.Windows.Forms.ImageList(this.components);
		this.linkLabel1 = new System.Windows.Forms.LinkLabel();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.txtCodigo = new System.Windows.Forms.TextBox();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.label4 = new System.Windows.Forms.Label();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.cmbFormaPago = new System.Windows.Forms.ComboBox();
		this.lbFormaPago = new System.Windows.Forms.Label();
		this.label15 = new System.Windows.Forms.Label();
		this.label14 = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.txtVariacion = new System.Windows.Forms.TextBox();
		this.btnCalularPrecios = new System.Windows.Forms.Button();
		this.cbRedondear = new System.Windows.Forms.CheckBox();
		this.cbActualiza = new System.Windows.Forms.CheckBox();
		this.cmbRedondeo = new System.Windows.Forms.ComboBox();
		this.label9 = new System.Windows.Forms.Label();
		this.cbMargenProv = new System.Windows.Forms.CheckBox();
		this.label7 = new System.Windows.Forms.Label();
		this.txtMargen = new System.Windows.Forms.TextBox();
		this.label6 = new System.Windows.Forms.Label();
		this.txtListaOrigen = new System.Windows.Forms.TextBox();
		this.cbPrecioProm = new System.Windows.Forms.CheckBox();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvListaPrecios).BeginInit();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		base.SuspendLayout();
		this.groupBox1.AccessibleDescription = "";
		this.groupBox1.Controls.Add(this.dgvListaPrecios);
		this.groupBox1.Controls.Add(this.label3);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtFiltro);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(554, 287);
		this.groupBox1.TabIndex = 19;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Listas Precios";
		this.dgvListaPrecios.AllowUserToAddRows = false;
		this.dgvListaPrecios.AllowUserToDeleteRows = false;
		this.dgvListaPrecios.AllowUserToResizeColumns = false;
		this.dgvListaPrecios.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvListaPrecios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
		this.dgvListaPrecios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvListaPrecios.Columns.AddRange(this.codigo, this.descripcion, this.estado, this.coduser, this.fecha, this.decimales, this.anulado);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dgvListaPrecios.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvListaPrecios.Location = new System.Drawing.Point(6, 45);
		this.dgvListaPrecios.MultiSelect = false;
		this.dgvListaPrecios.Name = "dgvListaPrecios";
		this.dgvListaPrecios.ReadOnly = true;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dgvListaPrecios.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.dgvListaPrecios.RowHeadersVisible = false;
		this.dgvListaPrecios.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvListaPrecios.Size = new System.Drawing.Size(542, 264);
		this.dgvListaPrecios.TabIndex = 8;
		this.dgvListaPrecios.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvListaPrecios_CellDoubleClick);
		this.dgvListaPrecios.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvListaPrecios_ColumnHeaderMouseClick);
		this.dgvListaPrecios.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvListaPrecios_CellClick);
		this.dgvListaPrecios.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvTransacciones_RowStateChanged);
		this.codigo.DataPropertyName = "codListaPrecio";
		this.codigo.HeaderText = "Codigo";
		this.codigo.Name = "codigo";
		this.codigo.ReadOnly = true;
		this.codigo.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codigo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.DataPropertyName = "nombre";
		this.descripcion.HeaderText = "Nombre";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.descripcion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.descripcion.Width = 330;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Visible = false;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coduser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coduser.Visible = false;
		this.fecha.DataPropertyName = "fecharegistro";
		this.fecha.HeaderText = "FechaReg";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fecha.Visible = false;
		this.decimales.DataPropertyName = "decimales";
		this.decimales.HeaderText = "decimales";
		this.decimales.Name = "decimales";
		this.decimales.ReadOnly = true;
		this.decimales.Visible = false;
		this.anulado.DataPropertyName = "anulado";
		this.anulado.HeaderText = "Estado";
		this.anulado.Name = "anulado";
		this.anulado.ReadOnly = true;
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(427, 22);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(12, 13);
		this.label3.TabIndex = 6;
		this.label3.Text = "x";
		this.label3.Visible = false;
		this.label2.AutoSize = true;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(96, 20);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(17, 16);
		this.label2.TabIndex = 5;
		this.label2.Text = "X";
		this.txtFiltro.Location = new System.Drawing.Point(200, 19);
		this.txtFiltro.Name = "txtFiltro";
		this.txtFiltro.Size = new System.Drawing.Size(221, 20);
		this.txtFiltro.TabIndex = 4;
		this.txtFiltro.TextChanged += new System.EventHandler(txtFiltro_TextChanged);
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(25, 22);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(65, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Buscar Por :";
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.groupBox3.Controls.Add(this.btnAnular);
		this.groupBox3.Controls.Add(this.linkLabel1);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Location = new System.Drawing.Point(11, 305);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(561, 63);
		this.groupBox3.TabIndex = 18;
		this.groupBox3.TabStop = false;
		this.btnAnular.Enabled = false;
		this.btnAnular.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnAnular.ImageKey = "DeleteRed.png";
		this.btnAnular.ImageList = this.imageList2;
		this.btnAnular.Location = new System.Drawing.Point(236, 25);
		this.btnAnular.Name = "btnAnular";
		this.btnAnular.Size = new System.Drawing.Size(76, 32);
		this.btnAnular.TabIndex = 33;
		this.btnAnular.Text = "Anular";
		this.btnAnular.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnAnular.UseVisualStyleBackColor = true;
		this.btnAnular.Click += new System.EventHandler(btnAnular_Click);
		this.imageList2.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList2.ImageStream");
		this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList2.Images.SetKeyName(0, "exit.png");
		this.imageList2.Images.SetKeyName(1, "pedido.png");
		this.imageList2.Images.SetKeyName(2, "carrito.png");
		this.imageList2.Images.SetKeyName(3, "delete-file-icon.png");
		this.imageList2.Images.SetKeyName(4, "DeleteRed.png");
		this.imageList2.Images.SetKeyName(5, "document_delete.png");
		this.imageList2.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList2.Images.SetKeyName(7, "document_print.png");
		this.linkLabel1.AutoSize = true;
		this.linkLabel1.Location = new System.Drawing.Point(169, 9);
		this.linkLabel1.Name = "linkLabel1";
		this.linkLabel1.Size = new System.Drawing.Size(101, 13);
		this.linkLabel1.TabIndex = 31;
		this.linkLabel1.TabStop = true;
		this.linkLabel1.Text = "Modificar Productos";
		this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(linkLabel1_LinkClicked);
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(487, 25);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 26;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.imageList1.Images.SetKeyName(6, "OK_Verde.png");
		this.imageList1.Images.SetKeyName(7, "converseen-24.png");
		this.imageList1.Images.SetKeyName(8, "checkmark.png");
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(6, 25);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(71, 32);
		this.btnNuevo.TabIndex = 20;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnEditar.Enabled = false;
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(83, 25);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 21;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(320, 25);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 32);
		this.btnReporte.TabIndex = 23;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnEliminar.Enabled = false;
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(155, 25);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 22;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(404, 25);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 24;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.txtCodigo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtCodigo.Enabled = false;
		this.txtCodigo.Location = new System.Drawing.Point(96, 22);
		this.txtCodigo.Name = "txtCodigo";
		this.txtCodigo.Size = new System.Drawing.Size(71, 20);
		this.txtCodigo.TabIndex = 1;
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Location = new System.Drawing.Point(96, 48);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.Size = new System.Drawing.Size(312, 20);
		this.txtNombre.TabIndex = 2;
		this.superValidator1.SetValidator1(this.txtNombre, this.customValidator1);
		this.txtNombre.WordWrap = false;
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(21, 25);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(46, 13);
		this.label5.TabIndex = 6;
		this.label5.Text = "Codigo :";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(21, 51);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(50, 13);
		this.label4.TabIndex = 7;
		this.label4.Text = "Nombre :";
		this.groupBox2.Controls.Add(this.cmbFormaPago);
		this.groupBox2.Controls.Add(this.lbFormaPago);
		this.groupBox2.Controls.Add(this.label15);
		this.groupBox2.Controls.Add(this.label14);
		this.groupBox2.Controls.Add(this.label8);
		this.groupBox2.Controls.Add(this.txtVariacion);
		this.groupBox2.Controls.Add(this.btnCalularPrecios);
		this.groupBox2.Controls.Add(this.cbRedondear);
		this.groupBox2.Controls.Add(this.cbActualiza);
		this.groupBox2.Controls.Add(this.cmbRedondeo);
		this.groupBox2.Controls.Add(this.label9);
		this.groupBox2.Controls.Add(this.cbMargenProv);
		this.groupBox2.Controls.Add(this.label7);
		this.groupBox2.Controls.Add(this.txtMargen);
		this.groupBox2.Controls.Add(this.label6);
		this.groupBox2.Controls.Add(this.txtListaOrigen);
		this.groupBox2.Controls.Add(this.cbPrecioProm);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label5);
		this.groupBox2.Controls.Add(this.txtNombre);
		this.groupBox2.Controls.Add(this.txtCodigo);
		this.groupBox2.Location = new System.Drawing.Point(11, 3);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(560, 296);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Lista Precio";
		this.groupBox2.Visible = false;
		this.groupBox2.Enter += new System.EventHandler(groupBox2_Enter);
		this.cmbFormaPago.FormattingEnabled = true;
		this.cmbFormaPago.Location = new System.Drawing.Point(146, 179);
		this.cmbFormaPago.Name = "cmbFormaPago";
		this.cmbFormaPago.Size = new System.Drawing.Size(168, 21);
		this.cmbFormaPago.TabIndex = 30;
		this.superValidator1.SetValidator1(this.cmbFormaPago, this.customValidator6);
		this.lbFormaPago.AutoSize = true;
		this.lbFormaPago.Location = new System.Drawing.Point(36, 182);
		this.lbFormaPago.Name = "lbFormaPago";
		this.lbFormaPago.Size = new System.Drawing.Size(82, 13);
		this.lbFormaPago.TabIndex = 29;
		this.lbFormaPago.Text = "Forma de Pago:";
		this.label15.AutoSize = true;
		this.label15.Location = new System.Drawing.Point(325, 77);
		this.label15.Name = "label15";
		this.label15.Size = new System.Drawing.Size(63, 13);
		this.label15.TabIndex = 28;
		this.label15.Text = "Lista Origen";
		this.label15.Visible = false;
		this.label14.AutoSize = true;
		this.label14.Location = new System.Drawing.Point(320, 103);
		this.label14.Name = "label14";
		this.label14.Size = new System.Drawing.Size(15, 13);
		this.label14.TabIndex = 27;
		this.label14.Text = "%";
		this.label8.AutoSize = true;
		this.label8.Location = new System.Drawing.Point(169, 103);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(57, 13);
		this.label8.TabIndex = 26;
		this.label8.Text = "Variación :";
		this.txtVariacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtVariacion.Location = new System.Drawing.Point(244, 100);
		this.txtVariacion.Name = "txtVariacion";
		this.txtVariacion.Size = new System.Drawing.Size(70, 20);
		this.txtVariacion.TabIndex = 5;
		this.superValidator1.SetValidator1(this.txtVariacion, this.customValidator5);
		this.txtVariacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtVariacion_KeyPress);
		this.btnCalularPrecios.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnCalularPrecios.ImageIndex = 8;
		this.btnCalularPrecios.ImageList = this.imageList1;
		this.btnCalularPrecios.Location = new System.Drawing.Point(356, 255);
		this.btnCalularPrecios.Name = "btnCalularPrecios";
		this.btnCalularPrecios.Size = new System.Drawing.Size(115, 32);
		this.btnCalularPrecios.TabIndex = 14;
		this.btnCalularPrecios.Text = "Calcula Precios";
		this.btnCalularPrecios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCalularPrecios.UseVisualStyleBackColor = true;
		this.btnCalularPrecios.Click += new System.EventHandler(btnCalularPrecios_Click);
		this.cbRedondear.AutoSize = true;
		this.cbRedondear.Location = new System.Drawing.Point(24, 152);
		this.cbRedondear.Name = "cbRedondear";
		this.cbRedondear.Size = new System.Drawing.Size(85, 17);
		this.cbRedondear.TabIndex = 11;
		this.cbRedondear.Text = "Redondear :";
		this.cbRedondear.UseVisualStyleBackColor = true;
		this.cbRedondear.CheckedChanged += new System.EventHandler(checkBox1_CheckedChanged);
		this.cbActualiza.AutoSize = true;
		this.cbActualiza.Location = new System.Drawing.Point(24, 206);
		this.cbActualiza.Name = "cbActualiza";
		this.cbActualiza.Size = new System.Drawing.Size(145, 17);
		this.cbActualiza.TabIndex = 13;
		this.cbActualiza.Text = "Actualización Automática";
		this.cbActualiza.UseVisualStyleBackColor = true;
		this.cmbRedondeo.Enabled = false;
		this.cmbRedondeo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.cmbRedondeo.FormattingEnabled = true;
		this.cmbRedondeo.Items.AddRange(new object[5] { "Entero", "1 Decimal", "2 Decimales", "3 Decimales", "4 Decimales" });
		this.cmbRedondeo.Location = new System.Drawing.Point(146, 151);
		this.cmbRedondeo.Name = "cmbRedondeo";
		this.cmbRedondeo.Size = new System.Drawing.Size(168, 20);
		this.cmbRedondeo.TabIndex = 12;
		this.superValidator1.SetValidator1(this.cmbRedondeo, this.customValidator4);
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(320, 130);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(15, 13);
		this.label9.TabIndex = 15;
		this.label9.Text = "%";
		this.cbMargenProv.AutoSize = true;
		this.cbMargenProv.Enabled = false;
		this.cbMargenProv.Location = new System.Drawing.Point(24, 129);
		this.cbMargenProv.Name = "cbMargenProv";
		this.cbMargenProv.Size = new System.Drawing.Size(114, 17);
		this.cbMargenProv.TabIndex = 6;
		this.cbMargenProv.Text = "Margen Proovedor";
		this.cbMargenProv.UseVisualStyleBackColor = true;
		this.cbMargenProv.CheckedChanged += new System.EventHandler(cbMargenProv_CheckedChanged);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(169, 129);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(49, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Margen :";
		this.txtMargen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtMargen.Enabled = false;
		this.txtMargen.Location = new System.Drawing.Point(244, 126);
		this.txtMargen.Name = "txtMargen";
		this.txtMargen.Size = new System.Drawing.Size(70, 20);
		this.txtMargen.TabIndex = 7;
		this.superValidator1.SetValidator1(this.txtMargen, this.customValidator3);
		this.txtMargen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtMargen_KeyPress);
		this.label6.AutoSize = true;
		this.label6.Location = new System.Drawing.Point(169, 77);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(69, 13);
		this.label6.TabIndex = 10;
		this.label6.Text = "Lista Origen :";
		this.txtListaOrigen.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtListaOrigen.Location = new System.Drawing.Point(244, 74);
		this.txtListaOrigen.Name = "txtListaOrigen";
		this.txtListaOrigen.Size = new System.Drawing.Size(70, 20);
		this.txtListaOrigen.TabIndex = 4;
		this.superValidator1.SetValidator1(this.txtListaOrigen, this.customValidator2);
		this.txtListaOrigen.KeyDown += new System.Windows.Forms.KeyEventHandler(txtListaOrigen_KeyDown);
		this.txtListaOrigen.Leave += new System.EventHandler(txtListaOrigen_Leave);
		this.txtListaOrigen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtListaOrigen_KeyPress);
		this.cbPrecioProm.AutoSize = true;
		this.cbPrecioProm.Location = new System.Drawing.Point(24, 77);
		this.cbPrecioProm.Name = "cbPrecioProm";
		this.cbPrecioProm.Size = new System.Drawing.Size(103, 17);
		this.cbPrecioProm.TabIndex = 3;
		this.cbPrecioProm.Text = "Precio Promedio";
		this.cbPrecioProm.UseVisualStyleBackColor = true;
		this.cbPrecioProm.CheckedChanged += new System.EventHandler(cbPrecioProm_CheckedChanged);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.customValidator1.ErrorMessage = "Ingrese el nombre para la lista.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator6.ErrorMessage = "Ingrese Forma Pago.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator5.ErrorMessage = "Ingrese el porcentaje de variación.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator4.ErrorMessage = "Seleccione una opcion.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese el margen de ganancia.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Escoja una lista de origen.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(578, 367);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmGestionListaPrecios";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Gestion Listas de Precios";
		base.Load += new System.EventHandler(frmListaPrecios_Load);
		base.Shown += new System.EventHandler(frmTransacciones_Shown);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvListaPrecios).EndInit();
		this.groupBox3.ResumeLayout(false);
		this.groupBox3.PerformLayout();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		base.ResumeLayout(false);
	}
}
