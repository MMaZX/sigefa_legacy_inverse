using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace SIGEFA.Administradores.Actualizador;

// Lee del .config del launcher la carpeta del servidor (appSettings\RutaActualizacion).
public static class LectorConfigLauncher
{
    // Devuelve null si el archivo no existe, no se puede leer o no tiene la clave.
    public static string LeerRutaActualizacion(string rutaConfig)
    {
        if (!File.Exists(rutaConfig))
        {
            return null;
        }
        try
        {
            var documento = XDocument.Load(rutaConfig);
            var nodo = documento.Descendants("appSettings").Descendants("add")
                .FirstOrDefault(a => (string)a.Attribute("key") == "RutaActualizacion");
            string valor = nodo == null ? null : (string)nodo.Attribute("value");
            return string.IsNullOrWhiteSpace(valor) ? null : valor.Trim();
        }
        catch (XmlException)
        {
            return null;
        }
        catch (IOException)
        {
            return null;
        }
        catch (UnauthorizedAccessException)
        {
            // El .config existe pero esta cuenta no puede leerlo: sin ruta de servidor, igual que si faltara.
            return null;
        }
    }
}
