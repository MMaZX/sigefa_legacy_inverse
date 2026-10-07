using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIGEFA.Administradores.ReqVenta;

namespace SIGEFA.Formularios;

public partial class frmProgresoOperacion : Form
{
    private readonly string textoSinPasos;
    private readonly IList<PasoOperacion> pasosIniciales;
    private readonly Func<IProgress<PasoOperacion>, ResultadoOperacion> trabajo;
    private readonly Dictionary<string, ListViewItem> mapaPasos;
    private readonly bool esModoSinPasos;

    private bool terminado;

    public ResultadoOperacion Resultado { get; private set; }

    public bool fueExitoso
    {
        get { return Resultado != null && Resultado.Ok; }
    }

    public string mensajeError
    {
        get { return Resultado != null && !Resultado.Ok ? Resultado.Mensaje : string.Empty; }
    }


    public frmProgresoOperacion(
        string titulo,
        string textoSinPasos,
        IList<PasoOperacion> pasosIniciales,
        Func<IProgress<PasoOperacion>, ResultadoOperacion> trabajo)
    {
        if (trabajo == null)
        {
            throw new ArgumentNullException(nameof(trabajo));
        }

        this.textoSinPasos = textoSinPasos;
        this.pasosIniciales = pasosIniciales;
        this.trabajo = trabajo;
        this.mapaPasos = new Dictionary<string, ListViewItem>(StringComparer.OrdinalIgnoreCase);
        this.esModoSinPasos = pasosIniciales == null || pasosIniciales.Count == 0;
        this.terminado = false;

        InitializeComponent();

        this.Text = !string.IsNullOrEmpty(titulo) ? titulo : "Operación en curso";
        configurarSegunModo();
    }

    private void configurarSegunModo()
    {
        if (esModoSinPasos)
        {
            lvwPasos.Visible = false;
            lblTexto.Text = !string.IsNullOrWhiteSpace(textoSinPasos)
                ? textoSinPasos
                : "Procesando operación, por favor espere...";
            lblTexto.Visible = true;
            pbMarquee.Visible = true;
            pbMarquee.Style = ProgressBarStyle.Marquee;
            pbMarquee.MarqueeAnimationSpeed = 30;

            ClientSize = new Size(520, 110);
        }
        else
        {
            lblTexto.Visible = false;
            pbMarquee.Visible = false;
            lvwPasos.Visible = true;

            inicializarListaPasos();
            ClientSize = new Size(560, 320);
        }

        txtDetalle.Visible = false;
        btnCopiar.Visible = false;
        btnCopiar.Enabled = false;
        btnCerrar.Visible = false;
        btnCerrar.Enabled = false;
    }

    private void inicializarListaPasos()
    {
        lvwPasos.Items.Clear();
        mapaPasos.Clear();

        if (pasosIniciales == null)
        {
            return;
        }

        foreach (PasoOperacion paso in pasosIniciales)
        {
            if (paso == null)
            {
                continue;
            }

            ListViewItem item = new ListViewItem(paso.Texto ?? paso.Clave ?? string.Empty);
            item.SubItems.Add(obtenerTextoEstado(paso.Estado));
            item.SubItems.Add(paso.Detalle ?? string.Empty);
            aplicarColorEstado(item, paso.Estado);

            lvwPasos.Items.Add(item);

            if (!string.IsNullOrEmpty(paso.Clave) && !mapaPasos.ContainsKey(paso.Clave))
            {
                mapaPasos.Add(paso.Clave, item);
            }
        }
    }

    private async void alMostrar(object sender, EventArgs e)
    {
        Progress<PasoOperacion> progreso = new Progress<PasoOperacion>(alRecibirProgreso);
        try
        {
            ResultadoOperacion res = await Task.Run(() => trabajo(progreso));
            Resultado = res ?? new ResultadoOperacion(false, "La operación no devolvió ningún resultado.");
        }
        catch (Exception ex)
        {
            string cadenaCausas = obtenerCadenaCausas(ex);
            Resultado = new ResultadoOperacion(false, cadenaCausas);
        }

        if (Resultado.Ok)
        {
            await alTerminarConExito();
        }
        else
        {
            alTerminarConError(Resultado.Mensaje);
        }
    }

    private void alRecibirProgreso(PasoOperacion avance)
    {
        if (avance == null)
        {
            return;
        }

        if (esModoSinPasos)
        {
            if (!string.IsNullOrEmpty(avance.Texto))
            {
                lblTexto.Text = avance.Texto;
            }
            return;
        }

        if (!string.IsNullOrEmpty(avance.Clave) && mapaPasos.TryGetValue(avance.Clave, out ListViewItem item))
        {
            if (!string.IsNullOrEmpty(avance.Texto))
            {
                item.Text = avance.Texto;
            }
            item.SubItems[1].Text = obtenerTextoEstado(avance.Estado);
            item.SubItems[2].Text = avance.Detalle ?? string.Empty;
            aplicarColorEstado(item, avance.Estado);
            item.EnsureVisible();
        }
        else
        {
            ListViewItem nuevoItem = new ListViewItem(avance.Texto ?? avance.Clave ?? string.Empty);
            nuevoItem.SubItems.Add(obtenerTextoEstado(avance.Estado));
            nuevoItem.SubItems.Add(avance.Detalle ?? string.Empty);
            aplicarColorEstado(nuevoItem, avance.Estado);
            lvwPasos.Items.Add(nuevoItem);
            if (!string.IsNullOrEmpty(avance.Clave))
            {
                mapaPasos[avance.Clave] = nuevoItem;
            }
            nuevoItem.EnsureVisible();
        }
    }

    private async Task alTerminarConExito()
    {
        terminado = true;
        DialogResult = DialogResult.OK;

        if (esModoSinPasos)
        {
            Close();
            return;
        }

        bool hayDetallesInformativos = false;
        foreach (ListViewItem item in lvwPasos.Items)
        {
            if (item.SubItems.Count > 2 && !string.IsNullOrWhiteSpace(item.SubItems[2].Text))
            {
                hayDetallesInformativos = true;
                break;
            }
        }

        if (hayDetallesInformativos)
        {
            btnCerrar.Visible = true;
            btnCerrar.Enabled = true;
            CancelButton = btnCerrar;
            AcceptButton = btnCerrar;
            btnCerrar.Focus();
        }
        else
        {
            await Task.Delay(500);
            if (!IsDisposed && IsHandleCreated)
            {
                Close();
            }
        }
    }

    private void alTerminarConError(string mensajeError)
    {
        terminado = true;
        DialogResult = DialogResult.Abort;

        if (!esModoSinPasos)
        {
            foreach (ListViewItem item in lvwPasos.Items)
            {
                if (item.SubItems.Count > 1 && item.SubItems[1].Text == "En curso")
                {
                    item.SubItems[1].Text = "Error";
                    aplicarColorEstado(item, EstadoPaso.Error);
                }
            }
        }

        mostrarDetalleError(mensajeError);
    }

    private void mostrarDetalleError(string detalle)
    {
        txtDetalle.Text = detalle ?? "Ocurrió un error inesperado durante la operación.";
        txtDetalle.Visible = true;

        btnCopiar.Visible = true;
        btnCopiar.Enabled = true;

        btnCerrar.Visible = true;
        btnCerrar.Enabled = true;
        CancelButton = btnCerrar;
        AcceptButton = btnCerrar;

        ajustarDisenioParaError();
        btnCerrar.Focus();
    }

    private void ajustarDisenioParaError()
    {
        if (esModoSinPasos)
        {
            pbMarquee.Visible = false;
            ClientSize = new Size(540, 290);
            txtDetalle.Location = new Point(16, 48);
            txtDetalle.Size = new Size(508, 180);
            btnCopiar.Location = new Point(16, 244);
            btnCerrar.Location = new Point(404, 244);
        }
        else
        {
            ClientSize = new Size(560, 470);
            txtDetalle.Location = new Point(16, 276);
            txtDetalle.Size = new Size(528, 140);
            btnCopiar.Location = new Point(16, 426);
            btnCerrar.Location = new Point(424, 426);
        }
    }

    private void alCopiarDetalle(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txtDetalle.Text))
        {
            return;
        }

        try
        {
            Clipboard.SetText(txtDetalle.Text);
        }
        catch
        {
            // Portapapeles no accesible o bloqueado
        }
    }

    private void alCerrar(object sender, EventArgs e)
    {
        Close();
    }

    private void alIntentarCerrar(object sender, FormClosingEventArgs e)
    {
        if (!terminado)
        {
            e.Cancel = true;
        }
    }

    private static string obtenerTextoEstado(EstadoPaso estado)
    {
        switch (estado)
        {
            case EstadoPaso.Pendiente:
                return "Pendiente";
            case EstadoPaso.EnCurso:
                return "En curso";
            case EstadoPaso.Listo:
                return "Listo";
            case EstadoPaso.Error:
                return "Error";
            default:
                return estado.ToString();
        }
    }

    private static void aplicarColorEstado(ListViewItem item, EstadoPaso estado)
    {
        item.UseItemStyleForSubItems = false;
        Color color;
        switch (estado)
        {
            case EstadoPaso.EnCurso:
                color = Color.Blue;
                break;
            case EstadoPaso.Listo:
                color = Color.DarkGreen;
                break;
            case EstadoPaso.Error:
                color = Color.Red;
                break;
            case EstadoPaso.Pendiente:
            default:
                color = Color.Gray;
                break;
        }

        item.SubItems[1].ForeColor = color;
        if (estado == EstadoPaso.Error)
        {
            item.SubItems[2].ForeColor = Color.Red;
        }
        else
        {
            item.SubItems[2].ForeColor = SystemColors.WindowText;
        }
    }

    private static string obtenerCadenaCausas(Exception ex)
    {
        if (ex == null)
        {
            return "Error desconocido.";
        }

        if (ex is AggregateException agg)
        {
            ex = agg.Flatten();
        }

        StringBuilder sb = new StringBuilder();
        Exception actual = ex;
        int nivel = 0;
        while (actual != null)
        {
            if (nivel > 0)
            {
                sb.Append("--> Causa: ");
            }
            sb.AppendLine(actual.Message);
            actual = actual.InnerException;
            nivel++;
        }

        return sb.ToString().TrimEnd();
    }
}
