using System;
using System.Collections.Generic;

namespace SIGEFA.Entidades;

public class clsRequerimiento
{
	public int CodRequerimientoNuevo { get; set; }

	public int CodRequerimiento { get; set; }

	public int CodAlmacen { get; set; }

	public string Comentario { get; set; }

	public int Tipo { get; set; }

	public int CodTipoDocumento { get; set; }

	public string SiglaDocumento { get; set; }

	public string DescripcionDocumento { get; set; }

	public int CodSerie { get; set; }

	public string Serie { get; set; }

	public string NumDoc { get; set; }

	public DateTime FechaOrden { get; set; }

	public int CodUser { get; set; }

	public DateTime FechaRegistro { get; set; }

	public int Estado { get; set; }

	public int Anulado { get; set; }

	public int Atendido { get; set; }

	public int codAlmaDestino { get; set; }

	public string comentarioRechazado { get; set; }

	public List<clsDetalleRequerimiento> Detalle { get; set; }
}
