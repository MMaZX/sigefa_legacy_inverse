using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SIGEFA.Administradores.Actualizador;

// Manifiesto de version que se publica en el servidor (pesa menos de 1 KB).
// Formato: texto plano, una linea "clave=valor". Las lineas vacias y las que empiezan con # se ignoran.
// Claves obligatorias: build, sha256, archivo. Opcionales: minbuild, commit, tamano, notas.
public sealed class ManifiestoVersion
{
    private static readonly Regex Sha256Valido = new Regex("^[0-9a-f]{64}$", RegexOptions.Compiled);

    private ManifiestoVersion()
    {
    }

    // Numero creciente: es lo que ordena las versiones. El hash solo prueba integridad.
    public int Build { get; private set; }

    // Build minimo que puede iniciar sesion. 0 = ninguno obligatorio.
    public int MinBuild { get; private set; }

    public string Sha256 { get; private set; }

    // Nombre del zip dentro de la carpeta del manifiesto. Nunca una ruta.
    public string Archivo { get; private set; }

    public string Commit { get; private set; }

    public long Tamano { get; private set; }

    public string Notas { get; private set; }

    public static ManifiestoVersion Leer(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ManifiestoInvalidoException("El manifiesto esta vacio.");
        }
        var valores = LeerPares(texto);
        var manifiesto = new ManifiestoVersion
        {
            Build = LeerEntero(valores, "build", obligatorio: true),
            MinBuild = LeerEntero(valores, "minbuild", obligatorio: false),
            Tamano = LeerEntero(valores, "tamano", obligatorio: false),
            Sha256 = LeerSha256(valores),
            Archivo = LeerArchivo(valores),
            Commit = Opcional(valores, "commit"),
            Notas = Opcional(valores, "notas")
        };
        if (manifiesto.Build <= 0)
        {
            throw new ManifiestoInvalidoException("build debe ser mayor que 0.");
        }
        if (manifiesto.MinBuild > manifiesto.Build)
        {
            throw new ManifiestoInvalidoException("minbuild no puede ser mayor que build.");
        }
        return manifiesto;
    }

    private static Dictionary<string, string> LeerPares(string texto)
    {
        var valores = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (string cruda in texto.Split('\n'))
        {
            string linea = cruda.Trim();
            int igual = linea.IndexOf('=');
            if (linea.Length == 0 || linea.StartsWith("#") || igual <= 0)
            {
                continue;
            }
            valores[linea.Substring(0, igual).Trim()] = linea.Substring(igual + 1).Trim();
        }
        return valores;
    }

    private static string Opcional(Dictionary<string, string> valores, string clave)
    {
        return valores.TryGetValue(clave, out string valor) ? valor : string.Empty;
    }

    private static int LeerEntero(Dictionary<string, string> valores, string clave, bool obligatorio)
    {
        if (!valores.TryGetValue(clave, out string texto))
        {
            if (obligatorio)
            {
                throw new ManifiestoInvalidoException("Falta la clave " + clave + ".");
            }
            return 0;
        }
        // Cultura invariante: el sistema fuerza es-PE y no debe cambiar como se lee un numero.
        if (!int.TryParse(texto, NumberStyles.None, CultureInfo.InvariantCulture, out int numero))
        {
            throw new ManifiestoInvalidoException("La clave " + clave + " no es un numero valido.");
        }
        return numero;
    }

    private static string LeerSha256(Dictionary<string, string> valores)
    {
        if (!valores.TryGetValue("sha256", out string sha))
        {
            throw new ManifiestoInvalidoException("Falta la clave sha256.");
        }
        string minuscula = sha.ToLowerInvariant();
        if (!Sha256Valido.IsMatch(minuscula))
        {
            throw new ManifiestoInvalidoException("sha256 debe tener 64 caracteres hexadecimales.");
        }
        return minuscula;
    }

    // El archivo se descarga de la carpeta del manifiesto: aceptar una ruta permitiria salir de ella.
    private static string LeerArchivo(Dictionary<string, string> valores)
    {
        if (!valores.TryGetValue("archivo", out string archivo))
        {
            throw new ManifiestoInvalidoException("Falta la clave archivo.");
        }
        bool esNombreSimple = archivo.Length > 0
            && archivo.IndexOfAny(new[] { '/', '\\', ':' }) < 0
            && !archivo.Contains("..")
            && archivo.EndsWith(".zip", StringComparison.OrdinalIgnoreCase);
        if (!esNombreSimple)
        {
            throw new ManifiestoInvalidoException("archivo debe ser el nombre de un .zip, sin ruta.");
        }
        return archivo;
    }
}
