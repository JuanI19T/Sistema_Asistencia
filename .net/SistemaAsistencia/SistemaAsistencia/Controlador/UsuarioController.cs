using System.Collections.Generic;
using SistemaAsistencia.Modelo.DAO;
using SistemaAsistencia.Modelo.Entidades;
using SistemaAsistencia.Utilidades;

namespace SistemaAsistencia.Controlador
{
    /// <summary>
    /// Controlador encargado de gestionar las operaciones relacionadas
    /// con los usuarios del sistema.
    /// </summary>
    public class UsuarioController
    {
        private readonly UsuarioDAO usuarioDAO;


        /// <summary>
        /// Constructor del controlador.
        /// Inicializa la conexión con la capa de datos.
        /// </summary>
        public UsuarioController()
        {
            usuarioDAO = new UsuarioDAO();
        }


        /// <summary>
        /// Valida las credenciales ingresadas por el usuario.
        /// </summary>
        /// <param name="nombreUsuario">Nombre de usuario.</param>
        /// <param name="contrasena">Contraseña.</param>
        /// <returns>
        /// Usuario autenticado o null si los datos son incorrectos.
        /// </returns>
        public Usuario IniciarSesion(string nombreUsuario, string contrasena)
        {
            return Ejecutor.Ejecutar("Usuario.IniciarSesion",
                () => usuarioDAO.Login(nombreUsuario, contrasena));
        }


        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        public List<Usuario> ObtenerUsuarios()
        {
            return Ejecutor.Ejecutar("Usuario.ObtenerUsuarios",
                () => usuarioDAO.ObtenerTodos());
        }


        /// <summary>
        /// Agrega un nuevo usuario.
        /// </summary>
        public bool AgregarUsuario(Usuario usuario)
        {
            return Ejecutor.Ejecutar("Usuario.AgregarUsuario",
                () => usuarioDAO.Agregar(usuario));
        }


        /// <summary>
        /// Modifica los datos de un usuario existente.
        /// </summary>
        public bool ModificarUsuario(Usuario usuario)
        {
            return Ejecutor.Ejecutar("Usuario.ModificarUsuario",
                () => usuarioDAO.Modificar(usuario));
        }


        /// <summary>
        /// Realiza una eliminación lógica del usuario.
        /// </summary>
        public bool EliminarUsuario(int idUsuario)
        {
            return Ejecutor.Ejecutar("Usuario.EliminarUsuario",
                () => usuarioDAO.Eliminar(idUsuario));
        }

        public bool ActivarUsuario(int idUsuario)
        {
            return Ejecutor.Ejecutar("Usuario.ActivarUsuario",
                () => usuarioDAO.Activar(idUsuario));
        }

        /// <summary>
        /// Verifica si la base de datos ya posee usuarios registrados.
        /// </summary>
        /// <returns>True si existe al menos un usuario; False si la tabla está vacía.</returns>
        public bool ExistenUsuarios()
        {
            return Ejecutor.Ejecutar("Usuario.ExistenUsuarios",
                () => usuarioDAO.ExistenUsuarios());
        }


    }
}