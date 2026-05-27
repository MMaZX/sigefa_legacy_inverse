using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Validator;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using SIGEFA.Properties;

namespace SIGEFA.Formularios;

public class frmModificarPrecioVentaProducto : Office2007Form
{
	private int codigoProducto;

	private bool tipoModificacionPrecio;

	private int unidadIngresada;

	private int moneda;

	private double tipoCambio;

	private clsValidar ok = new clsValidar();

	private clsProducto producto;

	private clsUnidadMedida unidad;

	private clsAdmProducto AdmPro = new clsAdmProducto();

	private clsAdmUnidad AdmUni = new clsAdmUnidad();

	private IContainer components = null;

	private TextBox txtPrecioVentaActual;

	private TextBox txtDescripcion;

	private Label label2;

	private Label label1;

	private Button btnGuardar;

	private Button btnCancelar;

	private Label lblMensaje;

	private TextBox txtUnidad;

	private Label label3;

	private SuperValidator superValidator1;

	private ErrorProvider errorProvider1;

	private Highlighter highlighter1;

	private GroupPanel groupPanel2;

	private GroupPanel groupPanel1;

	private Label label4;

	private TextBox txtUltPrecioCompra;

	private Label lblPVSoles;

	private TextBox txtPrecioVentaSoles;

	public frmModificarPrecioVentaProducto(int codigoProducto, bool tipoModificacionPrecio, int unidadIngresada, int moneda, double tipoCambio)
	{
		this.tipoModificacionPrecio = tipoModificacionPrecio;
		this.codigoProducto = codigoProducto;
		this.unidadIngresada = unidadIngresada;
		this.moneda = moneda;
		this.tipoCambio = tipoCambio;
		InitializeComponent();
	}

	private void txtPrecioVentaNuevo_KeyPress(object sender, KeyPressEventArgs e)
	{
		ok.SOLONumeros(sender, e);
	}

	private void frmModificarPrecioVentaProducto_Load(object sender, EventArgs e)
	{
		producto = AdmPro.CargaProductoDetalle(codigoProducto, frmLogin.iCodAlmacen, 1, 0, 0);
		unidad = AdmUni.CargaUnidad(unidadIngresada);
		txtDescripcion.Text = producto.Descripcion;
		txtPrecioVentaActual.Text = AdmPro.UltimoPrecioVentaProducto(codigoProducto, unidadIngresada).ToString();
		txtUnidad.Text = unidad.Descripcion;
		txtUltPrecioCompra.Text = AdmPro.UltimoPrecioCompraProducto(codigoProducto, unidadIngresada, 0).ToString();
		if (tipoModificacionPrecio)
		{
			lblMensaje.Text = "Se recomienda AUMENTAR el precio de venta del producto.";
		}
		else
		{
			lblMensaje.Text = "Se recomienda DISMINUIR el precio de venta del producto.";
		}
		base.ActiveControl = txtPrecioVentaSoles;
	}

	private void btnGuardar_Click(object sender, EventArgs e)
	{
		try
		{
			decimal nuevoPrecioVenta = Convert.ToDecimal(txtPrecioVentaSoles.Text.Trim());
			if (nuevoPrecioVenta < 0m)
			{
				MessageBox.Show("Nuevo Precio de Venta debe ser mayor a 0", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			else if (AdmPro.ActualizarPrecioVentaProductoPorUnidad(codigoProducto, unidadIngresada, frmLogin.iCodAlmacen, nuevoPrecioVenta))
			{
				MessageBox.Show("Precio de venta actualizado correctamente", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				Close();
			}
			else
			{
				MessageBox.Show("Ocurrió un problema al realizar la operación", "Producto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show("Error: " + ex.Message.ToString());
		}
	}

	private void btnCancelar_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void txtPrecioVentaSoles_Validating(object sender, CancelEventArgs e)
	{
		if (string.IsNullOrEmpty(txtPrecioVentaSoles.Text))
		{
			e.Cancel = true;
			errorProvider1.SetError(txtPrecioVentaSoles, "Este campo es requerido");
			highlighter1.SetHighlightColor(txtPrecioVentaSoles, eHighlightColor.Red);
		}
		else
		{
			errorProvider1.SetError(txtPrecioVentaSoles, "");
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SIGEFA.Formularios.frmModificarPrecioVentaProducto));
		this.txtUnidad = new System.Windows.Forms.TextBox();
		this.label3 = new System.Windows.Forms.Label();
		this.txtPrecioVentaActual = new System.Windows.Forms.TextBox();
		this.txtDescripcion = new System.Windows.Forms.TextBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label1 = new System.Windows.Forms.Label();
		this.lblMensaje = new System.Windows.Forms.Label();
		this.btnGuardar = new System.Windows.Forms.Button();
		this.btnCancelar = new System.Windows.Forms.Button();
		this.superValidator1 = new DevComponents.DotNetBar.Validator.SuperValidator();
		this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
		this.highlighter1 = new DevComponents.DotNetBar.Validator.Highlighter();
		this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.label4 = new System.Windows.Forms.Label();
		this.txtUltPrecioCompra = new System.Windows.Forms.TextBox();
		this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
		this.lblPVSoles = new System.Windows.Forms.Label();
		this.txtPrecioVentaSoles = new System.Windows.Forms.TextBox();
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).BeginInit();
		this.groupPanel1.SuspendLayout();
		this.groupPanel2.SuspendLayout();
		base.SuspendLayout();
		this.txtUnidad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtUnidad.Location = new System.Drawing.Point(14, 108);
		this.txtUnidad.Name = "txtUnidad";
		this.txtUnidad.ReadOnly = true;
		this.txtUnidad.Size = new System.Drawing.Size(441, 22);
		this.txtUnidad.TabIndex = 5;
		this.label3.AutoSize = true;
		this.label3.BackColor = System.Drawing.Color.Transparent;
		this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label3.Location = new System.Drawing.Point(11, 89);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(70, 16);
		this.label3.TabIndex = 4;
		this.label3.Text = "UNIDAD:";
		this.txtPrecioVentaActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioVentaActual.Location = new System.Drawing.Point(13, 164);
		this.txtPrecioVentaActual.Name = "txtPrecioVentaActual";
		this.txtPrecioVentaActual.ReadOnly = true;
		this.txtPrecioVentaActual.Size = new System.Drawing.Size(210, 22);
		this.txtPrecioVentaActual.TabIndex = 3;
		this.txtDescripcion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtDescripcion.Location = new System.Drawing.Point(14, 27);
		this.txtDescripcion.Multiline = true;
		this.txtDescripcion.Name = "txtDescripcion";
		this.txtDescripcion.ReadOnly = true;
		this.txtDescripcion.Size = new System.Drawing.Size(441, 50);
		this.txtDescripcion.TabIndex = 2;
		this.label2.AutoSize = true;
		this.label2.BackColor = System.Drawing.Color.Transparent;
		this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label2.Location = new System.Drawing.Point(11, 145);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(202, 16);
		this.label2.TabIndex = 1;
		this.label2.Text = "P. DE VENTA ACTUAL (S./):";
		this.label1.AutoSize = true;
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.Location = new System.Drawing.Point(11, 8);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(97, 16);
		this.label1.TabIndex = 0;
		this.label1.Text = "PRODUCTO:";
		this.lblMensaje.BackColor = System.Drawing.Color.Transparent;
		this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblMensaje.Location = new System.Drawing.Point(14, 17);
		this.lblMensaje.Name = "lblMensaje";
		this.lblMensaje.Size = new System.Drawing.Size(441, 53);
		this.lblMensaje.TabIndex = 1;
		this.lblMensaje.Text = "Mensaje";
		this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnGuardar.Image = SIGEFA.Properties.Resources.save;
		this.btnGuardar.Location = new System.Drawing.Point(283, 414);
		this.btnGuardar.Name = "btnGuardar";
		this.btnGuardar.Size = new System.Drawing.Size(98, 33);
		this.btnGuardar.TabIndex = 2;
		this.btnGuardar.Text = "GUARDAR";
		this.btnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnGuardar.UseVisualStyleBackColor = true;
		this.btnGuardar.Click += new System.EventHandler(btnGuardar_Click);
		this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.btnCancelar.Image = SIGEFA.Properties.Resources.x_button;
		this.btnCancelar.Location = new System.Drawing.Point(387, 414);
		this.btnCancelar.Name = "btnCancelar";
		this.btnCancelar.Size = new System.Drawing.Size(102, 33);
		this.btnCancelar.TabIndex = 3;
		this.btnCancelar.Text = "CANCELAR";
		this.btnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
		this.btnCancelar.UseVisualStyleBackColor = true;
		this.btnCancelar.Click += new System.EventHandler(btnCancelar_Click);
		this.superValidator1.ContainerControl = this;
		this.superValidator1.ErrorProvider = this.errorProvider1;
		this.superValidator1.Highlighter = this.highlighter1;
		this.errorProvider1.ContainerControl = this;
		this.errorProvider1.Icon = (System.Drawing.Icon)resources.GetObject("errorProvider1.Icon");
		this.highlighter1.ContainerControl = this;
		this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel1.Controls.Add(this.label4);
		this.groupPanel1.Controls.Add(this.txtUltPrecioCompra);
		this.groupPanel1.Controls.Add(this.txtPrecioVentaActual);
		this.groupPanel1.Controls.Add(this.txtUnidad);
		this.groupPanel1.Controls.Add(this.label2);
		this.groupPanel1.Controls.Add(this.txtDescripcion);
		this.groupPanel1.Controls.Add(this.label3);
		this.groupPanel1.Controls.Add(this.label1);
		this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
		this.groupPanel1.Location = new System.Drawing.Point(12, 12);
		this.groupPanel1.Name = "groupPanel1";
		this.groupPanel1.Size = new System.Drawing.Size(477, 219);
		this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.groupPanel1.Style.BackColorGradientAngle = 90;
		this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel1.Style.BorderBottomWidth = 1;
		this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel1.Style.BorderLeftWidth = 1;
		this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel1.Style.BorderRightWidth = 1;
		this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel1.Style.BorderTopWidth = 1;
		this.groupPanel1.Style.CornerDiameter = 4;
		this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
		this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
		this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
		this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel1.TabIndex = 4;
		this.groupPanel1.Text = "DATOS ACTUALES DEL PRODUCTO";
		this.label4.AutoSize = true;
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.Location = new System.Drawing.Point(243, 145);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(187, 16);
		this.label4.TabIndex = 7;
		this.label4.Text = "ULTIMO P. COMPRA (S./):";
		this.txtUltPrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtUltPrecioCompra.Location = new System.Drawing.Point(246, 164);
		this.txtUltPrecioCompra.Name = "txtUltPrecioCompra";
		this.txtUltPrecioCompra.ReadOnly = true;
		this.txtUltPrecioCompra.Size = new System.Drawing.Size(209, 22);
		this.txtUltPrecioCompra.TabIndex = 6;
		this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
		this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
		this.groupPanel2.Controls.Add(this.lblPVSoles);
		this.groupPanel2.Controls.Add(this.txtPrecioVentaSoles);
		this.groupPanel2.Controls.Add(this.lblMensaje);
		this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
		this.groupPanel2.Location = new System.Drawing.Point(12, 237);
		this.groupPanel2.Name = "groupPanel2";
		this.groupPanel2.Size = new System.Drawing.Size(477, 171);
		this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
		this.groupPanel2.Style.BackColorGradientAngle = 90;
		this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
		this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel2.Style.BorderBottomWidth = 1;
		this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
		this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel2.Style.BorderLeftWidth = 1;
		this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel2.Style.BorderRightWidth = 1;
		this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
		this.groupPanel2.Style.BorderTopWidth = 1;
		this.groupPanel2.Style.CornerDiameter = 4;
		this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
		this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
		this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
		this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
		this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
		this.groupPanel2.TabIndex = 5;
		this.groupPanel2.Text = "NUEVO PRECIO DE VENTA";
		this.lblPVSoles.BackColor = System.Drawing.Color.Transparent;
		this.lblPVSoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.lblPVSoles.Location = new System.Drawing.Point(30, 84);
		this.lblPVSoles.Name = "lblPVSoles";
		this.lblPVSoles.Size = new System.Drawing.Size(169, 37);
		this.lblPVSoles.TabIndex = 4;
		this.lblPVSoles.Text = "NUEVO P. VENTA (S/. SOLES):";
		this.txtPrecioVentaSoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.txtPrecioVentaSoles.Location = new System.Drawing.Point(207, 83);
		this.txtPrecioVentaSoles.Name = "txtPrecioVentaSoles";
		this.txtPrecioVentaSoles.Size = new System.Drawing.Size(230, 38);
		this.txtPrecioVentaSoles.TabIndex = 3;
		this.txtPrecioVentaSoles.Validating += new System.ComponentModel.CancelEventHandler(txtPrecioVentaSoles_Validating);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(497, 451);
		base.Controls.Add(this.groupPanel2);
		base.Controls.Add(this.groupPanel1);
		base.Controls.Add(this.btnCancelar);
		base.Controls.Add(this.btnGuardar);
		this.DoubleBuffered = true;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmModificarPrecioVentaProducto";
		base.ShowIcon = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		this.Text = "Modificar Precio de Venta del Producto";
		base.Load += new System.EventHandler(frmModificarPrecioVentaProducto_Load);
		((System.ComponentModel.ISupportInitialize)this.errorProvider1).EndInit();
		this.groupPanel1.ResumeLayout(false);
		this.groupPanel1.PerformLayout();
		this.groupPanel2.ResumeLayout(false);
		this.groupPanel2.PerformLayout();
		base.ResumeLayout(false);
	}
}
