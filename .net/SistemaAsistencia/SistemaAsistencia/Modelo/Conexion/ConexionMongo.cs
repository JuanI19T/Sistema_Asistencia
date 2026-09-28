using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SistemaAsistencia.Modelo.Conexion
{
    public class ConexionMongo
    {
        private readonly string nombreBase = "gestion_asistencia_eest";

        private static readonly Lazy<MongoClient> ClienteInterno = new Lazy<MongoClient>(() =>
        {
            var cadena = Credenciales.Obtener("MONGO", "MongoAtlas");
            var settings = MongoClientSettings.FromConnectionString(cadena);
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(5);
            return new MongoClient(settings);
        });

        public IMongoCollection<Modelo.Entidades.Usuario> ObtenerColeccionUsuarios()
        {
            return ClienteInterno.Value.GetDatabase(nombreBase)
                .GetCollection<Modelo.Entidades.Usuario>("usuarios");
        }

        public IMongoCollection<BsonDocument> ObtenerColeccionContadores()
        {
            return ClienteInterno.Value.GetDatabase(nombreBase)
                .GetCollection<BsonDocument>("contadores");
        }
    }
}