using System;
using System.IO;
using SIGEFA.Administradores.Actualizador;
using Xunit;

namespace SIGEFA.Tests.Actualizador;

// Pruebas de la consulta al servidor de versiones (una carpeta con latest.manifest).
public class ConsultaActualizacionTests : IDisposable
{
    private const string Sha = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";
    private static readonly TimeSpan Espera = TimeSpan.FromSeconds(5);

    private readonly string servidor = Path.Combine(Path.GetTempPath(), "sigefa_srv_" + Guid.NewGuid().ToString("N"));

    public ConsultaActualizacionTests()
    {
        Directory.CreateDirectory(servidor);
    }

    public void Dispose()
    {
        Directory.Delete(servidor, true);
    }

    private void Publicar(int build, int minBuild = 0)
    {
        string texto = "build=" + build + "\nminbuild=" + minBuild + "\nsha256=" + Sha + "\narchivo=SIGEFA-" + build + ".zip";
        File.WriteAllText(Path.Combine(servidor, "latest.manifest"), texto);
    }

    [Fact]
    public void Consultar_ConVersionMasNueva_EsDisponible()
    {
        Publicar(245);

        var r = ConsultaActualizacion.Consultar(servidor, 244, Espera);

        Assert.Equal(EstadoActualizacion.Disponible, r.Estado);
        Assert.Equal(245, r.Manifiesto.Build);
        Assert.True(r.ServidorRespondio);
        Assert.Null(r.Motivo);
    }

    [Fact]
    public void Consultar_BajoElMinimo_EsObligatoria()
    {
        Publicar(245, 245);

        Assert.Equal(EstadoActualizacion.Obligatoria, ConsultaActualizacion.Consultar(servidor, 244, Espera).Estado);
    }

    [Fact]
    public void Consultar_MismoBuild_EstaAlDia()
    {
        Publicar(245);

        Assert.Equal(EstadoActualizacion.AlDia, ConsultaActualizacion.Consultar(servidor, 245, Espera).Estado);
    }

    // Servidor sin ficha: no se avisa ni se bloquea a nadie, y se conserva el motivo para el registro.
    [Fact]
    public void Consultar_SinManifiesto_EstaAlDiaYDiceElMotivo()
    {
        var r = ConsultaActualizacion.Consultar(servidor, 244, Espera);

        Assert.Equal(EstadoActualizacion.AlDia, r.Estado);
        Assert.False(r.ServidorRespondio);
        Assert.False(string.IsNullOrWhiteSpace(r.Motivo));
    }

    [Fact]
    public void Consultar_ConFichaInvalida_EstaAlDiaYDiceElMotivo()
    {
        File.WriteAllText(Path.Combine(servidor, "latest.manifest"), "build=abc");

        var r = ConsultaActualizacion.Consultar(servidor, 244, Espera);

        Assert.Equal(EstadoActualizacion.AlDia, r.Estado);
        Assert.False(string.IsNullOrWhiteSpace(r.Motivo));
    }

    // Una compilacion sin numero de build (desarrollo) nunca se avisa ni se bloquea.
    [Fact]
    public void Consultar_SinNumeroDeBuildLocal_EstaAlDia()
    {
        Publicar(245, 245);

        Assert.Equal(EstadoActualizacion.AlDia, ConsultaActualizacion.Consultar(servidor, 0, Espera).Estado);
    }

    [Fact]
    public void SinConsulta_EsAlDiaConMotivo()
    {
        var r = ResultadoConsultaActualizacion.SinConsulta(244, "sin launcher");

        Assert.Equal(EstadoActualizacion.AlDia, r.Estado);
        Assert.Equal("sin launcher", r.Motivo);
        Assert.False(r.ServidorRespondio);
    }
}
