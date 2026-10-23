using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIGEFA.Formularios;

// Diálogo modal con barra de progreso indeterminada (marquee) para operaciones largas.
// Ejecuta el trabajo en un hilo aparte para que la barra siga animándose y el
// formulario de fondo no parezca congelado. El trabajo no debe tocar controles.
internal sealed class frmProgresoIndeterminado : Form
{
	private bool puedeCerrar;

	private frmProgresoIndeterminado(string mensaje)
	{
		Label lblMensaje = new Label
		{
			Text = mensaje,
			AutoSize = false,
			Dock = DockStyle.Top,
			Height = 34,
			TextAlign = ContentAlignment.MiddleLeft
		};
		ProgressBar barra = new ProgressBar
		{
			Style = ProgressBarStyle.Marquee,
			MarqueeAnimationSpeed = 30,
			Dock = DockStyle.Top,
			Height = 14
		};
		Padding = new Padding(16, 14, 16, 14);
		ClientSize = new Size(380, 90);
		Controls.Add(barra);
		Controls.Add(lblMensaje);
		FormBorderStyle = FormBorderStyle.FixedDialog;
		ControlBox = false;
		ShowInTaskbar = false;
		StartPosition = FormStartPosition.CenterParent;
		Text = "Por favor espere";
	}

	protected override void OnFormClosing(FormClosingEventArgs e)
	{
		e.Cancel = !puedeCerrar && e.CloseReason == CloseReason.UserClosing;
		base.OnFormClosing(e);
	}

	// Muestra el diálogo, corre el trabajo y lo cierra al terminar.
	// Si el trabajo lanza una excepción, se propaga al await del llamador.
	public static async Task<T> EjecutarAsync<T>(IWin32Window propietario, string mensaje, Func<T> trabajo)
	{
		using frmProgresoIndeterminado dialogo = new frmProgresoIndeterminado(mensaje);
		Task<T> tarea = Task.Run(trabajo);
		dialogo.Shown += async (object sender, EventArgs e) =>
		{
			await Task.WhenAny(tarea);
			dialogo.puedeCerrar = true;
			dialogo.Close();
		};
		dialogo.ShowDialog(propietario);
		return await tarea;
	}
}
