using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace SistemaAsistencia.Modelo.Conexion
{
    public static class Credenciales
    {
        private const string Archivo = "credenciales.env";

        private static readonly Dictionary<string, string> Valores = LeerArchivo();

        private static Dictionary<string, string> LeerArchivo()
        {
            var resultado = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Archivo);

            if (!File.Exists(ruta)) return resultado;

            foreach (var linea in File.ReadAllLines(ruta))
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
                    "Falta la credencial '" + clave + ". Copiá '" + Archivo + ".example' a '" +
                    Archivo + "' (en la carpeta del proyecto) y completá el valor real.");
            }

            return valor;
        }
    }
}