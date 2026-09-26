using System;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Clase encargada de almacenar la información
    /// del usuario que inició sesión actualmente.
    /// </summary>
    public static class Sesion
    {
        /// <summary>
        /// Usuario que se encuentra autenticado en el sistema.
        /// </summary>
        public static Usuario UsuarioActual { get; set; }


        /// <summary>
        /// Indica si existe un usuario con sesión iniciada.
        /// </summary>
        public static bool ExisteSesion()
        {
            return UsuarioActual != null;
        }


        /// <summary>
        /// Indica si el usuario autenticado tiene el rol Administrador.
        /// </summary>
        public static bool UsuarioActualRolAdministrador()
        {
            return UsuarioActual != null &&
                string.Equals(UsuarioActual.Rol, "Administrador",
                    StringComparison.OrdinalIgnoreCase);
        }


        /// <summary>
        /// Cierra la sesión del usuario actual.
        /// </summary>
        public static void CerrarSesion()
        {
            UsuarioActual = null;
        }
    }
}