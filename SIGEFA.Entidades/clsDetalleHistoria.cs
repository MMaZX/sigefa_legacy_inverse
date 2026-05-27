using System;

namespace SIGEFA.Entidades;

public class clsDetalleHistoria
{
	public int ID { get; set; }

	public int HistoriaID { get; set; }

	public DateTime FechaHora { get; set; }

	public decimal Temperatura { get; set; }

	public decimal Peso { get; set; }

	public string Notas { get; set; }

	public string Tratamientos { get; set; }

	public bool Fallecimiento { get; set; }
}
