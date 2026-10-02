using MySql.Data.MySqlClient;

namespace SistemaAsistencia.Modelo.Conexion
{
    public class ConexionBD
    {
        private readonly string cadenaConexion = Credenciales.Obtener("MYSQL", "MySQL");

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}