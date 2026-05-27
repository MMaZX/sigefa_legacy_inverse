using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Reportes;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmArqueos : Office2007Form
{
	private clsAdmArqueo admarq = new clsAdmArqueo();

	private clsArqueo arq = new clsArqueo();

	private clsDetalleArqueo detarque = new clsDetalleArqueo();

	public static BindingSource data = new BindingSource();

	public static BindingSource data2 = new BindingSource();

	private string encargao;

	private int mes;

	private int anio;

	private int op1;

	private int codArqueos;

	private string estaArqueo;

	private int nuevocodarqueo;

	private clsReporteArqueo ds = new clsReporteArqueo();

	private clsReporteArqueoCargado dsC = new clsReporteArqueoCargado();

	private IContainer components = null;

	private ImageList imageList1;

	private RibbonBar ribbonBar2;

	private ButtonItem biNuevo;

	private ButtonItem biModificar;

	private ButtonItem biActualizar;

	private ButtonItem biBuscar;

	private ButtonItem biImprimeLista;

	private GroupBox groupBox3;

	private DataGridView dgvProductos;

	private GroupBox groupBox2;

	private TextBox textBox3;

	private TextBox textBox2;

	private DateTimePicker dtpFecha;

	private TextBox textBox1;

	private Label label4;

	private Label label3;

	private Label label2;

	private Label label1;

	private GroupBox groupBox1;

	private DataGridView dgArqueos;

	private Button btnGuardar;

	private Label label5;

	private RadioButton rCargado;

	private RadioButton rGenerado;

	private ComboBox cMes;

	private RadioButton rAprobado;

	private ComboBox cAño;

	private CheckBox cTodosEs;

	private CheckBox chkMes;

	private CheckBox cTodos;

	private Button btAprobar;

	private Button btChekear;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn fecha;

	private DataGridViewTextBoxColumn estado;

	private DataGridViewTextBoxColumn observacion;

	private DataGridViewTextBoxColumn codUsuario2;

	private DataGridViewTextBoxColumn codusuario;

	private DataGridViewTextBoxColumn fecharegistro;

	private Button btImprim;

	private DataGridViewTextBoxColumn codproducto;

	private DataGridViewTextBoxColumn Column2;

	private DataGridViewTextBoxColumn codarqueo;

	private DataGridViewTextBoxColumn Column4;

	private DataGridViewTextBoxColumn codproductos;

	private DataGridViewTextBoxColumn referencia;

	private DataGridViewTextBoxColumn nombre;

	private DataGridViewTextBoxColumn um;

	private DataGridViewTextBoxColumn stockS;

	private DataGridViewTextBoxColumn stockF;

	private DataGridViewTextBoxColumn diferencia;

	private DataGridViewTextBoxColumn Column5;

	public frmArqueos()
	{
		InitializeComponent();
	}

	private void frmArqueos_LoadExtracted()
	{
		arq.ICodUsuario = frmLogin.iCodUser;
		arq.ICodAlma = frmLogin.iCodAlmacen;
		encargao = frmLogin.sNombreUser.ToString() + " " + frmLogin.sApellidoUSer.ToString();
	}

	private void frmArqueos_Load(object sender, EventArgs e)
	{
		cTodosEs.Checked = true;
		cTodos.Checked = true;
		if (cTodosEs.Checked)
		{
			rGenerado.Checked = false;
			rCargado.Checked = false;
			rAprobado.Checked = false;
		}
		if (cTodos.Checked)
		{
			chkMes.Checked = false;
			cMes.Enabled = false;
			cAño.Enabled = false;
		}
		else
		{
			chkMes.Checked = true;
			cMes.Enabled = true;
			cAño.Enabled = true;
		}
		frmArqueos_LoadExtracted();
		CargaArqueos(1, 3, 1, 1, frmLogin.iCodAlmacen);
	}

	private void CargaArqueos(int opcion1, int opcion2, int mes1, int anio1, int codAlman)
	{
		dgArqueos.DataSource = data;
		data.DataSource = admarq.MuestraArqueos(opcion1, opcion2, mes1, anio1, codAlman);
		dgArqueos.ClearSelection();
	}

	private void rGenerado_CheckedChanged(object sender, EventArgs e)
	{
		if (rGenerado.Checked)
		{
			cTodosEs.Checked = false;
			if (cTodos.Checked)
			{
				CargaArqueos(1, 1, 1, 1, frmLogin.iCodAlmacen);
				return;
			}
			mes = cMes.SelectedIndex + 1;
			if (cAño.SelectedItem != null)
			{
				anio = int.Parse(cAño.SelectedItem.ToString());
				if (mes != 0)
				{
					CargaArqueos(2, 1, mes, anio, frmLogin.iCodAlmacen);
				}
			}
		}
		else if (!rCargado.Checked && !rAprobado.Checked)
		{
			cTodosEs.Checked = true;
		}
	}

	private void cTodosEs_CheckedChanged(object sender, EventArgs e)
	{
		if (!cTodosEs.Checked)
		{
			return;
		}
		rGenerado.Checked = false;
		rCargado.Checked = false;
		rAprobado.Checked = false;
		if (cTodos.Checked)
		{
			CargaArqueos(1, 3, 1, 1, frmLogin.iCodAlmacen);
			return;
		}
		mes = cMes.SelectedIndex + 1;
		if (cAño.SelectedItem != null)
		{
			anio = int.Parse(cAño.SelectedItem.ToString());
			if (mes != 0)
			{
				CargaArqueos(2, 3, mes, anio, frmLogin.iCodAlmacen);
			}
		}
	}

	private void rCargado_CheckedChanged(object sender, EventArgs e)
	{
		if (rCargado.Checked)
		{
			cTodosEs.Checked = false;
			if (cTodos.Checked)
			{
				CargaArqueos(1, 2, 1, 1, frmLogin.iCodAlmacen);
				return;
			}
			mes = cMes.SelectedIndex + 1;
			if (cAño.SelectedItem != null)
			{
				anio = int.Parse(cAño.SelectedItem.ToString());
				if (mes != 0)
				{
					CargaArqueos(2, 2, mes, anio, frmLogin.iCodAlmacen);
				}
			}
		}
		else if (!rGenerado.Checked && !rAprobado.Checked)
		{
			cTodosEs.Checked = true;
		}
	}

	private void rAprobado_CheckedChanged(object sender, EventArgs e)
	{
		if (rAprobado.Checked)
		{
			cTodosEs.Checked = false;
			if (cTodos.Checked)
			{
				CargaArqueos(1, 0, 1, 1, frmLogin.iCodAlmacen);
				return;
			}
			mes = cMes.SelectedIndex + 1;
			if (cAño.SelectedItem != null)
			{
				anio = int.Parse(cAño.SelectedItem.ToString());
				if (mes != 0)
				{
					CargaArqueos(2, 0, mes, anio, frmLogin.iCodAlmacen);
				}
			}
		}
		else if (!rGenerado.Checked && !rCargado.Checked)
		{
			cTodosEs.Checked = true;
		}
	}

	private void chkMes_CheckedChanged(object sender, EventArgs e)
	{
		if (chkMes.Checked)
		{
			cMes.Enabled = true;
			cAño.Enabled = true;
			cTodos.Checked = false;
		}
		else
		{
			cMes.Enabled = false;
			cAño.Enabled = false;
		}
	}

	private void cTodos_CheckedChanged(object sender, EventArgs e)
	{
		if (cTodos.Checked)
		{
			chkMes.Checked = false;
			if (cTodosEs.Checked)
			{
				CargaArqueos(1, 3, 1, 1, frmLogin.iCodAlmacen);
				return;
			}
			op1 = chek_radio();
			CargaArqueos(1, op1, 1, 1, frmLogin.iCodAlmacen);
			return;
		}
		chkMes.Checked = true;
		if (cTodosEs.Checked)
		{
			mes = cMes.SelectedIndex + 1;
			if (cAño.SelectedItem != null)
			{
				anio = int.Parse(cAño.SelectedItem.ToString());
				if (mes != 0)
				{
					CargaArqueos(2, 3, mes, anio, frmLogin.iCodAlmacen);
				}
			}
			return;
		}
		mes = cMes.SelectedIndex + 1;
		if (cAño.SelectedItem != null)
		{
			anio = int.Parse(cAño.SelectedItem.ToString());
			if (mes != 0)
			{
				op1 = chek_radio();
				CargaArqueos(2, op1, mes, anio, frmLogin.iCodAlmacen);
			}
		}
	}

	private int chek_radio()
	{
		int op = 0;
		if (rGenerado.Checked)
		{
			op = 1;
		}
		if (rCargado.Checked)
		{
			op = 2;
		}
		if (rAprobado.Checked)
		{
			op = 0;
		}
		return op;
	}

	private void dgArqueos_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.RowIndex != -1 && dgArqueos.Rows[e.RowIndex].Cells[0].Value != null)
		{
			estaArqueo = dgArqueos.Rows[e.RowIndex].Cells[5].Value.ToString();
			switch (estaArqueo)
			{
			case "Sin Generar":
				btImprim.Enabled = false;
				btnGuardar.Enabled = true;
				btChekear.Enabled = false;
				btAprobar.Enabled = false;
				break;
			case "Generado":
				btImprim.Enabled = false;
				btnGuardar.Enabled = false;
				btChekear.Enabled = true;
				btAprobar.Enabled = false;
				break;
			case "Cargado":
				btImprim.Enabled = true;
				btnGuardar.Enabled = false;
				btChekear.Enabled = false;
				btAprobar.Enabled = true;
				break;
			case "Aprobado":
				btImprim.Enabled = true;
				btnGuardar.Enabled = false;
				btChekear.Enabled = false;
				btAprobar.Enabled = false;
				break;
			}
			codArqueos = int.Parse(dgArqueos.Rows[e.RowIndex].Cells[0].Value.ToString());
			textBox1.Text = codArqueos.ToString();
			textBox2.Text = dgArqueos.Rows[e.RowIndex].Cells[2].Value.ToString();
			textBox3.Text = dgArqueos.Rows[e.RowIndex].Cells[4].Value.ToString();
			CargaDetalleArqueos(codArqueos);
		}
	}

	private void CargaDetalleArqueos(int codArqueos)
	{
		dgvProductos.DataSource = data2;
		data2.DataSource = admarq.MuestraDetalleArqueos(codArqueos);
		dgvProductos.EditMode = DataGridViewEditMode.EditOnEnter;
	}

	private void dgvProductos_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
	{
	}

	private void biNuevo_Click(object sender, EventArgs e)
	{
		textBox2.Text = encargao.ToString();
		textBox3.Focus();
		arq.DFecha = dtpFecha.Value;
		arq.SObservacion = textBox3.Text;
		admarq.insert(arq);
		nuevocodarqueo = arq.ICodArqueoNuevo;
		textBox1.Text = nuevocodarqueo.ToString();
		CargaArqueos(1, 3, 1, 1, frmLogin.iCodAlmacen);
		btnGuardar.Enabled = true;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			detarque.ICodArqueo = Convert.ToInt32(textBox1.Text);
			detarque.ICodUsuario = frmLogin.iCodUser;
			detarque.SObservacion = "Nada";
			detarque.ICodAlma = frmLogin.iCodAlmacen;
			admarq.insertDetalle(detarque);
			MessageBox.Show("Generacion Correcta");
			CargaArqueos(1, 3, 1, 1, frmLogin.iCodAlmacen);
			btnGuardar.Enabled = false;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
	}

	private void biImprimeLista_Click(object sender, EventArgs e)
	{
		CRArqueo rpt = new CRArqueo();
		frmRptArqueo frm = new frmRptArqueo();
		rpt.SetDataSource(ds.Arqueo(codArqueos).Tables[0]);
		frm.crvArqueo.ReportSource = rpt;
		frm.Show();
	}

	private void dgvProductos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void dgvProductos_CellEndEdit(object sender, DataGridViewCellEventArgs e)
	{
		if (dgvProductos.Rows.Count > 0)
		{
			double dif = Convert.ToDouble(dgvProductos.Rows[e.RowIndex].Cells[9].Value) - Convert.ToDouble(dgvProductos.Rows[e.RowIndex].Cells[8].Value);
			dgvProductos.Rows[e.RowIndex].Cells[10].Value = dif;
			if (dif < 0.0)
			{
				dgvProductos.Rows[e.RowIndex].Cells[10].Style.BackColor = Color.Salmon;
			}
			else if (dif == 0.0)
			{
				dgvProductos.Rows[e.RowIndex].Cells[10].Style.BackColor = Color.Silver;
			}
			else
			{
				dgvProductos.Rows[e.RowIndex].Cells[10].Style.BackColor = Color.Aquamarine;
			}
		}
	}

	private void btActualizaDiferencias_Click(object sender, EventArgs e)
	{
		if (textBox1.Text == "")
		{
			MessageBox.Show("Por favor vuelva a seleccion el arqueo a APROBAR.");
			dgArqueos.Focus();
			return;
		}
		arq.ICodArqueo = Convert.ToInt32(textBox1.Text);
		arq.ICodUsuarioApro = frmLogin.iCodUser;
		arq.SObservacion = textBox3.Text;
		try
		{
			if (admarq.update(arq))
			{
				MessageBox.Show("Aprobacion Correcta.");
				CargaArqueos(1, 3, 1, 1, frmLogin.iCodAlmacen);
			}
			else
			{
				MessageBox.Show("Error en los Datos. No se APROBÓ");
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex);
		}
	}

	private void btChekear_Click(object sender, EventArgs e)
	{
		int codArq = Convert.ToInt32(textBox1.Text);
		if (dgvProductos.Rows.Count <= 0)
		{
			return;
		}
		int sumo = 0;
		for (int conta = 0; conta <= dgvProductos.Rows.Count - 1; conta++)
		{
			detarque.ICodDetalle = Convert.ToInt32(dgvProductos.Rows[conta].Cells[0].Value);
			detarque.DStockF = Convert.ToDecimal(dgvProductos.Rows[conta].Cells[7].Value);
			detarque.DDiferencia = Convert.ToDecimal(dgvProductos.Rows[conta].Cells[8].Value);
			detarque.SObservacion = dgvProductos.Rows[conta].Cells[10].Value.ToString();
			try
			{
				if (admarq.insertChekeoDetalle(detarque, codArq))
				{
					sumo++;
				}
				else
				{
					MessageBox.Show("Error en Procedure");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error: " + ex.Message);
			}
		}
		if (sumo == dgvProductos.Rows.Count)
		{
			MessageBox.Show("Chekeo Correcto");
			CargaArqueos(1, 3, 1, 1, frmLogin.iCodAlmacen);
		}
	}

	private void btImprim_Click(object sender, EventArgs e)
	{
		if (estaArqueo == null)
		{
			return;
		}
		string text = estaArqueo;
		string text2 = text;
		if (!(text2 == "Cargado"))
		{
			if (text2 == "Aprobado")
			{
				imprimeCargados(codArqueos, 1);
			}
		}
		else
		{
			imprimeCargados(codArqueos, 1);
		}
	}

	private void imprimeCargados(int codArqueos, int estaArq)
	{
		CRArqueoCargado rpt = new CRArqueoCargado();
		frmRptArqueoCargado frm = new frmRptArqueoCargado();
		rpt.SetDataSource(dsC.Arqueo(codArqueos, estaArq).Tables[0]);
		frm.crvArqueoCargado.ReportSource = rpt;
		frm.Show();
	}

	private void biImprimeArqueo_Click(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmArqueos));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.ribbonBar2 = new DevComponents.DotNetBar.RibbonBar();
		this.biNuevo = new DevComponents.DotNetBar.ButtonItem();
		this.biModificar = new DevComponents.DotNetBar.ButtonItem();
		this.biActualizar = new DevComponents.DotNetBar.ButtonItem();
		this.biBuscar = new DevComponents.DotNetBar.ButtonItem();
		this.biImprimeLista = new DevComponents.DotNetBar.ButtonItem();
		this.groupBox3 = new System.Windows.Forms.GroupBox();
		this.btImprim = new System.Windows.Forms.Button();
		this.btChekear = new System.Windows.Forms.Button();
		this.btAprobar = new System.Windows.Forms.Button();
		this.dgvProductos = new System.Windows.Forms.DataGridView();
		this.groupBox2 = new System.Windows.Forms.GroupBox();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.textBox3 = new System.Windows.Forms.TextBox();
		this.textBox2 = new System.Windows.Forms.TextBox();
		this.dtpFecha = new System.Windows.Forms.DateTimePicker();
		this.textBox1 = new System.Windows.Forms.TextBox();
		this.label4 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.chkMes = new System.Windows.Forms.CheckBox();
		this.cTodos = new System.Windows.Forms.CheckBox();
		this.cTodosEs = new System.Windows.Forms.CheckBox();
		this.cAño = new System.Windows.Forms.ComboBox();
		this.cMes = new System.Windows.Forms.ComboBox();
		this.rAprobado = new System.Windows.Forms.RadioButton();
		this.rCargado = new System.Windows.Forms.RadioButton();
		this.rGenerado = new System.Windows.Forms.RadioButton();
		this.label5 = new System.Windows.Forms.Label();
		this.dgArqueos = new System.Windows.Forms.DataGridView();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.observacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codUsuario2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codusuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.fecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codarqueo = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.codproductos = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.referencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.um = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockS = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.stockF = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.diferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.groupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).BeginInit();
		this.groupBox2.SuspendLayout();
		this.groupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgArqueos).BeginInit();
		base.SuspendLayout();
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "Add Green Button.png");
		this.imageList1.Images.SetKeyName(1, "Add.png");
		this.imageList1.Images.SetKeyName(2, "Remove.png");
		this.imageList1.Images.SetKeyName(3, "Write Document.png");
		this.imageList1.Images.SetKeyName(4, "New Document.png");
		this.imageList1.Images.SetKeyName(5, "Remove Document.png");
		this.imageList1.Images.SetKeyName(6, "1328102023_Copy.png");
		this.imageList1.Images.SetKeyName(7, "document-print.png");
		this.imageList1.Images.SetKeyName(8, "g-icon-new-update.png");
		this.imageList1.Images.SetKeyName(9, "refresh_256.png");
		this.imageList1.Images.SetKeyName(10, "Refresh-icon.png");
		this.imageList1.Images.SetKeyName(11, "search (1).png");
		this.imageList1.Images.SetKeyName(12, "search (5).png");
		this.imageList1.Images.SetKeyName(13, "search (6).png");
		this.imageList1.Images.SetKeyName(14, "search (8).png");
		this.imageList1.Images.SetKeyName(15, "search_top.png");
		this.imageList1.Images.SetKeyName(16, "icon-47203_640.png");
		this.imageList1.Images.SetKeyName(17, "Folder open.png");
		this.imageList1.Images.SetKeyName(18, "guardar-documento-icono-7840-48.png");
		this.imageList1.Images.SetKeyName(19, "check.png");
		this.imageList1.Images.SetKeyName(20, "check 2.png");
		this.imageList1.Images.SetKeyName(21, "j0432614.png");
		this.imageList1.Images.SetKeyName(22, "checkmark-resized-600.jpg");
		this.imageList1.Images.SetKeyName(23, "sello-aprobado.png");
		this.imageList1.Images.SetKeyName(24, "sello22.png");
		this.imageList1.Images.SetKeyName(25, "postage_stamp_256.png");
		this.ribbonBar2.AutoOverflowEnabled = true;
		this.ribbonBar2.ContainerControlProcessDialogKey = true;
		this.ribbonBar2.Dock = System.Windows.Forms.DockStyle.Top;
		this.ribbonBar2.Images = this.imageList1;
		this.ribbonBar2.Items.AddRange(new DevComponents.DotNetBar.BaseItem[5] { this.biNuevo, this.biModificar, this.biActualizar, this.biBuscar, this.biImprimeLista });
		this.ribbonBar2.Location = new System.Drawing.Point(0, 0);
		this.ribbonBar2.Name = "ribbonBar2";
		this.ribbonBar2.Size = new System.Drawing.Size(1184, 55);
		this.ribbonBar2.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
		this.ribbonBar2.TabIndex = 5;
		this.ribbonBar2.Text = "ribbonBar2";
		this.ribbonBar2.TitleVisible = false;
		this.biNuevo.ImageIndex = 4;
		this.biNuevo.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biNuevo.Name = "biNuevo";
		this.biNuevo.SubItemsExpandWidth = 14;
		this.biNuevo.Text = "Nuevo";
		this.biNuevo.Click += new System.EventHandler(biNuevo_Click);
		this.biModificar.ImageIndex = 3;
		this.biModificar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biModificar.Name = "biModificar";
		this.biModificar.SubItemsExpandWidth = 14;
		this.biModificar.Text = "Modificar";
		this.biActualizar.ImageIndex = 8;
		this.biActualizar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biActualizar.Name = "biActualizar";
		this.biActualizar.SubItemsExpandWidth = 14;
		this.biActualizar.Text = "Actualizar";
		this.biBuscar.ImageIndex = 11;
		this.biBuscar.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biBuscar.Name = "biBuscar";
		this.biBuscar.SubItemsExpandWidth = 14;
		this.biBuscar.Text = "Buscar";
		this.biImprimeLista.ImageIndex = 7;
		this.biImprimeLista.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
		this.biImprimeLista.Name = "biImprimeLista";
		this.biImprimeLista.SubItemsExpandWidth = 14;
		this.biImprimeLista.Text = "Lista";
		this.biImprimeLista.Click += new System.EventHandler(biImprimeLista_Click);
		this.groupBox3.Controls.Add(this.btImprim);
		this.groupBox3.Controls.Add(this.btChekear);
		this.groupBox3.Controls.Add(this.btAprobar);
		this.groupBox3.Controls.Add(this.dgvProductos);
		this.groupBox3.Dock = System.Windows.Forms.DockStyle.Right;
		this.groupBox3.Location = new System.Drawing.Point(404, 55);
		this.groupBox3.Name = "groupBox3";
		this.groupBox3.Size = new System.Drawing.Size(780, 507);
		this.groupBox3.TabIndex = 6;
		this.groupBox3.TabStop = false;
		this.groupBox3.Text = "Detalle de Arqueo";
		this.btImprim.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btImprim.Enabled = false;
		this.btImprim.ImageIndex = 7;
		this.btImprim.ImageList = this.imageList1;
		this.btImprim.Location = new System.Drawing.Point(331, 456);
		this.btImprim.Name = "btImprim";
		this.btImprim.Size = new System.Drawing.Size(92, 39);
		this.btImprim.TabIndex = 17;
		this.btImprim.Text = "Imprimir";
		this.btImprim.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btImprim.UseVisualStyleBackColor = true;
		this.btImprim.Click += new System.EventHandler(btImprim_Click);
		this.btChekear.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btChekear.Enabled = false;
		this.btChekear.ImageIndex = 20;
		this.btChekear.ImageList = this.imageList1;
		this.btChekear.Location = new System.Drawing.Point(166, 456);
		this.btChekear.Name = "btChekear";
		this.btChekear.Size = new System.Drawing.Size(95, 39);
		this.btChekear.TabIndex = 16;
		this.btChekear.Text = "Checkear";
		this.btChekear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btChekear.UseVisualStyleBackColor = true;
		this.btChekear.Click += new System.EventHandler(btChekear_Click);
		this.btAprobar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btAprobar.Enabled = false;
		this.btAprobar.ImageIndex = 25;
		this.btAprobar.ImageList = this.imageList1;
		this.btAprobar.Location = new System.Drawing.Point(513, 456);
		this.btAprobar.Name = "btAprobar";
		this.btAprobar.Size = new System.Drawing.Size(92, 39);
		this.btAprobar.TabIndex = 13;
		this.btAprobar.Text = "Aprobar";
		this.btAprobar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btAprobar.UseVisualStyleBackColor = true;
		this.btAprobar.Click += new System.EventHandler(btActualizaDiferencias_Click);
		this.dgvProductos.AllowUserToAddRows = false;
		this.dgvProductos.AllowUserToDeleteRows = false;
		this.dgvProductos.AllowUserToResizeColumns = false;
		this.dgvProductos.AllowUserToResizeRows = false;
		this.dgvProductos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
		this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgvProductos.Columns.AddRange(this.codproducto, this.Column2, this.codarqueo, this.Column4, this.codproductos, this.referencia, this.nombre, this.um, this.stockS, this.stockF, this.diferencia, this.Column5);
		this.dgvProductos.Location = new System.Drawing.Point(3, 16);
		this.dgvProductos.Name = "dgvProductos";
		this.dgvProductos.RowHeadersVisible = false;
		this.dgvProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.dgvProductos.Size = new System.Drawing.Size(771, 434);
		this.dgvProductos.TabIndex = 15;
		this.dgvProductos.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(dgvProductos_CellEndEdit);
		this.groupBox2.Controls.Add(this.btnGuardar);
		this.groupBox2.Controls.Add(this.textBox3);
		this.groupBox2.Controls.Add(this.textBox2);
		this.groupBox2.Controls.Add(this.dtpFecha);
		this.groupBox2.Controls.Add(this.textBox1);
		this.groupBox2.Controls.Add(this.label4);
		this.groupBox2.Controls.Add(this.label3);
		this.groupBox2.Controls.Add(this.label2);
		this.groupBox2.Controls.Add(this.label1);
		this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.groupBox2.Location = new System.Drawing.Point(0, 356);
		this.groupBox2.Name = "groupBox2";
		this.groupBox2.Size = new System.Drawing.Size(404, 206);
		this.groupBox2.TabIndex = 7;
		this.groupBox2.TabStop = false;
		this.groupBox2.Text = "Arqueo Seleccionado";
		this.btnGuardar.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageIndex = 21;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(287, 155);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(89, 39);
		this.btnGuardar.TabIndex = 12;
		this.btnGuardar.Text = "Generar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.textBox3.Location = new System.Drawing.Point(86, 79);
		this.textBox3.Multiline = true;
		this.textBox3.Name = "textBox3";
		this.textBox3.Size = new System.Drawing.Size(290, 58);
		this.textBox3.TabIndex = 7;
		this.textBox2.Location = new System.Drawing.Point(86, 53);
		this.textBox2.Name = "textBox2";
		this.textBox2.Size = new System.Drawing.Size(290, 20);
		this.textBox2.TabIndex = 6;
		this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFecha.Location = new System.Drawing.Point(295, 27);
		this.dtpFecha.Name = "dtpFecha";
		this.dtpFecha.Size = new System.Drawing.Size(81, 20);
		this.dtpFecha.TabIndex = 5;
		this.textBox1.Enabled = false;
		this.textBox1.Location = new System.Drawing.Point(86, 27);
		this.textBox1.Name = "textBox1";
		this.textBox1.Size = new System.Drawing.Size(100, 20);
		this.textBox1.TabIndex = 4;
		this.label4.AutoSize = true;
		this.label4.Location = new System.Drawing.Point(13, 82);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(67, 13);
		this.label4.TabIndex = 3;
		this.label4.Text = "Observación";
		this.label3.AutoSize = true;
		this.label3.Location = new System.Drawing.Point(13, 56);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(59, 13);
		this.label3.TabIndex = 2;
		this.label3.Text = "Encargado";
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(252, 30);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(37, 13);
		this.label2.TabIndex = 1;
		this.label2.Text = "Fecha";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(13, 30);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(40, 13);
		this.label1.TabIndex = 0;
		this.label1.Text = "Codigo";
		this.groupBox1.Controls.Add(this.chkMes);
		this.groupBox1.Controls.Add(this.cTodos);
		this.groupBox1.Controls.Add(this.cTodosEs);
		this.groupBox1.Controls.Add(this.cAño);
		this.groupBox1.Controls.Add(this.cMes);
		this.groupBox1.Controls.Add(this.rAprobado);
		this.groupBox1.Controls.Add(this.rCargado);
		this.groupBox1.Controls.Add(this.rGenerado);
		this.groupBox1.Controls.Add(this.label5);
		this.groupBox1.Controls.Add(this.dgArqueos);
		this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.groupBox1.Location = new System.Drawing.Point(0, 55);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(404, 301);
		this.groupBox1.TabIndex = 8;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Relacion de Arqueos";
		this.chkMes.AutoSize = true;
		this.chkMes.Location = new System.Drawing.Point(86, 56);
		this.chkMes.Name = "chkMes";
		this.chkMes.Size = new System.Drawing.Size(76, 17);
		this.chkMes.TabIndex = 28;
		this.chkMes.Text = "Mes y Año";
		this.chkMes.UseVisualStyleBackColor = true;
		this.chkMes.CheckedChanged += new System.EventHandler(chkMes_CheckedChanged);
		this.cTodos.AutoSize = true;
		this.cTodos.Location = new System.Drawing.Point(12, 56);
		this.cTodos.Name = "cTodos";
		this.cTodos.Size = new System.Drawing.Size(56, 17);
		this.cTodos.TabIndex = 27;
		this.cTodos.Text = "Todos";
		this.cTodos.UseVisualStyleBackColor = true;
		this.cTodos.CheckedChanged += new System.EventHandler(cTodos_CheckedChanged);
		this.cTodosEs.AutoSize = true;
		this.cTodosEs.Location = new System.Drawing.Point(12, 33);
		this.cTodosEs.Name = "cTodosEs";
		this.cTodosEs.Size = new System.Drawing.Size(77, 17);
		this.cTodosEs.TabIndex = 26;
		this.cTodosEs.Text = "T. Estados";
		this.cTodosEs.UseVisualStyleBackColor = true;
		this.cTodosEs.CheckedChanged += new System.EventHandler(cTodosEs_CheckedChanged);
		this.cAño.FormattingEnabled = true;
		this.cAño.Items.AddRange(new object[16]
		{
			"2014", "2015", "2016", "2017", "2018", "2019", "2020", "2021", "2022", "2023",
			"2024", "2025", "2026", "2027", "2028", "2030"
		});
		this.cAño.Location = new System.Drawing.Point(255, 54);
		this.cAño.Name = "cAño";
		this.cAño.Size = new System.Drawing.Size(76, 21);
		this.cAño.TabIndex = 25;
		this.cMes.FormattingEnabled = true;
		this.cMes.Items.AddRange(new object[12]
		{
			"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre",
			"Noviembre", "Diciembre"
		});
		this.cMes.Location = new System.Drawing.Point(173, 54);
		this.cMes.Name = "cMes";
		this.cMes.Size = new System.Drawing.Size(76, 21);
		this.cMes.TabIndex = 24;
		this.rAprobado.AutoSize = true;
		this.rAprobado.Location = new System.Drawing.Point(247, 32);
		this.rAprobado.Name = "rAprobado";
		this.rAprobado.Size = new System.Drawing.Size(71, 17);
		this.rAprobado.TabIndex = 22;
		this.rAprobado.Text = "Aprobado";
		this.rAprobado.UseVisualStyleBackColor = true;
		this.rAprobado.CheckedChanged += new System.EventHandler(rAprobado_CheckedChanged);
		this.rCargado.AutoSize = true;
		this.rCargado.Location = new System.Drawing.Point(176, 32);
		this.rCargado.Name = "rCargado";
		this.rCargado.Size = new System.Drawing.Size(65, 17);
		this.rCargado.TabIndex = 21;
		this.rCargado.Text = "Cargado";
		this.rCargado.UseVisualStyleBackColor = true;
		this.rCargado.CheckedChanged += new System.EventHandler(rCargado_CheckedChanged);
		this.rGenerado.AutoSize = true;
		this.rGenerado.Location = new System.Drawing.Point(98, 32);
		this.rGenerado.Name = "rGenerado";
		this.rGenerado.Size = new System.Drawing.Size(72, 17);
		this.rGenerado.TabIndex = 20;
		this.rGenerado.Text = "Generado";
		this.rGenerado.UseVisualStyleBackColor = true;
		this.rGenerado.CheckedChanged += new System.EventHandler(rGenerado_CheckedChanged);
		this.label5.AutoSize = true;
		this.label5.Location = new System.Drawing.Point(13, 16);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(29, 13);
		this.label5.TabIndex = 17;
		this.label5.Text = "Filtro";
		this.dgArqueos.AllowUserToAddRows = false;
		this.dgArqueos.AllowUserToDeleteRows = false;
		this.dgArqueos.AllowUserToResizeColumns = false;
		this.dgArqueos.AllowUserToResizeRows = false;
		this.dgArqueos.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.dgArqueos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
		this.dgArqueos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dgArqueos.Columns.AddRange(this.dataGridViewTextBoxColumn1, this.fecha, this.estado, this.observacion, this.codUsuario2, this.codusuario, this.fecharegistro);
		this.dgArqueos.Location = new System.Drawing.Point(3, 78);
		this.dgArqueos.Name = "dgArqueos";
		this.dgArqueos.ReadOnly = true;
		this.dgArqueos.RowHeadersVisible = false;
		this.dgArqueos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgArqueos.Size = new System.Drawing.Size(395, 210);
		this.dgArqueos.TabIndex = 16;
		this.dgArqueos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgArqueos_CellClick);
		this.dataGridViewTextBoxColumn1.DataPropertyName = "codarqueo";
		this.dataGridViewTextBoxColumn1.HeaderText = "Código";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.dataGridViewTextBoxColumn1.Width = 46;
		this.fecha.DataPropertyName = "fecha";
		this.fecha.HeaderText = "Fecha";
		this.fecha.Name = "fecha";
		this.fecha.ReadOnly = true;
		this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fecha.Width = 43;
		this.estado.DataPropertyName = "estado";
		this.estado.HeaderText = "Estado";
		this.estado.Name = "estado";
		this.estado.ReadOnly = true;
		this.estado.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.estado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.estado.Width = 46;
		this.observacion.DataPropertyName = "observacion";
		this.observacion.HeaderText = "Observacion";
		this.observacion.Name = "observacion";
		this.observacion.ReadOnly = true;
		this.observacion.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.observacion.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.observacion.Width = 73;
		this.codUsuario2.DataPropertyName = "codUsuario";
		this.codUsuario2.HeaderText = "codUsuario";
		this.codUsuario2.Name = "codUsuario2";
		this.codUsuario2.ReadOnly = true;
		this.codUsuario2.Visible = false;
		this.codUsuario2.Width = 86;
		this.codusuario.DataPropertyName = "nombre";
		this.codusuario.HeaderText = "Encargado";
		this.codusuario.Name = "codusuario";
		this.codusuario.ReadOnly = true;
		this.codusuario.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.codusuario.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.codusuario.Width = 65;
		this.fecharegistro.DataPropertyName = "fecharegistro";
		this.fecharegistro.HeaderText = "F. registro";
		this.fecharegistro.Name = "fecharegistro";
		this.fecharegistro.ReadOnly = true;
		this.fecharegistro.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.fecharegistro.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.fecharegistro.Width = 59;
		this.codproducto.DataPropertyName = "coddetalle";
		this.codproducto.HeaderText = "Código";
		this.codproducto.Name = "codproducto";
		this.codproducto.Visible = false;
		this.codproducto.Width = 65;
		this.Column2.DataPropertyName = "codUsuario";
		this.Column2.HeaderText = "codUsuario";
		this.Column2.Name = "Column2";
		this.Column2.Visible = false;
		this.Column2.Width = 86;
		this.codarqueo.DataPropertyName = "codarqueo";
		this.codarqueo.HeaderText = "codarqueo";
		this.codarqueo.Name = "codarqueo";
		this.codarqueo.Visible = false;
		this.codarqueo.Width = 83;
		this.Column4.DataPropertyName = "estado";
		this.Column4.HeaderText = "estado";
		this.Column4.Name = "Column4";
		this.Column4.Visible = false;
		this.Column4.Width = 64;
		this.codproductos.DataPropertyName = "codproducto";
		this.codproductos.HeaderText = "Cod.";
		this.codproductos.Name = "codproductos";
		this.codproductos.ReadOnly = true;
		this.codproductos.Width = 54;
		this.referencia.DataPropertyName = "referencia";
		this.referencia.HeaderText = "Referencia";
		this.referencia.Name = "referencia";
		this.referencia.Width = 84;
		this.nombre.DataPropertyName = "descripcion";
		this.nombre.HeaderText = "Nombre";
		this.nombre.Name = "nombre";
		this.nombre.ReadOnly = true;
		this.nombre.Width = 69;
		this.um.DataPropertyName = "um";
		this.um.HeaderText = "Unidad";
		this.um.Name = "um";
		this.um.ReadOnly = true;
		this.um.Width = 66;
		this.stockS.DataPropertyName = "stockS";
		dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		dataGridViewCellStyle1.Format = "N2";
		this.stockS.DefaultCellStyle = dataGridViewCellStyle1;
		this.stockS.HeaderText = "Stock";
		this.stockS.Name = "stockS";
		this.stockS.ReadOnly = true;
		this.stockS.Width = 60;
		this.stockF.DataPropertyName = "stockF";
		this.stockF.HeaderText = "Stock Fisico";
		this.stockF.Name = "stockF";
		this.stockF.Width = 90;
		this.diferencia.DataPropertyName = "diferencia";
		this.diferencia.HeaderText = "Diferencia";
		this.diferencia.Name = "diferencia";
		this.diferencia.ReadOnly = true;
		this.diferencia.Width = 80;
		this.Column5.DataPropertyName = "observacion";
		this.Column5.HeaderText = "Observacion";
		this.Column5.Name = "Column5";
		this.Column5.Width = 92;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1184, 562);
		base.Controls.Add(this.groupBox1);
		base.Controls.Add(this.groupBox2);
		base.Controls.Add(this.groupBox3);
		base.Controls.Add(this.ribbonBar2);
		this.DoubleBuffered = true;
		base.Name = "frmArqueos";
		this.Text = "Arqueos de Almacen";
		base.WindowState = System.Windows.Forms.FormWindowState.Maximized;
		base.Load += new System.EventHandler(frmArqueos_Load);
		this.groupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvProductos).EndInit();
		this.groupBox2.ResumeLayout(false);
		this.groupBox2.PerformLayout();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgArqueos).EndInit();
		base.ResumeLayout(false);
	}
}
