using System;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Envuelve la ejecución de operaciones de datos: registra en el log
    /// cualquier excepción y la relanza como DatosException (mensaje
    /// amigable). Si ya es DatosException, la deja pasar intacta.
    /// </summary>
    public static class Ejecutor
    {
        public static T Ejecutar<T>(string contexto, Func<T> accion)
        {
            try
            {
                return accion();
            }
            catch (DatosException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(contexto, ex);
                throw new DatosException(
                    "Ocurrió un error al procesar la operación '" + contexto +
                    "'. El detalle quedó guardado en el log.", ex);
            }
        }

        public static void Ejecutar(string contexto, Action accion)
        {
            try
            {
                accion();
            }
            catch (DatosException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(contexto, ex);
                throw new DatosException(
                    "Ocurrió un error al procesar la operación '" + contexto +
                    "'. El detalle quedó guardado en el log.", ex);
            }
        }
    }
}