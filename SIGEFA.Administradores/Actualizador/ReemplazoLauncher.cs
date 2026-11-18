using System;
using System.IO;
using System.Threading;

namespace SIGEFA.Administradores.Actualizador;

public enum ResultadoReemplazo
{
    // El launcher nuevo quedo en su sitio y arranca.
    Reemplazado,

    // El nuevo no paso la verificacion: se restauro el anterior.
    Revertido,

    // No se pudo tocar el launcher (por ejemplo, seguia abierto): queda el que ya estaba.
    NoSePudo
}

// Cambia el launcher instalado por uno nuevo, guardando el anterior como <destino>.anterior para poder volver atras.
public static class ReemplazoLauncher
{
    public const string SufijoAnterior = ".anterior";
    private const string SufijoPreparado = ".nuevo";

    // verificar: recibe la ruta del launcher ya colocado y dice si arranca bien.
    // intentos/pausa: mientras el launcher viejo siga abierto Windows no deja reemplazarlo; se reintenta.
    public static ResultadoReemplazo Reemplazar(string destino, string nuevo, Func<string, bool> verificar, int intentos, TimeSpan pausa)
    {
        string anterior = destino + SufijoAnterior;
        string preparado = destino + SufijoPreparado;
        try
        {
            // Se prepara junto al destino (mismo disco) para que el cambio final sea una sola operacion.
            File.Copy(nuevo, preparado, true);
            if (!Reintentar(() => Colocar(preparado, destino, anterior), intentos, pausa))
            {
                return ResultadoReemplazo.NoSePudo;
            }
            if (verificar(destino))
            {
                return ResultadoReemplazo.Reemplazado;
            }
            RestaurarAnterior(destino, anterior, intentos, pausa);
            return ResultadoReemplazo.Revertido;
        }
        catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
        {
            return ResultadoReemplazo.NoSePudo;
        }
        finally
        {
            BorrarSiExiste(preparado);
        }
    }

    private static void Colocar(string preparado, string destino, string anterior)
    {
        if (!File.Exists(destino))
        {
            File.Move(preparado, destino);
            return;
        }
        File.Replace(preparado, destino, anterior);
    }

    private static void RestaurarAnterior(string destino, string anterior, int intentos, TimeSpan pausa)
    {
        if (File.Exists(anterior))
        {
            Reintentar(() => File.Copy(anterior, destino, true), intentos, pausa);
        }
        else
        {
            // No habia launcher antes: se quita el nuevo, que no arranca.
            Reintentar(() => File.Delete(destino), intentos, pausa);
        }
    }

    private static bool Reintentar(Action accion, int intentos, TimeSpan pausa)
    {
        for (int i = 1; i <= Math.Max(1, intentos); i++)
        {
            try
            {
                accion();
                return true;
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException)
            {
                if (i < intentos)
                {
                    Thread.Sleep(pausa);
                }
            }
        }
        return false;
    }

    private static void BorrarSiExiste(string archivo)
    {
        try
        {
            if (File.Exists(archivo))
            {
                File.Delete(archivo);
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}
