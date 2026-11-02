import { useEffect, useMemo, useState } from 'react';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid } from 'recharts';
import { api } from './lib/api';
import type { CierreLog, LogDetail, Stats } from './types';
import { Card, CardHeader, CardTitle, CardContent, Badge, Button, Input, Select, Tabs } from './components/ui';
import { ChartContainer, ChartTooltip, ChartTooltipContent } from './components/ui/chart';
import type { ChartConfig } from './components/ui/chart';
import DataTable from './components/DataTable';
import type { DtColumn } from './components/DataTable';

function today() {
  return new Date().toISOString().slice(0, 10);
}

function fmtMs(ms: number | null) {
  if (ms == null) return '—';
  if (ms < 1000) return `${ms} ms`;
  return `${(ms / 1000).toFixed(2)} s`;
}

function fmtDate(s: string) {
  const d = new Date(s);
  return d.toLocaleString('es-PE', { dateStyle: 'short', timeStyle: 'medium' });
}

const DIAS = ['domingo', 'lunes', 'martes', 'miércoles', 'jueves', 'viernes', 'sábado'];
const DIAS_CORTO = ['dom', 'lun', 'mar', 'mié', 'jue', 'vie', 'sáb'];
function diaSemana(s: string) {
  const d = new Date(s + 'T00:00:00');
  if (isNaN(d.getTime())) return s;
  return `${DIAS[d.getDay()]} ${d.toLocaleDateString('es-PE', { day: 'numeric', month: 'short' })}`;
}
function diaCorto(s: string) {
  const d = new Date((s || '').slice(0, 10) + 'T00:00:00');
  if (isNaN(d.getTime())) return String(s).slice(5);
  return `${DIAS_CORTO[d.getDay()]} ${d.getDate()}`;
}

export default function App() {
  const [from, setFrom] = useState(today());
  const [to, setTo] = useState(today());
  const [estado, setEstado] = useState('');
  const [usuario, setUsuario] = useState('');
  const [equipo, setEquipo] = useState('');
  const [stats, setStats] = useState<Stats | null>(null);
  const [logs, setLogs] = useState<CierreLog[]>([]);
  const [meta, setMeta] = useState<{ usuarios: string[]; equipos: string[] }>({ usuarios: [], equipos: [] });
  const [dbInfo, setDbInfo] = useState<{ host: string; port: number; database: string; user: string; readonly: boolean } | null>(null);
  const [locales, setLocales] = useState<{ id: number; descripcion: string }[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [selected, setSelected] = useState<LogDetail | null>(null);
  const [modalTab, setModalTab] = useState('timeline');
  const [tablaTab, setTablaTab] = useState('transacciones');
  const [durTab, setDurTab] = useState('tiempo');
  const [txSearch, setTxSearch] = useState('');
  const [txQuick, setTxQuick] = useState<'all' | 'errors' | 'multi' | 'slow'>('all');
  const [detailLoading, setDetailLoading] = useState(false);

  const load = async () => {
    setLoading(true);
    setError(null);
    try {
      const [s, l, m, info, locs] = await Promise.all([
        api.stats(from, to),
        api.logs({ from, to, estado: estado || undefined, usuario: usuario || undefined, equipo: equipo || undefined, limit: 300 }),
        api.meta(from, to),
        api.info(),
        api.locales(),
      ]);
      setStats(s);
      setLogs(l);
      setMeta(m);
      setDbInfo(info);
      setLocales(locs);
    } catch (e: any) {
      setError(e.message || 'Error cargando datos');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { load(); /* eslint-disable-next-line */ }, []);

  const openDetail = async (id: number) => {
    setDetailLoading(true);
    try {
      const d = await api.logDetail(id);
      setSelected(d);
    } catch (e: any) {
      setError(e.message);
    } finally {
      setDetailLoading(false);
    }
  };

  const t = stats?.totals;

  const filteredLogs = useMemo(() => {
    const q = txSearch.trim().toLowerCase();
    return logs.filter((l) => {
      if (txQuick === 'errors' && l.estado !== 'ERROR') return false;
      if (txQuick === 'multi' && !l.multi) return false;
      if (txQuick === 'slow' && !(l.duracionMs && l.duracionMs > 5000)) return false;
      if (!q) return true;
      return (
        String(l.codPedido).includes(q) ||
        (l.usuario || '').toLowerCase().includes(q) ||
        (l.equipo || '').toLowerCase().includes(q) ||
        (l.locales || []).join(' ').toLowerCase().includes(q) ||
        (l.errorPaso || '').toLowerCase().includes(q) ||
        (l.errorMensaje || '').toLowerCase().includes(q)
      );
    });
  }, [logs, txSearch, txQuick]);

  const txColumns: DtColumn[] = [
    { title: 'Inicio', data: 'inicio', render: (v, _row, type) => (type && type !== 'display' ? v : fmtDate(v)) },
    { title: 'Pedido', data: 'codPedido', render: (v, row) => `<button class="dt-view font-medium text-primary hover:underline" data-id="${row.id}">${v}</button>` },
    { title: 'Usuario', data: 'usuario', render: (v) => v || '—' },
    { title: 'Equipo', data: 'equipo', render: (v) => v || '—' },
    { title: 'Local', data: 'locales', render: (v) => (Array.isArray(v) && v.length ? v.join(', ') : '—') },
    { title: 'Estado', data: 'estado' },
    { title: 'Duración', data: 'duracionMs', render: (v, _row, type) => (type && type !== 'display' ? v : fmtMs(v)) },
    { title: 'Multi', data: 'multi', render: (v) => (v ? 'sí' : '—') },
    { title: 'Error', data: 'errorPaso', render: (_v, row) => (row.errorPaso ? `${row.errorPaso}${row.errorMensaje ? ' — ' + row.errorMensaje : ''}` : '') },
  ];

  const userColumns: DtColumn[] = [
    { title: 'Usuario', data: 'usuario' },
    { title: 'Equipo', data: 'equipo' },
    { title: 'Total', data: 'total' },
    { title: 'OK', data: 'ok' },
    { title: 'Err', data: 'errores' },
    { title: 'Dur. prom.', data: 'dur_prom', render: (v, _row, type) => (type && type !== 'display' ? v : fmtMs(v)) },
  ];

  const [groupBy, setGroupBy] = useState<'estado' | 'local'>('estado');

  const localColorPalette = ['hsl(25 95% 53%)', 'hsl(262 83% 58%)', 'hsl(142 71% 45%)', 'hsl(217 91% 60%)', 'hsl(346 77% 50%)', 'hsl(48 96% 53%)'];

  const chartData = useMemo(() => {
    const perDayLocal: Record<string, Record<string, number>> = {};
    const descToId: Record<string, number> = {};
    for (const loc of locales) descToId[loc.descripcion] = loc.id;
    for (const l of logs) {
      const day = String(l.inicio).slice(0, 10);
      if (!perDayLocal[day]) perDayLocal[day] = {};
      for (const desc of l.locales || []) {
        const id = descToId[desc];
        if (id == null) continue;
        const key = `loc_${id}`;
        perDayLocal[day][key] = (perDayLocal[day][key] || 0) + 1;
      }
    }
    return (stats?.byDay || []).map((d) => {
      const day = String(d.dia).slice(0, 10);
      const row: any = { dia: d.dia, ok: d.ok, errores: d.errores, total: d.total, durProm: d.durProm };
      for (const loc of locales) row[`loc_${loc.id}`] = perDayLocal[day]?.[`loc_${loc.id}`] || 0;
      return row;
    });
  }, [stats, logs, locales]);

  const pedidosConfig = {
    ok: { label: 'OK', color: 'hsl(142 71% 45%)' },
    errores: { label: 'Error', color: 'hsl(0 84% 60%)' },
  } satisfies ChartConfig;

  const pedidosLocalConfig = locales.reduce<ChartConfig>((acc, loc, i) => {
    acc[`loc_${loc.id}`] = { label: loc.descripcion, color: localColorPalette[i % localColorPalette.length] };
    return acc;
  }, {});

  const durByDayLocal = useMemo(() => {
    const map: Record<string, Record<string, { n: number; sum: number; max: number }>> = {};
    const descToId: Record<string, number> = {};
    for (const loc of locales) descToId[loc.descripcion] = loc.id;
    for (const l of logs) {
      const day = String(l.inicio).slice(0, 10);
      if (!map[day]) map[day] = {};
      for (const desc of l.locales || []) {
        const id = descToId[desc];
        if (id == null) continue;
        const key = `loc_${id}`;
        if (!map[day][key]) map[day][key] = { n: 0, sum: 0, max: 0 };
        const b = map[day][key];
        b.n += 1;
        b.sum += l.duracionMs || 0;
        if ((l.duracionMs || 0) > b.max) b.max = l.duracionMs || 0;
      }
    }
    return Object.entries(map)
      .map(([day, v]) => {
        const row: any = { dia: day };
        for (const loc of locales) {
          const b = v[`loc_${loc.id}`] || { n: 0, sum: 0, max: 0 };
          row[`loc_${loc.id}_total`] = b.n;
          row[`loc_${loc.id}_durProm`] = b.n ? Math.round((b.sum / b.n / 1000) * 100) / 100 : 0;
          row[`loc_${loc.id}_durMax`] = Math.round((b.max / 1000) * 100) / 100;
        }
        return row;
      })
      .sort((a, b) => a.dia.localeCompare(b.dia));
  }, [logs, locales]);

  const durLocalAllConfig = locales.reduce<ChartConfig>((acc, loc, i) => {
    const color = localColorPalette[i % localColorPalette.length];
    for (const suffix of ['_total', '_durProm', '_durMax']) {
      acc[`loc_${loc.id}${suffix}`] = { label: loc.descripcion, color };
    }
    return acc;
  }, {});

  return (
    <div className="min-h-screen bg-background">
      <header className="border-b bg-card">
        <div className="max-w-7xl mx-auto px-4 py-3 flex items-center justify-between">
          <div>
            <h1 className="text-lg font-semibold">Venta Cierre — Auditoría</h1>
            <p className="text-xs text-muted-foreground">
              {dbInfo ? `${dbInfo.user}@${dbInfo.host}:${dbInfo.port} · ${dbInfo.database}` : 'Cargando conexión…'}
            </p>
          </div>
          <div className="flex items-center gap-2">
            <Badge variant="outline">Solo lectura</Badge>
            {loading && <Badge variant="muted">Cargando…</Badge>}
          </div>
        </div>
      </header>

      <main className="max-w-7xl mx-auto px-4 py-4 space-y-4">
        {/* Filtros */}
        <Card>
          <CardContent className="flex flex-wrap items-end gap-3 pt-4">
            <div className="flex flex-col gap-1">
              <label className="text-xs text-muted-foreground">Desde</label>
              <Input type="date" value={from} onChange={setFrom} />
            </div>
            <div className="flex flex-col gap-1">
              <label className="text-xs text-muted-foreground">Hasta</label>
              <Input type="date" value={to} onChange={setTo} />
            </div>
            <div className="flex flex-col gap-1 min-w-[140px]">
              <label className="text-xs text-muted-foreground">Estado</label>
              <Select value={estado} onChange={setEstado} options={[
                { value: '', label: 'Todos' },
                { value: 'OK', label: 'OK' },
                { value: 'ERROR', label: 'ERROR' },
                { value: 'EN_CURSO', label: 'EN_CURSO' },
              ]} />
            </div>
            <div className="flex flex-col gap-1 min-w-[160px]">
              <label className="text-xs text-muted-foreground">Usuario</label>
              <Select value={usuario} onChange={setUsuario} options={[
                { value: '', label: 'Todos' },
                ...meta.usuarios.map((u) => ({ value: u, label: u })),
              ]} />
            </div>
            <div className="flex flex-col gap-1 min-w-[160px]">
              <label className="text-xs text-muted-foreground">Equipo</label>
              <Select value={equipo} onChange={setEquipo} options={[
                { value: '', label: 'Todos' },
                ...meta.equipos.map((e) => ({ value: e, label: e })),
              ]} />
            </div>
            <Button onClick={load} disabled={loading}>Aplicar</Button>
            <Button variant="outline" onClick={() => { setFrom(today()); setTo(today()); setEstado(''); setUsuario(''); setEquipo(''); }} size="sm">
              Hoy
            </Button>
            <div className="basis-full text-xs text-muted-foreground">
              Desde: <span className="font-medium text-foreground">{diaSemana(from)}</span>
              {from !== to && (
                <> · Hasta: <span className="font-medium text-foreground">{diaSemana(to)}</span></>
              )}
            </div>
          </CardContent>
        </Card>

        {error && (
          <Card className="border-destructive">
            <CardContent className="pt-4 text-sm text-destructive">{error}</CardContent>
          </Card>
        )}

        {/* KPIs */}
        <div className="grid grid-cols-2 md:grid-cols-4 lg:grid-cols-6 gap-3">
          <Kpi label="Pedidos" value={t?.total ?? 0} />
          <Kpi label="OK" value={t?.ok ?? 0} tone="success" />
          <Kpi label="Errores" value={t?.errores ?? 0} tone="error" />
          <Kpi label="Éxito" value={`${t?.tasaExito ?? 0}%`} />
          <Kpi label="Duración prom." value={fmtMs(t?.durProm ?? null)} />
          <Kpi label="Multi-comprobante" value={t?.multi ?? 0} />
        </div>

        {/* Chart + top errors */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-3">
          <Card className="lg:col-span-2">
            <CardHeader className="pb-2 flex flex-row items-center justify-between gap-3 flex-wrap">
              <div>
                <CardTitle>Pedidos por día</CardTitle>
                <p className="text-xs text-muted-foreground">{diaSemana(from)}{from !== to ? ` – ${diaSemana(to)}` : ''}</p>
              </div>
              <Tabs
                value={groupBy}
                onChange={(v) => setGroupBy(v as 'estado' | 'local')}
                tabs={[
                  { value: 'estado', label: 'Por estado' },
                  { value: 'local', label: 'Por local' },
                ]}
              />
            </CardHeader>
            <CardContent className="h-64">
              <ChartContainer
                config={groupBy === 'estado' ? pedidosConfig : pedidosLocalConfig}
                className="h-full w-full"
              >
                <BarChart data={chartData} accessibilityLayer>
                  <CartesianGrid vertical={false} />
                  <XAxis dataKey="dia" tickLine={false} axisLine={false} tickMargin={8} tickFormatter={diaCorto} />
                  <YAxis allowDecimals={false} tickLine={false} axisLine={false} />
                  <ChartTooltip content={<ChartTooltipContent />} />
                  {groupBy === 'estado' ? (
                    <>
                      <Bar dataKey="ok" stackId="a" fill="var(--color-ok)" radius={4} />
                      <Bar dataKey="errores" stackId="a" fill="var(--color-errores)" radius={4} />
                    </>
                  ) : (
                    locales.map((loc) => (
                      <Bar key={loc.id} dataKey={`loc_${loc.id}`} stackId="a" fill={`var(--color-loc_${loc.id})`} radius={4} />
                    ))
                  )}
                </BarChart>
              </ChartContainer>
            </CardContent>
          </Card>
          <Card>
            <CardHeader><CardTitle>Errores más comunes</CardTitle></CardHeader>
            <CardContent className="space-y-2">
              {(stats?.topErrors || []).length === 0 && <p className="text-sm text-muted-foreground">Sin errores en el rango.</p>}
              {(stats?.topErrors || []).map((e, i) => (
                <div key={i} className="text-sm border-b pb-3 last:border-0 space-y-1">
                  <div className="flex items-start justify-between gap-2">
                    <div className="min-w-0">
                      <div className="font-medium break-words">{e.paso || '—'} · {e.procedimiento || ''}</div>
                      <div className="text-xs text-muted-foreground break-words whitespace-pre-wrap">{e.mensaje}</div>
                    </div>
                    <Badge variant="error">{e.cantidad}</Badge>
                  </div>
                  {e.usuarios && e.usuarios.length > 0 && (
                    <div className="text-xs text-muted-foreground pl-1 space-y-0.5">
                      {e.usuarios.map((u, j) => (
                        <div key={j} className="flex items-center gap-1">
                          <span className="font-medium text-foreground">{u.usuario || '—'}</span>
                          <span>· {u.equipo || '—'}</span>
                          <Badge variant="muted" className="ml-1">{u.cantidad}</Badge>
                        </div>
                      ))}
                    </div>
                  )}
                </div>
              ))}
            </CardContent>
          </Card>
        </div>

        {/* Duración por día + por paso */}
        <div className="grid grid-cols-1 lg:grid-cols-2 gap-3">
          <Card>
            <CardHeader className="flex flex-row items-center justify-between gap-3 flex-wrap">
              <CardTitle>
                {durTab === 'tiempo' ? 'Tiempo promedio por día (s)'
                  : durTab === 'pedidos' ? 'Pedidos por día'
                  : 'Duración máxima por día (s)'}
              </CardTitle>
              <Tabs
                value={durTab}
                onChange={setDurTab}
                tabs={[
                  { value: 'tiempo', label: 'Tiempo prom.' },
                  { value: 'pedidos', label: 'N° pedidos' },
                  { value: 'max', label: 'Dur. máx' },
                ]}
              />
            </CardHeader>
            <CardContent className="h-56">
              <ChartContainer config={durLocalAllConfig} className="h-full w-full">
                <BarChart data={durByDayLocal} accessibilityLayer>
                  <CartesianGrid vertical={false} />
                  <XAxis dataKey="dia" tickLine={false} axisLine={false} tickMargin={8} tickFormatter={diaCorto} />
                  <YAxis tickLine={false} axisLine={false} />
                  <ChartTooltip content={<ChartTooltipContent />} />
                  {durTab === 'tiempo' && locales.map((loc) => (
                    <Bar key={loc.id} dataKey={`loc_${loc.id}_durProm`} fill={`var(--color-loc_${loc.id}_durProm)`} radius={4} />
                  ))}
                  {durTab === 'pedidos' && locales.map((loc) => (
                    <Bar key={loc.id} dataKey={`loc_${loc.id}_total`} fill={`var(--color-loc_${loc.id}_total)`} radius={4} />
                  ))}
                  {durTab === 'max' && locales.map((loc) => (
                    <Bar key={loc.id} dataKey={`loc_${loc.id}_durMax`} fill={`var(--color-loc_${loc.id}_durMax)`} radius={4} />
                  ))}
                </BarChart>
              </ChartContainer>
            </CardContent>
          </Card>
          <Card>
            <CardHeader><CardTitle>Eventos por paso</CardTitle></CardHeader>
            <CardContent>
              <table className="w-full text-sm">
                <thead>
                  <tr className="text-left text-muted-foreground border-b">
                    <th className="py-1 font-medium">Paso</th>
                    <th className="py-1 font-medium text-right">Eventos</th>
                  </tr>
                </thead>
                <tbody>
                  {(stats?.byPaso || []).map((p) => (
                    <tr key={p.paso} className="border-b last:border-0">
                      <td className="py-1">{p.paso}</td>
                      <td className="py-1 text-right tabular-nums">{p.eventos}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </CardContent>
          </Card>
        </div>

        {/* Por local */}
        <Card>
          <CardHeader><CardTitle>Por local (pedidos y comprobantes)</CardTitle></CardHeader>
          <CardContent>
            {(stats?.byLocal || []).length === 0 ? (
              <p className="text-sm text-muted-foreground">Sin datos.</p>
            ) : (
              <div className="overflow-x-auto">
                <table className="w-full text-sm">
                  <thead>
                    <tr className="text-left text-muted-foreground border-b">
                      <th className="py-1 font-medium">Local</th>
                      <th className="py-1 font-medium text-right">Pedidos</th>
                      <th className="py-1 font-medium text-right">Comprobantes</th>
                      <th className="py-1 font-medium text-right">Ítems</th>
                    </tr>
                  </thead>
                  <tbody>
                    {(stats?.byLocal || []).map((l) => (
                      <tr key={l.local} className="border-b last:border-0">
                        <td className="py-1 font-medium">{l.local}</td>
                        <td className="py-1 text-right tabular-nums">{l.pedidos}</td>
                        <td className="py-1 text-right tabular-nums">{l.comprobantes}</td>
                        <td className="py-1 text-right tabular-nums">{l.items}</td>
                      </tr>
                    ))}
                  </tbody>
                </table>
              </div>
            )}
          </CardContent>
        </Card>

        {/* Tablas con tabs */}
        <Card>
          <CardHeader className="flex flex-row items-center justify-between gap-3 flex-wrap">
            <CardTitle>{tablaTab === 'transacciones' ? `Transacciones (${filteredLogs.length}${filteredLogs.length !== logs.length ? ` de ${logs.length}` : ''})` : 'Por usuario / equipo'}</CardTitle>
            <Tabs
              value={tablaTab}
              onChange={setTablaTab}
              tabs={[
                { value: 'transacciones', label: 'Transacciones' },
                { value: 'usuarios', label: 'Por usuario' },
              ]}
            />
          </CardHeader>
          <CardContent className="p-0 space-y-3">
            <div className="flex flex-wrap items-center gap-2 px-3 pt-3">
              <Input value={txSearch} onChange={setTxSearch} placeholder="Buscar pedido, usuario, equipo o error…" className="max-w-xs" />
              <div className="flex gap-1">
                {([
                  { v: 'all', l: 'Todos' },
                  { v: 'errors', l: 'Solo errores' },
                  { v: 'multi', l: 'Multi-comprobante' },
                  { v: 'slow', l: '> 5s' },
                ] as const).map((b) => (
                  <Button
                    key={b.v}
                    size="sm"
                    variant={txQuick === b.v ? 'default' : 'outline'}
                    onClick={() => setTxQuick(b.v)}
                  >
                    {b.l}
                  </Button>
                ))}
              </div>
            </div>
            {tablaTab === 'transacciones' ? (
              filteredLogs.length === 0 ? (
                <div className="h-[300px] flex items-center justify-center text-sm text-muted-foreground border rounded-md mx-3 mb-3">
                  No existen registros.
                </div>
              ) : (
                <div className="px-3 pb-3">
                  <DataTable
                    columns={txColumns}
                    data={filteredLogs}
                    onView={openDetail}
                  />
                </div>
              )
            ) : (
              (stats?.byUser || []).length === 0 ? (
                <div className="h-[300px] flex items-center justify-center text-sm text-muted-foreground border rounded-md mx-3 mb-3">
                  No existen registros.
                </div>
              ) : (
                <div className="px-3 pb-3">
                  <DataTable columns={userColumns} data={stats?.byUser || []} />
                </div>
              )
            )}
          </CardContent>
        </Card>
      </main>

      {/* Modal detalle */}
      {selected && (
        <div className="fixed inset-0 z-50 bg-black/50 flex items-center justify-center p-4" onClick={() => setSelected(null)}>
          <div className="bg-background rounded-lg shadow-lg max-w-4xl w-full max-h-[90vh] overflow-hidden flex flex-col" onClick={(e) => e.stopPropagation()}>
            <div className="bg-background border-b px-4 py-3 flex items-center justify-between shrink-0">
              <div>
                <h2 className="font-semibold">Pedido {selected.log.codPedido}</h2>
                <p className="text-xs text-muted-foreground">{fmtDate(selected.log.inicio)} · {selected.log.usuario} · {selected.log.equipo}</p>
              </div>
              <Button variant="outline" size="sm" onClick={() => setSelected(null)}>Cerrar</Button>
            </div>
            <div className="p-4 space-y-4 overflow-auto flex-1 min-h-0">
              <div className="grid grid-cols-2 md:grid-cols-5 gap-3">
                <Kpi label="Estado" value={selected.log.estado} tone={selected.log.estado === 'OK' ? 'success' : selected.log.estado === 'ERROR' ? 'error' : 'muted'} />
                <Kpi label="Duración" value={fmtMs(selected.log.duracionMs)} />
                <Kpi label="Bloques" value={`${selected.log.bloquesOk ?? 0}/${selected.log.totalBloques ?? 0}`} />
                <Kpi label="Multi" value={selected.log.multi ? 'Sí' : 'No'} />
                <Kpi label="Ítems" value={selected.facturas.reduce((a, f) => a + f.items.length, 0)} />
              </div>

              {selected.log.estado === 'ERROR' && (
                <Card className="border-destructive">
                  <CardContent className="pt-4 text-sm">
                    <div className="font-medium text-destructive">{selected.log.errorPaso} · {selected.log.errorProcedimiento}</div>
                    <div className="text-muted-foreground mt-1">{selected.log.errorMensaje}</div>
                    <div className="text-xs text-muted-foreground mt-1">MySQL #{selected.log.errorMysqlNum} · {selected.log.errorSqlstate}</div>
                  </CardContent>
                </Card>
              )}

              <Tabs
                value={modalTab}
                onChange={setModalTab}
                tabs={[
                  { value: 'timeline', label: 'Timeline' },
                  { value: 'comprobantes', label: 'Comprobantes' },
                ]}
              />

              {modalTab === 'timeline' ? (
                <div className="border rounded-md overflow-hidden">
                  <table className="w-full text-xs">
                    <thead className="sticky top-0 z-10 bg-muted/95 backdrop-blur">
                      <tr className="text-left text-muted-foreground">
                        <th className="px-2 py-1.5">#</th>
                        <th className="px-2 py-1.5">Hora</th>
                        <th className="px-2 py-1.5">Bloque</th>
                        <th className="px-2 py-1.5">Almacén</th>
                        <th className="px-2 py-1.5">Paso</th>
                        <th className="px-2 py-1.5">Detalle</th>
                      </tr>
                    </thead>
                    <tbody>
                      {selected.eventos.map((e) => (
                        <tr key={e.id} className="border-t">
                          <td className="px-2 py-1.5 tabular-nums">{e.orden}</td>
                          <td className="px-2 py-1.5 whitespace-nowrap">{new Date(e.hora).toLocaleTimeString('es-PE')}</td>
                          <td className="px-2 py-1.5">{e.bloque ?? '—'}</td>
                          <td className="px-2 py-1.5">{e.almacen || '—'}</td>
                          <td className="px-2 py-1.5 font-medium">{e.paso}</td>
                          <td className="px-2 py-1.5 text-muted-foreground">{e.detalle || '—'}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              ) : (
                selected.facturas.length > 0 ? (
                  <div className="space-y-3">
                    {selected.facturas.map((f) => (
                      <Card key={f.codFacturaV}>
                        <CardHeader className="py-2">
                          <div className="flex items-center justify-between">
                            <CardTitle>
                              {f.serie}-{f.numDocumento} · {f.local || '—'} · Almacén {f.codAlmacen}
                            </CardTitle>
                            <div className="flex items-center gap-3">
                              <Badge variant="muted">{f.items.length} ítems</Badge>
                              <span className="text-sm font-semibold">S/ {f.total.toFixed(2)}</span>
                            </div>
                          </div>
                        </CardHeader>
                        <CardContent className="p-0">
                          <table className="w-full text-xs">
                            <thead className="sticky top-0 z-10 bg-muted/95 backdrop-blur">
                              <tr className="text-left text-muted-foreground">
                                <th className="px-3 py-1.5 font-medium">Producto</th>
                                <th className="px-3 py-1.5 font-medium text-right">Cant.</th>
                                <th className="px-3 py-1.5 font-medium text-right">P. Unit</th>
                                <th className="px-3 py-1.5 font-medium text-right">Importe</th>
                              </tr>
                            </thead>
                            <tbody>
                              {f.items.map((it, i) => (
                                <tr key={i} className="border-t">
                                  <td className="px-3 py-1.5">{it.descripcion || `#${it.codProducto}`}</td>
                                  <td className="px-3 py-1.5 text-right tabular-nums">{it.cantidad}</td>
                                  <td className="px-3 py-1.5 text-right tabular-nums">{it.precioUnitario.toFixed(2)}</td>
                                  <td className="px-3 py-1.5 text-right tabular-nums">{it.importe.toFixed(2)}</td>
                                </tr>
                              ))}
                              {f.items.length === 0 && (
                                <tr><td colSpan={4} className="px-3 py-2 text-center text-muted-foreground">Sin detalle.</td></tr>
                              )}
                            </tbody>
                          </table>
                        </CardContent>
                      </Card>
                    ))}
                  </div>
                ) : (
                  <div className="h-[200px] flex items-center justify-center text-sm text-muted-foreground border rounded-md">
                    No existen comprobantes.
                  </div>
                )
              )}
            </div>
          </div>
        </div>
      )}

      {detailLoading && (
        <div className="fixed inset-0 z-50 bg-black/30 flex items-center justify-center">
          <div className="bg-background rounded-md px-4 py-2 text-sm">Cargando detalle…</div>
        </div>
      )}
    </div>
  );
}

function Kpi({ label, value, tone }: { label: string; value: string | number; tone?: 'success' | 'error' | 'muted' }) {
  const color = tone === 'success' ? 'text-emerald-700' : tone === 'error' ? 'text-red-600' : 'text-foreground';
  return (
    <Card>
      <CardContent className="pt-4">
        <div className="text-xs text-muted-foreground">{label}</div>
        <div className={`text-xl font-semibold tabular-nums ${color}`}>{value}</div>
      </CardContent>
    </Card>
  );
}
