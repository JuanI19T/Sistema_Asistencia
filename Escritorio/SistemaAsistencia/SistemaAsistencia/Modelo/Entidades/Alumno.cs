namespace SistemaAsistencia.Modelo.Entidades
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string ApellidoAlumno { get; set; }
        public string DniAlumno { get; set; }
        public string LegajoAlumno { get; set; }
        public string CorreoAlumno { get; set; }
        public string TelefonoAlumno { get; set; }
        public bool Activo { get; set; }
    }
}