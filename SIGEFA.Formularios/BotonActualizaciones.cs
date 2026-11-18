using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIGEFA.Administradores.Actualizador;

namespace SIGEFA.Formularios;

// Boton "Buscar Actualizaciones" arriba a la derecha, junto a los botones de la ventana.
// Cada 3 minutos busca solo. Si hay una version nueva cambia a un color de acento fijo (azul si es opcional, rojo si es
// obligatoria) con un punto de aviso; no parpadea. Si no hay nada, queda en gris suave.
// Al hacer clic busca en el acto o, si ya hay una pendiente, ofrece actualizar.
internal sealed class BotonActualizaciones : Button
{
    private const int IntervaloSondeoMs = 180000;
    private const int PrimeraBusquedaMs = 8000;
    private const int RadioEsquinas = 13;
    private const int MargenDerecho = 170;
    private const int MargenSuperior = 3;

    private readonly System.Windows.Forms.Timer sondeo = new System.Windows.Forms.Timer();
    private readonly ToolTip ayuda = new ToolTip();
    private ResultadoConsultaActualizacion ultimo;
    private bool consultando;
    private UrgenciaBotonActualizacion urgencia = UrgenciaBotonActualizacion.Ninguna;
    private bool encima;

    private BotonActualizaciones()
    {
        Text = PresentacionBotonActualizacion.TextoNormal;
        Font = new Font("Segoe UI", 9f, FontStyle.Bold);
        FlatStyle = FlatStyle.Flat;
        FlatAppearance.BorderSize = 0;
        SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        Cursor = Cursors.Hand;
        TabStop = false;
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        MinimumSize = new Size(160, 28);
        Padding = new Padding(14, 0, 14, 0);
        sondeo.Interval = PrimeraBusquedaMs;
        sondeo.Tick += async (emisor, e) =>
        {
            sondeo.Interval = IntervaloSondeoMs;
            await BuscarAsync(false);
        };
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

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
        encima = true;
        Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        encima = false;
        Invalidate();
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
        urgencia = presentacion.Urgencia;
        Reubicar();
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(Parent != null ? Parent.BackColor : SystemColors.Control);
        var area = new Rectangle(0, 0, Width - 1, Height - 1);
        using (var forma = FormaRedondeada(area, RadioEsquinas))
        using (var relleno = new SolidBrush(ColorFondo()))
        using (var borde = new Pen(ColorBorde()))
        {
            g.FillPath(relleno, forma);
            g.DrawPath(borde, forma);
        }
        var colorTexto = ColorTexto();
        int izquierda = Padding.Left;
        if (urgencia != UrgenciaBotonActualizacion.Ninguna)
        {
            // Punto de aviso fijo a la izquierda del texto.
            int d = 8;
            using (var punto = new SolidBrush(colorTexto))
            {
                g.FillEllipse(punto, Padding.Left - 4, (Height - d) / 2, d, d);
            }
            izquierda += d;
        }
        var zonaTexto = new Rectangle(izquierda, 0, Math.Max(0, Width - izquierda - Padding.Right), Height);
        TextRenderer.DrawText(g, Text, Font, zonaTexto, colorTexto,
            TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
    }

    private Color ColorFondo()
    {
        switch (urgencia)
        {
            case UrgenciaBotonActualizacion.Obligatoria:
                return encima ? Color.FromArgb(198, 40, 40) : Color.FromArgb(211, 47, 47);
            case UrgenciaBotonActualizacion.Aviso:
                return encima ? Color.FromArgb(21, 101, 192) : Color.FromArgb(30, 136, 229);
            default:
                return encima ? Color.FromArgb(222, 228, 240) : Color.FromArgb(236, 240, 248);
        }
    }

    private Color ColorBorde()
    {
        switch (urgencia)
        {
            case UrgenciaBotonActualizacion.Obligatoria:
                return Color.FromArgb(183, 28, 28);
            case UrgenciaBotonActualizacion.Aviso:
                return Color.FromArgb(13, 71, 161);
            default:
                return Color.FromArgb(190, 198, 214);
        }
    }

    private Color ColorTexto()
    {
        return urgencia == UrgenciaBotonActualizacion.Ninguna ? Color.FromArgb(55, 65, 85) : Color.White;
    }

    private static GraphicsPath FormaRedondeada(Rectangle area, int radio)
    {
        int d = Math.Min(radio * 2, Math.Min(area.Width, area.Height));
        var camino = new GraphicsPath();
        camino.AddArc(area.X, area.Y, d, d, 180, 90);
        camino.AddArc(area.Right - d, area.Y, d, d, 270, 90);
        camino.AddArc(area.Right - d, area.Bottom - d, d, d, 0, 90);
        camino.AddArc(area.X, area.Bottom - d, d, d, 90, 90);
        camino.CloseFigure();
        return camino;
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
