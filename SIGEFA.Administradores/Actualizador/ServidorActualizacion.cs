using System;
using System.IO;
using System.Threading.Tasks;

namespace SIGEFA.Administradores.Actualizador;

// Acceso a la carpeta del servidor: lee el manifiesto (con tiempo maximo) y copia el zip a esta PC.
// Lo usan el launcher y el propio SIGEFA (boton "Buscar Actualizaciones" y control del login).
internal sealed class ServidorActualizacion
{
    public const string NombreManifiesto = "latest.manifest";

    private readonly string ruta;
    private readonly TimeSpan espera;

    public ServidorActualizacion(string ruta, TimeSpan espera)
    {
        this.ruta = ruta ?? string.Empty;
        this.espera = espera;
    }

    // Devuelve null si no se pudo leer; "motivo" dice por que. Un share caido puede tardar mucho en fallar,
    // por eso la lectura corre en otro hilo y se abandona al vencer la espera.
    public ManifiestoVersion LeerManifiesto(out string motivo)
    {
        motivo = null;
        if (ruta.Length == 0)
        {
            motivo = "No esta configurada la ruta del servidor (RutaActualizacion).";
            return null;
        }
        string archivo = Path.Combine(ruta, NombreManifiesto);
        var lectura = Task.Run(() => File.ReadAllText(archivo));
        try
        {
            if (!lectura.Wait(espera))
            {
                motivo = "El servidor no respondio en " + (int)espera.TotalSeconds + " segundos.";
                return null;
            }
            return ManifiestoVersion.Leer(lectura.Result);
        }
        catch (AggregateException ex)
        {
            motivo = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            return null;
        }
        catch (ManifiestoInvalidoException ex)
        {
            motivo = ex.Message;
            return null;
        }
    }

    // Copia el zip del manifiesto a la carpeta de descargas y devuelve la ruta local.
    public string Descargar(ManifiestoVersion manifiesto, string carpetaDescargas)
    {
        Directory.CreateDirectory(carpetaDescargas);
        string origen = Path.Combine(ruta, manifiesto.Archivo);
        string destino = Path.Combine(carpetaDescargas, manifiesto.Archivo + ".parcial");
        File.Copy(origen, destino, true);
        return destino;
    }
}
