using System.IO;
using SIGEFA.Administradores.RutasSistema;
using Xunit;

namespace SIGEFA.Tests.RutasSistema;

// Pruebas de la carpeta de comprobantes electronicos.
public class RutaDocumentosTests
{
    private static readonly string Base = Path.Combine(Path.GetTempPath(), "sigefa_app");

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Resolver_SinConfigurar_UsaDocumentosJuntoAlEjecutable(string configurada)
    {
        Assert.Equal(Path.Combine(Base, "documentos"), RutaDocumentos.Resolver(configurada, Base));
    }

    [Fact]
    public void Resolver_RutaAbsoluta_SeUsaTalCual()
    {
        string absoluta = Path.Combine(Path.GetTempPath(), "comprobantes");

        Assert.Equal(absoluta, RutaDocumentos.Resolver(absoluta, Base));
    }

    // Carpeta compartida del servidor: es una ruta "rooted", no se le antepone la del ejecutable.
    [Fact]
    public void Resolver_RutaDeRed_SeUsaTalCual()
    {
        const string red = @"\\192.168.1.3\sgesystem\SISTEMA\documentos";

        Assert.Equal(red, RutaDocumentos.Resolver(red, Base));
    }

    [Fact]
    public void Resolver_RutaRelativa_ParteDelDirectorioBase()
    {
        Assert.Equal(Path.Combine(Base, "comprobantes"), RutaDocumentos.Resolver("comprobantes", Base));
    }

    [Fact]
    public void Resolver_ConEspaciosAlrededor_LosIgnora()
    {
        string absoluta = Path.Combine(Path.GetTempPath(), "comprobantes");

        Assert.Equal(absoluta, RutaDocumentos.Resolver("  " + absoluta + "  ", Base));
    }
}
