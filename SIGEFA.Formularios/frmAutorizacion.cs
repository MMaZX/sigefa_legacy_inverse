using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SIGEFA.Administradores;
using SIGEFA.Entidades;
using Telerik.WinControls.Themes;
using Telerik.WinControls.UI;

namespace SIGEFA.Formularios;

public class frmAutorizacion : RadForm
{
	private clsAdmUsuario admusu = new clsAdmUsuario();

	private clsUsuario autorizador = new clsUsuario();

	public int permiso = 0;

	public string user = "";

	public string pass = "";

	public int tipoAccion = 0;

	public bool PermitirAdministradores = false;

	private clsUsuario _usuario = null;

	public int tipoVentanaAAsignarUsuario = 0;

	public frmOrdenCompra ventanaOC = null;

	public frmOrdenesVigentes ventanaOrdVig = null;

	public frmDespacho ventanaDespacho = null;

	internal frmEntrega ventanaEntrega = null;

	internal frmReqAlmacen ventanaReqAlmacen = null;

	internal frmVentas ventanaVentas = null;

	internal frmNotadeCredito ventanaNotaCredito = null;

	internal listaplantillas ventanaListaPlantillas = null;

	internal F2TransferenciaEntreAlmacenes ventanaTransferencia = null;

	internal frmCajaVentasMovimientos ventanaCajaMovimientos = null;

	internal frmNotaIngreso ventanaNotaIngreso = null;

	internal frmDetalleSalida VentanaDetalleSalida = null;

	internal frmListaGuiasFacturacion VentanaListaGuiaFacturacion = null;

	internal frmCotizacionesVigentes VentanaListaCotizaciones = null;

	private IContainer components = null;

	private TelerikMetroBlueTheme telerikMetroBlueTheme1;

	private RadLabel radLabel1;

	private RadTextBox txtClave;

	private TelerikMetroTouchTheme telerikMetroTouchTheme1;

	private RadButton radButton1;

	private RadButton radButton2;

	private RadTextBox txtUsuario;

	private RadLabel radLabel2;

	public frmAutorizacion()
	{
		InitializeComponent();
	}

	private void radButton1_Click(object sender, EventArgs e)
	{
		string usuario = txtUsuario.Text;
		string clave = txtClave.Text;
		bool bandElse = false;
		switch (tipoAccion)
		{
		case 2:
			_usuario = admusu.MuestraUsuario(usuario, clave);
			if (_usuario != null)
			{
				if (verificadorPermisodeUsuario(_usuario, permiso))
				{
					asignarUsuarioaVentanaCorrespondiente(tipoVentanaAAsignarUsuario, _usuario);
					base.DialogResult = DialogResult.OK;
					Close();
					Dispose();
				}
				else
				{
					bandElse = true;
				}
			}
			else
			{
				bandElse = true;
			}
			if (bandElse)
			{
				switch (admusu.verificaUsuario(usuario, clave))
				{
				case 1:
					MessageBox.Show("Usuario No Tiene Permiso Para Esta Accion", "Mensaje Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				case 2:
					MessageBox.Show("Contraseña Incorrecta", "Mensaje Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					break;
				case 0:
					MessageBox.Show("Usuario No Existe", "Mensaje Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					break;
				}
				base.DialogResult = DialogResult.Cancel;
				Close();
				Dispose();
			}
			break;
		case 3:
			_usuario = admusu.MuestraUsuario(usuario, clave);
			if (_usuario != null)
			{
				asignarUsuarioaVentanaCorrespondiente(tipoVentanaAAsignarUsuario, _usuario);
				base.DialogResult = DialogResult.OK;
				Close();
				Dispose();
			}
			else
			{
				bandElse = true;
			}
			if (bandElse)
			{
				switch (admusu.verificaUsuario(usuario, clave))
				{
				case 1:
					MessageBox.Show("Usuario No Tiene Permiso Para Esta Accion", "Mensaje Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					break;
				case 2:
					MessageBox.Show("Contraseña Incorrecta", "Mensaje Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					break;
				case 0:
					MessageBox.Show("Usuario No Existe", "Mensaje Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					break;
				}
				base.DialogResult = DialogResult.Cancel;
				Close();
				Dispose();
			}
			break;
		default:
			autorizador = admusu.cargaAutorizador(usuario, clave);
			if (autorizador != null)
			{
				base.DialogResult = DialogResult.OK;
				Close();
				Dispose();
				user = Convert.ToString(autorizador.CodUsuario);
				break;
			}
			switch (admusu.verificaUsuario(usuario, clave))
			{
			case 1:
				MessageBox.Show("Usuario No Es Autorizador", "Mensaje Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				break;
			case 2:
				MessageBox.Show("Contraseña Incorrecta", "Mensaje Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				break;
			case 0:
				MessageBox.Show("Usuario No Existe", "Mensaje Autorizacion", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				break;
			}
			base.DialogResult = DialogResult.Cancel;
			Close();
			Dispose();
			break;
		}
	}

	private bool verificadorPermisodeUsuario(clsUsuario usuario, int permiso)
	{
		clsAdmAcceso AdmAcce = new clsAdmAcceso();
		List<int> accesos = AdmAcce.MuestraAccesos(usuario.CodUsuario, frmLogin.iCodAlmacen);
		return accesos.Contains(permiso) || (PermitirAdministradores && usuario.Nivel == 1);
	}

	private void asignarUsuarioaVentanaCorrespondiente(int tipoVentanaAAsignarUsuario, clsUsuario _usuario)
	{
		switch (tipoVentanaAAsignarUsuario)
		{
		case 1:
			ventanaOC.usuario_click = _usuario;
			break;
		case 2:
			ventanaOrdVig.usuario_click = _usuario;
			break;
		case 3:
			ventanaDespacho.usuario_click = _usuario;
			break;
		case 4:
			ventanaEntrega.usuario_click = _usuario;
			break;
		case 5:
			ventanaReqAlmacen.usuario_click = _usuario;
			break;
		case 6:
			ventanaVentas.usuario_click = _usuario;
			break;
		case 7:
			ventanaNotaCredito.usuario_click = _usuario;
			break;
		case 8:
			ventanaListaPlantillas.usuario_click = _usuario;
			break;
		case 9:
			ventanaTransferencia.usuario_click = _usuario;
			break;
		case 10:
			ventanaCajaMovimientos.usuario_click = _usuario;
			break;
		case 11:
			ventanaNotaIngreso.usuario_click = _usuario;
			break;
		case 12:
			VentanaDetalleSalida.usuario_click = _usuario;
			break;
		case 13:
			VentanaListaGuiaFacturacion.usuario_click = _usuario;
			break;
		case 14:
			VentanaListaCotizaciones.usuario_click = _usuario;
			break;
		}
	}

	private void frmAutorizacion_Load(object sender, EventArgs e)
	{
		txtUsuario.Focus();
	}

	private void txtClave_TextChanged(object sender, EventArgs e)
	{
	}

	private void radButton2_Click(object sender, EventArgs e)
	{
		Close();
		Dispose();
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
		this.telerikMetroBlueTheme1 = new Telerik.WinControls.Themes.TelerikMetroBlueTheme();
		this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
		this.txtClave = new Telerik.WinControls.UI.RadTextBox();
		this.telerikMetroTouchTheme1 = new Telerik.WinControls.Themes.TelerikMetroTouchTheme();
		this.radButton1 = new Telerik.WinControls.UI.RadButton();
		this.radButton2 = new Telerik.WinControls.UI.RadButton();
		this.txtUsuario = new Telerik.WinControls.UI.RadTextBox();
		this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
		((System.ComponentModel.ISupportInitialize)this.radLabel1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtClave).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radButton1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radButton2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtUsuario).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this).BeginInit();
		base.SuspendLayout();
		this.radLabel1.Location = new System.Drawing.Point(54, 80);
		this.radLabel1.Name = "radLabel1";
		this.radLabel1.Size = new System.Drawing.Size(46, 23);
		this.radLabel1.TabIndex = 0;
		this.radLabel1.Text = "Clave:";
		this.radLabel1.ThemeName = "TelerikMetroTouch";
		this.txtClave.Location = new System.Drawing.Point(112, 80);
		this.txtClave.Name = "txtClave";
		this.txtClave.PasswordChar = '*';
		this.txtClave.Size = new System.Drawing.Size(165, 24);
		this.txtClave.TabIndex = 1;
		this.txtClave.ThemeName = "TelerikMetroBlue";
		this.txtClave.TextChanged += new System.EventHandler(txtClave_TextChanged);
		this.radButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.radButton1.Location = new System.Drawing.Point(34, 130);
		this.radButton1.Name = "radButton1";
		this.radButton1.Size = new System.Drawing.Size(110, 24);
		this.radButton1.TabIndex = 2;
		this.radButton1.Text = "Aceptar";
		this.radButton1.ThemeName = "TelerikMetroBlue";
		this.radButton1.Click += new System.EventHandler(radButton1_Click);
		this.radButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.radButton2.Location = new System.Drawing.Point(184, 130);
		this.radButton2.Name = "radButton2";
		this.radButton2.Size = new System.Drawing.Size(110, 24);
		this.radButton2.TabIndex = 3;
		this.radButton2.Text = "Cancelar";
		this.radButton2.ThemeName = "TelerikMetroBlue";
		this.radButton2.Click += new System.EventHandler(radButton2_Click);
		this.txtUsuario.Location = new System.Drawing.Point(112, 41);
		this.txtUsuario.Name = "txtUsuario";
		this.txtUsuario.PasswordChar = '*';
		this.txtUsuario.Size = new System.Drawing.Size(165, 24);
		this.txtUsuario.TabIndex = 0;
		this.txtUsuario.ThemeName = "TelerikMetroBlue";
		this.radLabel2.Location = new System.Drawing.Point(39, 41);
		this.radLabel2.Name = "radLabel2";
		this.radLabel2.Size = new System.Drawing.Size(61, 23);
		this.radLabel2.TabIndex = 2;
		this.radLabel2.Text = "Usuario:";
		this.radLabel2.ThemeName = "TelerikMetroTouch";
		base.AcceptButton = this.radButton1;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.radButton2;
		base.ClientSize = new System.Drawing.Size(328, 193);
		base.Controls.Add(this.txtUsuario);
		base.Controls.Add(this.radLabel2);
		base.Controls.Add(this.radButton2);
		base.Controls.Add(this.radButton1);
		base.Controls.Add(this.txtClave);
		base.Controls.Add(this.radLabel1);
		base.Name = "frmAutorizacion";
		base.RootElement.ApplyShapeToControl = true;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "frmAutorizacion";
		base.ThemeName = "TelerikMetroBlue";
		base.Load += new System.EventHandler(frmAutorizacion_Load);
		((System.ComponentModel.ISupportInitialize)this.radLabel1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtClave).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radButton1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radButton2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtUsuario).EndInit();
		((System.ComponentModel.ISupportInitialize)this.radLabel2).EndInit();
		((System.ComponentModel.ISupportInitialize)this).EndInit();
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
