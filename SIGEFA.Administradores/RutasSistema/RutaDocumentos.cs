using System;
using System.Configuration;
using System.IO;

namespace SIGEFA.Administradores.RutasSistema;

// Carpeta donde se guardan los comprobantes electronicos (XML, CDR, PDF).
// Los comprobantes se guardan en cada PC, nunca en el servidor. Orden de prioridad: la clave RutaDocumentos de
// app.config, luego la variable de entorno SIGEFA_RUTA_DOCUMENTOS (la fija el launcher a una carpeta local fuera
// de la carpeta de la version, que se borra al actualizar) y por ultimo "documentos" junto al ejecutable.
public static class RutaDocumentos
{
    public const string Clave = "RutaDocumentos";

    public const string VariableEntorno = "SIGEFA_RUTA_DOCUMENTOS";

    // Carpeta raiz de los comprobantes segun app.config.
    public static string Raiz
    {
        get
        {
            return Resolver(
                ConfigurationManager.AppSettings[Clave],
                Environment.GetEnvironmentVariable(VariableEntorno),
                AppDomain.CurrentDomain.BaseDirectory);
        }
    }

    public static string Subcarpeta(string nombre)
    {
        return Path.Combine(Raiz, nombre);
    }

    // Sin valor: "documentos" junto al ejecutable. Ruta absoluta o de red: se usa tal cual.
    // Ruta relativa: se toma desde el directorio base.
    public static string Resolver(string configurada, string directorioBase)
    {
        return Resolver(configurada, null, directorioBase);
    }

    // La clave de app.config gana sobre la variable de entorno; sin ninguna de las dos, "documentos" junto al ejecutable.
    public static string Resolver(string configurada, string entorno, string directorioBase)
    {
        string destino = Primera(configurada, entorno) ?? "documentos";
        return Path.Combine(directorioBase, destino);
    }

    private static string Primera(string primera, string segunda)
    {
        if (!string.IsNullOrWhiteSpace(primera))
        {
            return primera.Trim();
        }
        return string.IsNullOrWhiteSpace(segunda) ? null : segunda.Trim();
    }
}
