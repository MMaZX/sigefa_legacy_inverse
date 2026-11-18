using System;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIGEFA.Launcher;

// Ventana minima de "Actualizando...": se muestra mientras corre el trabajo y se cierra sola al terminar.
internal sealed class FormProgreso : Form
{
    private FormProgreso(string texto)
    {
        Text = "SIGEFA";
        FormBorderStyle = FormBorderStyle.FixedDialog;
        StartPosition = FormStartPosition.CenterScreen;
        MaximizeBox = false;
        MinimizeBox = false;
        ControlBox = false;
        TopMost = true;
        ClientSize = new Size(380, 90);
        Controls.Add(new Label { Text = texto, AutoSize = false, Location = new Point(15, 12), Size = new Size(350, 30) });
        Controls.Add(new ProgressBar { Style = ProgressBarStyle.Marquee, Location = new Point(15, 50), Size = new Size(350, 20) });
    }

    // Corre "trabajo" en otro hilo mientras la ventana esta visible. Si falla, repite la excepcion aqui.
    public static void Ejecutar(string texto, Action trabajo)
    {
        Exception error = null;
        using (var form = new FormProgreso(texto))
        {
            // Se arranca al mostrarse: antes no existe el handle y no se podria cerrar desde otro hilo.
            form.Shown += (emisor, e) => Task.Run(() =>
            {
                try
                {
                    trabajo();
                }
                catch (Exception ex)
                {
                    error = ex;
                }
                form.BeginInvoke(new Action(form.Close));
            });
            form.ShowDialog();
        }
        if (error != null)
        {
            ExceptionDispatchInfo.Capture(error).Throw();
        }
    }
}
