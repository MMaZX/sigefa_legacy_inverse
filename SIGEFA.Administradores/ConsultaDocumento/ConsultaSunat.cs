using System;
using System.IO;
using System.Net;
using System.Text;

namespace SIGEFA.Administradores.ConsultaDocumento;

// Consulta un DNI (8 digitos) o RUC (11 digitos) en el servicio publico de SUNAT.
// Port de SunatDocumentoProvider de gruporicardoapi. No toca la base de datos.
public class ConsultaSunat
{
    public const string Url = "https://ww1.sunat.gob.pe/ol-ti-itfisdenreg/itfisdenreg.htm";
    private const int TimeoutMs = 10000;

    // Devuelve null si el documento no es consultable (no tiene 8 u 11 digitos) o si SUNAT no lo tiene.
    // Lanza ConsultaDocumentoException si SUNAT no responde o responde algo ilegible.
    public DocumentoConsultado Consultar(string documento)
    {
        string valor = (documento ?? string.Empty).Trim();
        TipoDocumentoConsulta tipo = TipoDocumento.Detectar(valor);
        if (tipo == TipoDocumentoConsulta.NoConsultable)
        {
            return null;
        }
        string query = tipo == TipoDocumentoConsulta.Dni
            ? "accion=obtenerDatosDni&numDocumento=" + valor
            : "accion=obtenerDatosRuc&nroRuc=" + valor;
        return SunatRespuesta.Interpretar(valor, tipo, Solicitar(Url + "?" + query));
    }

    private static string Solicitar(string url)
    {
        // .NET 4.6.1 no negocia TLS 1.2 por defecto y SUNAT lo exige.
        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "application/json";
            request.Timeout = TimeoutMs;
            request.ReadWriteTimeout = TimeoutMs;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
        catch (WebException ex)
        {
            throw new ConsultaDocumentoException("No se pudo conectar con SUNAT: " + ex.Message, ex);
        }
    }
}
