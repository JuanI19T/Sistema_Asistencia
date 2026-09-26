namespace SistemaAsistencia.Modelo.Entidades
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public int IdEspecialidad { get; set; }
        public string NombreMateria { get; set; }
        public int CargaHoraria { get; set; }
        public int AnioMateria { get; set; }
        /// <summary>
        /// Etiqueta para combos: "Matemática — Ciclo Básico (1°)".
        /// Evita ambigüedad cuando varias tecnicaturas comparten materia.
        /// </summary>
        public string Etiqueta
        {
            get { return $"{NombreMateria} — {NombreEspecialidad} ({AnioMateria}°)"; }
        }
        /// <summary>
        /// Ciclo calculado desde el año (no se guarda en BD):
        /// 1-3 Ciclo Básico, 4-7 Ciclo Superior (tecnicaturas).
        /// </summary>
        public string Ciclo
        {
            get
            {
                if (AnioMateria >= 1 && AnioMateria <= 3) return "Ciclo Básico";
                if (AnioMateria >= 4 && AnioMateria <= 7) return "Ciclo Superior";
                return string.Empty;
            }
        }
        public string NombreEspecialidad { get; set; }
        public bool Activo { get; set; }
    }
}