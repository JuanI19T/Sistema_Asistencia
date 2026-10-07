namespace SistemaAsistencia.Modelo.Entidades
{
    public class Dictado
    {
        public int IdDictado { get; set; }
        public int IdMateria { get; set; }
        public int IdProfesor { get; set; }
        public string Dia { get; set; }
        public string Horario { get; set; }
        public string HorarioFin { get; set; }

        /// <summary>
        /// División a la que va el dictado. Null = todo el curso.
        /// Ciclo Básico 1-7, Tecnicaturas 1-6 (rango fino: lo valida la app).
        /// </summary>
        public int? Division { get; set; }

        /// <summary>
        /// Grupo al que va el dictado. Null = todo el curso o toda la división.
        /// Grupo 1-2 en todos los casos (0/NULL = ambos grupos).
        /// Rango fino: lo valida la app.
        /// </summary>
        public int? Grupo { get; set; }

        public int AnioLectivo { get; set; }
        /// <summary>
        /// Preceptor asignado al dictado. NULL = sin asignar.
        /// </summary>
        public int? IdPreceptor { get; set; }
        /// <summary>
        /// Año curricular de la materia (1-7). Solo lectura para la vista.
        /// </summary>
        public int AnioMateria { get; set; }
        public string NombreMateria { get; set; }
        public string NombreEspecialidad { get; set; }
        public string ApellidoProfesor { get; set; }
        public bool Activo { get; set; }

        /// <summary>
        /// Texto del alcance, tal como se muestra en grillas y listados:
        ///   division+grupo -> "Div. 3 - Grupo 1"
        ///   solo division   -> "Div. 3 - Todos los grupos"
        ///   ninguno         -> "Todo el curso"
        /// </summary>
        public string Alcance
        {
            get
            {
                if (!Division.HasValue) return "Todo el curso";
                if (!Grupo.HasValue) return "Div. " + Division.Value + " - Todos los grupos";
                return "Div. " + Division.Value + " - Grupo " + Grupo.Value;
            }
        }

        public string Descripcion =>
            string.IsNullOrWhiteSpace(HorarioFin)
                ? $"{NombreMateria} - {Alcance} - {ApellidoProfesor} ({Dia} {Horario} / {AnioLectivo})"
                : $"{NombreMateria} - {Alcance} - {ApellidoProfesor} ({Dia} {Horario}–{HorarioFin} / {AnioLectivo})";

        /// <summary>
        /// Horario de inicio y fin en un solo campo, solo horas y minutos:
        /// "08:00–10:00". Sin fin muestra solo el inicio. Solo lectura.
        /// </summary>
        public string HorarioCompleto
        {
            get
            {
                string inicio = HoraCorta(Horario);
                if (string.IsNullOrWhiteSpace(HorarioFin))
                    return inicio;
                return inicio + "–" + HoraCorta(HorarioFin);
            }
        }

        private static string HoraCorta(string texto)
        {
            if (System.TimeSpan.TryParse(texto, out System.TimeSpan ts))
                return ts.ToString(@"hh\:mm");
            return texto ?? string.Empty;
        }
    }
}
