using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmListaPreciosProductos : Office2007Form
{
	private clsAdmTipoCambio admTipo = new clsAdmTipoCambio();

	private clsTipoCambio tipo = new clsTipoCambio();

	private clsAdmListaPrecio AdmLista = new clsAdmListaPrecio();

	private clsDetalleListaPrecio DtLista = new clsDetalleListaPrecio();

	private clsConsultasExternas ext = new clsConsultasExternas();

	public clsListaPrecio lista = new clsListaPrecio();

	private clsAdmFamilia admFam = new clsAdmFamilia();

	private clsAdmLinea admLin = new clsAdmLinea();

	private clsAdmProducto admProd = new clsAdmProducto();

	private clsFamilia fam = new clsFamilia();

	private clsLinea lin = new clsLinea();

	private clsProducto prod = new clsProducto();

	private clsAdmFormaPago AdmPago = new clsAdmFormaPago();

	private clsFormaPago fpago = new clsFormaPago();

	public int codlista = 0;

	public int Proceso = 0;

	public int Procede = 0;

	public int Codlista = 0;

	private clsValidar ok = new clsValidar();

	public int decimales = 0;

	public static BindingSource data = new BindingSource();

	private string filtro = string.Empty;

	private TextBox txtedit = new TextBox();

	private clsValidar val = new clsValidar();

	private decimal valor = default(decimal);

	private decimal mar = default(decimal);

	private decimal neto = default(decimal);

	private decimal precion = default(decimal);

	private decimal preciod = default(decimal);

	private int CodListaOrigen = 0;

	private IContainer components = null;

	private GroupBox groupBox3;

	private Button btnSalir;

	private Button btnGuardar;

	private Button btnReporte;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private ImageList imageList1;

	private CustomValidator customValidator1;

	private CustomValidator customValidator3;

	private CustomValidator customValidator5;

	private CustomValidator customValidator2;

	private GroupBox groupBox4;

	private Label label10;

	private Label label11;

	private Label lbLinea;

	public TextBox txtProveedorNomb;

	public TextBox txtProveedorCod;

	private TextBox txtRProduc2;

	private TextBox txtRProduc1;

	private TextBox txtRango2;

	private TextBox txtRango1;

	private TextBox txtCodPro1;

	private TextBox txtCodPro2;

	private CheckBox chbProveedor;

	private CheckBox chbRango;

	private CheckBox chbFamilia;

	private ComboBox cboLinea;

	private ComboBox cboFamilia;

	private GroupBox groupBox5;

	private DataGridView dgvDetalleListaPrecio;

	private CustomValidator customValidator4;

	private CustomValidator customValidator6;

	private CheckBox chbModificaMargen;

	private CustomValidator customValidator7;

	private TextBox textBox1;

	private Label lbmargen;

	private Label label1;

	private Label label4;

	public TextBox txtNombre;

	private DataGridViewCheckBoxColumn Modificar;

	private DataGridViewTextBoxColumn codprod;

	private DataGridViewTextBoxColumn refe;

	private DataGridViewTextBoxColumn descprod;

	private DataGridViewTextBoxColumn codUnidadMedida;

	private DataGridViewTextBoxColumn unidad;

	private DataGridViewTextBoxColumn valorprom;

	private DataGridViewTextBoxColumn margp;

	private DataGridViewTextBoxColumn net;

	private DataGridViewTextBoxColumn preciov;

	private DataGridViewTextBoxColumn PreciovSoles;

	private DataGridViewTextBoxColumn igv;

	public frmListaPreciosProductos()
	{
		InitializeComponent();
	}

	private void CargaFamilias()
	{
		cboFamilia.DataSource = admFam.MuestraFamilias();
		cboFamilia.DisplayMember = "descripcion";
		cboFamilia.ValueMember = "codFamilia";
		cboFamilia.SelectedIndex = -1;
	}

	private void CargaLineas(int codFami)
	{
		cboLinea.DataSource = admLin.MuestraLineas(codFami);
		cboLinea.DisplayMember = "descripcion";
		cboLinea.ValueMember = "codLinea";
		cboLinea.SelectedIndex = -1;
	}

	private void frmListaPrecios_Load(object sender, EventArgs e)
	{
		CargaFamilias();
		tipo = admTipo.CargaTipoCambio(DateTime.Now, 2);
	}

	private void btnSalir_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			if (dgvDetalleListaPrecio.Rows.Count > 0)
			{
				string estado = "";
				if (!(btnGuardar.Text == "Guardar"))
				{
					return;
				}
				foreach (DataGridViewRow row in (IEnumerable)dgvDetalleListaPrecio.Rows)
				{
					DtLista.Margen = Convert.ToDouble(row.Cells[margp.Name].Value);
					DtLista.Descuento1 = 0.0;
					DtLista.Descuento2 = 0.0;
					DtLista.Descuento3 = 0.0;
					DtLista.PrecioNeto = Convert.ToDouble(row.Cells[net.Name].Value);
					DtLista.Precio = Convert.ToDouble(row.Cells[preciov.Name].Value);
					DtLista.CodProducto = Convert.ToInt32(row.Cells[codprod.Name].Value);
					DtLista.CodListaPrecio = codlista;
					if (Convert.ToInt32(row.Cells[Modificar.Name].Value) == 1)
					{
						estado = ((!AdmLista.updatedetallePorFiltro(DtLista)) ? (estado + "0") : (estado + "1"));
					}
				}
				if (estado.Contains("0"))
				{
					MessageBox.Show("Verifique", "Listas de Precios Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}
				MessageBox.Show("Los datos se actualizaron correctamente", "Listas de Precios Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				btnGuardar.Enabled = false;
			}
			else
			{
				MessageBox.Show("Verifique! No hay Datos a Guardar", "Lista Precios Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
		catch (Exception)
		{
			MessageBox.Show("Verifique!", "Lista Precios Productos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
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

	private void btnReporte_Click(object sender, EventArgs e)
	{
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

	private void txtMargen_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void txtProveedorNomb_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (Application.OpenForms["frmProveedoresLista"] != null)
			{
				Application.OpenForms["frmProveedoresLista"].Activate();
			}
			else
			{
				frmProveedoresLista form = new frmProveedoresLista();
				form.Proceso = 3;
				form.Procede = 5;
				form.ShowDialog();
			}
			cargadatas();
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message);
		}
	}

	private void txtRProduc1_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode != Keys.F1)
			{
				return;
			}
			frmProductosLista frm = new frmProductosLista();
			frm.Procede = 13;
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (frm.referenciaPro == null)
				{
					txtRProduc1.Focus();
					return;
				}
				txtRProduc1.Text = frm.referenciaPro;
				txtRango1.Text = frm.descripcionPro;
				txtCodPro1.Text = frm.codigoPro.ToString();
				txtRProduc2.Focus();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(" " + ex);
		}
	}

	private void txtRProduc2_KeyDown(object sender, KeyEventArgs e)
	{
		try
		{
			if (e.KeyCode == Keys.F1)
			{
				frmProductosLista frm = new frmProductosLista();
				frm.Procede = 14;
				if (frm.ShowDialog() == DialogResult.OK)
				{
					if (frm.referenciaPro == null)
					{
						txtRProduc2.Focus();
					}
					else
					{
						txtRProduc2.Text = frm.referenciaPro.ToString();
						txtRango2.Text = frm.descripcionPro.ToString();
						txtCodPro2.Text = frm.codigoPro.ToString();
					}
				}
			}
			cargadatas();
		}
		catch (Exception ex)
		{
			MessageBox.Show(" " + ex);
		}
	}

	private void cargadatas()
	{
		try
		{
			if (chbRango.Checked && chbProveedor.Checked && chbFamilia.Checked)
			{
				if (txtRProduc1.Text != "" && txtRProduc2.Text != "" && txtProveedorNomb.Text != "" && cboFamilia.SelectedValue != null && cboLinea.SelectedValue != null)
				{
					cargaData6();
				}
				else if (txtRProduc1.Text != "" && txtRProduc2.Text != "" && txtProveedorNomb.Text != "" && cboFamilia.SelectedValue != null && cboLinea.SelectedValue == null)
				{
					cargaData9();
				}
			}
			else if (!chbRango.Checked && !chbProveedor.Checked && !chbFamilia.Checked)
			{
				if (data.DataSource != null)
				{
					DataTable dt = (DataTable)data.DataSource;
					dt.Clear();
				}
			}
			else if (chbRango.Checked && !chbProveedor.Checked && !chbFamilia.Checked)
			{
				if (txtRProduc1.Text != "" && txtRProduc2.Text != "")
				{
					cargaData();
				}
			}
			else if (!chbRango.Checked && chbProveedor.Checked && !chbFamilia.Checked)
			{
				if (txtProveedorNomb.Text != "")
				{
					cargaData2();
				}
			}
			else if (!chbRango.Checked && !chbProveedor.Checked && chbFamilia.Checked)
			{
				if (cboFamilia.SelectedValue != null && cboLinea.SelectedValue != null)
				{
					cargaData5();
				}
				else if (cboFamilia.SelectedValue != null && cboLinea.SelectedValue == null)
				{
					cargaData4();
				}
			}
			else if (chbRango.Checked && chbFamilia.Checked && !chbProveedor.Checked)
			{
				if (txtRProduc1.Text != "" && txtRProduc2.Text != "" && cboFamilia.SelectedValue != null && cboLinea.SelectedValue != null)
				{
					cargaData10();
				}
				else if (txtRProduc1.Text != "" && txtRProduc2.Text != "" && cboFamilia.SelectedValue != null && cboLinea.SelectedValue == null)
				{
					cargaData7();
				}
			}
			else if (chbRango.Checked && !chbFamilia.Checked && chbProveedor.Checked)
			{
				if (txtRProduc1.Text != "" && txtRProduc2.Text != "" && txtProveedorNomb.Text != "")
				{
					cargaData3();
				}
				else
				{
					dgvDetalleListaPrecio.Rows.Clear();
				}
			}
			else if (!chbRango.Checked && chbFamilia.Checked && chbProveedor.Checked)
			{
				if (txtProveedorNomb.Text != "" && chbProveedor.Checked && cboFamilia.SelectedValue != null && cboLinea.SelectedValue != null && chbFamilia.Checked)
				{
					cargaData11();
				}
				else if (txtProveedorNomb.Text != "" && chbProveedor.Checked && cboFamilia.SelectedValue != null && cboLinea.SelectedValue == null && chbFamilia.Checked)
				{
					cargaData8();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex?.ToString() ?? "");
		}
	}

	private void cargaData()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorFiltro(frmLogin.iCodAlmacen, Convert.ToInt32(txtCodPro1.Text), Convert.ToInt32(txtCodPro2.Text), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData2()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorProveedor(frmLogin.iCodAlmacen, Convert.ToInt32(txtProveedorCod.Text), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData3()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorRangoProv(frmLogin.iCodAlmacen, Convert.ToInt32(txtCodPro1.Text), Convert.ToInt32(txtCodPro2.Text), Convert.ToInt32(txtProveedorCod.Text), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData4()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorFamilia(frmLogin.iCodAlmacen, Convert.ToInt32(cboFamilia.SelectedValue), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData5()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorLinea(frmLogin.iCodAlmacen, Convert.ToInt32(cboFamilia.SelectedValue), Convert.ToInt32(cboLinea.SelectedValue), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData6()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorTodos(frmLogin.iCodAlmacen, Convert.ToInt32(txtCodPro1.Text), Convert.ToInt32(txtCodPro2.Text), Convert.ToInt32(txtProveedorCod.Text), Convert.ToInt32(cboFamilia.SelectedValue), Convert.ToInt32(cboLinea.SelectedValue), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData7()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorRangoFam(frmLogin.iCodAlmacen, Convert.ToInt32(txtCodPro1.Text), Convert.ToInt32(txtCodPro2.Text), Convert.ToInt32(cboFamilia.SelectedValue), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData8()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorProveedorFam(frmLogin.iCodAlmacen, Convert.ToInt32(txtProveedorCod.Text), Convert.ToInt32(cboFamilia.SelectedValue), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData9()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasParcial(frmLogin.iCodAlmacen, Convert.ToInt32(txtCodPro1.Text), Convert.ToInt32(txtCodPro2.Text), Convert.ToInt32(txtProveedorCod.Text), Convert.ToInt32(cboFamilia.SelectedValue), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData10()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorRangoFamLin(frmLogin.iCodAlmacen, Convert.ToInt32(txtCodPro1.Text), Convert.ToInt32(txtCodPro2.Text), Convert.ToInt32(cboFamilia.SelectedValue), Convert.ToInt32(cboLinea.SelectedValue), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void cargaData11()
	{
		dgvDetalleListaPrecio.DataSource = data;
		data.DataSource = AdmLista.MuestraListasPorProveedorFamLin(frmLogin.iCodAlmacen, Convert.ToInt32(txtProveedorCod.Text), Convert.ToInt32(cboFamilia.SelectedValue), Convert.ToInt32(cboLinea.SelectedValue), codlista, decimales);
		data.Filter = string.Empty;
		filtro = string.Empty;
		dgvDetalleListaPrecio.ClearSelection();
	}

	private void chbRango_CheckedChanged(object sender, EventArgs e)
	{
		if (chbRango.Checked)
		{
			txtRProduc1.Visible = true;
			txtRango1.Visible = true;
			txtRProduc2.Visible = true;
			txtRango2.Visible = true;
			label11.Visible = true;
			label10.Visible = true;
		}
		else
		{
			txtRProduc1.Visible = false;
			txtRango1.Visible = false;
			txtRProduc2.Visible = false;
			txtRango2.Visible = false;
			label11.Visible = false;
			label10.Visible = false;
			txtRProduc1.Text = "";
			txtRProduc2.Text = "";
			txtRango1.Text = "";
			txtRango2.Text = "";
			cargadatas();
		}
	}

	private void chbProveedor_CheckedChanged(object sender, EventArgs e)
	{
		if (chbProveedor.Checked)
		{
			txtProveedorNomb.Visible = true;
			return;
		}
		txtProveedorNomb.Visible = false;
		txtProveedorNomb.Text = "";
		cargadatas();
	}

	private void chbFamilia_CheckedChanged(object sender, EventArgs e)
	{
		if (chbFamilia.Checked)
		{
			cboFamilia.Visible = true;
			lbLinea.Visible = true;
			cboFamilia.SelectedIndex = -1;
			return;
		}
		cboFamilia.Visible = false;
		cboLinea.Visible = false;
		lbLinea.Visible = false;
		cboFamilia.SelectedIndex = -1;
		cboLinea.SelectedIndex = -1;
		cargadatas();
	}

	private void cboFamilia_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			fam = admFam.CargaFamilia(Convert.ToInt32(cboFamilia.SelectedValue));
			CargaLineas(Convert.ToInt32(cboFamilia.SelectedValue));
			if (cboFamilia.SelectedIndex != -1)
			{
				cboLinea.Visible = true;
			}
			else
			{
				cboLinea.Visible = false;
			}
		}
	}

	private void cboFamilia_Leave(object sender, EventArgs e)
	{
		fam = admFam.CargaFamilia(Convert.ToInt32(cboFamilia.SelectedValue));
		CargaLineas(Convert.ToInt32(cboFamilia.SelectedValue));
		if (cboFamilia.SelectedIndex != -1)
		{
			cboLinea.Visible = true;
		}
		else
		{
			cboLinea.Visible = false;
		}
	}

	private void cboLinea_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			lin = admLin.CargaLinea(Convert.ToInt32(cboLinea.SelectedValue));
		}
	}

	private void cboLinea_SelectionChangeCommitted(object sender, EventArgs e)
	{
		lin = admLin.CargaLinea(Convert.ToInt32(cboLinea.SelectedValue));
		cargadatas();
	}

	private void cboProducto_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void cboFamilia_SelectionChangeCommitted(object sender, EventArgs e)
	{
		fam = admFam.CargaFamilia(Convert.ToInt32(cboFamilia.SelectedValue));
		CargaLineas(Convert.ToInt32(cboFamilia.SelectedValue));
		if (cboFamilia.SelectedIndex != -1)
		{
			cboLinea.Visible = true;
		}
		else
		{
			cboLinea.Visible = false;
		}
		cargadatas();
	}

	private void customValidator6_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void customValidator7_ValidateValue(object sender, ValidateValueEventArgs e)
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

	private void chbModificaMargen_CheckedChanged(object sender, EventArgs e)
	{
		if (dgvDetalleListaPrecio.Rows.Count < 1)
		{
			return;
		}
		if (chbModificaMargen.Checked && textBox1.Text.Length > 0)
		{
			foreach (DataGridViewRow row in (IEnumerable)dgvDetalleListaPrecio.Rows)
			{
				row.Cells[Modificar.Name].Value = 1;
				if (Convert.ToDecimal(row.Cells[valorprom.Name].Value) > 0m && Convert.ToDecimal(row.Cells[margp.Name].Value) > 0m)
				{
					row.Cells[margp.Name].Value = textBox1.Text;
					row.Cells[net.Name].Value = decimal.Round(Convert.ToDecimal(row.Cells[valorprom.Name].Value) * (1m + Convert.ToDecimal(row.Cells[margp.Name].Value) / 100m), decimales);
					row.Cells[preciov.Name].Value = decimal.Round(Convert.ToDecimal(row.Cells[net.Name].Value) * (1m + Convert.ToDecimal(row.Cells[igv.Name].Value) / 100m), decimales);
					row.Cells[PreciovSoles.Name].Value = decimal.Round(Convert.ToDecimal(row.Cells[preciov.Name].Value) * Convert.ToDecimal(tipo.Venta), decimales);
				}
				else
				{
					row.Cells[margp.Name].Value = 0;
				}
			}
			return;
		}
		cargadatas();
	}

	private void textBox1_KeyUp(object sender, KeyEventArgs e)
	{
		if (chbModificaMargen.Checked && textBox1.Text.Length > 0)
		{
			chbModificaMargen_CheckedChanged(sender, e);
			return;
		}
		if (!chbModificaMargen.Checked && textBox1.Text.Length > 0)
		{
			chbModificaMargen.Enabled = true;
			return;
		}
		chbModificaMargen.Enabled = false;
		chbModificaMargen.Checked = false;
		cargadatas();
	}

	private void dgvDetalleListaPrecio_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void dgvDetalleListaPrecio_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			valor = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[valorprom.Name].Value);
			neto = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value);
			precion = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value);
			preciod = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value);
			if (dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "net" && txtedit.Text != "")
			{
				dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value = neto;
				if (valor > 0m)
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = decimal.Round((neto - valor) / valor * 100m, decimales);
				}
				else
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = 0;
				}
				dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value = decimal.Round(neto * (1m + Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[igv.Name].Value) / 100m), decimales);
				precion = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value);
				dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value = decimal.Round(precion * Convert.ToDecimal(tipo.Venta), decimales);
			}
			if (dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "preciov" && txtedit.Text != "")
			{
				dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value = precion;
				dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value = decimal.Round(precion * Convert.ToDecimal(tipo.Venta), decimales);
				dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value = decimal.Round(precion / (1m + Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[igv.Name].Value) / 100m), decimales);
				if (valor > 0m)
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = decimal.Round((neto - valor) / valor * 100m, decimales);
				}
				else
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = 0;
				}
			}
			if (dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "PreciovSoles" && txtedit.Text != "")
			{
				dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value = preciod;
				dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value = decimal.Round(preciod / Convert.ToDecimal(tipo.Venta), decimales);
				dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value = decimal.Round(precion / (1m + Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[igv.Name].Value) / 100m), decimales);
				if (valor > 0m)
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = decimal.Round((neto - valor) / valor * 100m, decimales);
				}
				else
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = 0;
				}
			}
			if (Convert.ToString(dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value) == "")
			{
				cargadatas();
			}
			if (Convert.ToString(dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value) == "")
			{
				cargadatas();
			}
			if (Convert.ToString(dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value) == "")
			{
				cargadatas();
			}
			if (Convert.ToString(dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value) == "")
			{
				cargadatas();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(" " + ex);
		}
	}

	private void dgvDetalleListaPrecio_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
	{
		txtedit = e.Control as TextBox;
		if (txtedit != null)
		{
			txtedit.KeyPress -= dgvDetalleListaPrecio_KeyPress;
			txtedit.KeyPress += dgvDetalleListaPrecio_KeyPress;
			txtedit.Leave -= dgvDetalleListaPrecio_Leave;
			txtedit.Leave += dgvDetalleListaPrecio_Leave;
			txtedit.KeyUp -= dgvDetalleListaPrecio_KeyUp;
			txtedit.KeyUp += dgvDetalleListaPrecio_KeyUp;
		}
	}

	private void dgvDetalleListaPrecio_KeyPress(object sender, KeyPressEventArgs e)
	{
		try
		{
			if (dgvDetalleListaPrecio.CurrentCell.ColumnIndex == 7)
			{
				val.decimalesNegativos(sender, e);
			}
			if (dgvDetalleListaPrecio.CurrentCell.ColumnIndex == 8)
			{
				val.SOLONumeros(sender, e);
			}
			if (dgvDetalleListaPrecio.CurrentCell.ColumnIndex == 9)
			{
				val.SOLONumeros(sender, e);
			}
			if (dgvDetalleListaPrecio.CurrentCell.ColumnIndex == 10)
			{
				val.SOLONumeros(sender, e);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(" " + ex);
		}
	}

	private void dgvDetalleListaPrecio_Leave(object sender, EventArgs e)
	{
		try
		{
			if (Convert.ToInt32(dgvDetalleListaPrecio.CurrentRow.Cells[Modificar.Name].Value) != 1)
			{
				return;
			}
			valor = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[valorprom.Name].Value);
			neto = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value);
			precion = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value);
			preciod = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value);
			if (dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "net" && txtedit.Text != "")
			{
				dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value = neto;
				if (valor > 0m)
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = decimal.Round((neto - valor) / valor * 100m, decimales);
				}
				else
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = 0;
				}
				dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value = decimal.Round(neto * (1m + Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[igv.Name].Value) / 100m), decimales);
				precion = Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value);
				dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value = decimal.Round(precion * Convert.ToDecimal(tipo.Venta), decimales);
			}
			else if (dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "preciov" && txtedit.Text != "")
			{
				dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value = precion;
				dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value = decimal.Round(precion * Convert.ToDecimal(tipo.Venta), decimales);
				dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value = decimal.Round(precion / (1m + Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[igv.Name].Value) / 100m), decimales);
				if (valor > 0m)
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = decimal.Round((neto - valor) / valor * 100m, decimales);
				}
				else
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = 0;
				}
			}
			else if (dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "PreciovSoles" && txtedit.Text != "")
			{
				dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value = preciod;
				dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value = decimal.Round(preciod / Convert.ToDecimal(tipo.Venta), decimales);
				dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value = decimal.Round(precion / (1m + Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[igv.Name].Value) / 100m), decimales);
				if (valor > 0m)
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = decimal.Round((neto - valor) / valor * 100m, decimales);
				}
				else
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = 0;
				}
			}
			else if (dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "margp" && txtedit.Text != "")
			{
				if (valor > 0m)
				{
					tipo = admTipo.CargaTipoCambio(DateTime.Now, 2);
					dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value = decimal.Round(Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[valorprom.Name].Value) * (1m + Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value) / 100m), decimales);
					dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value = decimal.Round(Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value) * (1m + Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[igv.Name].Value) / 100m), decimales);
					dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value = decimal.Round(Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value) * Convert.ToDecimal(tipo.Venta), decimales);
				}
				else
				{
					dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value = 0;
					dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value = 0;
					dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value = 0;
					dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value = 0;
				}
			}
			if (Convert.ToString(dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].Value) == "")
			{
				cargadatas();
			}
			if (Convert.ToString(dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value) == "")
			{
				cargadatas();
			}
			if (Convert.ToString(dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value) == "")
			{
				cargadatas();
			}
			if (Convert.ToString(dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value) == "")
			{
				cargadatas();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(" " + ex);
		}
	}

	private void dgvDetalleListaPrecio_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Convert.ToInt32(dgvDetalleListaPrecio.CurrentRow.Cells[Modificar.Name].Value) == 1)
		{
			dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].ReadOnly = false;
			dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].ReadOnly = false;
			dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].ReadOnly = false;
			dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].ReadOnly = false;
		}
		else
		{
			dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].ReadOnly = true;
			dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].ReadOnly = true;
			dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].ReadOnly = true;
			dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].ReadOnly = true;
		}
	}

	private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
	{
		val.decimalesNegativos(sender, e);
	}

	private void dgvDetalleListaPrecio_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		cargadatas();
	}

	private void txtRProduc1_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtRProduc1.Text != "" && BuscaProducto(txtRProduc1.Text, "1"))
		{
			ProcessTabKey(forward: true);
		}
	}

	private bool BuscaProducto(string txtPro, string cajatxt)
	{
		prod = admProd.CargaProductoDetalleR(txtPro, frmLogin.iCodAlmacen, 1, 0);
		if (prod != null)
		{
			if (cajatxt == "1")
			{
				txtRProduc1.Text = prod.Referencia;
				txtRango1.Text = prod.Descripcion;
				txtCodPro1.Text = prod.CodProducto.ToString();
				return true;
			}
			txtRProduc2.Text = prod.Referencia;
			txtRango2.Text = prod.Descripcion;
			txtCodPro2.Text = prod.CodProducto.ToString();
			return true;
		}
		MessageBox.Show("El producto no existe, Presione F1 para consultar la tabla de ayuda", "DETALLE DE ARTICULO", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		return false;
	}

	private void txtRProduc2_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (e.KeyChar == '\r' && txtRProduc2.Text != "" && BuscaProducto(txtRProduc2.Text, "2"))
		{
			ProcessTabKey(forward: true);
		}
	}

	private void dgvDetalleListaPrecio_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
	{
	}

	private void dgvDetalleListaPrecio_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Convert.ToInt32(dgvDetalleListaPrecio.CurrentRow.Cells[Modificar.Name].Value) == 1 && Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value) == 0m && Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value) == 0m && Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value) == 0m && codlista > 1)
		{
			dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].ReadOnly = true;
			dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].ReadOnly = true;
			dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].ReadOnly = true;
			dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].ReadOnly = true;
			if (dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "margp" || dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "net" || dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "preciov" || dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "PreciovSoles")
			{
				MessageBox.Show("No puede Modificar. No hay una Lista de Precios Base!!" + Environment.NewLine + "Lista Base:[CONTADO]", "Lista de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}

	private void dgvDetalleListaPrecio_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
	{
		if (Convert.ToInt32(dgvDetalleListaPrecio.CurrentRow.Cells[Modificar.Name].Value) == 1 && Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].Value) == 0m && Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].Value) == 0m && Convert.ToDecimal(dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].Value) == 0m && codlista > 1)
		{
			dgvDetalleListaPrecio.CurrentRow.Cells[margp.Name].ReadOnly = true;
			dgvDetalleListaPrecio.CurrentRow.Cells[net.Name].ReadOnly = true;
			dgvDetalleListaPrecio.CurrentRow.Cells[preciov.Name].ReadOnly = true;
			dgvDetalleListaPrecio.CurrentRow.Cells[PreciovSoles.Name].ReadOnly = true;
			if (dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "margp" || dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "net" || dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "preciov" || dgvDetalleListaPrecio.Columns[dgvDetalleListaPrecio.CurrentCell.ColumnIndex].Name == "PreciovSoles")
			{
				MessageBox.Show("No puede Modificar. No hay una Lista de Precios Base!!" + Environment.NewLine + "Lista Base:[CONTADO]", "Lista de Precios", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmListaPreciosProductos));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btnSalir = new System.Windows.Forms.Button();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnReporte = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.txtNombre = new System.Windows.Forms.TextBox();
		this.customValidator1 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator6 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator7 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator5 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator3 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator2 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.customValidator4 = new DevComponents.DotNetBar.Validator.CustomValidator();
		this.groupBox4 = new System.Windows.Forms.GroupBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.lbmargen = new System.Windows.Forms.Label();
		this.chbModificaMargen = new System.Windows.Forms.CheckBox();
		this.cboLinea = new System.Windows.Forms.ComboBox();
		this.cboFamilia = new System.Windows.Forms.ComboBox();
		this.chbFamilia = new System.Windows.Forms.CheckBox();
		this.chbProveedor = new System.Windows.Forms.CheckBox();
		this.chbRango = new System.Windows.Forms.CheckBox();
		this.txtCodPro2 = new System.Windows.Forms.TextBox();
		this.txtCodPro1 = new System.Windows.Forms.TextBox();
		this.lbLinea = new System.Windows.Forms.Label();
		this.txtProveedorCod = new System.Windows.Forms.TextBox();
		this.txtProveedorNomb = new System.Windows.Forms.TextBox();
		this.txtRango2 = new System.Windows.Forms.TextBox();
		this.txtRProduc2 = new System.Windows.Forms.TextBox();
		this.label10 = new System.Windows.Forms.Label();
		this.label11 = new System.Windows.Forms.Label();
		this.txtRango1 = new System.Windows.Forms.TextBox();
		this.txtRProduc1 = new System.Windows.Forms.TextBox();
		this.groupBox5 = new System.Windows.Forms.GroupBox();
		this.dgvDetalleListaPrecio = new System.Windows.Forms.DataGridView();
		this.Modificar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.codprod = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.refe = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.descprod = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUnidadMedida = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.unidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.valorprom = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.margp = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.net = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.preciov = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.PreciovSoles = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.igv = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupBox4.SuspendLayout();
		this.groupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvDetalleListaPrecio).BeginInit();
		base.SuspendLayout();
		this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
		this.groupBox3.Controls.Add(this.btnSalir);
		this.groupBox3.Controls.Add(this.btnReporte);
		this.groupBox3.Controls.Add(this.btnGuardar);
		this.groupBox3.Location = new System.Drawing.Point(316, 448);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(248, 48);
		this.groupBox3.TabIndex = 18;
		this.groupBox3.TabStop = false;
		this.btnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.btnSalir.ImageIndex = 5;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(173, 10);
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
		this.btnReporte.ImageIndex = 3;
		this.btnReporte.ImageList = this.imageList1;
		this.btnReporte.Location = new System.Drawing.Point(6, 10);
		this.btnReporte.Name = "btnReporte";
		this.btnReporte.Size = new System.Drawing.Size(78, 32);
		this.btnReporte.TabIndex = 23;
		this.btnReporte.Text = "Reporte";
		this.btnReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnReporte.UseVisualStyleBackColor = true;
		this.btnReporte.Click += new System.EventHandler(btnReporte_Click);
		this.btnGuardar.ImageIndex = 4;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(90, 10);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(77, 32);
		this.btnGuardar.TabIndex = 24;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtNombre.Location = new System.Drawing.Point(99, 32);
		this.txtNombre.Name = "txtNombre";
		this.txtNombre.ReadOnly = true;
		this.txtNombre.Size = new System.Drawing.Size(312, 20);
		this.txtNombre.TabIndex = 106;
		this.superValidator1.SetValidator1(this.txtNombre, this.customValidator1);
		this.txtNombre.WordWrap = false;
		this.customValidator1.ErrorMessage = "Ingrese el nombre para la lista.";
		this.customValidator1.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator1.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator1_ValidateValue);
		this.customValidator6.ErrorMessage = "Ingrese forma de Pago.";
		this.customValidator6.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator6.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator6_ValidateValue);
		this.customValidator7.ErrorMessage = "Lista Origen";
		this.customValidator7.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator7.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator7_ValidateValue);
		this.customValidator5.ErrorMessage = "Ingrese el porcentaje de variación.";
		this.customValidator5.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator5.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator5_ValidateValue);
		this.customValidator3.ErrorMessage = "Ingrese el margen de ganancia.";
		this.customValidator3.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator3.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator3_ValidateValue);
		this.customValidator2.ErrorMessage = "Escoja una lista de origen.";
		this.customValidator2.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.customValidator2.ValidateValue += new DevComponents.DotNetBar.Validator.ValidateValueEventHandler(customValidator2_ValidateValue);
		this.customValidator4.ErrorMessage = "Ingrese Forma de Pago.";
		this.customValidator4.HighlightColor = DevComponents.DotNetBar.Validator.eHighlightColor.Red;
		this.groupBox4.Controls.Add(this.label4);
		this.groupBox4.Controls.Add(this.txtNombre);
		this.groupBox4.Controls.Add(this.label1);
		this.groupBox4.Controls.Add(this.textBox1);
		this.groupBox4.Controls.Add(this.lbmargen);
		this.groupBox4.Controls.Add(this.chbModificaMargen);
		this.groupBox4.Controls.Add(this.cboLinea);
		this.groupBox4.Controls.Add(this.cboFamilia);
		this.groupBox4.Controls.Add(this.chbFamilia);
		this.groupBox4.Controls.Add(this.chbProveedor);
		this.groupBox4.Controls.Add(this.chbRango);
		this.groupBox4.Controls.Add(this.txtCodPro2);
		this.groupBox4.Controls.Add(this.txtCodPro1);
		this.groupBox4.Controls.Add(this.lbLinea);
		this.groupBox4.Controls.Add(this.txtProveedorCod);
		this.groupBox4.Controls.Add(this.txtProveedorNomb);
		this.groupBox4.Controls.Add(this.txtRango2);
		this.groupBox4.Controls.Add(this.txtRProduc2);
		this.groupBox4.Controls.Add(this.label10);
		this.groupBox4.Controls.Add(this.label11);
		this.groupBox4.Controls.Add(this.txtRango1);
		this.groupBox4.Controls.Add(this.txtRProduc1);
		this.groupBox4.Location = new System.Drawing.Point(6, 13);
		this.groupBox4.Name = "groupBox4";
		this.groupBox4.Size = new System.Drawing.Size(899, 169);
		this.groupBox4.TabIndex = 21;
		this.groupBox4.TabStop = false;
		this.groupBox4.Text = "Filtro";
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(13, 32);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(50, 13);
		this.label4.TabIndex = 107;
		this.label4.Text = "Nombre :";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(730, 137);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(15, 13);
		this.label1.TabIndex = 105;
		this.label1.Text = "%";
		this.textBox1.Location = new System.Drawing.Point(671, 133);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(53, 20);
		this.textBox1.TabIndex = 104;
		this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(textBox1_KeyUp);
		this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox1_KeyPress);
		this.lbmargen.AutoSize = true;
		this.lbmargen.Location = new System.Drawing.Point(619, 137);
		this.lbmargen.Name = "lbmargen";
		this.lbmargen.Size = new System.Drawing.Size(46, 13);
		this.lbmargen.TabIndex = 103;
		this.lbmargen.Text = "Margen:";
		this.chbModificaMargen.AutoSize = true;
		this.chbModificaMargen.Enabled = false;
		this.chbModificaMargen.Location = new System.Drawing.Point(773, 136);
		this.chbModificaMargen.Name = "chbModificaMargen";
		this.chbModificaMargen.Size = new System.Drawing.Size(108, 17);
		this.chbModificaMargen.TabIndex = 34;
		this.chbModificaMargen.Text = "Modificar Margen";
		this.chbModificaMargen.UseVisualStyleBackColor = true;
		this.chbModificaMargen.CheckedChanged += new System.EventHandler(chbModificaMargen_CheckedChanged);
		this.cboLinea.FormattingEnabled = true;
		this.cboLinea.Location = new System.Drawing.Point(543, 98);
		this.cboLinea.Name = "cboLinea";
		this.cboLinea.Size = new System.Drawing.Size(338, 21);
		this.cboLinea.TabIndex = 102;
		this.cboLinea.Visible = false;
		this.cboLinea.SelectionChangeCommitted += new System.EventHandler(cboLinea_SelectionChangeCommitted);
		this.cboLinea.KeyDown += new System.Windows.Forms.KeyEventHandler(cboLinea_KeyDown);
		this.cboFamilia.FormattingEnabled = true;
		this.cboFamilia.Location = new System.Drawing.Point(543, 65);
		this.cboFamilia.Name = "cboFamilia";
		this.cboFamilia.Size = new System.Drawing.Size(338, 21);
		this.cboFamilia.TabIndex = 101;
		this.cboFamilia.Visible = false;
		this.cboFamilia.SelectionChangeCommitted += new System.EventHandler(cboFamilia_SelectionChangeCommitted);
		this.cboFamilia.Leave += new System.EventHandler(cboFamilia_Leave);
		this.cboFamilia.KeyDown += new System.Windows.Forms.KeyEventHandler(cboFamilia_KeyDown);
		this.chbFamilia.AutoSize = true;
		this.chbFamilia.Location = new System.Drawing.Point(463, 67);
		this.chbFamilia.Name = "chbFamilia";
		this.chbFamilia.Size = new System.Drawing.Size(61, 17);
		this.chbFamilia.TabIndex = 100;
		this.chbFamilia.Text = "Familia:";
		this.chbFamilia.UseVisualStyleBackColor = true;
		this.chbFamilia.TextChanged += new System.EventHandler(btnCalularPrecios_Click);
		this.chbFamilia.CheckedChanged += new System.EventHandler(chbFamilia_CheckedChanged);
		this.chbProveedor.AutoSize = true;
		this.chbProveedor.Location = new System.Drawing.Point(6, 123);
		this.chbProveedor.Name = "chbProveedor";
		this.chbProveedor.Size = new System.Drawing.Size(78, 17);
		this.chbProveedor.TabIndex = 99;
		this.chbProveedor.Text = "Proveedor:";
		this.chbProveedor.UseVisualStyleBackColor = true;
		this.chbProveedor.CheckedChanged += new System.EventHandler(chbProveedor_CheckedChanged);
		this.chbRango.AutoSize = true;
		this.chbRango.Location = new System.Drawing.Point(6, 51);
		this.chbRango.Name = "chbRango";
		this.chbRango.Size = new System.Drawing.Size(61, 17);
		this.chbRango.TabIndex = 98;
		this.chbRango.Text = "Rango:";
		this.chbRango.UseVisualStyleBackColor = true;
		this.chbRango.CheckedChanged += new System.EventHandler(chbRango_CheckedChanged);
		this.txtCodPro2.Enabled = false;
		this.txtCodPro2.Location = new System.Drawing.Point(434, 93);
		this.txtCodPro2.Name = "txtCodPro2";
		this.txtCodPro2.Size = new System.Drawing.Size(20, 20);
		this.txtCodPro2.TabIndex = 97;
		this.txtCodPro2.Visible = false;
		this.txtCodPro1.Enabled = false;
		this.txtCodPro1.Location = new System.Drawing.Point(434, 68);
		this.txtCodPro1.Name = "txtCodPro1";
		this.txtCodPro1.Size = new System.Drawing.Size(20, 20);
		this.txtCodPro1.TabIndex = 96;
		this.txtCodPro1.Visible = false;
		this.lbLinea.AutoSize = true;
		this.lbLinea.Location = new System.Drawing.Point(480, 102);
		this.lbLinea.Name = "lbLinea";
		this.lbLinea.Size = new System.Drawing.Size(36, 13);
		this.lbLinea.TabIndex = 90;
		this.lbLinea.Text = "Linea:";
		this.lbLinea.Visible = false;
		this.txtProveedorCod.Enabled = false;
		this.txtProveedorCod.Location = new System.Drawing.Point(434, 120);
		this.txtProveedorCod.Name = "txtProveedorCod";
		this.txtProveedorCod.Size = new System.Drawing.Size(20, 20);
		this.txtProveedorCod.TabIndex = 84;
		this.txtProveedorCod.Visible = false;
		this.txtProveedorNomb.Location = new System.Drawing.Point(90, 121);
		this.txtProveedorNomb.Name = "txtProveedorNomb";
		this.txtProveedorNomb.ReadOnly = true;
		this.txtProveedorNomb.Size = new System.Drawing.Size(338, 20);
		this.txtProveedorNomb.TabIndex = 83;
		this.txtProveedorNomb.Visible = false;
		this.txtProveedorNomb.KeyDown += new System.Windows.Forms.KeyEventHandler(txtProveedorNomb_KeyDown);
		this.txtRango2.Enabled = false;
		this.txtRango2.Location = new System.Drawing.Point(180, 94);
		this.txtRango2.Name = "txtRango2";
		this.txtRango2.Size = new System.Drawing.Size(248, 20);
		this.txtRango2.TabIndex = 81;
		this.txtRango2.Visible = false;
		this.txtRProduc2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRProduc2.Location = new System.Drawing.Point(90, 94);
		this.txtRProduc2.Name = "txtRProduc2";
		this.txtRProduc2.ReadOnly = true;
		this.txtRProduc2.Size = new System.Drawing.Size(84, 20);
		this.txtRProduc2.TabIndex = 80;
		this.txtRProduc2.Visible = false;
		this.txtRProduc2.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRProduc2_KeyDown);
		this.txtRProduc2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRProduc2_KeyPress);
		this.label10.AutoSize = true;
		this.label10.BackColor = System.Drawing.Color.Transparent;
		this.label10.Location = new System.Drawing.Point(36, 97);
		this.label10.Name = "label10";
		this.label10.Size = new System.Drawing.Size(27, 13);
		this.label10.TabIndex = 79;
		this.label10.Text = "Fin :";
		this.label10.Visible = false;
		this.label11.AutoSize = true;
		this.label11.BackColor = System.Drawing.Color.Transparent;
		this.label11.Location = new System.Drawing.Point(36, 71);
		this.label11.Name = "label11";
		this.label11.Size = new System.Drawing.Size(38, 13);
		this.label11.TabIndex = 78;
		this.label11.Text = "Inicio :";
		this.label11.Visible = false;
		this.txtRango1.Enabled = false;
		this.txtRango1.Location = new System.Drawing.Point(180, 68);
		this.txtRango1.Name = "txtRango1";
		this.txtRango1.Size = new System.Drawing.Size(248, 20);
		this.txtRango1.TabIndex = 77;
		this.txtRango1.Visible = false;
		this.txtRProduc1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
		this.txtRProduc1.Location = new System.Drawing.Point(90, 68);
		this.txtRProduc1.Name = "txtRProduc1";
		this.txtRProduc1.ReadOnly = true;
		this.txtRProduc1.Size = new System.Drawing.Size(84, 20);
		this.txtRProduc1.TabIndex = 76;
		this.txtRProduc1.Visible = false;
		this.txtRProduc1.KeyDown += new System.Windows.Forms.KeyEventHandler(txtRProduc1_KeyDown);
		this.txtRProduc1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(txtRProduc1_KeyPress);
		this.groupBox5.Controls.Add(this.dgvDetalleListaPrecio);
		this.groupBox5.Location = new System.Drawing.Point(6, 172);
		this.groupBox5.Name = "groupBox5";
		this.groupBox5.Size = new System.Drawing.Size(899, 273);
		this.groupBox5.TabIndex = 22;
		this.groupBox5.TabStop = false;
		this.dgvDetalleListaPrecio.AllowUserToAddRows = false;
		this.dgvDetalleListaPrecio.AllowUserToDeleteRows = false;
		this.dgvDetalleListaPrecio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvDetalleListaPrecio.Columns.AddRange(this.Modificar, this.codprod, this.refe, this.descprod, this.codUnidadMedida, this.unidad, this.valorprom, this.margp, this.net, this.preciov, this.PreciovSoles, this.igv);
		this.dgvDetalleListaPrecio.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvDetalleListaPrecio.Location = new System.Drawing.Point(3, 16);
		this.dgvDetalleListaPrecio.Name = "dgvDetalleListaPrecio";
		this.dgvDetalleListaPrecio.RowHeadersVisible = false;
		this.dgvDetalleListaPrecio.Size = new System.Drawing.Size(893, 254);
		this.dgvDetalleListaPrecio.TabIndex = 22;
		this.dgvDetalleListaPrecio.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalleListaPrecio_CellValueChanged);
		this.dgvDetalleListaPrecio.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalleListaPrecio_CellDoubleClick);
		this.dgvDetalleListaPrecio.Leave += new System.EventHandler(dgvDetalleListaPrecio_Leave);
		this.dgvDetalleListaPrecio.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalleListaPrecio_CellClick);
		this.dgvDetalleListaPrecio.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(dgvDetalleListaPrecio_EditingControlShowing);
		this.dgvDetalleListaPrecio.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(dgvDetalleListaPrecio_DataError);
		this.dgvDetalleListaPrecio.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(dgvDetalleListaPrecio_RowsRemoved);
		this.dgvDetalleListaPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(dgvDetalleListaPrecio_KeyPress);
		this.dgvDetalleListaPrecio.KeyUp += new System.Windows.Forms.KeyEventHandler(dgvDetalleListaPrecio_KeyUp);
		this.dgvDetalleListaPrecio.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvDetalleListaPrecio_CellContentClick);
		this.Modificar.HeaderText = "Modificar";
		this.Modificar.Name = "Modificar";
		this.Modificar.Width = 60;
		this.codprod.DataPropertyName = "codProducto";
		this.codprod.HeaderText = "CodProducto";
		this.codprod.Name = "codprod";
		this.codprod.Visible = false;
		this.refe.DataPropertyName = "referencia";
		this.refe.HeaderText = "Referencia";
		this.refe.Name = "refe";
		this.refe.ReadOnly = true;
		this.descprod.DataPropertyName = "descripcion";
		this.descprod.HeaderText = "Descripcion";
		this.descprod.Name = "descprod";
		this.descprod.ReadOnly = true;
		this.descprod.Width = 200;
		this.codUnidadMedida.DataPropertyName = "codUnidadMedida";
		this.codUnidadMedida.HeaderText = "codUnidadMedida";
		this.codUnidadMedida.Name = "codUnidadMedida";
		this.codUnidadMedida.ReadOnly = true;
		this.codUnidadMedida.Visible = false;
		this.unidad.DataPropertyName = "unidad";
		this.unidad.HeaderText = "Unidad";
		this.unidad.Name = "unidad";
		this.unidad.ReadOnly = true;
		this.valorprom.DataPropertyName = "valorpromedio";
		dataGridViewCellStyle6.Format = "N2";
		dataGridViewCellStyle6.NullValue = null;
		this.valorprom.DefaultCellStyle = dataGridViewCellStyle6;
		this.valorprom.HeaderText = "ValorProm ($.)";
		this.valorprom.Name = "valorprom";
		this.valorprom.ReadOnly = true;
		this.valorprom.Width = 80;
		this.margp.DataPropertyName = "margen";
		dataGridViewCellStyle7.Format = "N2";
		dataGridViewCellStyle7.NullValue = "0";
		this.margp.DefaultCellStyle = dataGridViewCellStyle7;
		this.margp.HeaderText = "% Margen ($.)";
		this.margp.Name = "margp";
		this.margp.ReadOnly = true;
		this.net.DataPropertyName = "precioneto";
		dataGridViewCellStyle8.Format = "N2";
		dataGridViewCellStyle8.NullValue = null;
		this.net.DefaultCellStyle = dataGridViewCellStyle8;
		this.net.HeaderText = "Neto ($.)";
		this.net.Name = "net";
		this.net.ReadOnly = true;
		this.net.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.net.Width = 80;
		this.preciov.DataPropertyName = "precio";
		dataGridViewCellStyle9.Format = "N2";
		dataGridViewCellStyle9.NullValue = null;
		this.preciov.DefaultCellStyle = dataGridViewCellStyle9;
		this.preciov.HeaderText = "Precio Venta Dolares ($.)";
		this.preciov.Name = "preciov";
		this.preciov.ReadOnly = true;
		this.preciov.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.PreciovSoles.DataPropertyName = "preciosoles";
		dataGridViewCellStyle10.Format = "N2";
		dataGridViewCellStyle10.NullValue = null;
		this.PreciovSoles.DefaultCellStyle = dataGridViewCellStyle10;
		this.PreciovSoles.HeaderText = "Precio Venta Soles (S/.)";
		this.PreciovSoles.Name = "PreciovSoles";
		this.PreciovSoles.ReadOnly = true;
		this.PreciovSoles.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.igv.DataPropertyName = "igv";
		this.igv.HeaderText = "igv";
		this.igv.Name = "igv";
		this.igv.ReadOnly = true;
		this.igv.Visible = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.btnSalir;
		base.ClientSize = new System.Drawing.Size(909, 501);
		base.Controls.Add(this.groupBox5);
		base.Controls.Add(this.groupBox4);
		base.Controls.Add(this.groupBox3);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.Name = "frmListaPreciosProductos";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Listas de Precios Productos";
		base.Load += new System.EventHandler(frmListaPrecios_Load);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupBox4.ResumeLayout(false);
		this.groupBox4.PerformLayout();
		this.groupBox5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvDetalleListaPrecio).EndInit();
		base.ResumeLayout(false);
	}
}
