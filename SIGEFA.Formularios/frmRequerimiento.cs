using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmRequerimiento : Office2007Form
{
	public List<clsDetalleRequerimiento> detalle1 = new List<clsDetalleRequerimiento>();

	private clsDetalleRequerimiento deta1 = new clsDetalleRequerimiento();

	private clsAdmSerie Admser = new clsAdmSerie();

	private clsSerie ser = new clsSerie();

	private clsAdmRequerimiento AdmRequer = new clsAdmRequerimiento();

	private clsRequerimiento Ord = new clsRequerimiento();

	public List<int> config = new List<int>();

	public List<clsDetalleRequerimiento> detalle = new List<clsDetalleRequerimiento>();

	public int CodProveedor;

	public int CodRequer;

	public int CodDetalleRequer;

	public int CodProduct;

	private bool Validacion = true;

	public int Proceso = 0;

	public int Procede = 0;

	public int proc = 0;

	public int proce = 0;

	public int Tipo;

	public int CodDocumento;

	public int CodSerie;

	public int num;

	public List<int> codProd = new List<int>();

	public DataTable data = new DataTable();

	private clsAdmTipoDocumento Admdoc = new clsAdmTipoDocumento();

	private clsTipoDocumento doc = new clsTipoDocumento();

	private List<int> eliminados = new List<int>();

	private IContainer components = null;

	private ImageList imageList1;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnNuevo;

	private Button btnGuardar;

	private Button btnEditar;

	private Button btnEliminar;

	private GroupBox groupBox2;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator7;

	private CustomValidator customValidator6;

	private CustomValidator customValidator5;

	private CustomValidator customValidator4;

	private CustomValidator customValidator2;

	private CustomValidator customValidator1;

	private GroupBox groupBox1;

	private DateTimePicker dtpFechaOrden;

	private Button btnDetalle;

	private Label label9;

	private TextBox txtOrdenCompra;

	private Label label7;

	private Button btnImprimir;

	private Label lblFechaOrden;

	private TextBox txtNumero;

	private TextBox txtCodDoc;

	private Label label1;

	private TextBox txtDocRef;

	private Label label5;

	private Label lbDocumento;

	private TextBox txtcodserie;

	public DataGridView dgvDetalle;

	private TextBox txtcomentario;

	public TextBox txtSerie;

	private SuperValidator superValidator2;

	private ErrorProvider errorProvider2;

	private Highlighter highlighter2;

	private CustomValidator customValidator8;

	private CustomValidator customValidator9;

	private TextBox txtComentarioRechazado;

	private Label label2;

	private DataGridViewTextBoxColumn coddetalle;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn descripcion;

	private DataGridViewTextBoxColumn codunidad;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn cantidad;

	private DataGridViewTextBoxColumn coduser;

	private DataGridViewTextBoxColumn fecharegistro;

	public frmRequerimiento()
	{
		InitializeComponent();
	}

	private bool BuscaSerie()
	{
		ser = Admser.BuscaSerie(txtSerie.Text, CodDocumento, frmLogin.iCodAlmacen);
		if (ser != null)
		{
			CodSerie = ser.CodSerie;
			num = ser.Numeracion;
			return true;
		}
		CodSerie = 0;
		num = 0;
		return false;
	}

	private void txtSerie_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmSerie"] != null)
			{
				Application.OpenForms["frmSerie"].Activate();
				return;
			}
			frmSerie form = new frmSerie();
			form.Proceso = 3;
			form.DocSeleccionado = CodDocumento;
			form.ShowDialog();
			ser = form.ser;
			CodSerie = ser.CodSerie;
			num = ser.Numeracion;
			if (CodSerie != 0)
			{
				txtcodserie.Text = ser.CodSerie.ToString();
				txtSerie.Text = ser.Serie;
			}
			if (CodSerie != 0)
			{
				ProcessTabKey(forward: true);
			}
		}
		else
		{
			btnGuardar.Enabled = true;
		}
	}

	private void txtComentario_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtSerie_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtSerie_Leave(object sender, EventArgs e)
	{
	}

	private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void txtNumero_Leave(object sender, EventArgs e)
	{
		if (txtNumero.Text == "")
		{
			txtSerie.Focus();
		}
		else if (Validacion)
		{
			btnDetalle.Enabled = true;
		}
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnDetalle_Click(object sender, EventArgs e)
	{
		codProd.Clear();
		RecorreDetalle();
		if (Application.OpenForms["frmDetalleGuia"] != null)
		{
			Application.OpenForms["frmDetalleGuia"].Activate();
			return;
		}
		frmDetalleGuia form = new frmDetalleGuia();
		form.Procede = 11;
		form.Proceso = 1;
		proce = 1;
		form.Text = "Detalle de Productos";
		if (dgvDetalle.Rows.Count > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				codProd.Add(Convert.ToInt32(row.Cells[codproducto.Name].Value));
			}
		}
		else
		{
			codProd.Add(0);
		}
		form.ShowDialog();
	}

	private void btnNuevo_Click(object sender, EventArgs e)
	{
		codProd.Clear();
		if (Application.OpenForms["frmDetalleGuia"] != null)
		{
			Application.OpenForms["frmDetalleGuia"].Activate();
			return;
		}
		frmDetalleGuia form = new frmDetalleGuia();
		form.Procede = 11;
		form.Proceso = 1;
		form.Text = "Detalle de Productos";
		if (btnNuevo.Text == "Agregar")
		{
			proce = 3;
		}
		else
		{
			proce = 1;
		}
		if (dgvDetalle.Rows.Count > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalle.Rows)
			{
				codProd.Add(Convert.ToInt32(row.Cells[codproducto.Name].Value));
			}
		}
		else
		{
			codProd.Add(0);
		}
		form.ShowDialog();
	}

	private void frmOrdenCompra_Load(object sender, EventArgs e)
	{
		CodDocumento = 12;
		doc = Admdoc.CargaTipoDocumento(CodDocumento);
		txtCodDoc.Text = doc.CodTipoDocumento.ToString();
		txtDocRef.Text = doc.Sigla;
		lbDocumento.Text = doc.Descripcion;
		if (txtDocRef.Text == "")
		{
			txtSerie.Focus();
		}
		if (Proceso == 1)
		{
		}
		if (Proceso == 2)
		{
			CargaOrdenCompra();
		}
		else if (Proceso == 3)
		{
			CargaOrdenCompra();
			sololectura(estado: true);
		}
		else if (Proceso == 4)
		{
			CargaOrdenCompra();
			sololectura(estado: true);
		}
		else if (Proceso == 5)
		{
			CargaOrdenCompra();
			sololectura(estado: true);
		}
	}

	private void sololectura(bool estado)
	{
		if (Tipo == 1)
		{
			btnNuevo.Text = "Agregar";
			btnEditar.Enabled = estado;
			btnNuevo.Enabled = estado;
			btnEliminar.Enabled = estado;
			btnGuardar.Enabled = estado;
		}
		else
		{
			btnEditar.Enabled = !estado;
			btnNuevo.Enabled = !estado;
			btnEliminar.Enabled = !estado;
			btnGuardar.Enabled = !estado;
		}
		txtDocRef.ReadOnly = !estado;
		txtSerie.ReadOnly = !estado;
		btnDetalle.Enabled = !estado;
		btnImprimir.Visible = estado;
	}

	private void CargaOrdenCompra()
	{
		try
		{
			Ord = AdmRequer.CargaRequerimiento(Convert.ToInt32(CodRequer));
			if (Ord != null)
			{
				txtOrdenCompra.Text = Ord.CodRequerimiento.ToString().PadLeft(11, '0');
				if (txtDocRef.Enabled)
				{
					CodDocumento = Ord.CodTipoDocumento;
					txtDocRef.Text = Ord.SiglaDocumento;
					lbDocumento.Text = Ord.DescripcionDocumento;
				}
				txtSerie.Text = Ord.Serie;
				txtNumero.Text = Ord.NumDoc;
				CodSerie = Ord.CodSerie;
				dtpFechaOrden.Value = Ord.FechaOrden;
				txtcomentario.Text = Ord.Comentario;
				txtComentarioRechazado.Text = Ord.comentarioRechazado;
				CargaDetalle();
			}
			else
			{
				MessageBox.Show("El documento solicitado no existe", "Orden de Compra", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				frmRequerimiento frm = new frmRequerimiento();
				frm.Close();
			}
		}
		catch (Exception)
		{
		}
	}

	private void CargaDetalle()
	{
		dgvDetalle.DataSource = AdmRequer.CargaDetalle(Convert.ToInt32(CodRequer));
		data = (DataTable)dgvDetalle.DataSource;
		RecorreDetalle();
		Ord.Detalle = detalle;
	}

	private void txtCodProv_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.F1)
		{
			if (Application.OpenForms["frmProveedoresLista"] != null)
			{
				Application.OpenForms["frmProveedoresLista"].Activate();
				return;
			}
			frmProveedoresLista form = new frmProveedoresLista();
			form.Proceso = 3;
			form.Procede = Procede;
		}
	}

	private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r')
		{
			ProcessTabKey(forward: true);
		}
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		if (!superValidator2.Validate() || Proceso == 0)
		{
			return;
		}
		Ord.CodAlmacen = frmLogin.iCodAlmacen;
		Ord.Comentario = txtcomentario.Text;
		Ord.CodTipoDocumento = Convert.ToInt32(txtCodDoc.Text);
		ser = Admser.BuscaSerie(txtSerie.Text, CodDocumento, frmLogin.iCodAlmacen);
		Ord.CodSerie = CodSerie;
		Ord.FechaOrden = dtpFechaOrden.Value.Date;
		Ord.CodUser = frmLogin.iCodUser;
		if (Proceso == 1)
		{
			if (!AdmRequer.insert(Ord))
			{
				return;
			}
			RecorreDetalle();
			if (detalle.Count > 0)
			{
				foreach (clsDetalleRequerimiento det in detalle)
				{
					AdmRequer.insertdetalle(det);
				}
			}
			btnGuardar.Enabled = false;
			MessageBox.Show("Los datos se guardaron correctamente", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			CodRequer = Ord.CodRequerimientoNuevo;
			CargaOrdenCompra();
			sololectura(estado: true);
		}
		else
		{
			if (Proceso != 3 || !AdmRequer.update(Ord))
			{
				return;
			}
			RecorreDetalle();
			if (detalle.Count <= 0)
			{
				return;
			}
			foreach (clsDetalleRequerimiento det2 in detalle)
			{
				if (det2.CodDetalleRequerimiento == 0)
				{
					AdmRequer.insertdetalle(det2);
				}
				else
				{
					AdmRequer.updatedetalle(det2);
				}
			}
			if (eliminados.Count > 0)
			{
				foreach (int x in eliminados)
				{
					AdmRequer.deletedetalle(x);
				}
			}
			MessageBox.Show("Los datos se actualizaron correctamente", "Requerimiento", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			Tipo = 2;
			CargaOrdenCompra();
			sololectura(estado: true);
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

	private void RecorreDetalle1()
	{
		detalle1.Clear();
		if (dgvDetalle.SelectedRows.Count <= 0)
		{
			return;
		}
		foreach (DataGridViewRow row in dgvDetalle.SelectedRows)
		{
			añadedetalle1(row);
		}
	}

	private void añadedetalle(DataGridViewRow fila)
	{
		clsDetalleRequerimiento deta = new clsDetalleRequerimiento();
		if (Proceso == 3)
		{
			deta.CodDetalleRequerimiento = Convert.ToInt32(fila.Cells[coddetalle.Name].Value.ToString());
			deta.CodRequerimiento = Convert.ToInt32(Ord.CodRequerimiento);
		}
		else
		{
			deta.CodRequerimiento = Convert.ToInt32(Ord.CodRequerimientoNuevo);
		}
		deta.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta.Unidad = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta.CodUser = frmLogin.iCodUser;
		detalle.Add(deta);
	}

	private void añadedetalle1(DataGridViewRow fila)
	{
		if (Proceso == 3)
		{
			deta1.CodDetalleRequerimiento = Convert.ToInt32(fila.Cells[coddetalle.Name].Value.ToString());
			deta1.CodRequerimiento = Convert.ToInt32(Ord.CodRequerimiento);
		}
		else
		{
			deta1.CodRequerimiento = Convert.ToInt32(Ord.CodRequerimientoNuevo);
		}
		deta1.CodProducto = Convert.ToInt32(fila.Cells[codproducto.Name].Value);
		deta1.Unidad = Convert.ToInt32(fila.Cells[codunidad.Name].Value);
		deta1.Cantidad = Convert.ToDouble(fila.Cells[cantidad.Name].Value);
		deta1.CodUser = frmLogin.iCodUser;
		detalle1.Add(deta1);
	}

	private void btnEditar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.Rows.Count > 0 && dgvDetalle.SelectedRows.Count > 0)
		{
			DataGridViewRow row = dgvDetalle.SelectedRows[0];
			if (Application.OpenForms["frmDetalleGuia"] != null)
			{
				Application.OpenForms["frmDetalleGuia"].Activate();
				return;
			}
			frmDetalleGuia form = new frmDetalleGuia();
			form.Procede = 11;
			proce = 2;
			form.Proceso = 3;
			form.txtReferencia.Text = row.Cells[referencia.Name].Value.ToString();
			form.txtUnidad.Text = row.Cells[unidad.Name].Value.ToString();
			form.txtReferencia.Enabled = false;
			form.txtCodUnidad.Text = row.Cells[codunidad.Name].Value.ToString();
			form.cmbUnidad.SelectedValue = Convert.ToInt32(row.Cells[codunidad.Name].Value);
			form.txtDescripcion.Text = row.Cells[descripcion.Name].Value.ToString();
			form.txtCantidad.Focus();
			form.btnGuardar.Enabled = true;
			RecorreDetalle1();
			form.detalle = deta1;
			form.txtCodigo.Text = row.Cells[codproducto.Name].Value.ToString();
			form.txtCantidad.Text = row.Cells[cantidad.Name].Value.ToString();
			form.ShowDialog();
		}
	}

	private void btnEliminar_Click(object sender, EventArgs e)
	{
		if (dgvDetalle.SelectedRows.Count > 0 && dgvDetalle.Rows.Count >= 2)
		{
			if (Proceso == 1)
			{
				DialogResult dlgResult = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Requerimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult != DialogResult.No)
				{
					codProd.Remove(0);
					codProd.Remove(Convert.ToInt32(dgvDetalle.CurrentRow.Cells[codproducto.Name].Value));
					dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
				}
			}
			else
			{
				if (Proceso != 3)
				{
					return;
				}
				DialogResult dlgResult2 = MessageBox.Show("Esta seguro que desea eliminar los datos definitivamente", "Requerimiento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dlgResult2 != DialogResult.No)
				{
					if (CodDetalleRequer != 0)
					{
						eliminados.Add(CodDetalleRequer);
					}
					codProd.Remove(0);
					codProd.Remove(CodProduct);
					dgvDetalle.Rows.Remove(dgvDetalle.CurrentRow);
				}
			}
		}
		else
		{
			MessageBox.Show("Verifique, El Detalle no puede ser vacio");
		}
	}

	private void customValidator1_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator2_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator3_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator4_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator5_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0 && e.ControlToValidate.Visible)
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

	private void customValidator7_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		ComboBox c = (ComboBox)e.ControlToValidate;
		if (c.Enabled)
		{
			if (Proceso != 0 && c.Visible)
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

	private void txtcomentario_KeyDown(object sender, KeyEventArgs e)
	{
		if (txtcodserie.Text != "")
		{
			btnGuardar.Enabled = true;
		}
	}

	private void txtcomentario_TextChanged(object sender, EventArgs e)
	{
	}

	private void customValidator8_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator9_ValidateValue(object sender, ValidateValueEventArgs e)
	{
		if (Proceso != 0)
		{
			if (dgvDetalle.Rows.Count > 0)
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

	private void btnImprimir_Click(object sender, EventArgs e)
	{
		frmrptCotizacion frm = new frmrptCotizacion();
		frm.CodCotizacion = Convert.ToInt32(txtOrdenCompra.Text);
		frm.tipo = 2;
		frm.ShowDialog();
	}

	private void dgvDetalle_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
	{
		if (Proceso == 3 && dgvDetalle.Rows.Count >= 1 && e.Row.Selected)
		{
			CodDetalleRequer = Convert.ToInt32(e.Row.Cells[coddetalle.Name].Value);
			CodProduct = Convert.ToInt32(e.Row.Cells[codproducto.Name].Value);
		}
	}

	private void dgvDetalle_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmRequerimiento));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnImprimir = new System.Windows.Forms.Button();
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnNuevo = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnEditar = new System.Windows.Forms.Button();
		this.btnEliminar = new System.Windows.Forms.Button();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.dgvDetalle = new System.Windows.Forms.DataGridView();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.label7 = new System.Windows.Forms.Label();
		this.txtOrdenCompra = new System.Windows.Forms.TextBox();
		this.label9 = new System.Windows.Forms.Label();
		this.btnDetalle = new System.Windows.Forms.Button();
		this.dtpFechaOrden = new System.Windows.Forms.DateTimePicker();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.txtComentarioRechazado = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.txtcomentario = new System.Windows.Forms.TextBox();
		this.txtSerie = new System.Windows.Forms.TextBox();
		this.txtcodserie = new System.Windows.Forms.TextBox();
		this.lbDocumento = new System.Windows.Forms.Label();
		this.txtNumero = new System.Windows.Forms.TextBox();
		this.txtCodDoc = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.txtDocRef = new System.Windows.Forms.TextBox();
		this.label5 = new System.Windows.Forms.Label();
		this.lblFechaOrden = new System.Windows.Forms.Label();
		this.superValidator2 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.customValidator9 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator8 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter2 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.coddetalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codunidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.coduser = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox3.SuspendLayout();
		this.groupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider2).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Write Document.png");
		this.imageList1.Images.SetKeyName(1, "New Document.png");
		this.imageList1.Images.SetKeyName(2, "Remove Document.png");
		this.imageList1.Images.SetKeyName(3, "document-print.png");
		this.imageList1.Images.SetKeyName(4, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(5, "exit.png");
		this.groupBox3.Controls.Add(this.btnImprimir);
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnNuevo);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Controls.Add(this.btnEditar);
		this.groupBox3.Controls.Add(this.btnEliminar);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox3.Location = new System.Drawing.Point(0, 483);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(806, 48);
		this.groupBox3.TabIndex = 19;
		this.groupBox3.TabStop = false;
		this.btnImprimir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnImprimir.ImageIndex = 3;
		this.btnImprimir.ImageList = this.imageList1;
		this.btnImprimir.Location = new System.Drawing.Point(565, 10);
		this.btnImprimir.Name = "btnImprimir";
		this.btnImprimir.Size = new System.Drawing.Size(78, 32);
		this.btnImprimir.TabIndex = 12;
		this.btnImprimir.Text = "Im&primir";
		this.btnImprimir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnImprimir.UseVisualStyleBackColor = true;
		this.btnImprimir.Visible = false;
		this.btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
		this.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(732, 10);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(68, 32);
		this.btnSalir.TabIndex = 13;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnSalir_Click);
		this.btnNuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
		this.btnNuevo.ImageIndex = 1;
		this.btnNuevo.ImageList = this.imageList1;
		this.btnNuevo.Location = new System.Drawing.Point(159, 10);
		this.btnNuevo.Name = "btnNuevo";
		this.btnNuevo.Size = new System.Drawing.Size(97, 32);
		this.btnNuevo.TabIndex = 8;
		this.btnNuevo.Text = "Nuevo";
		this.btnNuevo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnNuevo.UseVisualStyleBackColor = true;
		this.btnNuevo.Click += new System.EventHandler(btnNuevo_Click);
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(649, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 11;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnEditar.ImageIndex = 0;
		this.btnEditar.ImageList = this.imageList1;
		this.btnEditar.Location = new System.Drawing.Point(6, 10);
		this.btnEditar.Name = "btnEditar";
		this.btnEditar.Size = new System.Drawing.Size(66, 32);
		this.btnEditar.TabIndex = 9;
		this.btnEditar.Text = "Editar";
		this.btnEditar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEditar.UseVisualStyleBackColor = true;
		this.btnEditar.Click += new System.EventHandler(btnEditar_Click);
		this.btnEliminar.ImageIndex = 2;
		this.btnEliminar.ImageList = this.imageList1;
		this.btnEliminar.Location = new System.Drawing.Point(78, 10);
		this.btnEliminar.Name = "btnEliminar";
		this.btnEliminar.Size = new System.Drawing.Size(75, 32);
		this.btnEliminar.TabIndex = 10;
		this.btnEliminar.Text = "Eliminar";
		this.btnEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnEliminar.UseVisualStyleBackColor = true;
		this.btnEliminar.Click += new System.EventHandler(btnEliminar_Click);
		this.groupBox2.Controls.Add(this.dgvDetalle);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox2.Location = new System.Drawing.Point(0, 257);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(806, 226);
		this.groupBox2.TabIndex = 20;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Detalle";
		this.dgvDetalle.AllowUserToAddRows = false;
		this.dgvDetalle.AllowUserToOrderColumns = true;
		this.dgvDetalle.AllowUserToResizeRows = false;
		this.dgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalle.Columns.AddRange(this.coddetalle, this.codproducto, this.referencia, this.descripcion, this.codunidad, this.unidad, this.cantidad, this.coduser, this.fecharegistro);
		this.dgvDetalle.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalle.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalle.Name = "dgvDetalle";
		this.dgvDetalle.ReadOnly = true;
		this.dgvDetalle.RowHeadersVisible = false;
		this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvDetalle.Size = new System.Drawing.Size(800, 207);
		this.dgvDetalle.TabIndex = 1;
		this.superValidator2.SetValidator1(this.dgvDetalle, this.customValidator9);
		this.dgvDetalle.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(dgvDetalle_RowStateChanged);
		this.dgvDetalle.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalle_CellContentClick);
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.customValidator2.ErrorMessage = "Escoja un proveedor.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator1.ErrorMessage = "Escoja la Transaccion.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator7.ErrorMessage = "Escoja la forma de pago.";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.customValidator3.ErrorMessage = "Escoja un cliente.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator6.ErrorMessage = "Complete el campo requerido.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator5.ErrorMessage = "Ingrese el numero de documento.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator4.ErrorMessage = "Escoja un documento.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator4.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator4_ValidateValue);
		this.label7.AutoSize = true;
		this.label7.Location = new System.Drawing.Point(684, 83);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(81, 13);
		this.label7.TabIndex = 12;
		this.label7.Text = "Requerimiento :";
		this.label7.Visible = false;
		this.txtOrdenCompra.Enabled = false;
		this.txtOrdenCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.txtOrdenCompra.Location = new System.Drawing.Point(771, 76);
		this.txtOrdenCompra.Name = "txtOrdenCompra";
		this.txtOrdenCompra.ReadOnly = true;
		this.txtOrdenCompra.Size = new System.Drawing.Size(28, 24);
		this.txtOrdenCompra.TabIndex = 2;
		this.txtOrdenCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtOrdenCompra.Visible = false;
		this.label9.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label9.AutoSize = true;
		this.label9.Location = new System.Drawing.Point(6, 93);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(66, 13);
		this.label9.TabIndex = 17;
		this.label9.Tag = "21";
		this.label9.Text = "Comentario :";
		this.btnDetalle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnDetalle.Location = new System.Drawing.Point(728, 228);
		this.btnDetalle.Name = "btnDetalle";
		this.btnDetalle.Size = new System.Drawing.Size(75, 23);
		this.btnDetalle.TabIndex = 7;
		this.btnDetalle.Text = "Detalle";
		this.btnDetalle.UseVisualStyleBackColor = true;
		this.btnDetalle.Click += new System.EventHandler(btnDetalle_Click);
		this.dtpFechaOrden.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.dtpFechaOrden.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaOrden.Location = new System.Drawing.Point(716, 20);
		this.dtpFechaOrden.Name = "dtpFechaOrden";
		this.dtpFechaOrden.Size = new System.Drawing.Size(81, 20);
		this.dtpFechaOrden.TabIndex = 4;
		this.dtpFechaOrden.Tag = "16";
		this.groupBox1.Controls.Add(this.txtComentarioRechazado);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.txtcomentario);
		this.groupBox1.Controls.Add(this.txtSerie);
		this.groupBox1.Controls.Add(this.txtcodserie);
		this.groupBox1.Controls.Add(this.lbDocumento);
		this.groupBox1.Controls.Add(this.txtNumero);
		this.groupBox1.Controls.Add(this.txtCodDoc);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtDocRef);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.lblFechaOrden);
		this.groupBox1.Controls.Add(this.dtpFechaOrden);
		this.groupBox1.Controls.Add(this.btnDetalle);
		this.groupBox1.Controls.Add(this.label9);
		this.groupBox1.Controls.Add(this.txtOrdenCompra);
		this.groupBox1.Controls.Add(this.label7);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.groupBox1.Location = new System.Drawing.Point(0, 0);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(806, 257);
		this.groupBox1.TabIndex = 0;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Cabecera";
		this.txtComentarioRechazado.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtComentarioRechazado.Enabled = false;
		this.txtComentarioRechazado.Location = new System.Drawing.Point(78, 166);
		this.txtComentarioRechazado.Multiline = true;
		this.txtComentarioRechazado.Name = "txtComentarioRechazado";
		this.txtComentarioRechazado.Size = new System.Drawing.Size(600, 71);
		this.txtComentarioRechazado.TabIndex = 6;
		this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.label2.Location = new System.Drawing.Point(6, 178);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(66, 35);
		this.label2.TabIndex = 57;
		this.label2.Tag = "21";
		this.label2.Text = "Comentario Rechazado:";
		this.txtcomentario.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtcomentario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtcomentario.Location = new System.Drawing.Point(78, 90);
		this.txtcomentario.Multiline = true;
		this.txtcomentario.Name = "txtcomentario";
		this.txtcomentario.Size = new System.Drawing.Size(600, 71);
		this.txtcomentario.TabIndex = 5;
		this.txtcomentario.TextChanged += new System.EventHandler(txtcomentario_TextChanged);
		this.txtcomentario.KeyDown += new System.Windows.Forms.KeyEventHandler(txtcomentario_KeyDown);
		this.txtcomentario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtComentario_KeyPress);
		this.txtSerie.Location = new System.Drawing.Point(78, 52);
		this.txtSerie.Name = "txtSerie";
		this.txtSerie.ReadOnly = true;
		this.txtSerie.Size = new System.Drawing.Size(55, 20);
		this.txtSerie.TabIndex = 2;
		this.superValidator2.SetValidator1(this.txtSerie, this.customValidator8);
		this.txtSerie.KeyDown += new System.Windows.Forms.KeyEventHandler(txtSerie_KeyDown);
		this.txtSerie.Leave += new System.EventHandler(txtSerie_Leave);
		this.txtSerie.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtSerie_KeyPress);
		this.txtcodserie.Location = new System.Drawing.Point(215, 52);
		this.txtcodserie.Name = "txtcodserie";
		this.txtcodserie.Size = new System.Drawing.Size(30, 20);
		this.txtcodserie.TabIndex = 54;
		this.txtcodserie.Visible = false;
		this.lbDocumento.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbDocumento.Location = new System.Drawing.Point(112, 27);
		this.lbDocumento.Name = "lbDocumento";
		this.lbDocumento.Size = new System.Drawing.Size(157, 16);
		this.lbDocumento.TabIndex = 53;
		this.lbDocumento.Text = "NombreDocumento";
		this.txtNumero.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNumero.Enabled = false;
		this.txtNumero.Location = new System.Drawing.Point(139, 52);
		this.txtNumero.Name = "txtNumero";
		this.txtNumero.ReadOnly = true;
		this.txtNumero.Size = new System.Drawing.Size(60, 20);
		this.txtNumero.TabIndex = 3;
		this.txtNumero.Tag = "11";
		this.txtNumero.Leave += new System.EventHandler(txtNumero_Leave);
		this.txtNumero.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtNumero_KeyPress);
		this.txtCodDoc.Enabled = false;
		this.txtCodDoc.Location = new System.Drawing.Point(251, 52);
		this.txtCodDoc.Name = "txtCodDoc";
		this.txtCodDoc.ReadOnly = true;
		this.txtCodDoc.Size = new System.Drawing.Size(40, 20);
		this.txtCodDoc.TabIndex = 50;
		this.txtCodDoc.Visible = false;
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(14, 59);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(31, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Serie";
		this.txtDocRef.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtDocRef.Enabled = false;
		this.txtDocRef.Location = new System.Drawing.Point(78, 24);
		this.txtDocRef.Name = "txtDocRef";
		this.txtDocRef.Size = new System.Drawing.Size(28, 20);
		this.txtDocRef.TabIndex = 1;
		this.txtDocRef.Tag = "10";
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(19, 27);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(53, 13);
		this.label5.TabIndex = 51;
		this.label5.Text = "Doc. Ref.";
		this.lblFechaOrden.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblFechaOrden.AutoSize = true;
		this.lblFechaOrden.Location = new System.Drawing.Point(635, 24);
		this.lblFechaOrden.Name = "lblFechaOrden";
		this.lblFechaOrden.Size = new System.Drawing.Size(75, 13);
		this.lblFechaOrden.TabIndex = 46;
		this.lblFechaOrden.Tag = "8";
		this.lblFechaOrden.Text = "Fecha Orden :";
		this.superValidator2.ContainerControl = this;
		this.superValidator2.ErrorProvider = this.errorProvider2;
		this.superValidator2.Highlighter = this.highlighter2;
		this.customValidator9.ErrorMessage = "Debe llenar el detalle.";
		this.customValidator9.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator9.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator9_ValidateValue);
		this.customValidator8.ErrorMessage = "Escoja Serie de Documento.";
		this.customValidator8.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator8.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator8_ValidateValue);
		this.errorProvider2.ContainerControl = this;
		this.errorProvider2.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider2.Icon");
		this.highlighter2.ContainerControl = this;
		this.coddetalle.DataPropertyName = "codDetalle";
		this.coddetalle.HeaderText = "CodDetalle";
		this.coddetalle.Name = "coddetalle";
		this.coddetalle.ReadOnly = true;
		this.coddetalle.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.coddetalle.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coddetalle.Visible = false;
		this.codproducto.DataPropertyName = "codProducto";
		this.codproducto.HeaderText = "CodProducto";
		this.codproducto.Name = "codproducto";
		this.codproducto.ReadOnly = true;
		this.codproducto.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codproducto.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codproducto.Visible = false;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.ReadOnly = true;
		this.referencia.Width = 90;
		this.descripcion.DataPropertyName = "producto";
		this.descripcion.HeaderText = "Descripcion";
		this.descripcion.Name = "descripcion";
		this.descripcion.ReadOnly = true;
		this.descripcion.Width = 350;
		this.codunidad.DataPropertyName = "codUnidadMedida";
		this.codunidad.HeaderText = "Cod. Unidad";
		this.codunidad.Name = "codunidad";
		this.codunidad.ReadOnly = true;
		this.codunidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codunidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codunidad.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.unidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.unidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.unidad.Width = 120;
		this.cantidad.DataPropertyName = "cantidad";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle2.Format = "N2";
		dataGridViewCellStyle2.NullValue = null;
		this.cantidad.DefaultCellStyle = dataGridViewCellStyle2;
		this.cantidad.HeaderText = "Cantidad";
		this.cantidad.Name = "cantidad";
		this.cantidad.ReadOnly = true;
		this.cantidad.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.cantidad.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.coduser.DataPropertyName = "codUser";
		this.coduser.HeaderText = "CodUser";
		this.coduser.Name = "coduser";
		this.coduser.ReadOnly = true;
		this.coduser.Visible = false;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "Fecha Reg";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(806, 531);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.groupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmRequerimiento";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Requerimiento";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmOrdenCompra_Load);
		this.groupBox3.ResumeLayout(false);
		this.groupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider2).EndInit();
		base.ResumeLayout(false);
	}
}
