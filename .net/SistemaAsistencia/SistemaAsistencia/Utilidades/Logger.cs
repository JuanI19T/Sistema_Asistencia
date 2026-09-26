using System;
using System.IO;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Registra errores y eventos en archivos de texto dentro de "logs",
    /// al lado del ejecutable.
    /// </summary>
    public static class Logger
    {
        private static readonly object Candado = new object();
        private static readonly string CarpetaLogs =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        public static void Error(string contexto, Exception ex)
        {
            Escribir("ERROR", contexto, ex?.ToString());
        }

        public static void Info(string contexto, string mensaje)
        {
            Escribir("INFO", contexto, mensaje);
        }

        private static void Escribir(string nivel, string contexto, string detalle)
        {
            try
            {
                lock (Candado)
                {
                    Directory.CreateDirectory(CarpetaLogs);

                    string ruta = Path.Combine(CarpetaLogs,
                        "log_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

                    File.AppendAllText(ruta,
                        "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " +
                        "[" + nivel + "] " + contexto +
                        Environment.NewLine + detalle + Environment.NewLine + Environment.NewLine);
                }
            }
            catch
            {
                // Nunca romper la aplicación por un problema de logging.
            }
        }
    }
}