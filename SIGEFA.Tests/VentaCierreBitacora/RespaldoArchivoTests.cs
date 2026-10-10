using System;
using System.Collections.Generic;
using System.IO;
using SIGEFA.Administradores.VentaCierreBitacora;
using Xunit;

namespace SIGEFA.Tests.VentaCierreBitacora;

// Pruebas del respaldo en archivo <pedido>.<intento>.log (T2 de venta-cierre-bitacora).
// Usa una carpeta temporal única por prueba; no toca la BD.
public class RespaldoArchivoTests : IDisposable
{
    private readonly CarpetaTemporal _carpeta = new CarpetaTemporal();

    public void Dispose()
    {
        _carpeta.Dispose();
    }

    private static List<EventoBitacora> DosEventos()
    {
        return new List<EventoBitacora>
        {
            new EventoBitacora(
                1, new DateTime(2026, 10, 10, 14, 3, 22, 456), 1, "ALMACEN CENTRAL", "abrirTransaccion",
                null, null, ResultadoEvento.Info, null, string.Empty),
            new EventoBitacora(
                2, new DateTime(2026, 10, 10, 14, 3, 23, 7), 1, "ALMACEN CENTRAL", "guardarDetalle",
                3, 5072, ResultadoEvento.Error, 12, "duplicado"),
        };
    }

    [Fact]
    public void Escribir_CreaLaCarpetaBajoDemandaYUsaElNumeroDeIntentoRecibido()
    {
        var respaldo = new RespaldoArchivo(_carpeta.Ruta);

        string ruta = respaldo.Escribir(DatosBitacora.Intento(7), 3, DatosBitacora.Ok(), DosEventos());

        Assert.Equal(_carpeta.Archivo("7.3.log"), ruta);
        Assert.True(File.Exists(ruta));
    }

    [Fact]
    public void Escribir_ConContenidoConCabeceraEventosConMilisegundosYResultado()
    {
        var respaldo = new RespaldoArchivo(_carpeta.Ruta);

        string ruta = respaldo.Escribir(DatosBitacora.Intento(7), 3, DatosBitacora.Error(), DosEventos());

        string texto = File.ReadAllText(ruta);
        Assert.Contains("Pedido: 7", texto);
        Assert.Contains("Intento: 3", texto);
        Assert.Contains("Usuario: jperez", texto);
        Assert.Contains("Equipo: PC-01", texto);
        Assert.Contains("Inicio: 2026-10-10 14:03:22.123", texto);
        Assert.Contains("Bloques: 2", texto);
        Assert.Contains("[14:03:22.456]", texto);
        Assert.Contains("[14:03:23.007]", texto);
        Assert.Contains("abrirTransaccion", texto);
        Assert.Contains("guardarDetalle", texto);
        Assert.Contains("duplicado", texto);
        Assert.Contains("Resultado: ERROR", texto);
        Assert.Contains("Fin: 2026-10-10 14:03:25.789", texto);
        Assert.Contains("Duracion: 3666 ms", texto);
        Assert.Contains("GuardaDetalleFacturaVenta", texto);
        Assert.Contains("1062", texto);
    }

    [Fact]
    public void Escribir_SinNumeroDeIntento_UsaElSiguienteLibreIgnorandoOtrosArchivos()
    {
        _carpeta.Crear();
        File.WriteAllText(_carpeta.Archivo("7.1.log"), "previo");
        File.WriteAllText(_carpeta.Archivo("7.4.log"), "previo");
        File.WriteAllText(_carpeta.Archivo("8.9.log"), "otro pedido");
        File.WriteAllText(_carpeta.Archivo("7.x.log"), "no numerico");
        File.WriteAllText(_carpeta.Archivo("7.7.txt"), "otra extension");
        var respaldo = new RespaldoArchivo(_carpeta.Ruta);

        string ruta = respaldo.Escribir(DatosBitacora.Intento(7), null, DatosBitacora.Ok(), DosEventos());

        Assert.Equal(_carpeta.Archivo("7.5.log"), ruta);
    }

    [Fact]
    public void Escribir_SinNumeroEnCarpetaVacia_EmpiezaEnUno()
    {
        var respaldo = new RespaldoArchivo(_carpeta.Ruta);

        string ruta = respaldo.Escribir(DatosBitacora.Intento(7), null, DatosBitacora.Ok(), DosEventos());

        Assert.Equal(_carpeta.Archivo("7.1.log"), ruta);
    }

    [Fact]
    public void Escribir_ConNumeroYaOcupado_NoPisaElArchivoExistente()
    {
        _carpeta.Crear();
        File.WriteAllText(_carpeta.Archivo("7.3.log"), "previo");
        var respaldo = new RespaldoArchivo(_carpeta.Ruta);

        string ruta = respaldo.Escribir(DatosBitacora.Intento(7), 3, DatosBitacora.Ok(), DosEventos());

        Assert.Equal("previo", File.ReadAllText(_carpeta.Archivo("7.3.log")));
        Assert.Equal(_carpeta.Archivo("7.4.log"), ruta);
    }

    [Fact]
    public void Escribir_ConCarpetaNoEscribible_NoLanzaYDevuelveNulo()
    {
        _carpeta.Crear();
        string archivoQueBloquea = _carpeta.Archivo("bloqueo");
        File.WriteAllText(archivoQueBloquea, "soy un archivo, no una carpeta");
        var respaldo = new RespaldoArchivo(Path.Combine(archivoQueBloquea, "SIGEFA_LOGS"));
        string ruta = "sin-cambio";

        Exception falla = Record.Exception(() =>
        {
            ruta = respaldo.Escribir(DatosBitacora.Intento(7), 1, DatosBitacora.Ok(), DosEventos());
        });

        Assert.Null(falla);
        Assert.Null(ruta);
    }
}
