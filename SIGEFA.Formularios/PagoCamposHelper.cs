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

    // Ids de método de pago (tabla metodo_pago):
    // 5 EFECTIVO (es efectivo), 6 DEPOSITO (requiere aprobación),
    // 7 DEPOSITO POR CHEQUE, 8 TARJETA (es POS), 9 TRANSFERENCIA,
    // 10 NOTA CREDITO, 11 TARJETA CREDITO (inactivo), 12 PENDIENTE,
    // 13 Redondeo (inactivo), 14 Extra (inactivo).
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
                // El banco no se limpia acá: CargarBancos() reenlaza después.
                fijar(tarjeta: false, banco: true, operacion: true, cheque: false, cuenta: false, monto: true, limpiaBanco: false);
                break;
            case 7:
                // Depósito por cheque: banco, operación y cheque.
                fijar(tarjeta: false, banco: true, operacion: true, cheque: true, cuenta: false, monto: true);
                break;
            case 8:
                // Tarjeta: conserva la cuenta corriente habilitada.
                fijar(tarjeta: true, banco: true, operacion: true, cheque: false, cuenta: true, monto: true);
                break;
            case 10:
                // Nota de crédito: todo bloqueado, el monto lo pone el diálogo.
                fijar(tarjeta: false, banco: false, operacion: false, cheque: false, cuenta: false, monto: false, bloqueaNc: true);
                break;
            default:
                // Efectivo (5) y cualquier otro (11, 12, 13, 14): todo bloqueado salvo el monto.
                fijar(tarjeta: false, banco: false, operacion: false, cheque: false, cuenta: false, monto: true);
                break;
        }
    }

    // Aplica un perfil: qué queda habilitado y qué se limpia para no arrastrar
    // valores del método anterior. Solo el 10 bloquea txtNc (como hoy, nada lo
    // vuelve a habilitar).
    private void fijar(bool tarjeta, bool banco, bool operacion, bool cheque, bool cuenta, bool monto, bool limpiaBanco = true, bool bloqueaNc = false)
    {
        cboTarjeta.Enabled = tarjeta;
        cboBanco.Enabled = banco;
        txtOperacion.Enabled = operacion;
        txtCheque.Enabled = cheque;
        cboNumCta.Enabled = cuenta;
        txtMontoPago.Enabled = monto;
        if (limpiaBanco)
        {
            cboBanco.SelectedIndex = -1;
        }
        cboTarjeta.SelectedIndex = -1;
        cboNumCta.SelectedIndex = -1;
        txtOperacion.Text = "";
        txtCheque.Text = "";
        txtNc.Text = "";
        if (bloqueaNc)
        {
            txtNc.Enabled = false;
        }
    }

    // Vuelve a efectivo: fija el método y aplica su perfil.
    public void reiniciarAEfectivo()
    {
        cmbMetodoPago.SelectedValue = 5;
        aplicarMetodo(5);
    }
}
