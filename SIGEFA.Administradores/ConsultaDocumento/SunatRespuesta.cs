using Newtonsoft.Json.Linq;

namespace SIGEFA.Administradores.ConsultaDocumento;

// Interpreta la respuesta del servicio publico de SUNAT (itfisdenreg). Particularidades:
//   - Responde HTTP 200 con text/plain y cuerpo JSON.
//   - Exito: {"message":"success","lista":[{...}]}
//   - No encontrado: {"error":"No existen datos para los filtros seleccionados"}
//   - DNI: solo trae "nombresapellidos" como "APELLIDOS,NOMBRES"; sin direccion.
//   - RUC: "apenomdenunciado" y "direstablecimiento" vienen rellenados con espacios.
public static class SunatRespuesta
{
    // Devuelve null si SUNAT no tiene el documento. Lanza ConsultaDocumentoException si la respuesta es ilegible.
    public static DocumentoConsultado Interpretar(string documento, TipoDocumentoConsulta tipo, string cuerpo)
    {
        JObject json;
        try
        {
            json = JObject.Parse(cuerpo ?? string.Empty);
        }
        catch (Newtonsoft.Json.JsonException ex)
        {
            throw new ConsultaDocumentoException("SUNAT devolvió una respuesta no válida.", ex);
        }

        if (json["error"] != null || !(json["lista"] is JArray lista) || lista.Count == 0 || !(lista[0] is JObject fila))
        {
            return null;
        }

        DocumentoConsultado resultado = tipo == TipoDocumentoConsulta.Dni
            ? DocumentoConsultado.Crear(documento, ((string)fila["nombresapellidos"] ?? string.Empty).Replace(',', ' '), null)
            : DocumentoConsultado.Crear(documento, (string)fila["apenomdenunciado"], (string)fila["direstablecimiento"]);

        // Sin denominacion se trata como no encontrado.
        return resultado.Denominacion.Length == 0 ? null : resultado;
    }
}
