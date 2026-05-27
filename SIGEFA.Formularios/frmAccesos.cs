using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using RikTheVeggie;
using SIGEFA.Administradores;
using SIGEFA.Entidades;

namespace SIGEFA.Formularios;

public class frmAccesos : Office2007Form
{
	private clsAdmUsuario admUsu = new clsAdmUsuario();

	private clsAdmEmpresa admEmp = new clsAdmEmpresa();

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	private clsAdmFormulario admForm = new clsAdmFormulario();

	private clsAdmAcceso admAcce = new clsAdmAcceso();

	public clsUsuario usu = new clsUsuario();

	private clsAccesos acce = new clsAccesos();

	private int EmpresaSeleccionada;

	private int AlmacenSeleccionado;

	public List<int> codigos = new List<int>();

	private IContainer components = null;

	private RadioButton rbUsuario;

	private RadioButton rbAdministrador;

	private GroupBox groupBox1;

	private Label label1;

	private ComboBox cmbAlmacen;

	private ComboBox cmbEmpresa;

	private Label label16;

	private Label label17;

	public TextBox txtCodUsuario;

	public Label lbNombreUsuario;

	private ImageList imageList1;

	private Button btnSalir;

	private Button btnGuardar;

	private TriStateTreeView tstvFormularios;

	public frmAccesos()
	{
		InitializeComponent();
	}

	private void frmAccesos_Load(object sender, EventArgs e)
	{
		CargaEmpresas();
		cmbEmpresa.SelectedValue = frmLogin.iCodEmpresa;
		EmpresaSeleccionada = Convert.ToInt32(cmbEmpresa.SelectedValue);
		CargaAlmacenes();
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
		AlmacenSeleccionado = Convert.ToInt32(cmbAlmacen.SelectedValue);
		txtCodUsuario.Text = usu.CodUsuario.ToString();
		lbNombreUsuario.Text = usu.Nombre + " " + usu.Apellido;
		llenaarbol(0, null);
		CargaAccesos();
	}

	private void CargaEmpresas()
	{
		cmbEmpresa.DataSource = admEmp.CargaEmpresas();
		cmbEmpresa.DisplayMember = "razonsocial";
		cmbEmpresa.ValueMember = "codEmpresa";
		cmbEmpresa.SelectedIndex = -1;
	}

	private void CargaAlmacenes()
	{
		cmbAlmacen.DataSource = admAlm.CargaAlmacenes(frmLogin.iNivelUser, Convert.ToInt32(cmbEmpresa.SelectedValue), frmLogin.iCodUser);
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.ValueMember = "codAlmacen";
		cmbAlmacen.SelectedIndex = 0;
	}

	private void cmbEmpresa_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CargaAlmacenes();
	}

	private void llenaarbol(int indicePadre, TreeNode nodoPadre)
	{
		DataView hijos = new DataView(admForm.MuestraFormularios());
		hijos.RowFilter = admForm.MuestraFormularios().Columns["padre"].ColumnName + " = " + indicePadre;
		foreach (DataRowView row in hijos)
		{
			TreeNode nuevonodo = new TreeNode();
			nuevonodo.Text = row["descripcion"].ToString();
			nuevonodo.Tag = row["codFormulario"].ToString();
			if (nodoPadre == null)
			{
				tstvFormularios.Nodes.Add(nuevonodo);
			}
			else
			{
				nodoPadre.Nodes.Add(nuevonodo);
			}
			llenaarbol(int.Parse(row["codFormulario"].ToString()), nuevonodo);
		}
	}

	private void RecorreArbol()
	{
		codigos.Clear();
		if (tstvFormularios.Enabled)
		{
			foreach (TreeNode nod in tstvFormularios.Nodes)
			{
				añadenodos(nod);
			}
		}
		usu.CodigosForm = codigos;
	}

	private void añadenodos(TreeNode nod)
	{
		int countChecked = 0;
		if (nod.Nodes.Count > 0)
		{
			foreach (TreeNode tnChild in nod.Nodes)
			{
				if (tnChild.StateImageIndex == 1)
				{
					countChecked++;
					break;
				}
			}
		}
		if (countChecked > 0)
		{
			codigos.Add(Convert.ToInt32(nod.Tag));
		}
		if (nod.StateImageIndex == 1)
		{
			codigos.Add(Convert.ToInt32(nod.Tag));
		}
		if (nod.Nodes.Count <= 0)
		{
			return;
		}
		foreach (TreeNode tnChild2 in nod.Nodes)
		{
			añadenodos(tnChild2);
		}
	}

	private void CargaAccesos()
	{
		codigos = admAcce.MuestraAccesos(usu.CodUsuario, Convert.ToInt32(cmbAlmacen.SelectedValue));
		foreach (TreeNode nod in tstvFormularios.Nodes)
		{
			marcanodo(nod);
		}
	}

	private void marcanodo(TreeNode nod)
	{
		int codi = Convert.ToInt32(nod.Tag);
		if (codigos.Contains(codi))
		{
			nod.Checked = true;
		}
		else
		{
			nod.Checked = false;
		}
		if (nod.Nodes.Count <= 0)
		{
			return;
		}
		foreach (TreeNode tn in nod.Nodes)
		{
			marcanodo(tn);
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		RecorreArbol();
		admAcce.LimpiarAccesos(usu.CodUsuario, Convert.ToInt32(cmbAlmacen.SelectedValue));
		if (codigos.Count > 0)
		{
			acce.CodUsuario = usu.CodUsuario;
			acce.CodAlmacen = AlmacenSeleccionado;
			acce.CodUser = frmLogin.iCodUser;
			foreach (int formu in usu.CodigosForm)
			{
				acce.CodFormulario = formu;
				admAcce.insert(acce);
			}
		}
		MessageBox.Show("Los datos se guardaron correctamente", "Otorga Accesos", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		btnGuardar.Enabled = false;
	}

	private void tstvFormularios_AfterCheck(object sender, TreeViewEventArgs e)
	{
	}

	private void cmbEmpresa_SelectionChangeCommitted_1(object sender, EventArgs e)
	{
		if (btnGuardar.Enabled)
		{
			DialogResult dlgResult = MessageBox.Show("Desea cambiar de Empresa sin guardar los cambios hechos", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult == DialogResult.No)
			{
				cmbEmpresa.SelectedValue = EmpresaSeleccionada;
				return;
			}
			EmpresaSeleccionada = Convert.ToInt32(cmbEmpresa.SelectedValue);
			CargaAlmacenes();
			CargaAccesos();
		}
		else
		{
			EmpresaSeleccionada = Convert.ToInt32(cmbEmpresa.SelectedValue);
			CargaAlmacenes();
			CargaAccesos();
		}
	}

	private void cmbAlmacen_SelectionChangeCommitted(object sender, EventArgs e)
	{
		if (btnGuardar.Enabled)
		{
			DialogResult dlgResult = MessageBox.Show("Desea cambiar de Almacen sin guardar los cambios hechos", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (dlgResult == DialogResult.No)
			{
				cmbAlmacen.SelectedValue = AlmacenSeleccionado;
				return;
			}
			AlmacenSeleccionado = Convert.ToInt32(cmbAlmacen.SelectedValue);
			CargaAccesos();
		}
		else
		{
			AlmacenSeleccionado = Convert.ToInt32(cmbAlmacen.SelectedValue);
			CargaAccesos();
		}
	}

	private void tstvFormularios_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		btnGuardar.Enabled = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmAccesos));
		this.rbUsuario = new System.Windows.Forms.RadioButton();
		this.rbAdministrador = new System.Windows.Forms.RadioButton();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.lbNombreUsuario = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.txtCodUsuario = new System.Windows.Forms.TextBox();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.cmbEmpresa = new System.Windows.Forms.ComboBox();
		this.label16 = new System.Windows.Forms.Label();
		this.label17 = new System.Windows.Forms.Label();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.btnSalir = new System.Windows.Forms.Button();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.tstvFormularios = new RikTheVeggie.TriStateTreeView();
		this.groupBox1.SuspendLayout();
		base.SuspendLayout();
		this.rbUsuario.AutoSize = true;
		this.rbUsuario.Checked = true;
		this.rbUsuario.Location = new System.Drawing.Point(29, 49);
		this.rbUsuario.Name = "rbUsuario";
		this.rbUsuario.Size = new System.Drawing.Size(61, 17);
		this.rbUsuario.TabIndex = 3;
		this.rbUsuario.TabStop = true;
		this.rbUsuario.Text = "Usuario";
		this.rbUsuario.UseVisualStyleBackColor = true;
		this.rbAdministrador.AutoSize = true;
		this.rbAdministrador.Location = new System.Drawing.Point(29, 232);
		this.rbAdministrador.Name = "rbAdministrador";
		this.rbAdministrador.Size = new System.Drawing.Size(88, 17);
		this.rbAdministrador.TabIndex = 2;
		this.rbAdministrador.Text = "Administrador";
		this.rbAdministrador.UseVisualStyleBackColor = true;
		this.groupBox1.Controls.Add(this.tstvFormularios);
		this.groupBox1.Controls.Add(this.lbNombreUsuario);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.txtCodUsuario);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.cmbEmpresa);
		this.groupBox1.Controls.Add(this.label16);
		this.groupBox1.Controls.Add(this.label17);
		this.groupBox1.Location = new System.Drawing.Point(12, 12);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(524, 284);
		this.groupBox1.TabIndex = 15;
		this.groupBox1.TabStop = false;
		this.groupBox1.Text = "Configuración de Accesos";
		this.lbNombreUsuario.AutoSize = true;
		this.lbNombreUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lbNombreUsuario.Location = new System.Drawing.Point(147, 19);
		this.lbNombreUsuario.Name = "lbNombreUsuario";
		this.lbNombreUsuario.Size = new System.Drawing.Size(41, 13);
		this.lbNombreUsuario.TabIndex = 20;
		this.lbNombreUsuario.Text = "label2";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(17, 19);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(74, 13);
		this.label1.TabIndex = 19;
		this.label1.Text = "Cod. Usuario :";
		this.txtCodUsuario.Location = new System.Drawing.Point(97, 16);
		this.txtCodUsuario.Name = "txtCodUsuario";
		this.txtCodUsuario.Size = new System.Drawing.Size(44, 20);
		this.txtCodUsuario.TabIndex = 18;
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(97, 69);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(194, 21);
		this.cmbAlmacen.TabIndex = 16;
		this.cmbAlmacen.SelectionChangeCommitted += new System.EventHandler(cmbAlmacen_SelectionChangeCommitted);
		this.cmbEmpresa.FormattingEnabled = true;
		this.cmbEmpresa.Location = new System.Drawing.Point(97, 42);
		this.cmbEmpresa.Name = "cmbEmpresa";
		this.cmbEmpresa.Size = new System.Drawing.Size(194, 21);
		this.cmbEmpresa.TabIndex = 15;
		this.cmbEmpresa.SelectionChangeCommitted += new System.EventHandler(cmbEmpresa_SelectionChangeCommitted_1);
		this.label16.AutoSize = true;
		this.label16.Location = new System.Drawing.Point(17, 72);
		this.label16.Name = "label16";
		this.label16.Size = new System.Drawing.Size(54, 13);
		this.label16.TabIndex = 14;
		this.label16.Text = "Almacen :";
		this.label17.AutoSize = true;
		this.label17.Location = new System.Drawing.Point(17, 45);
		this.label17.Name = "label17";
		this.label17.Size = new System.Drawing.Size(54, 13);
		this.label17.TabIndex = 13;
		this.label17.Text = "Empresa :";
		this.imageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList1.ImageStream");
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.imageList1.Images.SetKeyName(0, "cross.png");
		this.imageList1.Images.SetKeyName(1, "tick.png");
		this.imageList1.Images.SetKeyName(2, "Clear Green Button.ico");
		this.btnSalir.ImageIndex = 0;
		this.btnSalir.ImageList = this.imageList1;
		this.btnSalir.Location = new System.Drawing.Point(461, 302);
		this.btnSalir.Name = "btnSalir";
		this.btnSalir.Size = new System.Drawing.Size(69, 23);
		this.btnSalir.TabIndex = 17;
		this.btnSalir.Text = "Salir";
		this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnSalir.UseVisualStyleBackColor = true;
		this.btnSalir.Click += new System.EventHandler(btnCancelar_Click);
		this.btnGuardar.Enabled = false;
		this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.btnGuardar.ImageIndex = 1;
		this.btnGuardar.ImageList = this.imageList1;
		this.btnGuardar.Location = new System.Drawing.Point(372, 302);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(75, 23);
		this.btnGuardar.TabIndex = 16;
		this.btnGuardar.Text = "Guardar";
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.tstvFormularios.Location = new System.Drawing.Point(6, 96);
		this.tstvFormularios.Name = "tstvFormularios";
		this.tstvFormularios.Size = new System.Drawing.Size(512, 182);
		this.tstvFormularios.TabIndex = 21;
		this.tstvFormularios.TriStateStyleProperty = RikTheVeggie.TriStateTreeView.TriStateStyles.Standard;
		this.tstvFormularios.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(tstvFormularios_AfterCheck);
		this.tstvFormularios.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(tstvFormularios_NodeMouseClick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(548, 331);
		base.Controls.Add(this.btnSalir);
		base.Controls.Add(this.btnGuardar);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmAccesos";
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Accesos";
		base.Load += new System.EventHandler(frmAccesos_Load);
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		base.ResumeLayout(false);
	}
}
