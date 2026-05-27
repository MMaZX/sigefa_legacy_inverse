using System;

namespace SIGEFA.Entidades;

public class clsHistoria
{
	public int ID { get; set; }

	public string Numero { get; set; }

	public int PacienteID { get; set; }

	public int UsuarioID { get; set; }

	public DateTime FechaRegistro { get; set; }
}
