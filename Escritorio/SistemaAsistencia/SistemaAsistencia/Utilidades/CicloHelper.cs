using System;
using System.Windows.Forms;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Utilidades
{
    /// <summary>
    /// Regla EEST compartida: Ciclo Básico (años 1-3) vs tecnicaturas (años 4-7).
    /// Centraliza la regla que antes vivía copiada en cada formulario.
    /// División: Básico 1-7, tecnicaturas 1-6. Grupo: 1-2 (0 = todo).
    /// </summary>
    public static class CicloHelper
    {
        public static bool EsCicloBasico(string nombreEspecialidad)
        {
            string n = (nombreEspecialidad ?? string.Empty).Trim();
            return n.Equals("Ciclo Básico", StringComparison.OrdinalIgnoreCase)
                || n.Equals("Ciclo Basico", StringComparison.OrdinalIgnoreCase);
        }

        public static void RangoAnio(bool basico, out int min, out int max)
        {
            min = basico ? 1 : 4;
            max = basico ? 3 : 7;
        }

        public static int DivisionMaxima(bool basico)
        {
            return basico ? 7 : 6;
        }

        /// <summary>
        /// División fija de una especialidad de ciclo superior (NULL = libre:
        /// Ciclo Básico o tecnicatura sin mapeo).
        /// </summary>
        public static int? DivisionDeEspecialidad(Especialidad esp)
        {
            if (esp == null || EsCicloBasico(esp.NombreEspecialidad)) return null;
            return esp.Division;
        }

        public static string NombreEspecialidadDe(ComboBox combo)
        {
            var esp = combo.SelectedItem as Especialidad;
            if (esp != null && !string.IsNullOrWhiteSpace(esp.NombreEspecialidad))
                return esp.NombreEspecialidad;
            return combo.Text;
        }

        public static void AplicarRangoAnio(NumericUpDown nud, string nombreEspecialidad)
        {
            RangoAnio(EsCicloBasico(nombreEspecialidad), out int min, out int max);

            // Orden seguro: nunca dejar Value fuera de [Minimum, Maximum].
            decimal v = nud.Value;
            if (v < min) { nud.Maximum = max; nud.Minimum = min; nud.Value = min; }
            else if (v > max) { nud.Minimum = min; nud.Maximum = max; nud.Value = max; }
            else { nud.Minimum = min; nud.Maximum = max; }
        }
    }
}
