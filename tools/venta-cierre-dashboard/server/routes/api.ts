import { Router } from 'express';
import { pool } from '../db.js';

const router = Router();

router.get('/locales', async (_req, res) => {
  try {
    const [rows] = await pool.query<any[]>('SELECT id, descripcion FROM local ORDER BY id');
    res.json(rows.map((r: any) => ({ id: Number(r.id), descripcion: r.descripcion })));
  } catch (e: any) {
    res.status(500).json({ error: e.message });
  }
});

router.get('/info', (_req, res) => {
  res.json({
    host: process.env.DB_HOST,
    port: Number(process.env.DB_PORT || 3306),
    database: process.env.DB_NAME,
    user: process.env.DB_USER,
    readonly: true,
  });
});

function parseDateRange(q: any): { from: string; to: string } {
  const today = new Date().toISOString().slice(0, 10);
  const from = (q.from as string) || today;
  const to = (q.to as string) || today;
  return { from, to };
}

// ---- STATS ----
router.get('/stats', async (req, res) => {
  try {
    const { from, to } = parseDateRange(req.query);
    const [byDay] = await pool.query<any[]>(
      `SELECT DATE(inicio) AS dia,
              COUNT(*) AS total,
              SUM(estado='OK') AS ok,
              SUM(estado='ERROR') AS errores,
              AVG(duracion_ms) AS dur_prom,
              MAX(duracion_ms) AS dur_max,
              MIN(duracion_ms) AS dur_min,
              SUM(total_bloques > 1) AS multi
       FROM venta_cierre_log
       WHERE DATE(inicio) BETWEEN ? AND ?
       GROUP BY DATE(inicio)
       ORDER BY dia`,
      [from, to]
    );

    const [totals] = await pool.query<any[]>(
      `SELECT COUNT(*) AS total,
              SUM(estado='OK') AS ok,
              SUM(estado='ERROR') AS errores,
              AVG(duracion_ms) AS dur_prom,
              MAX(duracion_ms) AS dur_max,
              MIN(duracion_ms) AS dur_min,
              SUM(total_bloques > 1) AS multi,
              SUM(intento > 1) AS reintentos
       FROM venta_cierre_log
       WHERE DATE(inicio) BETWEEN ? AND ?`,
      [from, to]
    );

    const [topErrors] = await pool.query<any[]>(
      `SELECT error_paso AS paso, error_procedimiento AS procedimiento,
              error_mysql_num AS mysql_num, error_sqlstate AS sql_state,
              error_mensaje AS mensaje, COUNT(*) AS cantidad
       FROM venta_cierre_log
       WHERE estado='ERROR' AND DATE(inicio) BETWEEN ? AND ?
       GROUP BY error_paso, error_procedimiento, error_mysql_num, error_sqlstate, error_mensaje
       ORDER BY cantidad DESC
       LIMIT 5`,
      [from, to]
    );

    const [errorUsers] = await pool.query<any[]>(
      `SELECT error_paso AS paso, error_procedimiento AS procedimiento,
              error_mysql_num AS mysql_num, error_sqlstate AS sql_state,
              error_mensaje AS mensaje, usuario, equipo, COUNT(*) AS cantidad
       FROM venta_cierre_log
       WHERE estado='ERROR' AND DATE(inicio) BETWEEN ? AND ?
       GROUP BY error_paso, error_procedimiento, error_mysql_num, error_sqlstate, error_mensaje, usuario, equipo
       ORDER BY cantidad DESC`,
      [from, to]
    );

    const [byUser] = await pool.query<any[]>(
      `SELECT usuario, equipo, COUNT(*) AS total,
              SUM(estado='OK') AS ok, SUM(estado='ERROR') AS errores,
              AVG(duracion_ms) AS dur_prom
       FROM venta_cierre_log
       WHERE DATE(inicio) BETWEEN ? AND ?
       GROUP BY usuario, equipo
       ORDER BY total DESC
       LIMIT 20`,
      [from, to]
    );

    const [byPaso] = await pool.query<any[]>(
      `SELECT paso, COUNT(*) AS eventos,
              SUM(resultado='OK') AS ok, SUM(resultado='ERROR') AS errores
       FROM venta_cierre_log_evento e
       JOIN venta_cierre_log l ON l.id = e.log_id
       WHERE DATE(l.inicio) BETWEEN ? AND ?
       GROUP BY paso
       ORDER BY eventos DESC`,
      [from, to]
    );

    const [byLocal] = await pool.query<any[]>(
      `SELECT l.descripcion AS local,
              COUNT(DISTINCT fv.codPedido) AS pedidos,
              COUNT(DISTINCT fv.codFacturaV) AS comprobantes
       FROM factura_venta fv
       JOIN almacen a ON a.codAlmacen = fv.codAlmacen
       JOIN local l ON l.id = a.local_id
       WHERE fv.codPedido IN (SELECT cod_pedido FROM venta_cierre_log WHERE DATE(inicio) BETWEEN ? AND ?)
       GROUP BY l.id, l.descripcion
       ORDER BY comprobantes DESC`,
      [from, to]
    );

    const [byLocalItems] = await pool.query<any[]>(
      `SELECT l.descripcion AS local, COUNT(*) AS items
       FROM detallefactura_venta df
       JOIN factura_venta fv ON fv.codFacturaV = df.codFacturaV
       JOIN almacen a ON a.codAlmacen = fv.codAlmacen
       JOIN local l ON l.id = a.local_id
       WHERE fv.codPedido IN (SELECT cod_pedido FROM venta_cierre_log WHERE DATE(inicio) BETWEEN ? AND ?)
       GROUP BY l.id, l.descripcion`,
      [from, to]
    );

    const t = totals[0] || {};
    res.json({
      range: { from, to },
      totals: {
        total: Number(t.total || 0),
        ok: Number(t.ok || 0),
        errores: Number(t.errores || 0),
        tasaExito: t.total ? Math.round((Number(t.ok || 0) / Number(t.total)) * 1000) / 10 : 0,
        durProm: Math.round(Number(t.dur_prom || 0)),
        durMax: Number(t.dur_max || 0),
        durMin: Number(t.dur_min || 0),
        multi: Number(t.multi || 0),
        reintentos: Number(t.reintentos || 0),
      },
      byDay: byDay.map((r: any) => ({
        dia: r.dia instanceof Date ? r.dia.toISOString().slice(0, 10) : String(r.dia).slice(0, 10),
        total: Number(r.total),
        ok: Number(r.ok || 0),
        errores: Number(r.errores || 0),
        durProm: Math.round(Number(r.dur_prom || 0)),
        durMax: Number(r.dur_max || 0),
        multi: Number(r.multi || 0),
      })),
      topErrors: topErrors.map((r: any) => ({
        paso: r.paso,
        procedimiento: r.procedimiento,
        mysqlNum: r.mysql_num,
        sqlstate: r.sql_state,
        mensaje: r.mensaje,
        cantidad: Number(r.cantidad),
        usuarios: errorUsers
          .filter((e: any) => e.paso === r.paso && e.procedimiento === r.procedimiento && e.mysql_num === r.mysql_num && e.mensaje === r.mensaje)
          .map((e: any) => ({ usuario: e.usuario, equipo: e.equipo, cantidad: Number(e.cantidad) })),
      })),
      byUser: byUser.map((r: any) => ({
        usuario: r.usuario,
        equipo: r.equipo,
        total: Number(r.total),
        ok: Number(r.ok || 0),
        errores: Number(r.errores || 0),
        dur_prom: Math.round(Number(r.dur_prom || 0)),
      })),
      byPaso: byPaso.map((r: any) => ({
        paso: r.paso,
        eventos: Number(r.eventos),
        ok: Number(r.ok || 0),
        errores: Number(r.errores || 0),
      })),
      byLocal: byLocal.map((r: any) => {
        const it = byLocalItems.find((x: any) => x.local === r.local);
        return {
          local: r.local,
          pedidos: Number(r.pedidos),
          comprobantes: Number(r.comprobantes),
          items: Number(it?.items || 0),
        };
      }),
    });
  } catch (e: any) {
    res.status(500).json({ error: e.message });
  }
});

// ---- LOGS LIST ----
router.get('/logs', async (req, res) => {
  try {
    const { from, to } = parseDateRange(req.query);
    const estado = (req.query.estado as string) || '';
    const usuario = (req.query.usuario as string) || '';
    const equipo = (req.query.equipo as string) || '';
    const limit = Math.min(Number(req.query.limit || 200), 500);

    const where: string[] = ['DATE(inicio) BETWEEN ? AND ?'];
    const params: any[] = [from, to];
    if (estado) { where.push('estado = ?'); params.push(estado); }
    if (usuario) { where.push('usuario = ?'); params.push(usuario); }
    if (equipo) { where.push('equipo = ?'); params.push(equipo); }

    const [rows] = await pool.query<any[]>(
      `SELECT id, cod_pedido, intento, cod_usuario, usuario, equipo, version_app,
              estado, total_bloques, bloques_ok, inicio, fin, duracion_ms,
              error_paso, error_procedimiento, error_mysql_num, error_sqlstate,
              error_mensaje, cod_factura_venta
       FROM venta_cierre_log
       WHERE ${where.join(' AND ')}
       ORDER BY inicio DESC
       LIMIT ?`,
      [...params, limit]
    );

    const ids = rows.map((r: any) => r.id);
    let localesMap: Record<number, string[]> = {};
    if (ids.length) {
      const placeholders = ids.map(() => '?').join(',');
      const [locRows] = await pool.query<any[]>(
        `SELECT vcl.id, GROUP_CONCAT(DISTINCT l.descripcion) AS locales
         FROM venta_cierre_log vcl
         LEFT JOIN factura_venta fv ON fv.codPedido = vcl.cod_pedido
         LEFT JOIN almacen a ON a.codAlmacen = fv.codAlmacen
         LEFT JOIN local l ON l.id = a.local_id
         WHERE vcl.id IN (${placeholders})
         GROUP BY vcl.id`,
        ids
      );
      for (const lr of locRows) {
        localesMap[lr.id] = lr.locales ? String(lr.locales).split(',') : [];
      }
    }

    res.json(rows.map((r: any) => ({
      id: Number(r.id),
      codPedido: r.cod_pedido,
      intento: r.intento,
      codUsuario: r.cod_usuario,
      usuario: r.usuario,
      equipo: r.equipo,
      versionApp: r.version_app,
      estado: r.estado,
      totalBloques: r.total_bloques,
      bloquesOk: r.bloques_ok,
      inicio: r.inicio,
      fin: r.fin,
      duracionMs: r.duracion_ms,
      errorPaso: r.error_paso,
      errorProcedimiento: r.error_procedimiento,
      errorMysqlNum: r.error_mysql_num,
      errorSqlstate: r.error_sqlstate,
      errorMensaje: r.error_mensaje,
      codFacturaVenta: r.cod_factura_venta,
      multi: (r.total_bloques || 0) > 1,
      locales: localesMap[r.id] || [],
    })));
  } catch (e: any) {
    res.status(500).json({ error: e.message });
  }
});

// ---- LOG DETAIL ----
router.get('/logs/:id', async (req, res) => {
  try {
    const id = Number(req.params.id);
    const [logRows] = await pool.query<any[]>('SELECT * FROM venta_cierre_log WHERE id = ?', [id]);
    if (!logRows.length) return res.status(404).json({ error: 'not found' });
    const log = logRows[0];

    const [eventos] = await pool.query<any[]>(
      `SELECT id, orden, hora, bloque, almacen, paso, item, cod_producto, resultado, duracion_ms, detalle
       FROM venta_cierre_log_evento WHERE log_id = ? ORDER BY orden`,
      [id]
    );

    // facturas por cod_pedido
    const [facturas] = await pool.query<any[]>(
      `SELECT fv.codFacturaV, fv.serie, fv.numDocumento, fv.codAlmacen, fv.total, fv.fecharegistro, fv.anulado+0 AS anulado,
              l.descripcion AS local
       FROM factura_venta fv
       LEFT JOIN almacen a ON a.codAlmacen = fv.codAlmacen
       LEFT JOIN local l ON l.id = a.local_id
       WHERE fv.codPedido = ?`,
      [log.cod_pedido]
    );

    // detalles + producto
    const facturaIds = facturas.map((f: any) => f.codFacturaV);
    let detalles: any[] = [];
    if (facturaIds.length) {
      const placeholders = facturaIds.map(() => '?').join(',');
      const [d] = await pool.query<any[]>(
        `SELECT df.codFacturaV, df.codProducto, df.cantidad, df.preciounitario, df.importe, df.anulado+0 AS anulado,
                p.descripcion, p.referencia
         FROM detallefactura_venta df
         LEFT JOIN producto p ON p.codProducto = df.codProducto
         WHERE df.codFacturaV IN (${placeholders})`,
        facturaIds
      );
      detalles = d;
    }

    const facturasConDetalle = facturas.map((f: any) => ({
      codFacturaV: f.codFacturaV,
      serie: f.serie,
      numDocumento: f.numDocumento,
      codAlmacen: f.codAlmacen,
      total: Number(f.total),
      fecha: f.fecharegistro,
      anulado: !!f.anulado,
      local: f.local,
      items: detalles
        .filter((d: any) => d.codFacturaV === f.codFacturaV)
        .map((d: any) => ({
          codProducto: d.codProducto,
          descripcion: d.descripcion,
          referencia: d.referencia,
          cantidad: Number(d.cantidad),
          precioUnitario: Number(d.preciounitario),
          importe: Number(d.importe),
          anulado: !!d.anulado,
        })),
    }));

    res.json({
      log: {
        id: Number(log.id),
        codPedido: log.cod_pedido,
        intento: log.intento,
        codUsuario: log.cod_usuario,
        usuario: log.usuario,
        equipo: log.equipo,
        versionApp: log.version_app,
        estado: log.estado,
        totalBloques: log.total_bloques,
        bloquesOk: log.bloques_ok,
        inicio: log.inicio,
        fin: log.fin,
        duracionMs: log.duracion_ms,
        errorPaso: log.error_paso,
        errorProcedimiento: log.error_procedimiento,
        errorMysqlNum: log.error_mysql_num,
        errorSqlstate: log.error_sqlstate,
        errorMensaje: log.error_mensaje,
        codFacturaVenta: log.cod_factura_venta,
        multi: (log.total_bloques || 0) > 1,
      },
      eventos: eventos.map((e: any) => ({
        id: Number(e.id),
        orden: e.orden,
        hora: e.hora,
        bloque: e.bloque,
        almacen: e.almacen,
        paso: e.paso,
        item: e.item,
        codProducto: e.cod_producto,
        resultado: e.resultado,
        duracionMs: e.duracion_ms,
        detalle: e.detalle,
      })),
      facturas: facturasConDetalle,
    });
  } catch (e: any) {
    res.status(500).json({ error: e.message });
  }
});

// ---- META (usuarios/equipos distinct) ----
router.get('/meta', async (req, res) => {
  try {
    const { from, to } = parseDateRange(req.query);
    const [users] = await pool.query<any[]>(
      `SELECT DISTINCT usuario FROM venta_cierre_log WHERE DATE(inicio) BETWEEN ? AND ? ORDER BY usuario`,
      [from, to]
    );
    const [equipos] = await pool.query<any[]>(
      `SELECT DISTINCT equipo FROM venta_cierre_log WHERE DATE(inicio) BETWEEN ? AND ? ORDER BY equipo`,
      [from, to]
    );
    res.json({
      usuarios: users.map((r: any) => r.usuario).filter(Boolean),
      equipos: equipos.map((r: any) => r.equipo).filter(Boolean),
    });
  } catch (e: any) {
    res.status(500).json({ error: e.message });
  }
});

export default router;
