using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace SIGEFA.Administradores.Actualizador;

// Instala cada version de SIGEFA en su propia carpeta, junto a las anteriores:
//   <raiz>\versions\<build>\SIGEFA.exe ...
//   <raiz>\current.txt            (numero de la version activa)
// La version activa solo cambia cuando la nueva ya esta verificada y extraida completa.
// Si algo falla, queda activa la anterior y no se deja nada a medias.
public sealed class InstaladorVersiones
{
    public const string NombreExe = "SIGEFA.exe";

    // Se conserva la activa y la anterior inmediata (para volver atras).
    private const int VersionesAConservar = 2;

    private readonly string raiz;

    public InstaladorVersiones(string raiz)
    {
        if (string.IsNullOrWhiteSpace(raiz))
        {
            throw new ArgumentException("La carpeta de instalacion es obligatoria.", nameof(raiz));
        }
        this.raiz = raiz;
    }

    private string CarpetaVersiones => Path.Combine(raiz, "versions");

    private string ArchivoActual => Path.Combine(raiz, "current.txt");

    public string CarpetaVersion(int build)
    {
        return Path.Combine(CarpetaVersiones, build.ToString(CultureInfo.InvariantCulture));
    }

    // Build activo, o 0 si no hay ninguno instalado o la carpeta apuntada ya no tiene el ejecutable.
    public int BuildActual()
    {
        if (!File.Exists(ArchivoActual))
        {
            return 0;
        }
        string texto = File.ReadAllText(ArchivoActual).Trim();
        if (!int.TryParse(texto, NumberStyles.None, CultureInfo.InvariantCulture, out int build) || build <= 0)
        {
            return 0;
        }
        return File.Exists(Path.Combine(CarpetaVersion(build), NombreExe)) ? build : 0;
    }

    // Ruta del ejecutable de la version activa, o null si no hay ninguna.
    public string ExeActual()
    {
        int build = BuildActual();
        return build == 0 ? null : Path.Combine(CarpetaVersion(build), NombreExe);
    }

    // Verifica el zip ya descargado contra el manifiesto, lo extrae y recien entonces lo activa.
    public ResultadoInstalacion Instalar(ManifiestoVersion manifiesto, string rutaZip)
    {
        if (manifiesto == null)
        {
            throw new ArgumentNullException(nameof(manifiesto));
        }
        if (!File.Exists(rutaZip))
        {
            throw new InstalacionException("No existe el archivo descargado.");
        }
        if (!HashArchivo.Coincide(rutaZip, manifiesto.Sha256))
        {
            throw new InstalacionException("El archivo descargado no coincide con el sha256 del manifiesto.");
        }
        string destino = CarpetaVersion(manifiesto.Build);
        string temporal = destino + ".tmp";
        Directory.CreateDirectory(CarpetaVersiones);
        EliminarSiExiste(temporal);
        try
        {
            ExtraerSeguro(rutaZip, temporal);
            if (!File.Exists(Path.Combine(temporal, NombreExe)))
            {
                throw new InstalacionException("El paquete no contiene " + NombreExe + ".");
            }
            EliminarSiExiste(destino);
            Directory.Move(temporal, destino);
        }
        catch (InvalidDataException)
        {
            EliminarSiExiste(temporal);
            throw new InstalacionException("El archivo descargado no es un zip valido.");
        }
        catch
        {
            EliminarSiExiste(temporal);
            throw;
        }
        Activar(manifiesto.Build);
        return new ResultadoInstalacion(manifiesto.Build, destino, LimpiarAntiguas(manifiesto.Build));
    }

    // Extrae entrada por entrada comprobando que ninguna salga de la carpeta de destino ("zip slip").
    private static void ExtraerSeguro(string rutaZip, string carpeta)
    {
        Directory.CreateDirectory(carpeta);
        string baseCompleta = Path.GetFullPath(carpeta).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
        using (var zip = ZipFile.OpenRead(rutaZip))
        {
            foreach (var entrada in zip.Entries)
            {
                string destino = Path.GetFullPath(Path.Combine(carpeta, entrada.FullName));
                if (!destino.StartsWith(baseCompleta, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InstalacionException("El paquete contiene una ruta fuera de la carpeta de instalacion: " + entrada.FullName);
                }
                if (entrada.Name.Length == 0)
                {
                    Directory.CreateDirectory(destino);
                    continue;
                }
                Directory.CreateDirectory(Path.GetDirectoryName(destino));
                entrada.ExtractToFile(destino, true);
            }
        }
    }

    // Escribe el puntero en un archivo aparte y lo intercambia: nunca queda a medio escribir.
    private void Activar(int build)
    {
        string temporal = ArchivoActual + ".tmp";
        File.WriteAllText(temporal, build.ToString(CultureInfo.InvariantCulture));
        if (File.Exists(ArchivoActual))
        {
            File.Replace(temporal, ArchivoActual, null);
            return;
        }
        File.Move(temporal, ArchivoActual);
    }

    private List<string> LimpiarAntiguas(int buildActivo)
    {
        var noEliminadas = new List<string>();
        var conservar = ListarBuilds().Where(b => b != buildActivo).OrderByDescending(b => b).Take(VersionesAConservar - 1).ToList();
        foreach (int build in ListarBuilds().Where(b => b != buildActivo && !conservar.Contains(b)))
        {
            string carpeta = CarpetaVersion(build);
            try
            {
                Directory.Delete(carpeta, true);
            }
            catch (IOException)
            {
                noEliminadas.Add(carpeta);
            }
            catch (UnauthorizedAccessException)
            {
                noEliminadas.Add(carpeta);
            }
        }
        return noEliminadas;
    }

    private List<int> ListarBuilds()
    {
        var builds = new List<int>();
        foreach (string carpeta in Directory.GetDirectories(CarpetaVersiones))
        {
            if (int.TryParse(Path.GetFileName(carpeta), NumberStyles.None, CultureInfo.InvariantCulture, out int build))
            {
                builds.Add(build);
            }
        }
        return builds;
    }

    private static void EliminarSiExiste(string carpeta)
    {
        if (Directory.Exists(carpeta))
        {
            Directory.Delete(carpeta, true);
        }
    }
}
