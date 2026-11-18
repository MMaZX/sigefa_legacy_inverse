using System;
using System.Configuration;
using System.IO;

namespace SIGEFA.Administradores.RutasSistema;

// Carpeta donde se guardan los comprobantes electronicos (XML, CDR, PDF).
// Por defecto es "documentos" junto al ejecutable, como siempre. Si el cliente se instala en cada PC,
// la clave RutaDocumentos de app.config apunta todos al mismo lugar (por ejemplo una carpeta del servidor)
// para que los comprobantes no queden repartidos por equipo.
public static class RutaDocumentos
{
    public const string Clave = "RutaDocumentos";

    // Carpeta raiz de los comprobantes segun app.config.
    public static string Raiz
    {
        get { return Resolver(ConfigurationManager.AppSettings[Clave], AppDomain.CurrentDomain.BaseDirectory); }
    }

    public static string Subcarpeta(string nombre)
    {
        return Path.Combine(Raiz, nombre);
    }

    // Sin valor: "documentos" junto al ejecutable. Ruta absoluta o de red: se usa tal cual.
    // Ruta relativa: se toma desde el directorio base.
    public static string Resolver(string configurada, string directorioBase)
    {
        string destino = string.IsNullOrWhiteSpace(configurada) ? "documentos" : configurada.Trim();
        return Path.Combine(directorioBase, destino);
    }
}
