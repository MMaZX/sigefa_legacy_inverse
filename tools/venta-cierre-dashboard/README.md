# Venta Cierre — Dashboard de auditoría

Dashboard ligero (React + Vite + Tailwind + shadcn/ui) para auditar las tablas
`venta_cierre_log` y `venta_cierre_log_evento` de producción (solo lectura).

## Requisitos
- Node 18+
- Acceso de red a MySQL de producción (192.168.1.3:3306)

## Setup
```bash
cp .env.example .env   # ya viene con credenciales read-only
npm install
```

## Desarrollo
```bash
npm run dev
```
- Web: http://localhost:5173
- API: http://localhost:3001

## Funcionalidades
- Filtro por rango de fechas, estado, usuario, equipo.
- KPIs: pedidos, OK, errores, tasa de éxito, duración promedio, multi-comprobante.
- Gráficos: pedidos por día (stacked OK/error), duración promedio por día.
- Errores más comunes, eventos por paso, por usuario/equipo.
- Detalle por transacción: timeline de eventos, comprobantes y productos vendidos.
- Indicador multi-comprobante.

## Notas
- Solo lectura: el usuario de DB no tiene permisos de escritura.
- `.env` está en `.gitignore` (no commitear credenciales).
