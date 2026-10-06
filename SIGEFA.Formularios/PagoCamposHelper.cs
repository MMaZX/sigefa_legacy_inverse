using System.Windows.Forms;

// Centraliza qué campos del pago quedan habilitados y qué se limpia al elegir
// un método de pago en frmCancelarPago. Perfiles tomados del código actual del
// formulario: solo gobierna habilitado y limpieza; los efectos propios de cada
// método (CargarBancos, Focus, diálogo de nota de crédito) se quedan en el
// formulario. Sin lógica de negocio ni acceso a datos.
namespace SIGEFA.Formularios;

// Helper de habilitado y limpieza de campos por método de pago.
internal class PagoCamposHelper
{
    private readonly ComboBox cmbMetodoPago;
    private readonly ComboBox cboBanco;
    private readonly ComboBox cboTarjeta;
    private readonly ComboBox cboNumCta;
    private readonly TextBox txtOperacion;
    private readonly TextBox txtCheque;
    private readonly TextBox txtNc;
    private readonly TextBox txtMontoPago;

    public PagoCamposHelper(ComboBox cmbMetodoPago, ComboBox cboBanco, ComboBox cboTarjeta, ComboBox cboNumCta, TextBox txtOperacion, TextBox txtCheque, TextBox txtNc, TextBox txtMontoPago)
    {
        this.cmbMetodoPago = cmbMetodoPago;
        this.cboBanco = cboBanco;
        this.cboTarjeta = cboTarjeta;
        this.cboNumCta = cboNumCta;
        this.txtOperacion = txtOperacion;
        this.txtCheque = txtCheque;
        this.txtNc = txtNc;
        this.txtMontoPago = txtMontoPago;
    }

    // Fija habilitado y limpieza según el método. El 12 (pendiente) y cualquier
    // otro no listado se tratan como efectivo para no arrastrar banco u
    // operación de otro método.
    public void aplicarMetodo(int metodoId)
    {
        switch (metodoId)
        {
            case 6:
            case 9:
                // Depósito y transferencia: solo banco y operación. La cuenta se
                // habilita al elegir banco (cboBanco_SelectionChangeCommitted).
                cboTarjeta.Enabled = false;
                cboBanco.Enabled = true;
                cboTarjeta.SelectedIndex = -1;
                txtCheque.Text = "";
                txtOperacion.Text = "";
                txtNc.Text = "";
                txtOperacion.Enabled = true;
                txtCheque.Enabled = false;
                txtMontoPago.Enabled = true;
                cboNumCta.Enabled = false;
                cboNumCta.SelectedIndex = -1;
                break;
            case 7:
                // Depósito por cheque: banco, operación y cheque.
                cboTarjeta.Enabled = false;
                cboBanco.Enabled = true;
                cboBanco.SelectedIndex = -1;
                cboTarjeta.SelectedIndex = -1;
                txtOperacion.Text = "";
                txtNc.Text = "";
                txtCheque.Text = "";
                txtOperacion.Enabled = true;
                txtCheque.Enabled = true;
                txtMontoPago.Enabled = true;
                cboNumCta.Enabled = false;
                cboNumCta.SelectedIndex = -1;
                break;
            case 8:
                // Tarjeta: conserva la cuenta corriente habilitada.
                cboTarjeta.Enabled = true;
                cboBanco.Enabled = true;
                cboBanco.SelectedIndex = -1;
                cboTarjeta.SelectedIndex = -1;
                txtOperacion.Text = "";
                txtNc.Text = "";
                txtCheque.Text = "";
                txtOperacion.Enabled = true;
                txtCheque.Enabled = false;
                txtMontoPago.Enabled = true;
                cboNumCta.Enabled = true;
                cboNumCta.SelectedIndex = -1;
                break;
            case 10:
                // Nota de crédito: todo bloqueado, el monto lo pone el diálogo.
                cboTarjeta.Enabled = false;
                cboBanco.Enabled = false;
                cboBanco.SelectedIndex = -1;
                cboTarjeta.SelectedIndex = -1;
                txtOperacion.Text = "";
                txtCheque.Text = "";
                txtNc.Text = "";
                txtOperacion.Enabled = false;
                txtCheque.Enabled = false;
                txtNc.Enabled = false;
                cboNumCta.Enabled = false;
                txtMontoPago.Enabled = false;
                cboNumCta.SelectedIndex = -1;
                break;
            default:
                // Efectivo (5) y cualquier otro: todo bloqueado salvo el monto.
                cboTarjeta.Enabled = false;
                cboBanco.Enabled = false;
                cboBanco.SelectedIndex = -1;
                cboTarjeta.SelectedIndex = -1;
                txtCheque.Text = "";
                txtNc.Text = "";
                txtOperacion.Text = "";
                txtOperacion.Enabled = false;
                txtCheque.Enabled = false;
                txtMontoPago.Enabled = true;
                cboNumCta.Enabled = false;
                cboNumCta.SelectedIndex = -1;
                break;
        }
    }

    // Vuelve a efectivo: fija el método y aplica su perfil.
    public void reiniciarAEfectivo()
    {
        cmbMetodoPago.SelectedValue = 5;
        aplicarMetodo(5);
    }
}
