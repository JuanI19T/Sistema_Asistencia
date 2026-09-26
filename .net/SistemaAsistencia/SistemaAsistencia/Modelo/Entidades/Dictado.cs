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
        public string Grupo { get; set; }
        public int AnioLectivo { get; set; }
        public string NombreMateria { get; set; }
        public string ApellidoProfesor { get; set; }
        public bool Activo { get; set; }
        public string Descripcion =>
            string.IsNullOrWhiteSpace(HorarioFin)
                ? $"{NombreMateria} - {Grupo} - {ApellidoProfesor} ({Dia} {Horario} / {AnioLectivo})"
                : $"{NombreMateria} - {Grupo} - {ApellidoProfesor} ({Dia} {Horario}–{HorarioFin} / {AnioLectivo})";
    }
}