using System;
using System.Collections.Generic;
using SIGEFA.Conexion;

namespace SIGEFA.Administradores.ReqVenta;

// Guardado de un requerimiento de venta en segundo plano (T6 de ui-progreso-requerimiento).
// Replica 1+N de clsAdmRequerimientoAlmacen.insert (GuardaRequerimientoAlmacen +
// GuardaDetalleRequerimientoAlmacen en una sola transacción) con los mismos
// procedimientos y el mismo orden, pero devuelve ResultadoOperacion en vez de
// mostrar cuadros: la capa de administración muestra MessageBoxEx en errores
// (R1) y no es seguro llamarla desde el hilo de fondo con el diálogo abierto.
// La lógica pura vive aquí para probarla sin interfaz; el formulario arma los
// datos en el hilo de la interfaz y corre Guardar dentro del diálogo.
public sealed class DatosGuardadoRequerimiento
{
    public DatosGuardadoRequerimiento(
        int codTipoDocumento, string numDocumento, int codSerie, string numSerie,
        int codAlmacenRegistro, int codUsuarioRegistro, DateTime fechaRegistro,
        int codAlmacenSolicitante, int codAlmacenDespacho, DateTime fechaRequerimiento,
        int estado, string comentarioSolicitante, string comentarioDespacho, int tipoReq,
        int codPropuestaPedido, string codPedidoVenta,
        string nombreContacto, string telefonoContacto, int delivery,
        string direccionDelivery, string autorizadoPor)
    {
        CodTipoDocumento = codTipoDocumento;
        NumDocumento = numDocumento;
        CodSerie = codSerie;
        NumSerie = numSerie;
        CodAlmacenRegistro = codAlmacenRegistro;
        CodUserRegistro = codUsuarioRegistro;
        FechaRegistro = fechaRegistro;
        CodAlmacenSolicitante = codAlmacenSolicitante;
        CodAlmacenDespacho = codAlmacenDespacho;
        FechaRequerimiento = fechaRequerimiento;
        Estado = estado;
        ComentarioSolicitante = comentarioSolicitante;
        ComentarioDespacho = comentarioDespacho;
        TipoReq = tipoReq;
        CodPropuestaPedido = codPropuestaPedido;
        CodPedidoVenta = codPedidoVenta;
        NombreContacto = nombreContacto;
        TelefonoContacto = telefonoContacto;
        Delivery = delivery;
        DireccionDelivery = direccionDelivery;
        AutorizadoPor = autorizadoPor;
    }

    // Código asignado por la base (newid). Cero antes de guardar.
    public int Codigo { get; set; }

    public int CodTipoDocumento { get; }

    public string NumDocumento { get; }

    public int CodSerie { get; }

    public string NumSerie { get; }

    public int CodAlmacenRegistro { get; }

    public int CodUserRegistro { get; }

    public DateTime FechaRegistro { get; }

    public int CodAlmacenSolicitante { get; }

    public int CodAlmacenDespacho { get; }

    public DateTime FechaRequerimiento { get; }

    public int Estado { get; }

    public string ComentarioSolicitante { get; }

    public string ComentarioDespacho { get; }

    public int TipoReq { get; }

    public int CodPropuestaPedido { get; }

    public string CodPedidoVenta { get; }

    public string NombreContacto { get; }

    public string TelefonoContacto { get; }

    public int Delivery { get; }

    public string DireccionDelivery { get; }

    public string AutorizadoPor { get; }
}

public sealed class DatosGuardadoDetalle
{
    public DatosGuardadoDetalle(
        int codigo, int codProducto, int codUnidad,
        decimal cantidad, decimal cantidadPedida, decimal cantidadPendiente,
        decimal cantidadConfirmada, decimal cantidadPendienteAprobada)
    {
        Codigo = codigo;
        CodProducto = codProducto;
        CodUnidad = codUnidad;
        Cantidad = cantidad;
        CantidadPedida = cantidadPedida;
        CantidadPendiente = cantidadPendiente;
        CantidadConfirmada = cantidadConfirmada;
        CantidadPendienteAprobada = cantidadPendienteAprobada;
    }

    public int Codigo { get; }

    public int CodProducto { get; }

    public int CodUnidad { get; }

    public decimal Cantidad { get; }

    public decimal CantidadPedida { get; }

    public decimal CantidadConfirmada { get; }

    public decimal CantidadPendiente { get; }

    public decimal CantidadPendienteAprobada { get; }
}

public static class ReqVentaGuardado
{
    // Señal interna para forzar el rollback: Db.Transaccion confirma cualquier acción
    // que termine sin lanzar, y el guardado devuelve el fallo como resultado.
    private sealed class GuardadoRevertidoException : Exception
    {
        public GuardadoRevertidoException(ResultadoOperacion resultado)
            : base(resultado.Mensaje)
        {
            Resultado = resultado;
        }

        public ResultadoOperacion Resultado { get; }
    }

    // Guarda contra la base configurada en una sola transacción. Todo el bloque
    // corre dentro del Task.Run del diálogo (R2: TransactionScope sensible al hilo,
    // aquí una sola conexión de Db.Transaccion con la misma garantía todo o nada).
    public static ResultadoOperacion Guardar(
        DatosGuardadoRequerimiento cabecera, IList<DatosGuardadoDetalle> detalles)
    {
        return Guardar(cabecera, detalles, Db.Transaccion<ResultadoOperacion>);
    }

    // Variante con el ejecutor de transacción inyectado (pruebas).
    public static ResultadoOperacion Guardar(
        DatosGuardadoRequerimiento cabecera, IList<DatosGuardadoDetalle> detalles,
        Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion)
    {
        if (cabecera == null)
        {
            return Fallo("No hay datos del requerimiento para guardar.");
        }

        if (detalles == null)
        {
            return Fallo("No hay detalle del requerimiento para guardar.");
        }

        if (transaccion == null)
        {
            return Fallo("El ejecutor de la transacción es obligatorio.");
        }

        try
        {
            return transaccion(tx => GuardarConfirmando(tx, cabecera, detalles));
        }
        catch (GuardadoRevertidoException revertido)
        {
            return revertido.Resultado;
        }
        catch (Exception ex)
        {
            return Fallo(MensajeError(ex));
        }
    }

    // Núcleo con el consultor del llamador: no abre conexiones ni confirma ni
    // revierte; cualquier fallo sale como resultado para que el llamador revierta.
    public static ResultadoOperacion GuardarEn(
        IConsultor consultor,
        DatosGuardadoRequerimiento cabecera, IList<DatosGuardadoDetalle> detalles)
    {
        if (consultor == null)
        {
            return Fallo("El consultor es obligatorio.");
        }

        if (cabecera == null)
        {
            return Fallo("No hay datos del requerimiento para guardar.");
        }

        if (detalles == null)
        {
            return Fallo("No hay detalle del requerimiento para guardar.");
        }

        try
        {
            return Ejecutar(consultor, cabecera, detalles);
        }
        catch (Exception ex)
        {
            return Fallo(MensajeError(ex));
        }
    }

    // Acción que corre dentro de la transacción: si el resultado es fallido, lanza
    // para forzar el rollback en vez de confirmar un guardado a medias.
    private static ResultadoOperacion GuardarConfirmando(
        IConsultor consultor,
        DatosGuardadoRequerimiento cabecera, IList<DatosGuardadoDetalle> detalles)
    {
        ResultadoOperacion resultado = GuardarEn(consultor, cabecera, detalles);
        if (!resultado.Ok)
        {
            throw new GuardadoRevertidoException(resultado);
        }

        return resultado;
    }

    // 1+N en el mismo orden que el legacy: cabecera y luego cada detalle con el
    // código recién asignado. Un newid en cero equivale al false del legacy.
    private static ResultadoOperacion Ejecutar(
        IConsultor consultor,
        DatosGuardadoRequerimiento cabecera, IList<DatosGuardadoDetalle> detalles)
    {
        int nuevoCodigo = InsertarCabecera(consultor, cabecera);
        if (nuevoCodigo <= 0)
        {
            return Fallo("No se pudo guardar el requerimiento.");
        }

        cabecera.Codigo = nuevoCodigo;

        foreach (DatosGuardadoDetalle detalle in detalles)
        {
            if (detalle == null)
            {
                return Fallo("Hay una línea del detalle sin datos.");
            }

            int nuevoDetalle = InsertarDetalle(consultor, nuevoCodigo, detalle);
            if (detalle.Codigo == 0 && nuevoDetalle <= 0)
            {
                return Fallo("No se pudo guardar el detalle del requerimiento.");
            }
        }

        return new ResultadoOperacion(true, "Requerimiento guardado en estado pendiente.");
    }

    // Los 2 CALL usan SET @newid con variable de sesión; requieren Allow User
    // Variables normalizada en ConsultorMySql, igual que los 6 CALL del extorno.
    // Órdenes verificados contra SHOW CREATE PROCEDURE en dev (2026-10-07):
    // cabecera en el mismo orden que MysqlRequerimientoAlmacen.insert (26 args);
    // detalle (_codReqAlmacen, _codProducto, _codUnidad, _cantidad,
    // _cantidadPedida, _cantidadPendiente, _cantidadConfirmada,
    // _cantidadPendienteAprobada, _codDetalleReqAlmacen, OUT newid).
    private static int InsertarCabecera(IConsultor consultor, DatosGuardadoRequerimiento cabecera)
    {
        object codPropuesta = null;
        if (cabecera.CodPropuestaPedido != 0)
        {
            codPropuesta = cabecera.CodPropuestaPedido;
        }

        object codPedido = null;
        if (!string.IsNullOrEmpty(cabecera.CodPedidoVenta))
        {
            codPedido = cabecera.CodPedidoVenta;
        }

        var parametros = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        parametros["codTipo"] = cabecera.CodTipoDocumento;
        parametros["numDoc"] = cabecera.NumDocumento;
        parametros["codSerie"] = cabecera.CodSerie;
        parametros["numSerie"] = cabecera.NumSerie;
        parametros["almReg"] = cabecera.CodAlmacenRegistro;
        parametros["userReg"] = cabecera.CodUserRegistro;
        parametros["fechaReg"] = cabecera.FechaRegistro;
        parametros["almSol"] = cabecera.CodAlmacenSolicitante;
        parametros["almDes"] = cabecera.CodAlmacenDespacho;
        parametros["fechaReq"] = cabecera.FechaRequerimiento;
        parametros["estado"] = cabecera.Estado;
        parametros["comSol"] = cabecera.ComentarioSolicitante;
        parametros["comDes"] = cabecera.ComentarioDespacho;
        parametros["tipo"] = cabecera.TipoReq;
        parametros["codProp"] = codPropuesta;
        parametros["codPed"] = codPedido;
        parametros["contacto"] = cabecera.NombreContacto;
        parametros["tel"] = cabecera.TelefonoContacto;
        parametros["delivery"] = cabecera.Delivery;
        parametros["dir"] = cabecera.DireccionDelivery;
        parametros["autorizado"] = cabecera.AutorizadoPor;

        Dictionary<string, object> fila = consultor.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaRequerimientoAlmacen(@codTipo, @numDoc, @codSerie, @numSerie, " +
            "@almReg, @userReg, @fechaReg, NULL, NULL, @almSol, @almDes, @fechaReq, " +
            "NULL, NULL, @estado, @comSol, @comDes, @tipo, @codProp, @codPed, " +
            "@contacto, @tel, @delivery, @dir, @autorizado, @newid); " +
            "SELECT @newid AS newid;",
            parametros).First();

        return LeerNewId(fila);
    }

    private static int InsertarDetalle(IConsultor consultor, int codRequerimiento, DatosGuardadoDetalle detalle)
    {
        object codDetalle = null;
        if (detalle.Codigo != 0)
        {
            codDetalle = detalle.Codigo;
        }

        var parametros = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        parametros["codReq"] = codRequerimiento;
        parametros["codProd"] = detalle.CodProducto;
        parametros["codUnd"] = detalle.CodUnidad;
        parametros["cant"] = detalle.Cantidad;
        parametros["cantPed"] = detalle.CantidadPedida;
        parametros["cantPend"] = detalle.CantidadPendiente;
        parametros["cantConf"] = detalle.CantidadConfirmada;
        parametros["cantPendAprob"] = detalle.CantidadPendienteAprobada;
        parametros["codDet"] = codDetalle;

        Dictionary<string, object> fila = consultor.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaDetalleRequerimientoAlmacen(@codReq, @codProd, @codUnd, " +
            "@cant, @cantPed, @cantPend, @cantConf, @cantPendAprob, @codDet, @newid); " +
            "SELECT @newid AS newid;",
            parametros).First();

        return LeerNewId(fila);
    }

    private static int LeerNewId(Dictionary<string, object> fila)
    {
        if (fila == null)
        {
            return 0;
        }

        object valor;
        if (!fila.TryGetValue("newid", out valor) || valor == null)
        {
            return 0;
        }

        try
        {
            return Convert.ToInt32(valor);
        }
        catch
        {
            return 0;
        }
    }

    // Mismo texto que el legacy para no cambiar lo que ve el usuario:
    // documento repetido tiene aviso propio, el resto lleva la causa.
    private static string MensajeError(Exception ex)
    {
        if (ex != null && ex.Message != null && ex.Message.Contains("Duplicate entry"))
        {
            return "Se encontró el siguiente problema: N°- de Documento Repetido";
        }

        string causa = ex == null ? string.Empty : ex.Message;
        return "Se encontró el siguiente problema: " + causa;
    }

    private static ResultadoOperacion Fallo(string mensaje)
    {
        return new ResultadoOperacion(false, mensaje);
    }
}
