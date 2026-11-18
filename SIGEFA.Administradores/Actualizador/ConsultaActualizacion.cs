using System;

namespace SIGEFA.Administradores.Actualizador;

// Pregunta al servidor por la version publicada y la compara con la de este SIGEFA.
public static class ConsultaActualizacion
{
    public static ResultadoConsultaActualizacion Consultar(string rutaServidor, int buildLocal, TimeSpan espera)
    {
        var servidor = new ServidorActualizacion(rutaServidor, espera);
        var manifiesto = servidor.LeerManifiesto(out string motivo);
        return new ResultadoConsultaActualizacion(DecisorActualizacion.Evaluar(buildLocal, manifiesto), manifiesto, buildLocal, motivo);
    }
}
