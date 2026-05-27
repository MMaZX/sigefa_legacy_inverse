using System.Data;
using SIGEFA.Entidades;

namespace SIGEFA.Interfaces;

internal interface IUsuario
{
	bool Insert(clsUsuario UsuarioNuevo);

	bool Update(clsUsuario UsuarioNuevo);

	bool Delete(int Codigo);

	bool Login(clsUsuario Usuario);

	clsUsuario CargaUsuario(int Codigo);

	clsUsuario CargaUsuarioSinAdmin(int Codigo);

	clsUsuario CargaUsuarioNivel();

	DataTable ListaUsuarios();

	DataTable BuscaUsuarios(int Criterio, string Filtro);

	DataTable ListaCorreosUsuarios();

	DataTable correoTesoreria();

	DataTable ListaNiveles();

	DataTable ListaUsuarios_Empresa(int Codigo);

	DataTable CargaUsuarios();

	DataTable ListaUsuariosxNivel(int codnivel);

	clsUsuario cargaAutorizador(string usuario, string clave);

	int verificaUsuario(string usuario, string clave);

	clsUsuario cargaUsuario(string usuario, string clave);

	DataTable ListaUsuariosDespacho(int codAlmacen, int codSucursal);
}
