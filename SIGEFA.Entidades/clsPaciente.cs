using System;

namespace SIGEFA.Entidades;

public class clsPaciente
{
	public int ID { get; set; }

	public string Nombre { get; set; }

	public string Propietario { get; set; }

	public string Direccion { get; set; }

	public DateTime FechaNacimiento { get; set; }

	public string Especie { get; set; }

	public string Raza { get; set; }

	public string Sexo { get; set; }

	public int UsuarioID { get; set; }

	public DateTime FechaRegistro { get; set; }

	public int Estado { get; set; }
}
