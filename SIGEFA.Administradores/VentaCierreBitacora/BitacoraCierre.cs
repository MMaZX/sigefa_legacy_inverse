using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SIGEFA.Administradores.VentaCierreBitacora;

// Orquestador de la bitácora de un intento de cierre de venta.
// Acumula los eventos en memoria y los guarda de una sola vez en Finalizar
// (UPDATE de cabecera + un INSERT múltiple). REGLA: ningún método lanza al
// llamador; la bitácora nunca interrumpe ni cambia el resultado del cierre.
// Si la BD falla (al crear el intento o al finalizar), escribe un archivo de respaldo.
public sealed class BitacoraCierre
{
    // Topes de texto que acepta la tabla de bitácora.
    public const int TopeDetalle = 300;
    public const int TopeMensaje = 500;

    // Topes de las columnas de venta_cierre_log y venta_cierre_log_evento.
    private const int TopeAlmacen = 80;
    private const int TopePaso = 40;
    private const int TopeUsuario = 80;
    private const int TopeEquipo = 60;
    private const int TopeVersion = 20;
    private const int TopeErrorProcedimiento = 80;
    private const int TopeSqlState = 5;

    // Copia del patrón de VentaCierreRegistroErrores (aquel arrastra MySql y no se puede probar).
    private const string PatronCredenciales = @"(?i)\b(pwd|password|uid|user\s*id)\s*=\s*[^;,\s]+";

    private readonly IBitacoraRepositorio _repositorio;
    private readonly IRespaldoArchivo _respaldo;
    private readonly Func<DateTime> _reloj;
    private readonly object _candado = new object();
    private readonly List<EventoBitacora> _eventos = new List<EventoBitacora>();

    private IntentoBitacora _intento;
    private long _logId;
    private int? _numeroIntento;
    private bool _enRespaldo;
    private bool _finalizado;

    public BitacoraCierre(IBitacoraRepositorio repositorio, IRespaldoArchivo respaldo, Func<DateTime> reloj)
    {
        _repositorio = repositorio;
        _respaldo = respaldo;
        _reloj = reloj;
    }

    // Abre el intento. Si la BD falla, sigue en modo respaldo (solo memoria).
    public void Iniciar(IntentoBitacora intento)
    {
        try
        {
            lock (_candado)
            {
                if (intento == null || _intento != null)
                {
                    return;
                }

                _intento = SanearIntento(intento);
                CrearIntentoOPasarARespaldo(_intento);
            }
        }
        catch (Exception)
        {
        }
    }

    // Agrega un evento con la hora del reloj al llamar. Seguro entre hilos.
    public void RegistrarEvento(
        int? bloque, string almacen, string paso, int? item, int? codProducto,
        ResultadoEvento resultado, int? duracionMs, string detalle)
    {
        try
        {
            string detalleLimpio = Limpiar(detalle, TopeDetalle);
            string almacenLimpio = Limpiar(almacen, TopeAlmacen);
            string pasoLimpio = Limpiar(paso, TopePaso);
            lock (_candado)
            {
                if (_intento == null || _finalizado)
                {
                    return;
                }

                _eventos.Add(new EventoBitacora(
                    _eventos.Count + 1, _reloj(), bloque, almacenLimpio, pasoLimpio,
                    item, codProducto, resultado, duracionMs, detalleLimpio));
            }
        }
        catch (Exception)
        {
        }
    }

    // Cierra el intento: BD si está sana; si no, archivo de respaldo. Una sola vez.
    public void Finalizar(ResultadoBitacora resultado)
    {
        try
        {
            IReadOnlyList<EventoBitacora> eventos;
            lock (_candado)
            {
                if (_intento == null || _finalizado || resultado == null)
                {
                    return;
                }

                _finalizado = true;
                eventos = _eventos.ToArray();
            }

            ResultadoBitacora limpio = Sanear(resultado);
            bool guardado = !_enRespaldo && IntentarGuardarEnBd(limpio, eventos);
            if (!guardado)
            {
                EscribirRespaldo(limpio, eventos);
            }
        }
        catch (Exception)
        {
        }
    }

    private void CrearIntentoOPasarARespaldo(IntentoBitacora intento)
    {
        try
        {
            long logId;
            _numeroIntento = _repositorio.CrearIntento(intento, out logId);
            _logId = logId;
        }
        catch (Exception)
        {
            _enRespaldo = true;
        }
    }

    private bool IntentarGuardarEnBd(ResultadoBitacora resultado, IReadOnlyList<EventoBitacora> eventos)
    {
        try
        {
            _repositorio.Finalizar(_logId, resultado, eventos);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private void EscribirRespaldo(ResultadoBitacora resultado, IReadOnlyList<EventoBitacora> eventos)
    {
        try
        {
            _respaldo.Escribir(_intento, _numeroIntento, resultado, eventos);
        }
        catch (Exception)
        {
        }
    }

    // Copia el intento con usuario, equipo y versión sin saltos de línea y con tope.
    private static IntentoBitacora SanearIntento(IntentoBitacora intento)
    {
        return new IntentoBitacora(
            intento.CodPedido, intento.CodUsuario, Limpiar(intento.Usuario, TopeUsuario),
            Limpiar(intento.Equipo, TopeEquipo), Limpiar(intento.VersionApp, TopeVersion),
            intento.TotalBloques, intento.Inicio);
    }

    // Copia el resultado con el mensaje de error enmascarado y recortado.
    private static ResultadoBitacora Sanear(ResultadoBitacora resultado)
    {
        return new ResultadoBitacora(
            resultado.Estado, resultado.BloquesOk, resultado.Fin, resultado.DuracionMs,
            Limpiar(resultado.ErrorPaso, TopePaso),
            Limpiar(resultado.ErrorProcedimiento, TopeErrorProcedimiento), resultado.ErrorMysqlNum,
            Limpiar(resultado.ErrorSqlState, TopeSqlState), Limpiar(resultado.ErrorMensaje, TopeMensaje),
            resultado.CodFacturaVenta);
    }

    // Quita saltos de línea, enmascara credenciales y aplica el tope (en ese orden,
    // para que el recorte nunca deje a la vista parte de una credencial).
    private static string Limpiar(string texto, int tope)
    {
        if (string.IsNullOrEmpty(texto))
        {
            return string.Empty;
        }

        string sinSaltos = texto.Replace("\r", " ").Replace("\n", " ").Trim();
        string enmascarado = Regex.Replace(sinSaltos, PatronCredenciales, "$1=***");
        return enmascarado.Length > tope ? enmascarado.Substring(0, tope) : enmascarado;
    }
}
