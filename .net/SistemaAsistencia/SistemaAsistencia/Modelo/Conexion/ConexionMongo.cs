using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Configuration;

namespace SistemaAsistencia.Modelo.Conexion
{
    public class ConexionMongo
    {
        private static readonly string cadenaAtlas =
        ConfigurationManager.ConnectionStrings["MongoAtlas"].ConnectionString;

        private static readonly MongoClient cliente;

        static ConexionMongo()
        {
            var settings = MongoClientSettings.FromConnectionString(cadenaAtlas);
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(5);
            cliente = new MongoClient(settings);
        }

        private readonly string nombreBase = "gestion_asistencia_eest";

        public IMongoCollection<Modelo.Entidades.Usuario> ObtenerColeccionUsuarios()
        {
            var db = cliente.GetDatabase(nombreBase);
            return db.GetCollection<Modelo.Entidades.Usuario>("usuarios");
        }
        public IMongoCollection<BsonDocument> ObtenerColeccionContadores()
        {
            var db = cliente.GetDatabase(nombreBase);
            return db.GetCollection<BsonDocument>("contadores");
        }
    }
}