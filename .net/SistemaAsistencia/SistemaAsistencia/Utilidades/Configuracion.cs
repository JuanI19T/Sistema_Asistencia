using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Lee/escribe las cadenas de conexión en "secreto.config" dentro de la
    /// carpeta del programa. Los valores se ofuscan con XOR+Base64: no frena
    /// a alguien decidido, pero evita leer el URI con un Bloc de notas.
    /// </summary>
    public static class Configuracion
    {
        private const string NombreArchivo = "secreto.config";
        private const string ClaveOfuscacion = "SistemaAsistencia";

        public static string RutaConfiguracion()
        {
            // 1) Carpeta que CONTIENE a la solución (.slnx/.sln): se sube
            //    desde el exe y, al encontrar la solución, se usa su padre.
            //    En este proyecto queda en ".net\" (al lado del CHECKLIST).
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            while (dir != null)
            {
                if (dir.GetFiles("*.slnx").Length > 0 || dir.GetFiles("*.sln").Length > 0)
                {
                    DirectoryInfo padre = dir.Parent;
                    return Path.Combine(padre != null ? padre.FullName : dir.FullName,
                                        NombreArchivo);
                }
                dir = dir.Parent;
            }

            // 2) Fallback: al lado del exe (despliegue sin archivo de solución).
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, NombreArchivo);
        }

        public static bool ExisteConfiguracion()
        {
            return File.Exists(RutaConfiguracion());
        }

        public static string ObtenerCadena(string nombre)
        {
            // 1) secreto.config al lado del exe (dentro de la carpeta)
            string ruta = RutaConfiguracion();
            if (File.Exists(ruta))
            {
                foreach (string lineaRaw in File.ReadAllLines(ruta))
                {
                    string linea = lineaRaw.Trim();
                    if (linea.Length == 0 || linea.StartsWith("#")) continue;

                    int i = linea.IndexOf('=');
                    if (i <= 0) continue;

                    string clave = linea.Substring(0, i).Trim();
                    string valor = linea.Substring(i + 1).Trim();

                    if (string.Equals(clave, nombre, StringComparison.OrdinalIgnoreCase))
                        return Ofuscar(valor, desofuscar: true);
                }
            }

            // 2) App.config como fallback (solo MySQL local, no sensible)
            return ConfigurationManager.ConnectionStrings[nombre]?.ConnectionString;
        }

        public static void GuardarCadena(string nombre, string valor)
        {
            var lineas = new List<string>();
            if (File.Exists(RutaConfiguracion()))
                lineas.AddRange(File.ReadAllLines(RutaConfiguracion()));

            lineas.RemoveAll(l =>
                l.Trim().StartsWith(nombre + "=", StringComparison.OrdinalIgnoreCase) ||
                l.Trim().StartsWith(nombre + " =", StringComparison.OrdinalIgnoreCase));

            lineas.Add(nombre + "=" + Ofuscar(valor, desofuscar: false));

            File.WriteAllLines(RutaConfiguracion(), lineas);
        }

        private static string Ofuscar(string texto, bool desofuscar)
        {
            byte[] datos = desofuscar
                ? Convert.FromBase64String(texto)
                : Encoding.UTF8.GetBytes(texto);

            byte[] clave = Encoding.UTF8.GetBytes(ClaveOfuscacion);
            for (int i = 0; i < datos.Length; i++)
                datos[i] ^= clave[i % clave.Length];

            return desofuscar
                ? Encoding.UTF8.GetString(datos)
                : Convert.ToBase64String(datos);
        }
    }
}