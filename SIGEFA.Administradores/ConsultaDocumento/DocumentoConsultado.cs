using System.Globalization;
using System.Text.RegularExpressions;

namespace SIGEFA.Administradores.ConsultaDocumento;

// Resultado de consultar un DNI o RUC. Misma sanitizacion que DocumentoConsultadoDTO de la API
// (gruporicardoapi): caracteres de control fuera, espacios colapsados, trim y mayusculas;
// direccion vacia => "-".
public class DocumentoConsultado
{
    public const string DireccionVacia = "-";

    public string Documento { get; }
    public string Denominacion { get; }
    public string Direccion { get; }

    private DocumentoConsultado(string documento, string denominacion, string direccion)
    {
        Documento = documento;
        Denominacion = denominacion;
        Direccion = direccion;
    }

    public static DocumentoConsultado Crear(string documento, string denominacion, string direccion)
    {
        string direccionLimpia = Sanitizar(direccion);
        return new DocumentoConsultado(
            (documento ?? string.Empty).Trim(),
            Sanitizar(denominacion),
            direccionLimpia.Length == 0 ? DireccionVacia : direccionLimpia);
    }

    public static string Sanitizar(string texto)
    {
        if (texto == null)
        {
            return string.Empty;
        }
        string limpio = Regex.Replace(texto, @"[\p{Cc}\p{Cf}]+", " ");
        limpio = Regex.Replace(limpio, @"[\s ]+", " ");
        return limpio.Trim().ToUpper(CultureInfo.GetCultureInfo("es-PE"));
    }
}
