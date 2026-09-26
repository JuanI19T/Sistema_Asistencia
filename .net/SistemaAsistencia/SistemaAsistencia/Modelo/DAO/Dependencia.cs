namespace SistemaAsistencia.Modelo.DAO
{
    /// <summary>
    /// Representa un registro vinculado a una entidad (relación de dependencia)
    /// y la cantidad encontrada, usada antes de una eliminación definitiva.
    /// </summary>
    public class Dependencia
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        public Dependencia(string nombre, int cantidad)
        {
            Nombre = nombre;
            Cantidad = cantidad;
        }
    }
}