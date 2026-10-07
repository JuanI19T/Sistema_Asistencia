using System;
using System.Collections.Generic;
using System.Globalization;
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
        /// Indica si el usuario autenticado puede dar de alta o de baja
        /// registros (roles Administrador y Directivo). El resto de roles
        /// no ve el boton de activar/desactivar.
        /// </summary>
        public static bool PuedeGestionarActivo()
        {
            if (UsuarioActual == null || string.IsNullOrWhiteSpace(UsuarioActual.Rol))
                return false;
            string rol = UsuarioActual.Rol.Trim();
            return string.Equals(rol, "Administrador", StringComparison.OrdinalIgnoreCase)
                || string.Equals(rol, "Directivo", StringComparison.OrdinalIgnoreCase);
        }


        /// <summary>
        /// Cierra la sesión del usuario actual y limpia los catálogos en memoria.
        /// </summary>
        public static void CerrarSesion()
        {
            UsuarioActual = null;
            Alumnos = null;
            Profesores = null;
            Preceptores = null;
            Materias = null;
            Especialidades = null;
        }


        // ---------- Catálogos en memoria (cargados una sola vez al login) ----------
        public static List<Alumno> Alumnos { get; set; } = new List<Alumno>();
        public static List<Profesor> Profesores { get; set; } = new List<Profesor>();
        public static List<Preceptor> Preceptores { get; set; } = new List<Preceptor>();
        public static List<Materia> Materias { get; set; } = new List<Materia>();
        public static List<Especialidad> Especialidades { get; set; } = new List<Especialidad>();
        // Agrega aquí más catálogos según tu necesidad (Ej: List<Dictado> Dictados, etc.)


        /// <summary>
        /// Carga todos los catálogos principales desde la base de datos
        /// y los deja disponibles en memoria para todo el resto de la aplicación.
        /// </summary>
        public static void CargarCatalogos()
        {
            try
            {
                var daoAlumno = new Modelo.DAO.AlumnoDAO();
                Alumnos = daoAlumno.ObtenerTodos();

                var daoProfesor = new Modelo.DAO.ProfesorDAO();
                Profesores = daoProfesor.ObtenerTodos();

                var daoPreceptor = new Modelo.DAO.PreceptorDAO();
                Preceptores = daoPreceptor.ObtenerTodos();

                var daoMateria = new Modelo.DAO.MateriaDAO();
                Materias = daoMateria.ObtenerTodos();

                var daoEspecialidad = new Modelo.DAO.EspecialidadDAO();
                Especialidades = daoEspecialidad.ObtenerTodas();
                // Carga el resto si agregaste más propiedades arriba
            }
            catch
            {
                // Si falla la carga de catálogos, las listas se quedan vacías
                // y los forms pueden cargar bajo demanda o mostrar mensaje.
            }
        }
    }
}