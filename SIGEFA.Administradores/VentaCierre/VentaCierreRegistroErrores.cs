using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

// Registrador local de incidencias y errores del cierre de venta.
// Escribe en %LOCALAPPDATA%\SIGEFA\venta_cierre_errores.log con enmascaramiento de credenciales.
// No abre conexiones a base de datos ni modifica esquemas.
namespace SIGEFA.Administradores.VentaCierre;

public static class VentaCierreRegistroErrores
{
    // Patrón regular para detectar credenciales sensibles en cadenas de conexión o mensajes de error.
    private const string PatronCredenciales = @"(?i)\b(pwd|password|uid|user\s*id)\s*=\s*[^;,\s]+";

    // Enmascara credenciales comunes como Pwd=..., Password=..., Uid=..., User Id=...
    public static string enmascararCredenciales(string texto)
    {
        if (string.IsNullOrEmpty(texto))
        {
            return string.Empty;
        }

        return Regex.Replace(texto, PatronCredenciales, "$1=***");
    }

    // Devuelve la ruta absoluta al archivo de bitácora local en %LOCALAPPDATA%\SIGEFA\venta_cierre_errores.log.
    // Asegura que la carpeta contenedora exista.
    public static string obtenerRutaArchivoLog()
    {
        string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        if (string.IsNullOrEmpty(localAppData))
        {
            return null;
        }

        string directorioSigefa = Path.Combine(localAppData, "SIGEFA");
        if (!Directory.Exists(directorioSigefa))
        {
            Directory.CreateDirectory(directorioSigefa);
        }

        return Path.Combine(directorioSigefa, "venta_cierre_errores.log");
    }

    // Registra una incidencia de la etapa transaccional de guardado en el archivo de bitácora.
    // Mantiene el formato exacto de registro previo del servicio.
    public static void registrarErrorTransaccional(
        Exception ex,
        VentaCierrePaso paso,
        string procedimiento,
        int? itemIndice,
        int? productoId,
        string parametros)
    {
        try
        {
            string rutaLog = obtenerRutaArchivoLog();
            if (string.IsNullOrEmpty(rutaLog))
            {
                return;
            }

            int mysqlNumero = 0;
            string sqlState = string.Empty;
            string mysqlMensaje = ex != null ? ex.Message : string.Empty;

            if (ex is VentaCierreException vcEx)
            {
                mysqlNumero = vcEx.mysqlNumero;
                sqlState = vcEx.sqlState;
                mysqlMensaje = vcEx.mysqlMensaje;
                if (!string.IsNullOrEmpty(vcEx.procedimiento))
                {
                    procedimiento = vcEx.procedimiento;
                }
                if (vcEx.itemIndice.HasValue)
                {
                    itemIndice = vcEx.itemIndice;
                }
                if (vcEx.productoId.HasValue)
                {
                    productoId = vcEx.productoId;
                }
            }
            else if (ex is MySqlException myEx)
            {
                mysqlNumero = myEx.Number;
                sqlState = myEx.SqlState;
                mysqlMensaje = myEx.Message;
            }

            // Filtrar credenciales y garantizar una sola línea
            string mensajeLimpio = enmascararCredenciales((mysqlMensaje ?? string.Empty).Replace("\r", " ").Replace("\n", " ").Trim());
            string linea = string.Format(
                "{0:yyyy-MM-dd HH:mm:ss} | Paso: {1} | Procedimiento: {2} | Item: {3} | Producto: {4} | ErrorMySQL: {5} [{6}] | Mensaje: {7}",
                DateTime.Now,
                paso,
                procedimiento ?? string.Empty,
                itemIndice.HasValue ? itemIndice.Value.ToString() : "N/A",
                productoId.HasValue ? productoId.Value.ToString() : "N/A",
                mysqlNumero,
                sqlState ?? string.Empty,
                mensajeLimpio);

            File.AppendAllText(rutaLog, linea + Environment.NewLine, Encoding.UTF8);
        }
        catch
        {
            // El diagnóstico no debe ocultar la excepción original
        }
    }

    // Registra una incidencia de pasos posteriores al cierre en el archivo de bitácora local.
    public static void registrarErrorPaso(ErrorPaso error)
    {
        if (error == null)
        {
            return;
        }

        try
        {
            string rutaLog = obtenerRutaArchivoLog();
            if (string.IsNullOrEmpty(rutaLog))
            {
                return;
            }

            string mensajeLimpio = enmascararCredenciales((error.mensaje ?? string.Empty).Replace("\r", " ").Replace("\n", " ").Trim());
            string linea = string.Format(
                "{0:yyyy-MM-dd HH:mm:ss} | PostPaso: {1} | Factura: {2} | SerieNum: {3} | Almacen: {4} | Pedido: {5} | Usuario: {6} | Tipo: {7} | Mensaje: {8}",
                error.hora,
                error.paso ?? string.Empty,
                error.facturaId ?? "N/A",
                error.serieNumero ?? "N/A",
                error.almacen ?? "N/A",
                error.pedido ?? "N/A",
                error.usuario ?? "N/A",
                error.tipoExcepcion ?? string.Empty,
                mensajeLimpio);

            File.AppendAllText(rutaLog, linea + Environment.NewLine, Encoding.UTF8);
        }
        catch
        {
            // El diagnóstico no debe afectar el flujo principal
        }
    }

    // Registra múltiples incidencias en lote en el archivo de bitácora local.
    public static void registrarErroresPaso(IEnumerable<ErrorPaso> errores)
    {
        if (errores == null)
        {
            return;
        }

        foreach (ErrorPaso err in errores)
        {
            registrarErrorPaso(err);
        }
    }
}
