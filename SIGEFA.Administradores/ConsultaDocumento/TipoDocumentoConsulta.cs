using System.Text.RegularExpressions;

namespace SIGEFA.Administradores.ConsultaDocumento;

public enum TipoDocumentoConsulta
{
    NoConsultable,
    Dni,
    Ruc
}

public static class TipoDocumento
{
    // Solo se consulta un DNI (8 digitos) o un RUC (11 digitos); cualquier otra cosa no se busca.
    public static TipoDocumentoConsulta Detectar(string documento)
    {
        string valor = (documento ?? string.Empty).Trim();
        if (Regex.IsMatch(valor, @"^\d{8}$"))
        {
            return TipoDocumentoConsulta.Dni;
        }
        if (Regex.IsMatch(valor, @"^\d{11}$"))
        {
            return TipoDocumentoConsulta.Ruc;
        }
        return TipoDocumentoConsulta.NoConsultable;
    }
}
