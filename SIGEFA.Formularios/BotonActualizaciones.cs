using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIGEFA.Administradores.Actualizador;

namespace SIGEFA.Formularios;

// Boton "Buscar Actualizaciones" arriba a la derecha, junto a los botones de la ventana.
// Cada 3 minutos busca solo. Si hay una version nueva se pone rojo/amarillo y parpadea hasta que se actualice;
// si no hay nada, no muestra nada. Al hacer clic busca en el acto o, si ya hay una pendiente, ofrece actualizar.
internal sealed class BotonActualizaciones : Button
{
    private const int IntervaloSondeoMs = 180000;
    private const int PrimeraBusquedaMs = 8000;
    private const int IntervaloParpadeoMs = 500;
    private const int MargenDerecho = 170;
    private const int MargenSuperior = 3;

    private readonly System.Windows.Forms.Timer sondeo = new System.Windows.Forms.Timer();
    private readonly System.Windows.Forms.Timer parpadeo = new System.Windows.Forms.Timer();
    private readonly ToolTip ayuda = new ToolTip();
    private ResultadoConsultaActualizacion ultimo;
    private bool consultando;
    private bool faseAlerta;

    private BotonActualizaciones()
    {
        Text = PresentacionBotonActualizacion.TextoNormal;
        Font = new Font("Segoe UI", 9f, FontStyle.Bold);
        FlatStyle = FlatStyle.Flat;
        Cursor = Cursors.Hand;
        TabStop = false;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        MinimumSize = new Size(160, 26);
        Padding = new Padding(8, 0, 8, 0);
        AspectoNormal();
        sondeo.Interval = PrimeraBusquedaMs;
        sondeo.Tick += async (emisor, e) =>
        {
            sondeo.Interval = IntervaloSondeoMs;
            await BuscarAsync(false);
        };
        parpadeo.Interval = IntervaloParpadeoMs;
        parpadeo.Tick += (emisor, e) => AlternarAlerta();
    }

    // Lo deja instalado sobre la ventana principal. Si SIGEFA no fue instalado por el launcher no hace nada.
    public static void Instalar(Form ventana)
    {
        if (!ActualizacionCliente.Disponible)
        {
            return;
        }
        var boton = new BotonActualizaciones();
        ventana.Controls.Add(boton);
        boton.Reubicar();
        boton.BringToFront();
        ventana.Resize += (emisor, e) => boton.Reubicar();
        ventana.Shown += (emisor, e) => boton.BringToFront();
        boton.sondeo.Start();
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        Reubicar();
    }

    protected override void OnClick(EventArgs e)
    {
        base.OnClick(e);
        if (ultimo != null && ultimo.Estado != EstadoActualizacion.AlDia)
        {
            ActualizacionCliente.OfrecerActualizar(FindForm(), MensajeActualizar(ultimo));
            return;
        }
        _ = BuscarAsync(true);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            sondeo.Dispose();
            parpadeo.Dispose();
            ayuda.Dispose();
        }
        base.Dispose(disposing);
    }

    // Arriba a la derecha, a la izquierda de los botones de minimizar/maximizar/cerrar.
    private void Reubicar()
    {
        if (Parent == null)
        {
            return;
        }
        Left = Math.Max(0, Parent.ClientSize.Width - Width - MargenDerecho);
        Top = MargenSuperior;
    }

    private async Task BuscarAsync(bool manual)
    {
        if (consultando)
        {
            return;
        }
        consultando = true;
        if (manual)
        {
            Text = "Buscando...";
        }
        ResultadoConsultaActualizacion resultado;
        try
        {
            resultado = await ActualizacionCliente.ConsultarAsync();
        }
        finally
        {
            consultando = false;
        }
        if (!IsDisposed)
        {
            Aplicar(resultado);
        }
    }

    private void Aplicar(ResultadoConsultaActualizacion resultado)
    {
        ultimo = resultado;
        int buildNueva = resultado.Manifiesto != null ? resultado.Manifiesto.Build : 0;
        var presentacion = PresentacionBotonActualizacion.Para(resultado.Estado, buildNueva);
        Text = presentacion.Texto;
        ayuda.SetToolTip(this, TextoAyuda(resultado));
        if (presentacion.Parpadea)
        {
            parpadeo.Start();
            AlternarAlerta();
        }
        else
        {
            parpadeo.Stop();
            AspectoNormal();
        }
        Reubicar();
    }

    private void AlternarAlerta()
    {
        faseAlerta = !faseAlerta;
        BackColor = faseAlerta ? Color.Red : Color.Gold;
        ForeColor = faseAlerta ? Color.White : Color.Black;
        FlatAppearance.BorderColor = Color.DarkRed;
    }

    private void AspectoNormal()
    {
        BackColor = Color.FromArgb(236, 240, 248);
        ForeColor = Color.FromArgb(40, 40, 40);
        FlatAppearance.BorderColor = Color.FromArgb(150, 160, 180);
    }

    private static string TextoAyuda(ResultadoConsultaActualizacion resultado)
    {
        string hora = DateTime.Now.ToString("HH:mm");
        if (resultado.Estado != EstadoActualizacion.AlDia)
        {
            return "Hay una versión nueva. Haga clic para actualizar. (Última búsqueda: " + hora + ")";
        }
        return resultado.ServidorRespondio
            ? "SIGEFA está actualizado. (Última búsqueda: " + hora + ")"
            : "No se pudo consultar el servidor de versiones. (Última búsqueda: " + hora + ")";
    }

    private static string MensajeActualizar(ResultadoConsultaActualizacion resultado)
    {
        return "Hay una nueva versión de SIGEFA (build " + resultado.Manifiesto.Build + ").\n\n"
            + "Para actualizar, SIGEFA se cerrará, se instalará la nueva versión y se volverá a abrir.\n"
            + "Guarde su trabajo antes de continuar.\n\n"
            + "¿Actualizar ahora?";
    }
}
