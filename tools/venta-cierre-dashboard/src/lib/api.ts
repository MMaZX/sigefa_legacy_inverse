const BASE = (import.meta as any).env?.VITE_API_BASE || '';

async function get<T>(path: string): Promise<T> {
  const r = await fetch(`${BASE}${path}`);
  if (!r.ok) throw new Error(`${r.status} ${r.statusText}`);
  return r.json();
}

export const api = {
  health: () => get<{ ok: boolean }>('/api/health'),
  info: () => get<{ host: string; port: number; database: string; user: string; readonly: boolean }>('/api/info'),
  stats: (from: string, to: string) => get<import('../types').Stats>(`/api/stats?from=${from}&to=${to}`),
  logs: (params: { from: string; to: string; estado?: string; usuario?: string; equipo?: string; limit?: number }) => {
    const q = new URLSearchParams({ from: params.from, to: params.to });
    if (params.estado) q.set('estado', params.estado);
    if (params.usuario) q.set('usuario', params.usuario);
    if (params.equipo) q.set('equipo', params.equipo);
    if (params.limit) q.set('limit', String(params.limit));
    return get<import('../types').CierreLog[]>(`/api/logs?${q}`);
  },
  logDetail: (id: number) => get<import('../types').LogDetail>(`/api/logs/${id}`),
  meta: (from: string, to: string) => get<{ usuarios: string[]; equipos: string[] }>(`/api/meta?from=${from}&to=${to}`),
  locales: () => get<{ id: number; descripcion: string }[]>('/api/locales'),
};
