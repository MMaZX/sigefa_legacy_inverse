using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SIGEFA.Administradores;
using SIGEFA.Reportes.clsReportes;

namespace SIGEFA.Formularios;

public class frmExistencias : Office2007Form
{
	private static BindingSource data = new BindingSource();

	private clsReportProductos rp = new clsReportProductos();

	private clsAdmAlmacen admAlm = new clsAdmAlmacen();

	private int id_existencia_a_mostrar = 0;

	private int in_fila_cms_dgv = -1;

	private IContainer components = null;

	private DataGridView dgvExistencias;

	private Button btnActualizar;

	private DataGridViewTextBoxColumn idExistencia;

	private DataGridViewTextBoxColumn txtTituloExistencia;

	private DataGridViewTextBoxColumn txtfecharegistro;

	private GroupBox groupBox1;

	private DateTimePicker dtPFechaGeneracion;

	private Label label2;

	private Label label1;

	private ComboBox cmbAlmacen;

	private Button btnGenerarReporte;

	private DateTimePicker dtpFechaGeneracionFinal;

	private Button btnEliminarExistenciaListada;

	private ContextMenuStrip cMSFilaOpciones;

	private ToolStripMenuItem verReporteToolStripMenuItem;

	private ToolStripMenuItem eliminarReporteToolStripMenuItem;

	public frmExistencias()
	{
		InitializeComponent();
	}

	private void frmExistencias_Load(object sender, EventArgs e)
	{
		cargarLista();
		CargaAlmacenes();
	}

	private void cargarLista()
	{
		dgvExistencias.DataSource = rp.ListaExistencia();
	}

	private void cargarLista(DateTime fecha, DateTime fechafin)
	{
		dgvExistencias.DataSource = rp.ListaExistencia(fecha, fechafin);
	}

	private void btnActualizar_Click(object sender, EventArgs e)
	{
		cargarLista();
	}

	private void CargaAlmacenes()
	{
		cmbAlmacen.ValueMember = "cod";
		cmbAlmacen.DisplayMember = "nombre";
		cmbAlmacen.DataSource = admAlm.listaAlmacenxNombre(frmLogin.iCodAlmacen);
		cmbAlmacen.SelectedValue = frmLogin.iCodAlmacen;
	}

	private void dtPFechaGeneracion_ValueChanged(object sender, EventArgs e)
	{
		cargarLista(Convert.ToDateTime(dtPFechaGeneracion.Value), Convert.ToDateTime(dtpFechaGeneracionFinal.Value));
	}

	private void cmbAlmacen_SelectedIndexChanged(object sender, EventArgs e)
	{
	}

	private void btnGenerarReporte_Click(object sender, EventArgs e)
	{
		if (id_existencia_a_mostrar != 0)
		{
			rptExistencia frm = new rptExistencia();
			frm.codExist = id_existencia_a_mostrar;
			frm.alma = Convert.ToInt32(cmbAlmacen.SelectedValue);
			frm.ShowDialog();
		}
	}

	private void dgvExistencias_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void dgvExistencias_SelectionChanged(object sender, EventArgs e)
	{
	}

	private void dgvExistencias_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		try
		{
			id_existencia_a_mostrar = Convert.ToInt32(dgvExistencias.Rows[e.RowIndex].Cells[idExistencia.Name].Value);
			btnGenerarReporte.Enabled = true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Error Al Seleccionar Columna [frmExistencia]", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void dtpFechaGeneracionFinal_ValueChanged(object sender, EventArgs e)
	{
		cargarLista(Convert.ToDateTime(dtPFechaGeneracion.Value), Convert.ToDateTime(dtpFechaGeneracionFinal.Value));
	}

	private void btnEliminarExistenciaListada_Click(object sender, EventArgs e)
	{
		if (dgvExistencias.Rows.Count > 0)
		{
			if (dgvExistencias.SelectedRows.Count < 40)
			{
				string txt = "";
				int contador = 0;
				foreach (DataGridViewRow fila in dgvExistencias.SelectedRows)
				{
					contador++;
					txt = txt + "\n" + contador + ": " + fila.Cells[txtTituloExistencia.Name].Value.ToString();
				}
				DialogResult rspta = MessageBox.Show("Esta seguro de eliminar las existencias listadas?" + txt + "\n\nADVERTENCIA: SI ELIMINA ESTAS EXISTENCIAS NO SE PODRAN RECUPERAR.", "Listado de Existencias dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
				if (rspta == DialogResult.Yes)
				{
					DialogResult dr = DialogResult.None;
					frmAutorizacion frm = new frmAutorizacion();
					dr = frm.ShowDialog();
					if (dr == DialogResult.OK)
					{
						Cursor = Cursors.WaitCursor;
						int aux_contador = 1;
						int contador_eliminados = 0;
						int contador_no_eliminados = 0;
						string relacion_no_eliminados = "";
						foreach (DataGridViewRow fila2 in dgvExistencias.SelectedRows)
						{
							int codigo_existencia = Convert.ToInt32(fila2.Cells[idExistencia.Name].Value);
							if (rp.EliminaExistencia(codigo_existencia))
							{
								contador_eliminados++;
								continue;
							}
							contador_no_eliminados++;
							relacion_no_eliminados = relacion_no_eliminados + "\n" + aux_contador + ": " + fila2.Cells[txtTituloExistencia.Name].Value.ToString();
							aux_contador++;
						}
						if (contador_no_eliminados > 0)
						{
							MessageBox.Show("Las Siguientes (" + contador_no_eliminados + ") Existencias no se pudieron eliminar:" + relacion_no_eliminados, "Listado de Existencias dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						else
						{
							MessageBox.Show("Las Existencias han sido eliminadas con exito.", "Listado de Existencias dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						}
						Cursor = Cursors.Default;
					}
					else
					{
						MessageBox.Show("No se borraron los elementos. No cuenta con el permiso suficiente", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
			}
			else
			{
				MessageBox.Show("Ha seleccionado muchos elementos para eliminar. \nIntente seleccionando una cantidad menor", "Listado de Existencias dice:", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		else
		{
			MessageBox.Show("No hay datos para eliminar.", "Listado de Existencias dice:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		cargarLista();
	}

	private void verReporteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (in_fila_cms_dgv == -1)
		{
			MessageBox.Show("Ocurrio un Error Al Seleccionar Fila", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		try
		{
			rptExistencia frm = new rptExistencia();
			frm.codExist = Convert.ToInt32(dgvExistencias.Rows[in_fila_cms_dgv].Cells[idExistencia.Name].Value);
			frm.alma = Convert.ToInt32(cmbAlmacen.SelectedValue);
			frm.ShowDialog();
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void eliminarReporteToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (in_fila_cms_dgv == -1)
		{
			MessageBox.Show("Ocurrio un Error Al Seleccionar Fila", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		try
		{
			int codigo_existencia = Convert.ToInt32(dgvExistencias.Rows[in_fila_cms_dgv].Cells[idExistencia.Name].Value);
			string txt = "";
			txt = txt + "\n1: " + dgvExistencias.Rows[in_fila_cms_dgv].Cells[txtTituloExistencia.Name].Value.ToString();
			DialogResult rspta = MessageBox.Show("Esta seguro de eliminar la siguiente existencia:" + txt + "\n\nADVERTENCIA: SI ELIMINA ESTA EXISTENCIA NO SE PODRA RECUPERAR.", "Listado de Existencias dice:", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			if (rspta == DialogResult.Yes)
			{
				DialogResult dr = DialogResult.None;
				frmAutorizacion frm = new frmAutorizacion();
				dr = frm.ShowDialog();
				if (dr == DialogResult.OK)
				{
					Cursor = Cursors.WaitCursor;
					rp.EliminaExistencia(codigo_existencia);
					MessageBox.Show("La Existencia ha sido eliminada con exito.", "Listado de Existencias dice:", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					Cursor = Cursors.Default;
				}
			}
			else
			{
				MessageBox.Show("No se borraron los elementos. No cuenta con el permiso suficiente", Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, Text + " dice: ", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
		cargarLista();
	}

	private void dgvExistencias_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.Button == MouseButtons.Right && e.ColumnIndex >= 0 && e.RowIndex >= 0)
		{
			Point aux = dgvExistencias.PointToClient(Cursor.Position);
			dgvExistencias.ClearSelection();
			dgvExistencias.Rows[e.RowIndex].Selected = true;
			in_fila_cms_dgv = e.RowIndex;
			cMSFilaOpciones.Show(dgvExistencias, aux);
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
		this.dgvExistencias = new System.Windows.Forms.DataGridView();
		this.idExistencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtTituloExistencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.txtfecharegistro = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.btnActualizar = new System.Windows.Forms.Button();
		this.groupBox1 = new System.Windows.Forms.GroupBox();
		this.btnEliminarExistenciaListada = new System.Windows.Forms.Button();
		this.dtpFechaGeneracionFinal = new System.Windows.Forms.DateTimePicker();
		this.btnGenerarReporte = new System.Windows.Forms.Button();
		this.dtPFechaGeneracion = new System.Windows.Forms.DateTimePicker();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.cmbAlmacen = new System.Windows.Forms.ComboBox();
		this.cMSFilaOpciones = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.verReporteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.eliminarReporteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		((System.ComponentModel.ISupportInitialize)this.dgvExistencias).BeginInit();
		this.groupBox1.SuspendLayout();
		this.cMSFilaOpciones.SuspendLayout();
		base.SuspendLayout();
		this.dgvExistencias.AllowUserToAddRows = false;
		this.dgvExistencias.AllowUserToDeleteRows = false;
		this.dgvExistencias.AllowUserToResizeRows = false;
		this.dgvExistencias.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom;
		this.dgvExistencias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvExistencias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvExistencias.Columns.AddRange(this.idExistencia, this.txtTituloExistencia, this.txtfecharegistro);
		this.dgvExistencias.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
		this.dgvExistencias.Location = new System.Drawing.Point(0, 106);
		this.dgvExistencias.Name = "dgvExistencias";
		this.dgvExistencias.RowHeadersVisible = false;
		this.dgvExistencias.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.dgvExistencias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.dgvExistencias.Size = new System.Drawing.Size(495, 299);
		this.dgvExistencias.TabIndex = 0;
		this.dgvExistencias.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvExistencias_CellClick);
		this.dgvExistencias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvExistencias_CellContentClick);
		this.dgvExistencias.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvExistencias_CellMouseClick);
		this.dgvExistencias.SelectionChanged += new System.EventHandler(dgvExistencias_SelectionChanged);
		this.idExistencia.DataPropertyName = "idExistencia";
		this.idExistencia.HeaderText = "idExistencia";
		this.idExistencia.Name = "idExistencia";
		this.idExistencia.Visible = false;
		this.txtTituloExistencia.DataPropertyName = "tituloDetalleExistencia";
		this.txtTituloExistencia.HeaderText = "Titulo";
		this.txtTituloExistencia.Name = "txtTituloExistencia";
		this.txtfecharegistro.DataPropertyName = "fecharegistro";
		this.txtfecharegistro.HeaderText = "Fecha de Generacion";
		this.txtfecharegistro.Name = "txtfecharegistro";
		this.btnActualizar.Location = new System.Drawing.Point(386, 14);
		this.btnActualizar.Name = "btnActualizar";
		this.btnActualizar.Size = new System.Drawing.Size(75, 23);
		this.btnActualizar.TabIndex = 1;
		this.btnActualizar.Text = "Actualizar";
		this.btnActualizar.UseVisualStyleBackColor = true;
		this.btnActualizar.Click += new System.EventHandler(btnActualizar_Click);
		this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.groupBox1.Controls.Add(this.btnEliminarExistenciaListada);
		this.groupBox1.Controls.Add(this.dtpFechaGeneracionFinal);
		this.groupBox1.Controls.Add(this.btnGenerarReporte);
		this.groupBox1.Controls.Add(this.dtPFechaGeneracion);
		this.groupBox1.Controls.Add(this.label2);
		this.groupBox1.Controls.Add(this.label1);
		this.groupBox1.Controls.Add(this.cmbAlmacen);
		this.groupBox1.Controls.Add(this.dgvExistencias);
		this.groupBox1.Controls.Add(this.btnActualizar);
		this.groupBox1.Location = new System.Drawing.Point(4, 2);
		this.groupBox1.Name = "groupBox1";
		this.groupBox1.Size = new System.Drawing.Size(496, 411);
		this.groupBox1.TabIndex = 2;
		this.groupBox1.TabStop = false;
		this.btnEliminarExistenciaListada.Location = new System.Drawing.Point(387, 51);
		this.btnEliminarExistenciaListada.Name = "btnEliminarExistenciaListada";
		this.btnEliminarExistenciaListada.Size = new System.Drawing.Size(75, 23);
		this.btnEliminarExistenciaListada.TabIndex = 7;
		this.btnEliminarExistenciaListada.Text = "Eliminar Existencias Listadas";
		this.btnEliminarExistenciaListada.UseVisualStyleBackColor = true;
		this.btnEliminarExistenciaListada.Click += new System.EventHandler(btnEliminarExistenciaListada_Click);
		this.dtpFechaGeneracionFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtpFechaGeneracionFinal.Location = new System.Drawing.Point(11, 51);
		this.dtpFechaGeneracionFinal.Name = "dtpFechaGeneracionFinal";
		this.dtpFechaGeneracionFinal.Size = new System.Drawing.Size(151, 20);
		this.dtpFechaGeneracionFinal.TabIndex = 6;
		this.dtpFechaGeneracionFinal.ValueChanged += new System.EventHandler(dtpFechaGeneracionFinal_ValueChanged);
		this.btnGenerarReporte.Enabled = false;
		this.btnGenerarReporte.Location = new System.Drawing.Point(467, 19);
		this.btnGenerarReporte.Name = "btnGenerarReporte";
		this.btnGenerarReporte.Size = new System.Drawing.Size(75, 41);
		this.btnGenerarReporte.TabIndex = 5;
		this.btnGenerarReporte.Text = "Visualizar Reporte";
		this.btnGenerarReporte.UseVisualStyleBackColor = true;
		this.btnGenerarReporte.Visible = false;
		this.btnGenerarReporte.Click += new System.EventHandler(btnGenerarReporte_Click);
		this.dtPFechaGeneracion.Format = System.Windows.Forms.DateTimePickerFormat.Short;
		this.dtPFechaGeneracion.Location = new System.Drawing.Point(11, 27);
		this.dtPFechaGeneracion.Name = "dtPFechaGeneracion";
		this.dtPFechaGeneracion.Size = new System.Drawing.Size(151, 20);
		this.dtPFechaGeneracion.TabIndex = 4;
		this.dtPFechaGeneracion.ValueChanged += new System.EventHandler(dtPFechaGeneracion_ValueChanged);
		this.label2.AutoSize = true;
		this.label2.Location = new System.Drawing.Point(8, 10);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(154, 13);
		this.label2.TabIndex = 3;
		this.label2.Text = "Listar por fecha de generacion:";
		this.label1.AutoSize = true;
		this.label1.Location = new System.Drawing.Point(212, 20);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(48, 13);
		this.label1.TabIndex = 3;
		this.label1.Text = "Almacen";
		this.cmbAlmacen.FormattingEnabled = true;
		this.cmbAlmacen.Location = new System.Drawing.Point(213, 37);
		this.cmbAlmacen.Name = "cmbAlmacen";
		this.cmbAlmacen.Size = new System.Drawing.Size(134, 21);
		this.cmbAlmacen.TabIndex = 2;
		this.cmbAlmacen.SelectedIndexChanged += new System.EventHandler(cmbAlmacen_SelectedIndexChanged);
		this.cMSFilaOpciones.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.verReporteToolStripMenuItem, this.eliminarReporteToolStripMenuItem });
		this.cMSFilaOpciones.Name = "cMSFilaOpciones";
		this.cMSFilaOpciones.Size = new System.Drawing.Size(162, 48);
		this.cMSFilaOpciones.Text = "Opciones de Fila:";
		this.verReporteToolStripMenuItem.Name = "verReporteToolStripMenuItem";
		this.verReporteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
		this.verReporteToolStripMenuItem.Text = "Ver Reporte";
		this.verReporteToolStripMenuItem.Click += new System.EventHandler(verReporteToolStripMenuItem_Click);
		this.eliminarReporteToolStripMenuItem.Name = "eliminarReporteToolStripMenuItem";
		this.eliminarReporteToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
		this.eliminarReporteToolStripMenuItem.Text = "Eliminar Reporte";
		this.eliminarReporteToolStripMenuItem.Click += new System.EventHandler(eliminarReporteToolStripMenuItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(501, 411);
		base.Controls.Add(this.groupBox1);
		this.DoubleBuffered = true;
		base.Name = "frmExistencias";
		this.Text = "Listado de Existencias";
		base.Load += new System.EventHandler(frmExistencias_Load);
		((System.ComponentModel.ISupportInitialize)this.dgvExistencias).EndInit();
		this.groupBox1.ResumeLayout(false);
		this.groupBox1.PerformLayout();
		this.cMSFilaOpciones.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
