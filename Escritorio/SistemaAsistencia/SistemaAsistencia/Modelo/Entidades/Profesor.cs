namespace SistemaAsistencia.Modelo.Entidades
{
    public class Profesor
    {
        public int IdProfesor { get; set; }
        public string NombreProfesor { get; set; }
        public string ApellidoProfesor { get; set; }
        public string DniProfesor { get; set; }
        public string LegajoProfesor { get; set; }
        public string CorreoProfesor { get; set; }
        public string TelefonoProfesor { get; set; }
        public bool Activo { get; set; }
        public string NombreCompleto => $"{ApellidoProfesor}, {NombreProfesor}";
    }
}