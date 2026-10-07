using CryptSharp;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Reglas universales de acceso para personas (alumno, profesor, preceptor).
    /// - El DNI es el identificador válido en los 3 casos: 7 u 8 dígitos numéricos.
    /// - La contraseña inicial es siempre el DNI (solo al crear; al modificar
    ///   se conserva la existente). Se guarda con hash bcrypt, formato
    ///   compatible con el login de la API.
    /// </summary>
    public static class SeguridadAcceso
    {
        /// <summary>
        /// Normaliza y valida un DNI: no vacío, solo dígitos, 7 u 8 de largo.
        /// </summary>
        /// <param name="dni">DNI tal como viene de la vista.</param>
        /// <returns>El DNI recortado, listo para guardar.</returns>
        public static string ValidarDni(string dni)
        {
            string d = (dni ?? string.Empty).Trim();
            if (d.Length == 0)
                throw new DatosException("Ingrese el DNI.");
            if (!SoloDigitos(d) || d.Length < 7 || d.Length > 8)
                throw new DatosException("El DNI debe tener 7 u 8 dígitos numéricos.");
            return d;
        }

        /// <summary>
        /// Genera el hash bcrypt de la clave inicial (el DNI).
        /// </summary>
        public static string HashClaveInicial(string dni)
        {
            return Crypter.Blowfish.Crypt(dni, Crypter.Blowfish.GenerateSalt(10));
        }

        /// <summary>
        /// Indica si el texto contiene solo dígitos (no vacío).
        /// </summary>
        public static bool SoloDigitos(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            foreach (char c in s)
                if (!char.IsDigit(c)) return false;
            return true;
        }
    }
}
