using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace SistemaAsistencia.Modelo.Conexion
{
    public static class Credenciales
    {
        private const string Archivo = "credenciales.env";

        private static readonly object Candado = new object();
        private static Dictionary<string, string> valores;

        private static Dictionary<string, string> Valores
        {
            get
            {
                lock (Candado)
                {
                    if (valores == null)
                        valores = LeerArchivo();

                    return valores;
                }
            }
        }

        private static Dictionary<string, string> LeerArchivo()
        {
            var resultado = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string rutaSalida = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Archivo);

            if (!File.Exists(rutaSalida))
            {
                var dirActual = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                while (dirActual != null && dirActual.Parent != null)
                {
                    var prueba = Path.Combine(dirActual.FullName, Archivo);
                    if (File.Exists(prueba))
                    {
                        throw new InvalidOperationException(
                            $"El archivo '{Archivo}' existe en la carpeta del proyecto pero no se copió al directorio de salida ({AppDomain.CurrentDomain.BaseDirectory}). " +
                            "Recompilá la solución (Rebuild) para copiarlo.");
                    }
                    dirActual = dirActual.Parent;
                }

                throw new InvalidOperationException(
                    $"No se encontró el archivo '{Archivo}'. Copiá '{Archivo}.example' como '{Archivo}' " +
                    "en la carpeta del proyecto y completá los valores reales.");
            }

            foreach (var linea in File.ReadAllLines(rutaSalida))
            {
                string texto = linea.Trim();
                if (texto.Length == 0 || texto.StartsWith("#")) continue;

                int corte = texto.IndexOf('=');
                if (corte <= 0) continue;

                string clave = texto.Substring(0, corte).Trim();
                string valor = texto.Substring(corte + 1).Trim();
                if (clave.Length > 0) resultado[clave] = valor;
            }

            return resultado;
        }

        public static string Obtener(string clave, string nombreConnectionString)
        {
            string valor;
            if (Valores.TryGetValue(clave, out valor) && !string.IsNullOrWhiteSpace(valor))
            {
                return valor;
            }

            valor = ConfigurationManager.ConnectionStrings[nombreConnectionString]?.ConnectionString;

            if (string.IsNullOrWhiteSpace(valor) || valor.Contains("PLACEHOLDER"))
            {
                throw new InvalidOperationException(
                    "Falta la credencial '" + clave + "'. Copiá '" + Archivo + ".example' a '" +
                    Archivo + "' (en la carpeta del proyecto) y completá el valor real.");
            }

            return valor;
        }
    }
}