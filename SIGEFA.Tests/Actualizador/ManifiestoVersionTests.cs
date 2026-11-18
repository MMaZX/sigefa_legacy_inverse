using System;
using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas del manifiesto de version (texto clave=valor) que publica el servidor.
public class ManifiestoVersionTests
{
    private const string Sha = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";

    private static string Texto(string extra = "")
    {
        return "build=42\nsha256=" + Sha + "\narchivo=SIGEFA-42.zip\n" + extra;
    }

    [Fact]
    public void Leer_ConTodasLasClaves_CargaCadaCampo()
    {
        var m = ManifiestoVersion.Leer(Texto("minbuild=40\ncommit=b4c46c5\ntamano=1234\nnotas=Cambios de venta"));

        Assert.Equal(42, m.Build);
        Assert.Equal(40, m.MinBuild);
        Assert.Equal(Sha, m.Sha256);
        Assert.Equal("SIGEFA-42.zip", m.Archivo);
        Assert.Equal("b4c46c5", m.Commit);
        Assert.Equal(1234, m.Tamano);
        Assert.Equal("Cambios de venta", m.Notas);
    }

    [Fact]
    public void Leer_SinOpcionales_UsaValoresPorDefecto()
    {
        var m = ManifiestoVersion.Leer(Texto());

        Assert.Equal(0, m.MinBuild);
        Assert.Equal(string.Empty, m.Commit);
        Assert.Equal(0, m.Tamano);
    }

    // El archivo puede venir con saltos CRLF de Windows, comentarios, lineas vacias y espacios.
    [Fact]
    public void Leer_ToleraCrlfComentariosYEspacios()
    {
        string texto = "# manifiesto\r\n\r\n build = 42 \r\nSHA256=" + Sha.ToUpperInvariant() + "\r\narchivo=SIGEFA-42.zip\r\n";

        var m = ManifiestoVersion.Leer(texto);

        Assert.Equal(42, m.Build);
        Assert.Equal(Sha, m.Sha256);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   \n  ")]
    public void Leer_Vacio_Falla(string texto)
    {
        Assert.Throws<ManifiestoInvalidoException>(() => ManifiestoVersion.Leer(texto));
    }

    [Theory]
    [InlineData("sha256=" + "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef" + "\narchivo=a.zip")]
    [InlineData("build=1\narchivo=a.zip")]
    [InlineData("build=1\nsha256=0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef")]
    public void Leer_FaltaUnaClaveObligatoria_Falla(string texto)
    {
        Assert.Throws<ManifiestoInvalidoException>(() => ManifiestoVersion.Leer(texto));
    }

    [Theory]
    [InlineData("build=abc")]
    [InlineData("build=0")]
    [InlineData("build=-3")]
    [InlineData("build=1.5")]
    public void Leer_BuildInvalido_Falla(string build)
    {
        string texto = build + "\nsha256=" + Sha + "\narchivo=a.zip";

        Assert.Throws<ManifiestoInvalidoException>(() => ManifiestoVersion.Leer(texto));
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("zz23456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef")]
    public void Leer_Sha256Invalido_Falla(string sha)
    {
        string texto = "build=1\nsha256=" + sha + "\narchivo=a.zip";

        Assert.Throws<ManifiestoInvalidoException>(() => ManifiestoVersion.Leer(texto));
    }

    // El zip se baja de la carpeta del manifiesto: una ruta permitiria leer o escribir fuera de ella.
    [Theory]
    [InlineData("..\\otro.zip")]
    [InlineData("../otro.zip")]
    [InlineData("C:\\temp\\a.zip")]
    [InlineData("sub/a.zip")]
    [InlineData("a..b.zip")]
    [InlineData("programa.exe")]
    public void Leer_ArchivoConRutaOQueNoEsZip_Falla(string archivo)
    {
        string texto = "build=1\nsha256=" + Sha + "\narchivo=" + archivo;

        Assert.Throws<ManifiestoInvalidoException>(() => ManifiestoVersion.Leer(texto));
    }

    [Fact]
    public void Leer_MinBuildMayorQueBuild_Falla()
    {
        Assert.Throws<ManifiestoInvalidoException>(() => ManifiestoVersion.Leer(Texto("minbuild=43")));
    }
}
