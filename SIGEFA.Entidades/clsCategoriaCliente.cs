using System;

namespace SIGEFA.Entidades;

internal class clsCategoriaCliente
{
	public int CodCategoriaCliente { get; set; }

	public string Descripcion { get; set; }

	public bool Estado { get; set; }

	public int CodUser { get; set; }

	public DateTime dtFechaRegistro { get; set; }
}
