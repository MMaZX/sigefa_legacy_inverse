import { useEffect, useRef } from 'react';
import DataTableLib from 'datatables.net';
import 'datatables.net-dt/css/dataTables.dataTables.css';

export interface DtColumn {
  title: string;
  data?: string;
  render?: (value: any, row: any, type?: string) => string | number;
  className?: string;
  orderable?: boolean;
}

interface DataTableProps {
  columns: DtColumn[];
  data: any[];
  onView?: (id: number) => void;
  className?: string;
}

export default function DataTable({ columns, data, onView, className = '' }: DataTableProps) {
  const tableRef = useRef<HTMLTableElement>(null);
  const dtRef = useRef<any>(null);

  useEffect(() => {
    if (!tableRef.current) return;

    if (dtRef.current) {
      try { dtRef.current.destroy(); } catch {}
      dtRef.current = null;
    }

    const table = tableRef.current;
    // clear
    table.innerHTML = '';

    dtRef.current = new DataTableLib(table, {
      data,
      columns: columns.map((c) => ({
        title: c.title,
        data: c.data ?? null,
        render: c.render ? (d: any, type: any, row: any) => c.render!(d, row, type) : undefined,
        className: c.className,
        orderable: c.orderable ?? true,
      })),
      pageLength: 25,
      lengthMenu: [10, 25, 50, 100],
      order: [],
      scrollY: '500px',
      scrollCollapse: true,
      scrollX: true,
      language: {
        search: 'Buscar:',
        lengthMenu: 'Mostrar _MENU_ filas',
        info: '_START_–_END_ de _TOTAL_',
        infoEmpty: 'Sin registros',
        infoFiltered: '(filtrado de _MAX_)',
        zeroRecords: 'No existen registros.',
        paginate: { first: '«', last: '»', next: '›', previous: '‹' },
      },
      autoWidth: false,
    });

    const handler = (e: Event) => {
      const target = (e.target as HTMLElement).closest('.dt-view');
      if (target && onView) {
        const id = Number(target.getAttribute('data-id'));
        if (!Number.isNaN(id)) onView(id);
      }
    };
    table.addEventListener('click', handler);

    return () => {
      table.removeEventListener('click', handler);
      if (dtRef.current) {
        try { dtRef.current.destroy(); } catch {}
        dtRef.current = null;
      }
    };
  }, [data, columns, onView]);

  return (
    <div className={`dt-wrapper ${className}`}>
      <table ref={tableRef} className="display w-full" style={{ width: '100%' }} />
    </div>
  );
}
