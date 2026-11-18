using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SIGEFA.Administradores.Actualizador;

// SHA256 de un archivo descargado. Prueba que llego completo, no quien lo publico.
public static class HashArchivo
{
    // Hexadecimal en minusculas, el mismo formato del manifiesto.
    public static string Calcular(string ruta)
    {
        using (var sha = SHA256.Create())
        using (var archivo = File.OpenRead(ruta))
        {
            var bytes = sha.ComputeHash(archivo);
            var texto = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                texto.Append(b.ToString("x2"));
            }
            return texto.ToString();
        }
    }

    public static bool Coincide(string ruta, string sha256Esperado)
    {
        if (string.IsNullOrEmpty(sha256Esperado))
        {
            return false;
        }
        return string.Equals(Calcular(ruta), sha256Esperado.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}
