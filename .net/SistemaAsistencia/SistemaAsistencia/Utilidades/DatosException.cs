using System;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Excepción de la capa de datos/negocio: mensaje amigable para el
    /// usuario y detalle técnico en el log / InnerException.
    /// </summary>
    public class DatosException : Exception
    {
        public DatosException(string mensaje) : base(mensaje) { }

        public DatosException(string mensaje, Exception inner) : base(mensaje, inner) { }
    }
}