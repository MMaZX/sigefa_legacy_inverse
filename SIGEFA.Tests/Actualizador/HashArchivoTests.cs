using System;
using System.IO;
using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas del SHA256 de archivos descargados.
public class HashArchivoTests : IDisposable
{
    // SHA256 de los 3 bytes "abc" (vector de prueba estandar).
    private const string ShaAbc = "ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad";

    private readonly string carpeta = Path.Combine(Path.GetTempPath(), "sigefa_hash_" + Guid.NewGuid().ToString("N"));

    public HashArchivoTests()
    {
        Directory.CreateDirectory(carpeta);
    }

    public void Dispose()
    {
        Directory.Delete(carpeta, true);
    }

    private string Archivo(string contenido)
    {
        string ruta = Path.Combine(carpeta, Guid.NewGuid().ToString("N") + ".bin");
        File.WriteAllBytes(ruta, System.Text.Encoding.ASCII.GetBytes(contenido));
        return ruta;
    }

    [Fact]
    public void Calcular_DevuelveHexadecimalEnMinusculas()
    {
        Assert.Equal(ShaAbc, HashArchivo.Calcular(Archivo("abc")));
    }

    [Fact]
    public void Coincide_ConElHashCorrecto_EsVerdadero()
    {
        Assert.True(HashArchivo.Coincide(Archivo("abc"), ShaAbc));
    }

    [Fact]
    public void Coincide_IgnoraMayusculasYEspacios()
    {
        Assert.True(HashArchivo.Coincide(Archivo("abc"), "  " + ShaAbc.ToUpperInvariant() + " "));
    }

    // Descarga corrupta o truncada: el contenido no coincide con el manifiesto.
    [Fact]
    public void Coincide_ConContenidoDistinto_EsFalso()
    {
        Assert.False(HashArchivo.Coincide(Archivo("abd"), ShaAbc));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Coincide_SinHashEsperado_EsFalso(string esperado)
    {
        Assert.False(HashArchivo.Coincide(Archivo("abc"), esperado));
    }
}
