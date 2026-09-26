using System.Configuration;
using MySql.Data.MySqlClient;

namespace SistemaAsistencia.Modelo.Conexion
{
    public class ConexionBD
    {
        private readonly string cadenaConexion =
            ConfigurationManager.ConnectionStrings["MySQL"].ConnectionString;

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadenaConexion);
        }
    }
}