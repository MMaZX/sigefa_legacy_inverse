using SIGEFA.Administradores.ConsultaDocumento;
using Xunit;

namespace SIGEFA.Tests.ConsultaDocumento;

// Pruebas de la interpretacion de la respuesta de SUNAT, sin red.
public class ConsultaSunatTests
{
    [Theory]
    [InlineData("72701825", TipoDocumentoConsulta.Dni)]
    [InlineData("20131312955", TipoDocumentoConsulta.Ruc)]
    [InlineData(" 20131312955 ", TipoDocumentoConsulta.Ruc)]
    [InlineData("1234567", TipoDocumentoConsulta.NoConsultable)]
    [InlineData("123456789", TipoDocumentoConsulta.NoConsultable)]
    [InlineData("2013131295A", TipoDocumentoConsulta.NoConsultable)]
    [InlineData("", TipoDocumentoConsulta.NoConsultable)]
    [InlineData(null, TipoDocumentoConsulta.NoConsultable)]
    public void Detectar_SoloDniDe8ORucDe11Digitos(string documento, TipoDocumentoConsulta esperado)
    {
        Assert.Equal(esperado, TipoDocumento.Detectar(documento));
    }

    // Respuesta real de SUNAT para su propio RUC: denominacion rellenada con espacios.
    [Fact]
    public void Interpretar_Ruc_LimpiaDenominacionYDireccion()
    {
        string cuerpo = "{\"message\":\"success\",\"lista\":[{\"apenomdenunciado\":\"SUPERINTENDENCIA NACIONAL DE ADUANAS Y DE ADMINISTRACION TRIBUTARIA - SUNAT                         \",\"direstablecimiento\":\"AV. GARCILASO DE LA VEGA - Nro: 1472  - LIMA\"}]}";

        DocumentoConsultado resultado = SunatRespuesta.Interpretar("20131312955", TipoDocumentoConsulta.Ruc, cuerpo);

        Assert.Equal("20131312955", resultado.Documento);
        Assert.Equal("SUPERINTENDENCIA NACIONAL DE ADUANAS Y DE ADMINISTRACION TRIBUTARIA - SUNAT", resultado.Denominacion);
        Assert.Equal("AV. GARCILASO DE LA VEGA - NRO: 1472 - LIMA", resultado.Direccion);
    }

    // DNI: "APELLIDOS,NOMBRES" y sin direccion.
    [Fact]
    public void Interpretar_Dni_CambiaComaPorEspacioYDireccionGuion()
    {
        string cuerpo = "{\"message\":\"success\",\"lista\":[{\"nombresapellidos\":\"PEREZ GOMEZ,juan carlos\"}]}";

        DocumentoConsultado resultado = SunatRespuesta.Interpretar("12345678", TipoDocumentoConsulta.Dni, cuerpo);

        Assert.Equal("PEREZ GOMEZ JUAN CARLOS", resultado.Denominacion);
        Assert.Equal(DocumentoConsultado.DireccionVacia, resultado.Direccion);
    }

    [Theory]
    [InlineData("{\"error\":\"No existen datos para los filtros seleccionados\"}")]
    [InlineData("{\"message\":\"success\",\"lista\":[]}")]
    [InlineData("{\"message\":\"success\",\"lista\":[{\"apenomdenunciado\":\"   \"}]}")]
    public void Interpretar_SinDatos_DevuelveNulo(string cuerpo)
    {
        Assert.Null(SunatRespuesta.Interpretar("20000000001", TipoDocumentoConsulta.Ruc, cuerpo));
    }

    [Fact]
    public void Interpretar_RespuestaNoJson_LanzaConsultaDocumentoException()
    {
        Assert.Throws<ConsultaDocumentoException>(() => SunatRespuesta.Interpretar("20131312955", TipoDocumentoConsulta.Ruc, "<html>caido</html>"));
    }

    [Fact]
    public void Consultar_DocumentoNoConsultable_NoBuscaYDevuelveNulo()
    {
        Assert.Null(new ConsultaSunat().Consultar("12345"));
    }
}
