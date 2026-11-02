export interface CierreLog {
  id: number;
  codPedido: number;
  intento: number;
  codUsuario: number | null;
  usuario: string | null;
  equipo: string | null;
  versionApp: string | null;
  estado: 'EN_CURSO' | 'OK' | 'ERROR';
  totalBloques: number | null;
  bloquesOk: number | null;
  inicio: string;
  fin: string | null;
  duracionMs: number | null;
  errorPaso: string | null;
  errorProcedimiento: string | null;
  errorMysqlNum: number | null;
  errorSqlstate: string | null;
  errorMensaje: string | null;
  codFacturaVenta: number | null;
  multi: boolean;
  locales: string[];
}

export interface CierreEvento {
  id: number;
  orden: number;
  hora: string;
  bloque: number | null;
  almacen: string | null;
  paso: string;
  item: number | null;
  codProducto: number | null;
  resultado: 'OK' | 'ERROR' | 'INFO';
  duracionMs: number | null;
  detalle: string | null;
}

export interface FacturaItem {
  codProducto: number;
  descripcion: string | null;
  referencia: string | null;
  cantidad: number;
  precioUnitario: number;
  importe: number;
  anulado: boolean;
}

export interface Factura {
  codFacturaV: number;
  serie: string | null;
  numDocumento: string | null;
  codAlmacen: number;
  total: number;
  fecha: string;
  anulado: boolean;
  local: string | null;
  items: FacturaItem[];
}

export interface LogDetail {
  log: CierreLog;
  eventos: CierreEvento[];
  facturas: Factura[];
}

export interface Stats {
  range: { from: string; to: string };
  totals: {
    total: number;
    ok: number;
    errores: number;
    tasaExito: number;
    durProm: number;
    durMax: number;
    durMin: number;
    multi: number;
    reintentos: number;
  };
  byDay: { dia: string; total: number; ok: number; errores: number; durProm: number; durMax: number; multi: number }[];
  topErrors: { paso: string | null; procedimiento: string | null; mysqlNum: number | null; sqlstate: string | null; mensaje: string | null; cantidad: number; usuarios: { usuario: string | null; equipo: string | null; cantidad: number }[] }[];
  byUser: { usuario: string; equipo: string; total: number; ok: number; errores: number; dur_prom: number }[];
  byPaso: { paso: string; eventos: number; ok: number; errores: number }[];
  byLocal: { local: string; pedidos: number; comprobantes: number; items: number }[];
}
