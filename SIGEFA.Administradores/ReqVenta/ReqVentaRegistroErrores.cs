using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SIGEFA.Administradores.ReqVenta;

// Bitácora local de las anulaciones de requerimientos de venta que fallan.
// Escribe en %LOCALAPPDATA%\SIGEFA\req_venta_errores.log una línea por incidencia, con las
// credenciales enmascaradas. No abre conexiones ni oculta nunca la causa original.
public static class ReqVentaRegistroErrores
{
    private const string PatronCredenciales = @"(?i)\b(pwd|password|uid|user\s*id)\s*=\s*[^;,\s]+";

    // Enmascara credenciales como Pwd=..., Password=..., Uid=... o User Id=... en un texto libre.
    public static string Enmascarar(string texto)
    {
        if (string.IsNullOrEmpty(texto))
        {
            return string.Empty;
        }

        return Regex.Replace(texto, PatronCredenciales, "$1=***");
    }

    // Agrega una línea a la bitácora. Si el disco falla, el error de registro se descarta
    // para no tapar el resultado de la anulación.
    public static void Registrar(string linea)
    {
        try
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (string.IsNullOrEmpty(localAppData))
            {
                return;
            }

            string directorio = Path.Combine(localAppData, "SIGEFA");
            Directory.CreateDirectory(directorio);

            string limpia = Enmascarar((linea ?? string.Empty).Replace("\r", " ").Replace("\n", " ").Trim());
            string registro = string.Format("{0:yyyy-MM-dd HH:mm:ss} | {1}", DateTime.Now, limpia);
            File.AppendAllText(Path.Combine(directorio, "req_venta_errores.log"), registro + Environment.NewLine, Encoding.UTF8);
        }
        catch
        {
            // El diagnóstico no debe ocultar el resultado de la anulación.
        }
    }
}
