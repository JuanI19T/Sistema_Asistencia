using System;
using System.Linq;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Teléfono argentino en 3 cajas: país (54) + área (máx 3) + número.
    /// La BD sigue guardando un solo string "54 AAA NNNNNN".
    /// </summary>
    public static class TelefonoHelper
    {
        public const string PaisFijo = "54";
        public const int AreaMin = 2;
        public const int AreaMax = 3;
        public const int NumeroMin = 6;
        public const int NumeroMax = 8;

        public static bool SoloDigitos(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;
            foreach (char c in s)
                if (!char.IsDigit(c)) return false;
            return true;
        }

        public static string SoloDigitosONull(string s)
        {
            if (s == null) return string.Empty;
            return new string(s.Where(char.IsDigit).ToArray());
        }

        /// <summary>
        /// Une las 3 cajas al formato guardado. Vacío total -> string.Empty.
        /// </summary>
        public static string Unir(string pais, string area, string numero)
        {
            string p = (pais ?? string.Empty).Trim();
            string a = SoloDigitosONull(area);
            string n = SoloDigitosONull(numero);
            if (a.Length == 0 && n.Length == 0) return string.Empty;
            if (p.Length == 0) p = PaisFijo;
            if (a.Length == 0 || n.Length == 0) return string.Empty;
            return $"{p} {a} {n}";
        }

        /// <summary>
        /// Reparte un teléfono guardado ("54 011 1234567") en las 3 cajas.
        /// Si no se puede interpretar, devuelve vacíos (el usuario reescribe).
        /// </summary>
        public static void Separar(string telefono, out string pais, out string area, out string numero)
        {
            pais = PaisFijo;
            area = string.Empty;
            numero = string.Empty;
            if (string.IsNullOrWhiteSpace(telefono)) return;

            string[] partes = telefono.Split(
                new[] { ' ', '-', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            if (partes.Length >= 3)
            {
                pais = SoloDigitosONull(partes[0]);
                if (pais.Length == 0) pais = PaisFijo;
                area = SoloDigitosONull(partes[1]);
                numero = SoloDigitosONull(string.Join(string.Empty, partes, 2, partes.Length - 2));
            }
            else if (partes.Length == 2)
            {
                area = SoloDigitosONull(partes[0]);
                numero = SoloDigitosONull(partes[1]);
            }
            else
            {
                string dig = SoloDigitosONull(telefono);
                if (dig.StartsWith(PaisFijo) && dig.Length > NumeroMax)
                    dig = dig.Substring(PaisFijo.Length);
                if (dig.Length > NumeroMax)
                {
                    int nLen = Math.Min(NumeroMax, dig.Length - AreaMin);
                    if (nLen < NumeroMin) nLen = Math.Min(dig.Length, NumeroMax);
                    area = dig.Substring(0, dig.Length - nLen);
                    numero = dig.Substring(dig.Length - nLen);
                    if (area.Length > AreaMax)
                    {
                        area = string.Empty;
                        numero = string.Empty;
                    }
                }
                else
                {
                    numero = dig;
                }
            }

            if (area.Length > AreaMax || numero.Length > NumeroMax)
            {
                area = string.Empty;
                numero = string.Empty;
            }
        }

        /// <summary>
        /// Vacío total permitido. Si completa una caja exige las dos.
        /// Área 2-3 dígitos, número 6-8 dígitos.
        /// </summary>
        public static bool EsValido(string area, string numero, out string error)
        {
            string a = SoloDigitosONull(area);
            string n = SoloDigitosONull(numero);
            if (a.Length == 0 && n.Length == 0)
            {
                error = null;
                return true;
            }
            if (a.Length == 0 || n.Length == 0)
            {
                error = "Complete código de área y número, o deje ambos vacíos.";
                return false;
            }
            if (a.Length < AreaMin || a.Length > AreaMax)
            {
                error = $"El código de área debe tener {AreaMin} a {AreaMax} dígitos.";
                return false;
            }
            if (n.Length < NumeroMin || n.Length > NumeroMax)
            {
                error = $"El número debe tener {NumeroMin} a {NumeroMax} dígitos.";
                return false;
            }
            error = null;
            return true;
        }
    }
}
