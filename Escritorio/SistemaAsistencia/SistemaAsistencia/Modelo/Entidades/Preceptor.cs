namespace SistemaAsistencia.Modelo.Entidades
{
    public class Preceptor
    {
        public int IdPreceptor { get; set; }
        public string NombrePreceptor { get; set; }
        public string ApellidoPreceptor { get; set; }
        public string LegajoPreceptor { get; set; }
        public string Dni { get; set; }
        public string CorreoPreceptor { get; set; }
        public string TelefonoPreceptor { get; set; }
        public string Contrasena { get; set; }
        public bool Activo { get; set; }
        public string NombreCompleto => $"{ApellidoPreceptor}, {NombrePreceptor}";
    }
}