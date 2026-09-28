using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SistemaAsistencia.Modelo.Conexion
{
    public class ConexionMongo
    {
        private const string NombreBase = "gestion_asistencia_eest";

        private static readonly Lazy<MongoClient> ClienteInterno = new Lazy<MongoClient>(() =>
        {
            try
            {
                var cadena = Credenciales.Obtener("MONGO", "MongoAtlas");
                var settings = MongoClientSettings.FromConnectionString(cadena);
                settings.ServerSelectionTimeout = TimeSpan.FromSeconds(5);
                var cliente = new MongoClient(settings);

                cliente.GetDatabase(NombreBase)
                    .RunCommand<BsonDocument>(new BsonDocument("ping", 1));

                return cliente;
            }
            catch (InvalidOperationException)
            {
                // Ya tiene un mensaje claro (casos A y B). No lo re-envolvemos.
                throw;
            }
            catch (MongoCommandException ex) when (ex.Code == 18)
            {
                throw new InvalidOperationException(
                    "MongoDB rechazó el usuario o la contraseña. Revisá las credenciales MONGO en 'credenciales.env' y que ese usuario exista en Atlas.", ex);
            }
            catch (MongoConfigurationException ex)
            {
                throw new InvalidOperationException(
                    "La cadena de conexión MONGO de 'credenciales.env' está mal escrita. Revisá el formato.", ex);
            }
            catch (MongoConnectionException ex)
            {
                throw new InvalidOperationException(
                    "No se pudo conectar con MongoDB. Revisá tu internet, y que el clúster de Atlas esté activo.", ex);
            }
            catch (TimeoutException ex)
            {
                throw new InvalidOperationException(
                    "MongoDB tardó demasiado en responder. El clúster de Atlas podría estar inactivo o tu IP no estar autorizada.", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    "Error inesperado al conectar con MongoDB: " + ex.Message, ex);
            }
        });

        public IMongoCollection<Modelo.Entidades.Usuario> ObtenerColeccionUsuarios()
        {
            return ClienteInterno.Value.GetDatabase(NombreBase)
                .GetCollection<Modelo.Entidades.Usuario>("usuarios");
        }

        public IMongoCollection<BsonDocument> ObtenerColeccionContadores()
        {
            return ClienteInterno.Value.GetDatabase(NombreBase)
                .GetCollection<BsonDocument>("contadores");
        }
    }
}