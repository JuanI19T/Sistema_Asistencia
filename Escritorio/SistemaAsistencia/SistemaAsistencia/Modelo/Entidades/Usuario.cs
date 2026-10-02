using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SistemaAsistencia.Modelo.Entidades
{
    [BsonIgnoreExtraElements]
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int IdUsuario { get; set; }

        [BsonElement("usuario")]
        public string NombreUsuario { get; set; }

        [BsonElement("contrasena")]
        public string Contrasena { get; set; }

        [BsonElement("rol")]
        public string Rol { get; set; }

        [BsonElement("activo")]
        public bool Activo { get; set; }
    }
}