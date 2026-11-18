using System;
using System.IO;
using System.IO.Compression;
using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas del instalador por versiones: verifica, extrae, activa y conserva la anterior.
public class InstaladorVersionesTests : IDisposable
{
    private readonly string carpeta = Path.Combine(Path.GetTempPath(), "sigefa_inst_" + Guid.NewGuid().ToString("N"));
    private readonly string raiz;
    private readonly InstaladorVersiones instalador;

    public InstaladorVersionesTests()
    {
        raiz = Path.Combine(carpeta, "instalacion");
        Directory.CreateDirectory(carpeta);
        instalador = new InstaladorVersiones(raiz);
    }

    public void Dispose()
    {
        Directory.Delete(carpeta, true);
    }

    // Crea un zip con las entradas dadas y el manifiesto que lo describe.
    private (ManifiestoVersion Manifiesto, string Zip) Paquete(int build, params (string Nombre, string Contenido)[] entradas)
    {
        string zip = Path.Combine(carpeta, "SIGEFA-" + build + "-" + Guid.NewGuid().ToString("N") + ".zip");
        using (var archivo = ZipFile.Open(zip, ZipArchiveMode.Create))
        {
            foreach (var (nombre, contenido) in entradas)
            {
                var entrada = archivo.CreateEntry(nombre);
                using (var escritor = new StreamWriter(entrada.Open()))
                {
                    escritor.Write(contenido);
                }
            }
        }
        return (ManifiestoPara(build, zip), zip);
    }

    private static ManifiestoVersion ManifiestoPara(int build, string zip)
    {
        return ManifiestoVersion.Leer("build=" + build + "\nsha256=" + HashArchivo.Calcular(zip) + "\narchivo=SIGEFA-" + build + ".zip");
    }

    private (ManifiestoVersion Manifiesto, string Zip) PaqueteValido(int build)
    {
        return Paquete(build, ("SIGEFA.exe", "exe " + build), ("sub/datos.txt", "datos " + build));
    }

    [Fact]
    public void Instalar_ConPaqueteValido_ExtraeYActivaLaVersion()
    {
        var p = PaqueteValido(7);

        var resultado = instalador.Instalar(p.Manifiesto, p.Zip);

        Assert.Equal(7, resultado.Build);
        Assert.Equal(7, instalador.BuildActual());
        Assert.Equal(Path.Combine(raiz, "versions", "7", "SIGEFA.exe"), instalador.ExeActual());
        Assert.Equal("exe 7", File.ReadAllText(instalador.ExeActual()));
        Assert.Equal("datos 7", File.ReadAllText(Path.Combine(raiz, "versions", "7", "sub", "datos.txt")));
    }

    [Fact]
    public void BuildActual_SinNadaInstalado_EsCeroYNoHayExe()
    {
        Assert.Equal(0, instalador.BuildActual());
        Assert.Null(instalador.ExeActual());
    }

    // Descarga corrupta o manipulada: no coincide con el sha256 del manifiesto.
    [Fact]
    public void Instalar_ConHashQueNoCoincide_FallaYNoInstalaNada()
    {
        var p = PaqueteValido(7);
        var manifiestoAjeno = ManifiestoVersion.Leer("build=7\nsha256=" + new string('a', 64) + "\narchivo=a.zip");

        Assert.Throws<InstalacionException>(() => instalador.Instalar(manifiestoAjeno, p.Zip));

        Assert.Equal(0, instalador.BuildActual());
        Assert.False(Directory.Exists(Path.Combine(raiz, "versions", "7")));
        Assert.False(Directory.Exists(Path.Combine(raiz, "versions", "7.tmp")));
    }

    [Fact]
    public void Instalar_ZipSinElEjecutable_FallaYNoInstalaNada()
    {
        var p = Paquete(7, ("leeme.txt", "hola"));

        Assert.Throws<InstalacionException>(() => instalador.Instalar(p.Manifiesto, p.Zip));

        Assert.Equal(0, instalador.BuildActual());
        Assert.False(Directory.Exists(Path.Combine(raiz, "versions", "7")));
        Assert.False(Directory.Exists(Path.Combine(raiz, "versions", "7.tmp")));
    }

    // Una entrada con ".." escribiria fuera de la carpeta de la version.
    [Fact]
    public void Instalar_ConEntradaQueSaleDeLaCarpeta_FallaYNoEscribeAfuera()
    {
        var p = Paquete(7, ("SIGEFA.exe", "exe"), ("../../fuera.txt", "malo"));

        Assert.Throws<InstalacionException>(() => instalador.Instalar(p.Manifiesto, p.Zip));

        Assert.False(File.Exists(Path.Combine(raiz, "fuera.txt")));
        Assert.False(File.Exists(Path.Combine(carpeta, "fuera.txt")));
        Assert.Equal(0, instalador.BuildActual());
    }

    [Fact]
    public void Instalar_ArchivoQueNoEsZip_FallaConInstalacionException()
    {
        string falso = Path.Combine(carpeta, "falso.zip");
        File.WriteAllText(falso, "esto no es un zip");
        var manifiesto = ManifiestoPara(7, falso);

        Assert.Throws<InstalacionException>(() => instalador.Instalar(manifiesto, falso));

        Assert.Equal(0, instalador.BuildActual());
        Assert.False(Directory.Exists(Path.Combine(raiz, "versions", "7.tmp")));
    }

    [Fact]
    public void Instalar_ArchivoInexistente_Falla()
    {
        var manifiesto = ManifiestoVersion.Leer("build=7\nsha256=" + new string('a', 64) + "\narchivo=a.zip");

        Assert.Throws<InstalacionException>(() => instalador.Instalar(manifiesto, Path.Combine(carpeta, "no_existe.zip")));
    }

    // Si la actualizacion falla, la version que ya funcionaba sigue activa e intacta.
    [Fact]
    public void Instalar_QueFalla_ConservaLaVersionActivaAnterior()
    {
        var bueno = PaqueteValido(7);
        instalador.Instalar(bueno.Manifiesto, bueno.Zip);
        var malo = Paquete(8, ("leeme.txt", "sin exe"));

        Assert.Throws<InstalacionException>(() => instalador.Instalar(malo.Manifiesto, malo.Zip));

        Assert.Equal(7, instalador.BuildActual());
        Assert.Equal("exe 7", File.ReadAllText(instalador.ExeActual()));
    }

    [Fact]
    public void Instalar_NuevaVersion_LaActivaYDejaLaAnteriorParaVolverAtras()
    {
        var v7 = PaqueteValido(7);
        var v8 = PaqueteValido(8);

        instalador.Instalar(v7.Manifiesto, v7.Zip);
        instalador.Instalar(v8.Manifiesto, v8.Zip);

        Assert.Equal(8, instalador.BuildActual());
        Assert.True(File.Exists(Path.Combine(raiz, "versions", "7", "SIGEFA.exe")));
    }

    [Fact]
    public void Instalar_ConservaSoloLaActivaYLaAnterior()
    {
        foreach (int build in new[] { 7, 8, 9 })
        {
            var p = PaqueteValido(build);
            instalador.Instalar(p.Manifiesto, p.Zip);
        }

        Assert.False(Directory.Exists(Path.Combine(raiz, "versions", "7")));
        Assert.True(Directory.Exists(Path.Combine(raiz, "versions", "8")));
        Assert.True(Directory.Exists(Path.Combine(raiz, "versions", "9")));
    }

    // Los comprobantes electronicos nunca deben perderse al limpiar versiones viejas.
    [Fact]
    public void Instalar_NoBorraUnaVersionVieja_QueTieneComprobantesDentro()
    {
        var v7 = PaqueteValido(7);
        var v8 = PaqueteValido(8);
        var v9 = PaqueteValido(9);
        instalador.Instalar(v7.Manifiesto, v7.Zip);
        string comprobante = Path.Combine(raiz, "versions", "7", "documentos", "CDR", "R-001.zip");
        Directory.CreateDirectory(Path.GetDirectoryName(comprobante));
        File.WriteAllText(comprobante, "comprobante");
        instalador.Instalar(v8.Manifiesto, v8.Zip);

        var resultado = instalador.Instalar(v9.Manifiesto, v9.Zip);

        Assert.True(File.Exists(comprobante));
        Assert.Contains(Path.Combine(raiz, "versions", "7"), resultado.VersionesNoEliminadas);
    }

    // SIGEFA crea las carpetas de documentos vacias al arrancar: eso solo no impide limpiar la version.
    [Fact]
    public void Instalar_BorraUnaVersionVieja_CuyaCarpetaDocumentosEstaVacia()
    {
        var v7 = PaqueteValido(7);
        var v8 = PaqueteValido(8);
        var v9 = PaqueteValido(9);
        instalador.Instalar(v7.Manifiesto, v7.Zip);
        Directory.CreateDirectory(Path.Combine(raiz, "versions", "7", "documentos", "CDR"));
        instalador.Instalar(v8.Manifiesto, v8.Zip);

        var resultado = instalador.Instalar(v9.Manifiesto, v9.Zip);

        Assert.False(Directory.Exists(Path.Combine(raiz, "versions", "7")));
        Assert.Empty(resultado.VersionesNoEliminadas);
    }

    [Fact]
    public void Instalar_MismoBuildDeNuevo_Reemplaza()
    {
        var primero = Paquete(7, ("SIGEFA.exe", "primero"));
        var segundo = Paquete(7, ("SIGEFA.exe", "segundo"));
        instalador.Instalar(primero.Manifiesto, primero.Zip);

        instalador.Instalar(segundo.Manifiesto, segundo.Zip);

        Assert.Equal("segundo", File.ReadAllText(instalador.ExeActual()));
    }

    // El puntero apunta a una carpeta que alguien borro: no hay ejecutable que lanzar.
    [Fact]
    public void BuildActual_ConCarpetaBorrada_EsCero()
    {
        var p = PaqueteValido(7);
        instalador.Instalar(p.Manifiesto, p.Zip);
        Directory.Delete(Path.Combine(raiz, "versions", "7"), true);

        Assert.Equal(0, instalador.BuildActual());
        Assert.Null(instalador.ExeActual());
    }
}
