using System;
using System.Collections.Generic;
using SIGEFA.Conexion;

namespace SIGEFA.Administradores.ReqVenta;

// Anulación de un requerimiento de venta en UNA transacción (T2e).
// Lee el requerimiento con bloqueo, deja que ReqVentaReglas decida el camino y delega en
// la anulación de pendiente (estado 7) o en la de aprobado con extorno (estado 13).
// Si algo falla, la transacción se revierte completa: no queda nada a medias.
public static class ReqVentaFlujoService
{
    // Señal interna para forzar el rollback: Db.Transaccion confirma cualquier acción que termine
    // sin lanzar, y los subservicios devuelven el fallo como resultado en vez de lanzarlo.
    private sealed class AnulacionRevertidaException : Exception
    {
        public AnulacionRevertidaException(ResultadoAnulacion resultado)
            : base(resultado.Mensaje)
        {
            Resultado = resultado;
        }

        public ResultadoAnulacion Resultado { get; }
    }

    // Reenvía cada paso al progreso real y recuerda el último estado por clave.
    // Así el servicio puede cerrar MarcarAnulado en Error cuando la anulación
    // falla antes de cerrarlo, sin duplicarlo si ya quedó en Error.
    private sealed class SeguimientoProgreso : IProgress<PasoOperacion>
    {
        private readonly IProgress<PasoOperacion> _destino;
        private readonly Dictionary<string, EstadoPaso> _estados = new Dictionary<string, EstadoPaso>(StringComparer.Ordinal);

        public SeguimientoProgreso(IProgress<PasoOperacion> destino)
        {
            _destino = destino;
        }

        public void Report(PasoOperacion paso)
        {
            if (paso == null)
            {
                return;
            }

            _estados[paso.Clave] = paso.Estado;
            if (_destino != null)
            {
                _destino.Report(paso);
            }
        }

        public bool EstaCerrado(string clave)
        {
            EstadoPaso estado;
            if (!_estados.TryGetValue(clave, out estado))
            {
                return false;
            }

            return estado == EstadoPaso.Listo || estado == EstadoPaso.Error;
        }
    }

    // Anula el requerimiento contra la base configurada. Confirma solo si todo salió bien.
    public static ResultadoAnulacion Anular(int codReq, int codUser)
    {
        return Anular(codReq, codUser, null);
    }

    // Anula informando cada paso al progreso (puede ser null). MarcarAnulado queda
    // Listo solo cuando la transacción ya confirmó; ante cualquier fallo queda en Error.
    public static ResultadoAnulacion Anular(int codReq, int codUser, IProgress<PasoOperacion> progreso)
    {
        return Anular(codReq, codUser, BuscarUsuario, Db.Transaccion<ResultadoAnulacion>, ReqVentaRegistroErrores.Registrar, progreso);
    }

    // Login del usuario (columna usuario.usuario) por su código real, usuario.codUsuario. No confundir con la columna
    // usuario.codUser, que es otro dato y devolvería a otra persona. Devuelve null si no existe.
    internal static string BuscarUsuario(int codUser)
    {
        Dictionary<string, object> fila = Db.Consultar(
            "SELECT usuario FROM usuario WHERE codUsuario = @id",
            new { id = codUser }).First();
        if (fila == null)
        {
            return null;
        }

        return fila.Valor<string>("usuario");
    }

    // Variante con el buscador de usuario, el ejecutor de transacción y el registrador inyectados (pruebas).
    // El buscador solo se invoca al registrar un fallo. El ejecutor debe confirmar si la acción termina y revertir
    // si lanza, igual que Db.Transaccion.
    public static ResultadoAnulacion Anular(
        int codReq,
        int codUser,
        Func<int, string> buscarUsuario,
        Func<Func<IConsultor, ResultadoAnulacion>, ResultadoAnulacion> transaccion,
        Action<string> registrar,
        IProgress<PasoOperacion> progreso = null)
    {
        SeguimientoProgreso seguimiento = new SeguimientoProgreso(progreso);
        if (transaccion == null)
        {
            return FalloConPaso(seguimiento, "El ejecutor de la transacción es obligatorio.");
        }

        if (codReq <= 0)
        {
            return FalloConPaso(seguimiento, "El requerimiento debe ser mayor que cero.");
        }

        if (codUser <= 0)
        {
            return FalloConPaso(seguimiento, "Debe indicar el usuario que solicita la anulación.");
        }

        ResultadoAnulacion resultado = EjecutarRevirtiendoSiFalla(codReq, codUser, transaccion, seguimiento);
        if (resultado.Ok)
        {
            seguimiento.Report(ReqVentaTextos.Paso(ReqVentaTextos.MarcarAnulado, EstadoPaso.Listo));
        }
        else
        {
            CerrarMarcarAnuladoEnError(seguimiento, resultado.Mensaje);
            Registrar(registrar, codReq, DescribirUsuario(codUser, buscarUsuario), resultado.Mensaje);
        }

        return resultado;
    }

    // Corre la anulación y traduce cualquier fallo, devuelto o lanzado, en un resultado fallido
    // dejando la transacción revertida.
    private static ResultadoAnulacion EjecutarRevirtiendoSiFalla(
        int codReq,
        int codUser,
        Func<Func<IConsultor, ResultadoAnulacion>, ResultadoAnulacion> transaccion,
        IProgress<PasoOperacion> progreso)
    {
        try
        {
            return transaccion(tx => AnularConfirmando(tx, codReq, codUser, progreso));
        }
        catch (AnulacionRevertidaException revertida)
        {
            return revertida.Resultado;
        }
        catch (Exception ex)
        {
            return Fallo("No se pudo anular el requerimiento " + codReq + ": " + MensajeConCausa(ex));
        }
    }

    // Acción que corre dentro de la transacción: si el resultado es fallido, lanza para forzar el rollback.
    private static ResultadoAnulacion AnularConfirmando(IConsultor consultor, int codReq, int codUser, IProgress<PasoOperacion> progreso)
    {
        ResultadoAnulacion resultado = AnularEn(consultor, codReq, codUser, progreso);
        if (!resultado.Ok)
        {
            throw new AnulacionRevertidaException(resultado);
        }

        return resultado;
    }

    // Decide el camino con el estado leído (FOR UPDATE) y delega. No confirma ni revierte.
    // Reenvía el progreso a la anulación elegida, que informa sus pasos dentro de la transacción.
    public static ResultadoAnulacion AnularEn(IConsultor consultor, int codReq, int codUser, IProgress<PasoOperacion> progreso = null)
    {
        Dictionary<string, object> requerimiento = ReqVentaConsultas.ObtenerRequerimiento(consultor, codReq, true);
        if (requerimiento == null)
        {
            return Fallo("El requerimiento " + codReq + " no existe.");
        }

        int tipoReq = requerimiento.Valor<int>("tipo_req");
        int estado = requerimiento.Valor<int>("estado");
        DecisionAnulacion decision = ReqVentaReglas.Evaluar(tipoReq, estado);
        switch (decision.Accion)
        {
            case AccionAnulacion.AnularPendiente:
                return ReqVentaAnulacionPendiente.AnularPendiente(consultor, codReq, codUser, progreso);
            case AccionAnulacion.AnularConExtorno:
                return ReqVentaAnulacionConExtorno.AnularConExtorno(consultor, codReq, codUser, progreso);
            default:
                return Fallo(decision.Motivo);
        }
    }

    // Usuario para la bitácora. Si no hay buscador, la búsqueda falla (por ejemplo, la base no responde) o no
    // devuelve nada, se registra el código: la bitácora nunca se pierde ni queda con el usuario en blanco.
    private static string DescribirUsuario(int codUser, Func<int, string> buscarUsuario)
    {
        string usuario = null;
        if (buscarUsuario != null)
        {
            try
            {
                usuario = buscarUsuario(codUser);
            }
            catch
            {
                // Un fallo al buscar el nombre no debe impedir registrar el fallo de la anulación.
            }
        }

        if (string.IsNullOrWhiteSpace(usuario))
        {
            return "código " + codUser;
        }

        return usuario.Trim();
    }

    private static void Registrar(Action<string> registrar, int codReq, string usuario, string mensaje)
    {
        if (registrar == null)
        {
            return;
        }

        try
        {
            registrar("Anulación de requerimiento | Req: " + codReq + " | Usuario: " + usuario + " | " + mensaje);
        }
        catch
        {
            // Un registrador defectuoso no debe ocultar el resultado de la anulación.
        }
    }

    // Une el tipo y el mensaje de la excepción con los de sus causas internas.
    private static string MensajeConCausa(Exception ex)
    {
        List<string> partes = new List<string>();
        Exception actual = ex;
        while (actual != null)
        {
            partes.Add(actual.GetType().Name + ": " + actual.Message);
            actual = actual.InnerException;
        }

        return string.Join(" <- ", partes.ToArray());
    }

    // Fallo que además deja MarcarAnulado en Error si aún no se cerró.
    private static ResultadoAnulacion FalloConPaso(SeguimientoProgreso seguimiento, string mensaje)
    {
        CerrarMarcarAnuladoEnError(seguimiento, mensaje);
        return Fallo(mensaje);
    }

    // Garantiza MarcarAnulado en Error al salir con fallo, salvo que ya esté cerrado.
    private static void CerrarMarcarAnuladoEnError(SeguimientoProgreso seguimiento, string motivo)
    {
        if (seguimiento == null || seguimiento.EstaCerrado(ReqVentaTextos.MarcarAnulado))
        {
            return;
        }

        seguimiento.Report(ReqVentaTextos.Paso(ReqVentaTextos.MarcarAnulado, EstadoPaso.Error, motivo));
    }

    private static ResultadoAnulacion Fallo(string mensaje)
    {
        return new ResultadoAnulacion(false, mensaje);
    }
}
