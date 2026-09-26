namespace SistemaAsistencia.Modelo.Entidades
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public int IdEspecialidad { get; set; }
        public string NombreMateria { get; set; }
        public int CargaHoraria { get; set; }
        public int AnioMateria { get; set; }
        public string NombreEspecialidad { get; set; }
        public bool Activo { get; set; }
    }
}