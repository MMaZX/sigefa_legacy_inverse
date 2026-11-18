using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIGEFA.Administradores.Actualizador;

namespace SIGEFA.Formularios;

// Control de version al iniciar sesion: si el servidor exige un build mayor al de este SIGEFA, no deja entrar
// y ofrece actualizar. Si el servidor esta caido o tarda, NO bloquea: una caida no debe parar las ventas.
internal static class ControlVersionLogin
{
    private static readonly TimeSpan EsperaMaxima = TimeSpan.FromSeconds(3);

    // La respuesta se considera vigente este tiempo; pasado, se vuelve a preguntar al iniciar sesion.
    private static readonly TimeSpan Vigencia = TimeSpan.FromMinutes(3);

    private static Task<ResultadoConsultaActualizacion> consulta;
    private static DateTime momento;

    // Se llama al abrir el login: la consulta corre mientras la persona escribe su usuario.
    public static void Precargar()
    {
        consulta = ActualizacionCliente.Disponible ? ActualizacionCliente.ConsultarAsync() : null;
        momento = DateTime.UtcNow;
    }

    // true = puede iniciar sesion. false = debe actualizar (ya se le informo).
    public static async Task<bool> PermiteIniciarSesionAsync(Form login, Label mensaje)
    {
        if (consulta != null && DateTime.UtcNow - momento > Vigencia)
        {
            Precargar();
        }
        var pendiente = consulta;
        if (pendiente == null)
        {
            return true;
        }
        var primera = await Task.WhenAny(pendiente, Task.Delay(EsperaMaxima));
        if (primera != pendiente)
        {
            return true;
        }
        var resultado = await pendiente;
        if (!resultado.BloqueaInicioDeSesion)
        {
            return true;
        }
        mensaje.Text = "Debe actualizar SIGEFA para iniciar sesión.";
        ActualizacionCliente.OfrecerActualizar(login, MensajeObligatoria(resultado));
        return false;
    }

    private static string MensajeObligatoria(ResultadoConsultaActualizacion resultado)
    {
        return "Esta versión de SIGEFA (build " + resultado.BuildLocal + ") ya no puede iniciar sesión: "
            + "el servidor exige la build " + resultado.Manifiesto.MinBuild + " o superior.\n\n"
            + "SIGEFA se cerrará, se instalará la build " + resultado.Manifiesto.Build + " y se volverá a abrir.\n\n"
            + "¿Actualizar ahora?";
    }
}
