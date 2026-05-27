using System;

namespace SIGEFA.Entidades;

public class clsComboProductos
{
	public int CodCombo { get; set; }

	public string NombreCombo { get; set; }

	public decimal Total { get; set; }

	public DateTime FechaVencimiento { get; set; }

	public DateTime FechaRegistro { get; set; }

	public int CodUsuario { get; set; }

	public bool Estado { get; set; }

	public int stockcombo { get; set; }

	public int stockcombodisponible { get; set; }
}
