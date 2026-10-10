using System;
using System.Collections.Generic;
using System.IO;
using SIGEFA.Administradores.VentaCierreBitacora;

namespace SIGEFA.Tests.VentaCierreBitacora;

// Dobles de prueba compartidos por las pruebas de la bitácora del cierre de venta.
// Nada toca la BD: el repositorio es un falso en memoria y el archivo usa una
// carpeta temporal única que se borra al terminar.

// Repositorio falso: registra lo recibido y puede lanzar en cada operación.
internal sealed class RepositorioFalso : IBitacoraRepositorio
{
    public int NumeroIntento = 1;
    public long LogId = 100;
    public bool LanzarEnCrear;
    public bool LanzarEnFinalizar;

    public readonly List<IntentoBitacora> Intentos = new List<IntentoBitacora>();
    public readonly List<long> LogIdsFinalizados = new List<long>();
    public readonly List<ResultadoBitacora> Resultados = new List<ResultadoBitacora>();
    public readonly List<IReadOnlyList<EventoBitacora>> EventosFinalizados = new List<IReadOnlyList<EventoBitacora>>();

    public int CrearIntento(IntentoBitacora intento, out long logId)
    {
        logId = 0;
        if (LanzarEnCrear)
        {
            throw new InvalidOperationException("falla al crear el intento");
        }

        Intentos.Add(intento);
        logId = LogId;
        return NumeroIntento;
    }

    public void Finalizar(long logId, ResultadoBitacora resultado, IReadOnlyList<EventoBitacora> eventos)
    {
        if (LanzarEnFinalizar)
        {
            throw new InvalidOperationException("falla al finalizar");
        }

        LogIdsFinalizados.Add(logId);
        Resultados.Add(resultado);
        EventosFinalizados.Add(eventos);
    }
}

// Respaldo que siempre lanza: comprueba que la bitácora no propaga ese fallo.
internal sealed class RespaldoQueLanza : IRespaldoArchivo
{
    public string Escribir(IntentoBitacora intento, int? numeroIntento, ResultadoBitacora resultado, IReadOnlyList<EventoBitacora> eventos)
    {
        throw new IOException("disco lleno");
    }
}

// Reloj falso y controlable: devuelve siempre el valor de Actual.
internal sealed class RelojFalso
{
    public DateTime Actual = new DateTime(2026, 10, 10, 14, 3, 22, 123);

    public DateTime Ahora()
    {
        return Actual;
    }
}

// Carpeta temporal única; se borra entera al liberar.
internal sealed class CarpetaTemporal : IDisposable
{
    public CarpetaTemporal()
    {
        Ruta = Path.Combine(Path.GetTempPath(), "bitacora_cierre_" + Guid.NewGuid().ToString("N"));
    }

    public string Ruta { get; }

    public void Crear()
    {
        Directory.CreateDirectory(Ruta);
    }

    public string Archivo(string nombre)
    {
        return Path.Combine(Ruta, nombre);
    }

    public void Dispose()
    {
        try
        {
            if (Directory.Exists(Ruta))
            {
                Directory.Delete(Ruta, true);
            }
        }
        catch (IOException)
        {
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}

// Fábricas de datos de prueba.
internal static class DatosBitacora
{
    public static IntentoBitacora Intento(int codPedido = 7, int totalBloques = 2)
    {
        return new IntentoBitacora(
            codPedido, 15, "jperez", "PC-01", "1.0.0.0", totalBloques,
            new DateTime(2026, 10, 10, 14, 3, 22, 123));
    }

    public static ResultadoBitacora Ok()
    {
        return new ResultadoBitacora(
            EstadoBitacora.Ok, 2, new DateTime(2026, 10, 10, 14, 3, 25, 789), 3666,
            string.Empty, string.Empty, null, string.Empty, string.Empty, 555);
    }

    public static ResultadoBitacora Error(string mensaje = "boom")
    {
        return new ResultadoBitacora(
            EstadoBitacora.Error, 1, new DateTime(2026, 10, 10, 14, 3, 25, 789), 3666,
            "guardarDetalle", "GuardaDetalleFacturaVenta", 1062, "23000", mensaje, null);
    }

    public static EventoBitacora Evento(int orden = 1, string paso = "abrirTransaccion", ResultadoEvento resultado = ResultadoEvento.Info)
    {
        return new EventoBitacora(
            orden, new DateTime(2026, 10, 10, 14, 3, 22, 456), 1, "ALMACEN CENTRAL", paso,
            null, null, resultado, null, string.Empty);
    }
}
