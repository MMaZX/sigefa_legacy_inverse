using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Respaldo en archivo de la bitácora: <carpeta>/<codPedido>.<intento>.log.
// Un solo WriteAllText por intento. La carpeta la inyecta el llamador y se crea
// bajo demanda. Si no se puede escribir, devuelve null y no lanza.
public sealed class RespaldoArchivo : IRespaldoArchivo
{
    private const string FormatoFechaHora = "yyyy-MM-dd HH:mm:ss.fff";
    private const string FormatoHora = "HH:mm:ss.fff";
    private const string Linea = "------------------------------------------------------------";

    private readonly string _carpeta;

    public RespaldoArchivo(string carpeta)
    {
        _carpeta = carpeta;
    }

    public string Escribir(
        IntentoBitacora intento, int? numeroIntento, ResultadoBitacora resultado,
        IReadOnlyList<EventoBitacora> eventos)
    {
        try
        {
            Directory.CreateDirectory(_carpeta);
            string ruta = ElegirRuta(intento.CodPedido, numeroIntento);
            File.WriteAllText(ruta, ArmarTexto(intento, ObtenerNumero(ruta), resultado, eventos), Encoding.UTF8);
            return ruta;
        }
        catch (Exception)
        {
            return null;
        }
    }

    // Usa el número recibido si su archivo está libre; si no, el siguiente libre.
    private string ElegirRuta(int codPedido, int? numeroIntento)
    {
        if (numeroIntento.HasValue && !File.Exists(RutaDe(codPedido, numeroIntento.Value)))
        {
            return RutaDe(codPedido, numeroIntento.Value);
        }

        return RutaDe(codPedido, SiguienteLibre(codPedido));
    }

    private string RutaDe(int codPedido, int numero)
    {
        return Path.Combine(_carpeta, codPedido.ToString(CultureInfo.InvariantCulture) + "." + numero.ToString(CultureInfo.InvariantCulture) + ".log");
    }

    // Máximo número existente en <pedido>.<n>.log más uno; ignora otros nombres.
    private int SiguienteLibre(int codPedido)
    {
        string prefijo = codPedido.ToString(CultureInfo.InvariantCulture) + ".";
        int maximo = 0;
        foreach (string archivo in Directory.GetFiles(_carpeta, prefijo + "*.log"))
        {
            bool esLog = string.Equals(Path.GetExtension(archivo), ".log", StringComparison.OrdinalIgnoreCase);
            int numero = esLog ? LeerNumero(Path.GetFileNameWithoutExtension(archivo), prefijo) : 0;
            maximo = Math.Max(maximo, numero);
        }

        return maximo + 1;
    }

    // Número entre el prefijo "<pedido>." y la extensión; 0 si no es numérico.
    private static int LeerNumero(string nombreSinExtension, string prefijo)
    {
        if (!nombreSinExtension.StartsWith(prefijo, StringComparison.Ordinal))
        {
            return 0;
        }

        string texto = nombreSinExtension.Substring(prefijo.Length);
        int numero;
        bool esNumero = int.TryParse(texto, NumberStyles.None, CultureInfo.InvariantCulture, out numero);
        return esNumero ? numero : 0;
    }

    // Número de intento que quedó en el nombre del archivo elegido.
    private static int ObtenerNumero(string ruta)
    {
        string nombre = Path.GetFileNameWithoutExtension(ruta);
        string texto = nombre.Substring(nombre.LastIndexOf('.') + 1);
        return int.Parse(texto, CultureInfo.InvariantCulture);
    }

    private static string ArmarTexto(
        IntentoBitacora intento, int numero, ResultadoBitacora resultado,
        IReadOnlyList<EventoBitacora> eventos)
    {
        var texto = new StringBuilder();
        texto.AppendLine("Pedido: " + intento.CodPedido.ToString(CultureInfo.InvariantCulture));
        texto.AppendLine("Intento: " + numero.ToString(CultureInfo.InvariantCulture));
        texto.AppendLine("Usuario: " + intento.Usuario);
        texto.AppendLine("Equipo: " + intento.Equipo);
        texto.AppendLine("Version: " + intento.VersionApp);
        texto.AppendLine("Inicio: " + intento.Inicio.ToString(FormatoFechaHora, CultureInfo.InvariantCulture));
        texto.AppendLine("Bloques: " + intento.TotalBloques.ToString(CultureInfo.InvariantCulture));
        texto.AppendLine(Linea);
        foreach (EventoBitacora evento in eventos)
        {
            texto.AppendLine(LineaDeEvento(evento));
        }

        texto.AppendLine(Linea);
        AgregarResultado(texto, resultado);
        return texto.ToString();
    }

    private static string LineaDeEvento(EventoBitacora evento)
    {
        var linea = new StringBuilder();
        linea.Append('[').Append(evento.Hora.ToString(FormatoHora, CultureInfo.InvariantCulture)).Append("] ");
        linea.Append(evento.Resultado.ToString().ToUpperInvariant()).Append(' ');
        linea.Append("bloque=").Append(Valor(evento.Bloque)).Append(' ');
        linea.Append("almacen=").Append(evento.Almacen).Append(' ');
        linea.Append("paso=").Append(evento.Paso).Append(' ');
        linea.Append("item=").Append(Valor(evento.Item)).Append(' ');
        linea.Append("producto=").Append(Valor(evento.CodProducto)).Append(' ');
        linea.Append("duracion=").Append(Valor(evento.DuracionMs)).Append("ms");
        if (evento.Detalle.Length > 0)
        {
            linea.Append(" | ").Append(evento.Detalle);
        }

        return linea.ToString();
    }

    private static void AgregarResultado(StringBuilder texto, ResultadoBitacora resultado)
    {
        texto.AppendLine("Resultado: " + resultado.Estado.ToString().ToUpperInvariant());
        texto.AppendLine("Bloques OK: " + resultado.BloquesOk.ToString(CultureInfo.InvariantCulture));
        texto.AppendLine("Fin: " + resultado.Fin.ToString(FormatoFechaHora, CultureInfo.InvariantCulture));
        texto.AppendLine("Duracion: " + resultado.DuracionMs.ToString(CultureInfo.InvariantCulture) + " ms");
        texto.AppendLine("Factura: " + Valor(resultado.CodFacturaVenta));
        if (resultado.Estado != EstadoBitacora.Error)
        {
            return;
        }

        texto.AppendLine("Error paso: " + resultado.ErrorPaso);
        texto.AppendLine("Error procedimiento: " + resultado.ErrorProcedimiento);
        texto.AppendLine("Error MySQL: " + Valor(resultado.ErrorMysqlNum) + " [" + resultado.ErrorSqlState + "]");
        texto.AppendLine("Error mensaje: " + resultado.ErrorMensaje);
    }

    private static string Valor(int? numero)
    {
        return numero.HasValue ? numero.Value.ToString(CultureInfo.InvariantCulture) : "-";
    }
}
