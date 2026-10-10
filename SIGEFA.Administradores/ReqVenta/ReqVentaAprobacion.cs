using System;
using System.Collections.Generic;
using SIGEFA.Conexion;

namespace SIGEFA.Administradores.ReqVenta;

// Datos que el formulario lee en el hilo de la interfaz para aprobar un requerimiento de venta (T5b-2).
// Es un DTO puro: todo lo demás (cabecera, detalle, serie del almacén de despacho, último precio de
// compra, transacción 15 y tipo de documento "TD") lo lee el servicio por IConsultor.
public sealed class DatosAprobacionRequerimiento
{
    public DatosAprobacionRequerimiento(
        int codRequerimiento, int codUser, int codAutorizador,
        string comentarioDespacho, string codPedido, double igv, DateTime fechaIngreso)
    {
        CodRequerimiento = codRequerimiento;
        CodUser = codUser;
        CodAutorizador = codAutorizador;
        ComentarioDespacho = comentarioDespacho;
        CodPedido = codPedido;
        Igv = igv;
        FechaIngreso = fechaIngreso;
    }

    public int CodRequerimiento { get; }

    // Usuario que aprueba (frmLogin.iCodUser).
    public int CodUser { get; }

    // Usuario autorizador o despachador elegido en la pantalla.
    public int CodAutorizador { get; }

    // Texto que el usuario escribió en el comentario de despacho; se agrega al que ya tenga el requerimiento.
    public string ComentarioDespacho { get; }

    // Código del pedido de venta, solo para el comentario de la transferencia.
    public string CodPedido { get; }

    // Porcentaje de IGV de la configuración (18 para 18 %).
    public double Igv { get; }

    // Fecha de ingreso de las líneas de la nota de ingreso (dtpFecha del formulario).
    // Nota: GuardaDetalleIngreso guarda NOW() y no persiste este valor en base, como en el legacy.
    public DateTime FechaIngreso { get; }

    // Opcional: cantidad a despachar por código de línea (colCtdadRequerimiento de la grilla) cuando el
    // usuario la editó y todavía no está guardada. Sin valor para una línea se usa la que tiene la base.
    public IDictionary<int, decimal> CantidadesADespachar { get; set; }
}

// Aprobación de un requerimiento de venta (TipoReq == 2) en segundo plano (T5b-2).
// Replica btnAprobar_Click rama TipoReq==2 de frmReqAlmacen con los mismos procedimientos y el mismo orden,
// pero sin controles, sin MessageBox y sin campos de instancia compartidos. Nunca lanza por errores de
// negocio: devuelve un ResultadoOperacion y deja el paso fallido en Error.
// Sin transacción global (R3): los mismos tramos que el legacy (actualizar el requerimiento, nota de salida
// y nota de ingreso, cada uno en su transacción) y el resto en autocommit.
// Decisión del usuario (2026-10-10): si la nota de salida falla, se corta y se informa; no se registra el
// ingreso ni se aprueba la transferencia.
public static class ReqVentaAprobacion
{
    private const int TipoDocumentoTransferencia = 14;
    private const int TransaccionTransferencia = 15;
    private const string SiglaTransferenciaDirecta = "TD";
    private const double IgvFijoDelIngreso = 0.18;
    private const double FactorFijoDelIngreso = 1.18;

    // Señal interna para forzar el rollback de un tramo: Db.Transaccion confirma cualquier acción que
    // termine sin lanzar, y el tramo devuelve el fallo como texto.
    private sealed class TramoRevertidoException : Exception
    {
        public TramoRevertidoException(string mensaje)
            : base(mensaje)
        {
        }
    }

    // Línea del requerimiento con lo que se calcula para la transferencia. Las fórmulas son las de
    // obtenerDetalleParaTransferencia del legacy, en double como allá.
    private sealed class LineaRequerimiento
    {
        public int CodDetalle;
        public int CodProducto;
        public int CodUnidad;
        public decimal Cantidad;
        public decimal CantidadADespachar;

        public double CantidadDouble;
        public double Precio;
        public double Subtotal;
        public double ValorVenta;
        public double Igv;
        public double PrecioReal;
        public double ValoReal;

        public bool Viaja
        {
            get { return CantidadADespachar > 0m; }
        }

        public void Calcular(decimal ultimoPrecio, double factorIgv)
        {
            CantidadDouble = Convert.ToDouble(CantidadADespachar);
            Precio = Convert.ToDouble(ultimoPrecio);
            Subtotal = Precio * CantidadDouble;
            ValorVenta = Subtotal / factorIgv;
            PrecioReal = Subtotal / CantidadDouble;
            ValoReal = ValorVenta / CantidadDouble;
            Igv = Subtotal - ValorVenta;
        }
    }

    // Todo el estado de una aprobación vive aquí, local a la llamada: nada se comparte entre llamadas.
    private sealed class Contexto
    {
        public DatosAprobacionRequerimiento Datos;
        public int AlmacenSolicitante;
        public int AlmacenDespacho;
        public int CodSerie;
        public string Serie;
        public string NumeroDocumento;
        public int CodTransaccion;
        public int CodTipoDocumento;
        public readonly List<LineaRequerimiento> Lineas = new List<LineaRequerimiento>();
        public decimal MontoBruto;
        public decimal MontoDscto;
        public decimal Igv;
        public decimal Total;
        public int CodTransferencia;
    }

    // Corre cada paso informando el progreso; el primer fallo corta la cadena y deja los demás pasos
    // sin informar (el diálogo los muestra en Pendiente).
    private sealed class Corredor
    {
        private readonly IProgress<PasoOperacion> _progreso;
        private string _ultimoPasoConfirmadoClave;
        private string _ultimoPasoConfirmadoTexto;

        public Corredor(IProgress<PasoOperacion> progreso)
        {
            _progreso = progreso;
        }

        public string Mensaje { get; private set; }

        public bool Ejecutar(string clave, Func<string> accion)
        {
            Informar(clave, EstadoPaso.EnCurso, null);
            string error;
            try
            {
                error = accion();
            }
            catch (Exception ex)
            {
                error = MensajeError(ex);
            }

            if (error != null)
            {
                if (_ultimoPasoConfirmadoClave != null &&
                    _ultimoPasoConfirmadoClave != ReqVentaTextos.ComprobarDatosAprobacion)
                {
                    error = error + " (Quedó confirmado hasta el paso: " + _ultimoPasoConfirmadoTexto + ").";
                }

                Mensaje = error;
                Informar(clave, EstadoPaso.Error, error);
                return false;
            }

            _ultimoPasoConfirmadoClave = clave;
            _ultimoPasoConfirmadoTexto = ObtenerTexto(clave);
            Informar(clave, EstadoPaso.Listo, null);
            return true;
        }

        private static string ObtenerTexto(string clave)
        {
            foreach (PasoOperacion paso in ReqVentaTextos.PasosAprobacion())
            {
                if (paso.Clave == clave)
                {
                    return paso.Texto;
                }
            }

            return clave;
        }

        private void Informar(string clave, EstadoPaso estado, string detalle)
        {
            if (_progreso == null)
            {
                return;
            }

            _progreso.Report(new PasoOperacion(clave, ObtenerTexto(clave), estado, detalle));
        }
    }

    // Consultor sobre la base configurada, en autocommit: una conexión por llamada.
    private sealed class ConsultorDb : IConsultor
    {
        public Consulta Consultar(string sql, object parametros = null)
        {
            return Db.Consultar(sql, parametros);
        }

        public ResultadoEjecucion Ejecutar(string sql, object parametros = null)
        {
            return Db.Ejecutar(sql, parametros);
        }
    }

    // Aprueba contra la base configurada. Corre dentro del Task.Run del diálogo.
    public static ResultadoOperacion Aprobar(DatosAprobacionRequerimiento datos, IProgress<PasoOperacion> progreso)
    {
        return AprobarEn(new ConsultorDb(), Db.Transaccion<ResultadoOperacion>, datos, progreso, ReqVentaRegistroErrores.Registrar);
    }

    // Núcleo con el consultor de autocommit y el ejecutor de transacción de los tramos inyectados (pruebas).
    // El ejecutor debe confirmar si la acción termina y revertir si lanza, igual que Db.Transaccion.
    public static ResultadoOperacion AprobarEn(
        IConsultor consultor,
        Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion,
        DatosAprobacionRequerimiento datos,
        IProgress<PasoOperacion> progreso,
        Action<string> registrar = null)
    {
        var corredor = new Corredor(progreso);
        var contexto = new Contexto { Datos = datos };

        bool ok =
            corredor.Ejecutar(ReqVentaTextos.ComprobarDatosAprobacion, () => PasoComprobarDatos(consultor, transaccion, contexto))
            && corredor.Ejecutar(ReqVentaTextos.AprobarRequerimiento, () => PasoAprobarRequerimiento(consultor, transaccion, contexto))
            && corredor.Ejecutar(ReqVentaTextos.SepararStock, () => PasoSepararStock(consultor, contexto))
            && corredor.Ejecutar(ReqVentaTextos.CrearTransferencia, () => PasoCrearTransferencia(consultor, contexto))
            && corredor.Ejecutar(ReqVentaTextos.RegistrarSalidaDespacho, () => PasoRegistrarSalida(consultor, transaccion, contexto))
            && corredor.Ejecutar(ReqVentaTextos.RegistrarIngresoSolicitante, () => PasoRegistrarIngreso(consultor, transaccion, contexto))
            && corredor.Ejecutar(ReqVentaTextos.AprobarTransferencia, () => PasoAprobarTransferencia(consultor, contexto))
            && corredor.Ejecutar(ReqVentaTextos.ActualizarRequerimiento, () => PasoActualizarRequerimiento(consultor, contexto));

        if (ok)
        {
            return new ResultadoOperacion(
                true,
                "Requerimiento aprobado. Se generó la transferencia " + contexto.Serie + "-" + contexto.NumeroDocumento + ".");
        }

        Registrar(registrar, datos, corredor.Mensaje);
        return new ResultadoOperacion(false, corredor.Mensaje);
    }

    // Paso 1. Todo lo que se lee antes de escribir: requerimiento, líneas, serie 14 del almacén de despacho,
    // último precio de compra, transacción 15 y tipo de documento "TD". Si algo falta no se escribe nada.
    private static string PasoComprobarDatos(
        IConsultor consultor,
        Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion,
        Contexto contexto)
    {
        string error = ValidarEntradas(consultor, transaccion, contexto.Datos);
        if (error != null)
        {
            return error;
        }

        int codReq = contexto.Datos.CodRequerimiento;
        Dictionary<string, object> requerimiento = CargarRequerimiento(consultor, codReq);
        error = ValidarRequerimiento(requerimiento, codReq);
        if (error != null)
        {
            return error;
        }

        contexto.AlmacenSolicitante = requerimiento.Valor<int>("cod_almacen_solicitante");
        contexto.AlmacenDespacho = requerimiento.Valor<int>("cod_almacen_despacho");

        error = LeerLineas(consultor, contexto);
        if (error != null)
        {
            return error;
        }

        error = LeerSerie(consultor, contexto);
        if (error != null)
        {
            return error;
        }

        LeerPreciosYMontos(consultor, contexto);
        return LeerTransaccionYTipoDocumento(consultor, contexto);
    }

    private static string ValidarEntradas(
        IConsultor consultor,
        Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion,
        DatosAprobacionRequerimiento datos)
    {
        if (consultor == null)
        {
            return "El consultor es obligatorio.";
        }

        if (transaccion == null)
        {
            return "El ejecutor de la transacción es obligatorio.";
        }

        if (datos == null)
        {
            return "No hay datos del requerimiento para aprobar.";
        }

        if (datos.CodRequerimiento <= 0)
        {
            return "El requerimiento debe ser mayor que cero.";
        }

        if (datos.CodUser <= 0)
        {
            return "Debe indicar el usuario que aprueba el requerimiento.";
        }

        if (datos.CodAutorizador <= 0)
        {
            return "Debe definir un usuario autorizador o despachador.";
        }

        return null;
    }

    private static string ValidarRequerimiento(Dictionary<string, object> requerimiento, int codReq)
    {
        if (requerimiento == null)
        {
            return "El requerimiento " + codReq + " no existe.";
        }

        if (requerimiento.Valor<int>("tipo_req") != ReqVentaReglas.TipoReqVenta)
        {
            return "Solo se pueden aprobar desde aquí los requerimientos de venta.";
        }

        if (requerimiento.Valor<int>("estado") != ReqVentaReglas.Pendiente)
        {
            return "El requerimiento " + codReq + " ya no está pendiente, no se puede aprobar de nuevo.";
        }

        return null;
    }

    // Líneas con la cantidad a despachar de la grilla (ctdadRequerimiento) y, si el formulario la envió,
    // la que el usuario editó en pantalla.
    private static string LeerLineas(IConsultor consultor, Contexto contexto)
    {
        int codReq = contexto.Datos.CodRequerimiento;
        List<Dictionary<string, object>> filas = consultor.Consultar(
            "CALL ListadoDetalleRequerimientoAlmacen(@codReq)",
            Parametros("codReq", codReq)).Get();
        if (filas.Count == 0)
        {
            return "El requerimiento " + codReq + " no tiene productos.";
        }

        foreach (Dictionary<string, object> fila in filas)
        {
            var linea = new LineaRequerimiento
            {
                CodDetalle = fila.Valor<int>("codDetalle"),
                CodProducto = fila.Valor<int>("codProducto"),
                CodUnidad = fila.Valor<int>("codUnidad"),
                Cantidad = fila.Valor<decimal>("cantidad"),
                CantidadADespachar = fila.Valor<decimal>("ctdadRequerimiento"),
            };
            decimal editada;
            IDictionary<int, decimal> editadas = contexto.Datos.CantidadesADespachar;
            if (editadas != null && editadas.TryGetValue(linea.CodDetalle, out editada))
            {
                linea.CantidadADespachar = editada;
            }

            if (linea.CantidadADespachar < 0m)
            {
                return "La cantidad a despachar del producto " + linea.CodProducto + " no puede ser negativa.";
            }

            contexto.Lineas.Add(linea);
        }

        if (!contexto.Lineas.Exists(l => l.Viaja))
        {
            return "No hay productos con cantidad a despachar mayor que cero.";
        }

        return null;
    }

    private static string LeerSerie(IConsultor consultor, Contexto contexto)
    {
        IList<Dictionary<string, object>> series = consultor.Consultar(
            "CALL BuscaSeriexDocumento(@doc, @alm)",
            Parametros("doc", TipoDocumentoTransferencia, "alm", contexto.AlmacenDespacho)).Get();
        if (series == null || series.Count == 0)
        {
            return "No existe serie creada para transferencia en el almacén de despacho.";
        }

        // El legacy (MysqlSerie.BuscaSeriexDocumento) recorre las filas con while (dr.Read())
        // y retorna la última; tomamos la última para coincidir fielmente.
        Dictionary<string, object> serie = series[series.Count - 1];
        contexto.CodSerie = serie.Valor<int>("codSerie");
        contexto.Serie = serie.Valor<string>("serie");
        contexto.NumeroDocumento = serie.Valor<int>("numeracion").ToString().PadLeft(6, '0');
        return null;
    }

    // Último precio de compra por línea que viaja y montos de la transferencia, como
    // obtenerDetalleParaTransferencia: sumas en decimal, fórmulas por línea en double.
    private static void LeerPreciosYMontos(IConsultor consultor, Contexto contexto)
    {
        double factorIgv = contexto.Datos.Igv / 100.0 + 1.0;
        foreach (LineaRequerimiento linea in contexto.Lineas)
        {
            if (!linea.Viaja)
            {
                continue;
            }

            Dictionary<string, object> precio = consultor.Consultar(
                "CALL MuestraUltimoPrecioCompraPorProductoyUnidad(@prod, @und, 0)",
                Parametros("prod", linea.CodProducto, "und", linea.CodUnidad)).First();
            decimal ultimoPrecio = precio == null ? 0m : precio.Valor<decimal>("ultimo_precio_compra");
            linea.Calcular(ultimoPrecio, factorIgv);

            contexto.MontoBruto += Convert.ToDecimal(linea.Subtotal);
            contexto.Igv += Convert.ToDecimal(linea.Igv);
            contexto.Total += Convert.ToDecimal(linea.Subtotal);
        }
    }

    private static string LeerTransaccionYTipoDocumento(IConsultor consultor, Contexto contexto)
    {
        Dictionary<string, object> tran = consultor.Consultar(
            "CALL MuestraTransaccion(@codTran)",
            Parametros("codTran", TransaccionTransferencia)).First();
        if (tran == null)
        {
            return "No existe la transacción de transferencia directa.";
        }

        Dictionary<string, object> tipo = consultor.Consultar(
            "CALL BuscaTipoDocumento(@sigla)",
            Parametros("sigla", SiglaTransferenciaDirecta)).First();
        if (tipo == null)
        {
            return "No existe el tipo de documento de transferencia directa.";
        }

        contexto.CodTransaccion = tran.Valor<int>("codTransaccion");
        contexto.CodTipoDocumento = tipo.Valor<int>("codTipoDocumento");
        return null;
    }

    // Paso 2. aprobar + asignar autorizador + recargar + el tramo transaccional del update legacy
    // (cabecera, borrar detalle y volver a guardar todas las líneas).
    private static string PasoAprobarRequerimiento(
        IConsultor consultor,
        Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion,
        Contexto contexto)
    {
        DatosAprobacionRequerimiento datos = contexto.Datos;
        consultor.Ejecutar(
            "CALL AprobarRequerimientoAlmacen(@codReq, @codUser)",
            Parametros("codReq", datos.CodRequerimiento, "codUser", datos.CodUser));
        consultor.Ejecutar(
            "CALL SetAutorizadorEnRequerimientoAlmacen(@codReq, @codAut)",
            Parametros("codReq", datos.CodRequerimiento, "codAut", datos.CodAutorizador));

        Dictionary<string, object> requerimiento = CargarRequerimiento(consultor, datos.CodRequerimiento);
        if (requerimiento == null)
        {
            return "No se pudo volver a leer el requerimiento " + datos.CodRequerimiento + " después de aprobarlo.";
        }

        string comentario = ComentarioDespacho(requerimiento.Valor<string>("comentario_despacho"), datos.ComentarioDespacho);
        return EnTramo(transaccion, tx => ActualizarRequerimiento(tx, contexto, requerimiento, comentario));
    }

    private static string ActualizarRequerimiento(
        IConsultor tx, Contexto contexto, Dictionary<string, object> requerimiento, string comentarioDespacho)
    {
        int codReq = contexto.Datos.CodRequerimiento;
        int userModifico = requerimiento.Valor<int>("cod_user_modifico");
        int userAnulo = requerimiento.Valor<int>("cod_user_anulo");

        // ActualizaRequerimientoAlmacen: _codUsuarioAnulacion va ANTES de _fechaAnulacion (el DAL llama
        // por nombre y no le afecta; un CALL posicional sí). Firma verificada en dev el 2026-10-10.
        tx.Ejecutar(
            "CALL ActualizaRequerimientoAlmacen(@codReq, @codTipoDoc, @numDoc, @codSerie, @numSerie, @almReg, " +
            "@userReg, @fechaReg, @userMod, @fechaMod, @almSol, @almDes, @fechaReq, @userAnu, @fechaAnu, " +
            "@estado, @comSol, @comDes, @tipoReq, @delivery, @dir, @autorizado)",
            Parametros(
                "codReq", codReq,
                "codTipoDoc", requerimiento.Valor<int>("cod_tipo_documento"),
                "numDoc", requerimiento.Valor<string>("num_documento"),
                "codSerie", requerimiento.Valor<int>("cod_serie"),
                "numSerie", requerimiento.Valor<string>("num_serie"),
                "almReg", requerimiento.Valor<int>("cod_almacen_registro"),
                "userReg", requerimiento.Valor<int>("cod_user_registro"),
                "fechaReg", requerimiento.Valor<DateTime>("fecha_registro"),
                "userMod", userModifico > 0 ? (object)userModifico : null,
                "fechaMod", userModifico > 0 ? (object)requerimiento.Valor<DateTime>("fecha_modifico") : null,
                "almSol", requerimiento.Valor<int>("cod_almacen_solicitante"),
                "almDes", requerimiento.Valor<int>("cod_almacen_despacho"),
                "fechaReq", requerimiento.Valor<DateTime>("fecha_requerimiento"),
                "userAnu", userAnulo > 0 ? (object)userAnulo : null,
                "fechaAnu", userAnulo > 0 ? (object)requerimiento.Valor<DateTime>("fecha_anulo") : null,
                "estado", requerimiento.Valor<int>("estado"),
                "comSol", requerimiento.Valor<string>("comentario_solicitante") ?? string.Empty,
                "comDes", comentarioDespacho,
                "tipoReq", requerimiento.Valor<int>("tipo_req"),
                "delivery", requerimiento.Valor<int>("Delivery"),
                "dir", requerimiento.Valor<string>("DireccionDelivery") ?? string.Empty,
                "autorizado", requerimiento.Valor<string>("AutorizadoPor") ?? string.Empty));
        tx.Ejecutar(
            "CALL EliminaDetalleRequerimientoAlmacen(@codReq)",
            Parametros("codReq", codReq));

        foreach (LineaRequerimiento linea in contexto.Lineas)
        {
            GuardarDetalleRequerimiento(tx, codReq, linea);
        }

        return null;
    }

    // GuardaDetalleRequerimientoAlmacen: _codDetalleReqAlmacen es el 9º argumento (el DAL lo manda primero).
    // Como en convertirRGVaListado con vieneDeAprobar: pendiente, pendiente aprobada y confirmada valen la
    // cantidad a despachar; cantidad y pedida valen la cantidad del requerimiento. El código de la línea ya
    // existe, así que el newid de salida no se usa.
    private static void GuardarDetalleRequerimiento(IConsultor tx, int codReq, LineaRequerimiento linea)
    {
        tx.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaDetalleRequerimientoAlmacen(@codReq, @codProd, @codUnd, @cant, @cantPed, @cantPend, " +
            "@cantConf, @cantPendAprob, @codDet, @newid); " +
            "SELECT @newid AS newid;",
            Parametros(
                "codReq", codReq,
                "codProd", linea.CodProducto,
                "codUnd", linea.CodUnidad,
                "cant", linea.Cantidad,
                "cantPed", linea.Cantidad,
                "cantPend", linea.CantidadADespachar,
                "cantConf", linea.CantidadADespachar,
                "cantPendAprob", linea.CantidadADespachar,
                "codDet", linea.CodDetalle)).First();
    }

    // Paso 3. Separa el stock de todas las líneas, también las de cantidad cero, como el legacy.
    private static string PasoSepararStock(IConsultor consultor, Contexto contexto)
    {
        foreach (LineaRequerimiento linea in contexto.Lineas)
        {
            consultor.Ejecutar(
                "CALL SeparandoStockAlAprobarReqAlmacen(@alm, @prod, @und, @cant, @codDet)",
                Parametros(
                    "alm", contexto.AlmacenDespacho,
                    "prod", linea.CodProducto,
                    "und", linea.CodUnidad,
                    "cant", linea.CantidadADespachar,
                    "codDet", linea.CodDetalle));
        }

        return null;
    }

    // Paso 4. Cabecera de la transferencia, vínculo con el requerimiento y una línea por producto que viaja.
    private static string PasoCrearTransferencia(IConsultor consultor, Contexto contexto)
    {
        DatosAprobacionRequerimiento datos = contexto.Datos;
        DateTime ahora = DateTime.Now;
        string comentario = "Req Almacen para Ventas generado de O.V: " + datos.CodPedido;
        if (comentario.Length > 110)
        {
            comentario = comentario.Substring(0, 110);
        }

        // GuardaTransferencia: codlista va después de fechapago; el tipo de documento es 14.
        Dictionary<string, object> fila = consultor.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaTransferencia(@almOrig, 14, @almDest, 1, 0, @fechaEnvio, @fechaEntrega, '', @comentario, " +
            "@bruto, @montoDscto, @igv, @total, 1, 0, @fechaPago, 0, @codUser, @codSerie, @serie, @numDoc, " +
            "@codReq, NULL, @newid); " +
            "SELECT @newid AS newid;",
            Parametros(
                "almOrig", contexto.AlmacenDespacho,
                "almDest", contexto.AlmacenSolicitante,
                "fechaEnvio", ahora,
                "fechaEntrega", ahora,
                "comentario", comentario,
                "bruto", contexto.MontoBruto,
                "montoDscto", contexto.MontoDscto,
                "igv", contexto.Igv,
                "total", contexto.Total,
                "fechaPago", ahora.Date,
                "codUser", datos.CodUser,
                "codSerie", contexto.CodSerie,
                "serie", contexto.Serie,
                "numDoc", contexto.NumeroDocumento,
                "codReq", datos.CodRequerimiento)).First();
        contexto.CodTransferencia = LeerNewId(fila);
        if (contexto.CodTransferencia <= 0)
        {
            return "No se pudo crear la transferencia entre almacenes.";
        }

        consultor.Ejecutar(
            "CALL RegistrarTransferenciaRequerimientoAlmacen(@codReq, @codTrans, @codUser)",
            Parametros("codReq", datos.CodRequerimiento, "codTrans", contexto.CodTransferencia, "codUser", datos.CodUser));

        foreach (LineaRequerimiento linea in contexto.Lineas)
        {
            string error = GuardarDetalleTransferencia(consultor, contexto, linea);
            if (error != null)
            {
                return error;
            }
        }

        return null;
    }

    // GuardaDetalleTransferencia: unidad va antes de codalmadest y _coddetallereqalm al final.
    private static string GuardarDetalleTransferencia(IConsultor consultor, Contexto contexto, LineaRequerimiento linea)
    {
        if (!linea.Viaja)
        {
            return null;
        }

        Dictionary<string, object> fila = consultor.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaDetalleTransferencia(@codProd, @codTrans, @almOrig, @und, @almDest, '', @cant, @precio, " +
            "@subtotal, 0, 0, 0, 0, @igv, @importe, @precioReal, @valoReal, @codUser, @cantPend, 0, 0, " +
            "@promedio, @codDet, @newid); " +
            "SELECT @newid AS newid;",
            Parametros(
                "codProd", linea.CodProducto,
                "codTrans", contexto.CodTransferencia,
                "almOrig", contexto.AlmacenDespacho,
                "und", linea.CodUnidad,
                "almDest", contexto.AlmacenSolicitante,
                "cant", linea.CantidadDouble,
                "precio", linea.Precio,
                "subtotal", linea.Subtotal,
                "igv", linea.Igv,
                "importe", linea.Subtotal,
                "precioReal", linea.PrecioReal,
                "valoReal", linea.ValoReal,
                "codUser", contexto.Datos.CodUser,
                "cantPend", linea.CantidadDouble,
                "promedio", Convert.ToDecimal(linea.Precio),
                "codDet", linea.CodDetalle)).First();
        if (LeerNewId(fila) <= 0)
        {
            return "No se pudo guardar el producto " + linea.CodProducto + " en la transferencia.";
        }

        return null;
    }

    // Paso 5. Nota de salida del almacén de despacho con su detalle, en su propia transacción como el
    // Scope legacy. Un newid en cero en un detalle es "No hay stock suficiente": se revierte y se corta.
    private static string PasoRegistrarSalida(
        IConsultor consultor,
        Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion,
        Contexto contexto)
    {
        return EnTramo(transaccion, tx => RegistrarSalida(tx, contexto));
    }

    private static string RegistrarSalida(IConsultor tx, Contexto contexto)
    {
        DateTime hoy = DateTime.Now.Date;

        // GuardaNotaSalida: documentorefe antes de aplicad, sin fechacancelado, _area y _responsable al final.
        Dictionary<string, object> fila = tx.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaNotaSalida(@almOrig, @almOrig, @codTransaccion, @codTipoDoc, @codSerie, @serie, @numDoc, 0, NULL, " +
            "1, 0, @fecha, '', @bruto, 0, 0, @total, @total, 1, NULL, @fechaPago, 0, @codUser, NULL, NULL, NULL, " +
            "0, 0, NULL, NULL, NULL, @codTrans, NULL, 0, @newid); " +
            "SELECT @newid AS newid;",
            Parametros(
                "almOrig", contexto.AlmacenDespacho,
                "codTransaccion", contexto.CodTransaccion,
                "codTipoDoc", contexto.CodTipoDocumento,
                "codSerie", contexto.CodSerie,
                "serie", contexto.Serie,
                "numDoc", contexto.NumeroDocumento,
                "fecha", hoy,
                "bruto", Convert.ToDouble(contexto.MontoBruto),
                "total", Convert.ToDouble(contexto.Total),
                "fechaPago", hoy,
                "codUser", contexto.Datos.CodUser,
                "codTrans", contexto.CodTransferencia)).First();
        int codNota = LeerNewId(fila);
        if (codNota <= 0)
        {
            throw new TramoRevertidoException("No se pudo registrar la nota de salida de la transferencia.");
        }

        foreach (LineaRequerimiento linea in contexto.Lineas)
        {
            if (linea.Viaja)
            {
                GuardarDetalleSalida(tx, contexto, codNota, linea);
            }
        }

        return null;
    }

    private static void GuardarDetalleSalida(IConsultor tx, Contexto contexto, int codNota, LineaRequerimiento linea)
    {
        Dictionary<string, object> fila = tx.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaDetalleSalida(@codProd, @codNota, @almOrig, 0, 0, 0, @und, '0', @cant, @precio, @subtotal, " +
            "0, 0, 0, 0, @igv, @importe, @precioReal, @valoReal, @codUser, 0, 0, @newid); " +
            "SELECT @newid AS newid;",
            Parametros(
                "codProd", linea.CodProducto,
                "codNota", codNota,
                "almOrig", contexto.AlmacenDespacho,
                "und", linea.CodUnidad,
                "cant", linea.CantidadDouble,
                "precio", linea.Precio,
                "subtotal", linea.Subtotal,
                "igv", linea.Igv,
                "importe", linea.Subtotal,
                "precioReal", linea.PrecioReal,
                "valoReal", linea.ValoReal,
                "codUser", contexto.Datos.CodUser)).First();
        if (LeerNewId(fila) == 0)
        {
            throw new TramoRevertidoException("No hay stock suficiente del producto código: " + linea.CodProducto);
        }
    }

    // Paso 6. Nota de ingreso del almacén solicitante con su detalle, en su propia transacción.
    private static string PasoRegistrarIngreso(
        IConsultor consultor,
        Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion,
        Contexto contexto)
    {
        return EnTramo(transaccion, tx => RegistrarIngreso(tx, contexto));
    }

    private static string RegistrarIngreso(IConsultor tx, Contexto contexto)
    {
        DateTime hoy = DateTime.Now.Date;

        // GuardaNotaIngreso: sin fechacancelado, codref va después de numdoc y _area antes de _responsable.
        // El monto bruto va en cero como en el legacy: allá se asigna por error a la nota de salida (NS.MontoBruto
        // dentro del bloque de la nota de ingreso) y NI.MontoBruto queda por defecto. Corregirlo cambia montos
        // contables y se decide aparte (decisión del usuario, 2026-10-10).
        Dictionary<string, object> fila = tx.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaNotaIngreso(@almDest, @codTransaccion, @codTipoDoc, @numDoc, 0, NULL, 1, 0, @fecha, '', " +
            "0, 0, 0, 0, @total, @total, 0, 1, NULL, @fechaPago, 0, @codUser, @codSerie, @serie, " +
            "0, 0, 0, NULL, NULL, @codTrans, 0, NULL, 0, @newid); " +
            "SELECT @newid AS newid;",
            Parametros(
                "almDest", contexto.AlmacenSolicitante,
                "codTransaccion", contexto.CodTransaccion,
                "codTipoDoc", contexto.CodTipoDocumento,
                "numDoc", contexto.NumeroDocumento,
                "fecha", hoy,
                "total", Convert.ToDouble(contexto.Total),
                "fechaPago", hoy,
                "codUser", contexto.Datos.CodUser,
                "codSerie", contexto.CodSerie,
                "serie", contexto.Serie,
                "codTrans", contexto.CodTransferencia)).First();
        int codNota = LeerNewId(fila);
        if (codNota <= 0)
        {
            throw new TramoRevertidoException("No se pudo registrar la nota de ingreso de la transferencia.");
        }

        foreach (LineaRequerimiento linea in contexto.Lineas)
        {
            if (linea.Viaja)
            {
                GuardarDetalleIngreso(tx, contexto, codNota, linea);
            }
        }

        return null;
    }

    // GuardaDetalleIngreso: sin _estado (el DAL lo manda y el procedimiento no lo tiene). Montos como
    // añadedetalleNI: IGV fijo de 18 % sobre precio/1.18, importe y subtotal = precio por cantidad,
    // precio real = precio unitario y valor real = el de la línea de la transferencia.
    private static void GuardarDetalleIngreso(IConsultor tx, Contexto contexto, int codNota, LineaRequerimiento linea)
    {
        double importe = linea.Precio * linea.CantidadDouble;
        double igv = (linea.Precio / FactorFijoDelIngreso) * IgvFijoDelIngreso;
        Dictionary<string, object> fila = tx.Consultar(
            "SET @newid = 0; " +
            "CALL GuardaDetalleIngreso(@codProd, @codNota, @almDest, 0, @und, '0', @cant, @precio, @subtotal, " +
            "0, 0, 0, 0, @igv, 0, @importe, @precioReal, @valoReal, @fecha, @codUser, 0, 0, 0, 0, @newid); " +
            "SELECT @newid AS newid;",
            Parametros(
                "codProd", linea.CodProducto,
                "codNota", codNota,
                "almDest", contexto.AlmacenSolicitante,
                "und", linea.CodUnidad,
                "cant", linea.CantidadDouble,
                "precio", linea.Precio,
                "subtotal", importe,
                "igv", igv,
                "importe", importe,
                "precioReal", linea.Precio,
                "valoReal", linea.ValoReal,
                "fecha", contexto.Datos.FechaIngreso,
                "codUser", contexto.Datos.CodUser)).First();
        if (LeerNewId(fila) == 0)
        {
            throw new TramoRevertidoException("No se pudo guardar el producto " + linea.CodProducto + " en la nota de ingreso.");
        }
    }

    // Paso 7.
    private static string PasoAprobarTransferencia(IConsultor consultor, Contexto contexto)
    {
        consultor.Ejecutar(
            "CALL AprobarTransferencia(@codTrans)",
            Parametros("codTrans", contexto.CodTransferencia));
        return null;
    }

    // Paso 8. Estado 13 = aprobado y transferido.
    private static string PasoActualizarRequerimiento(IConsultor consultor, Contexto contexto)
    {
        int codReq = contexto.Datos.CodRequerimiento;
        consultor.Ejecutar(
            "CALL ActualizaCantidadPendienteReqAlmacen(@codReq)",
            Parametros("codReq", codReq));
        consultor.Ejecutar(
            "CALL ActualizaEstadoReqAlmacen(@codReq, @estado)",
            Parametros("codReq", codReq, "estado", ReqVentaReglas.AprobadoTransferido));
        return null;
    }

    private static Dictionary<string, object> CargarRequerimiento(IConsultor consultor, int codReq)
    {
        return consultor.Consultar(
            "CALL CargaRequerimientoAlmacen(@codReq)",
            Parametros("codReq", codReq)).First();
    }

    // Igual que el legacy: si el usuario escribió algo se agrega al comentario que ya tenía el requerimiento.
    private static string ComentarioDespacho(string actual, string escrito)
    {
        string anterior = actual ?? string.Empty;
        string nuevo = escrito ?? string.Empty;
        if (nuevo.Trim() == string.Empty)
        {
            return anterior;
        }

        return (anterior != string.Empty ? anterior + "\n" : string.Empty) + nuevo;
    }

    // Corre un tramo en su propia transacción. Si la acción devuelve un error, lanza para que el ejecutor
    // revierta, y lo devuelve como texto. Las excepciones de la base también revierten y salen al llamador.
    private static string EnTramo(
        Func<Func<IConsultor, ResultadoOperacion>, ResultadoOperacion> transaccion,
        Func<IConsultor, string> accion)
    {
        try
        {
            transaccion(tx => ConfirmarSiNoHayError(accion(tx)));
            return null;
        }
        catch (TramoRevertidoException revertido)
        {
            return revertido.Message;
        }
    }

    private static ResultadoOperacion ConfirmarSiNoHayError(string error)
    {
        if (error != null)
        {
            throw new TramoRevertidoException(error);
        }

        return new ResultadoOperacion(true, string.Empty);
    }

    // Los CALL con salida usan SET @newid con variable de sesión; requieren Allow User Variables
    // normalizada en ConsultorMySql, igual que ReqVentaGuardado y el extorno.
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

    // Arma el diccionario de parámetros a partir de pares nombre, valor. El valor nulo viaja como NULL.
    private static Dictionary<string, object> Parametros(params object[] paresNombreValor)
    {
        var parametros = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        for (int i = 0; i < paresNombreValor.Length; i += 2)
        {
            parametros[(string)paresNombreValor[i]] = paresNombreValor[i + 1];
        }

        return parametros;
    }

    private static string MensajeError(Exception ex)
    {
        string causa = ex == null ? string.Empty : ex.Message;
        return "Se encontró el siguiente problema: " + causa;
    }

    private static void Registrar(Action<string> registrar, DatosAprobacionRequerimiento datos, string mensaje)
    {
        if (registrar == null)
        {
            return;
        }

        int codReq = datos == null ? 0 : datos.CodRequerimiento;
        string usuario = datos == null ? "desconocido" : "código " + datos.CodUser;
        try
        {
            registrar("Aprobación de requerimiento | Req: " + codReq + " | Usuario: " + usuario + " | " + mensaje);
        }
        catch
        {
            // Un registrador defectuoso no debe ocultar el resultado de la aprobación.
        }
    }
}
