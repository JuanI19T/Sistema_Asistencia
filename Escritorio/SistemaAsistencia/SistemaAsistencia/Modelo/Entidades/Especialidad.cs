namespace SistemaAsistencia.Modelo.Entidades
{
    public class Especialidad
    {
        public int IdEspecialidad { get; set; }
        public string NombreEspecialidad { get; set; }
        // División fija de la tecnicatura (1-6). NULL = libre (Ciclo Básico).
        public int? Division { get; set; }
        public bool Activo { get; set; }
    }
}