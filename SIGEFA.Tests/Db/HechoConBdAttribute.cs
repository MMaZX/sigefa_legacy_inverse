using System;
using Xunit;

namespace SIGEFA.Tests.Helper;

/// <summary>
/// Prueba de integracion contra MySQL real. La cadena de conexion se lee SOLO de la variable de
/// entorno SIGEFA_TEST_CONN; si no existe, la prueba se omite. Nunca se guarda una clave en el codigo.
/// </summary>
public sealed class HechoConBdAttribute : FactAttribute
{
    public const string VariableEntorno = "SIGEFA_TEST_CONN";

    public HechoConBdAttribute()
    {
        if (string.IsNullOrWhiteSpace(CadenaConexion()))
        {
            Skip = "Defina la variable de entorno " + VariableEntorno + " para ejecutar pruebas contra MySQL.";
        }
    }

    public static string CadenaConexion()
    {
        return Environment.GetEnvironmentVariable(VariableEntorno);
    }
}
