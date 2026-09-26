using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using SistemaAsistencia.Modelo.Conexion;
using SistemaAsistencia.Modelo.Entidades;

namespace SistemaAsistencia.Modelo.DAO
{
    public class UsuarioDAO
    {
        private readonly IMongoCollection<Usuario> coleccion;
        private readonly IMongoCollection<BsonDocument> contadores;

        public UsuarioDAO()
        {
            coleccion = new ConexionMongo().ObtenerColeccionUsuarios();
            contadores = new ConexionMongo().ObtenerColeccionContadores();

            // Índice único para replicar el UNIQUE de MySQL
            var keys = Builders<Usuario>.IndexKeys.Ascending(u => u.NombreUsuario);
            try { coleccion.Indexes.CreateOne(new CreateIndexModel<Usuario>(keys, new CreateIndexOptions { Unique = true })); }
            catch (TimeoutException) { }

            try { AsegurarContadorInicial(); }
            catch (TimeoutException) { }
        }

        private void AsegurarContadorInicial()
        {
            bool existe = contadores.CountDocuments(
                Builders<BsonDocument>.Filter.Eq("_id", "usuario")) > 0;

            if (existe) return;

            int ultimoId = coleccion.Find(_ => true)
                .SortByDescending(u => u.IdUsuario)
                .Limit(1)
                .Project(u => u.IdUsuario)
                .FirstOrDefault();

            contadores.InsertOne(new BsonDocument
            {
                { "_id", "usuario" },
                { "secuencia", ultimoId }
            });
        }

        private int ObtenerSiguienteId()
        {
            var filtro = Builders<BsonDocument>.Filter.Eq("_id", "usuario");
            var incremento = Builders<BsonDocument>.Update.Inc("secuencia", 1);
            var opciones = new FindOneAndUpdateOptions<BsonDocument>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var contador = contadores.FindOneAndUpdate(filtro, incremento, opciones);
            return contador["secuencia"].AsInt32;
        }

        public Usuario Login(string usuario, string contrasena)
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.NombreUsuario, usuario)
                & Builders<Usuario>.Filter.Eq(u => u.Activo, true);

            Usuario user = coleccion.Find(filtro).FirstOrDefault();
            if (user == null) return null;

            if (!VerificarContrasena(contrasena, user.Contrasena))
                return null;

            // Migración transparente: si el hash almacenado es el SHA-256
            // viejo (64 hex de v06), se recalcula con PBKDF2 en este login.
            if (EsHashViejo(user.Contrasena))
            {
                coleccion.UpdateOne(
                    Builders<Usuario>.Filter.Eq(u => u.IdUsuario, user.IdUsuario),
                    Builders<Usuario>.Update.Set(
                        u => u.Contrasena, HashearContrasena(contrasena)));
            }

            return user;
        }

        public List<Usuario> ObtenerTodos()
        {
            return coleccion.Find(_ => true)
                .SortBy(u => u.NombreUsuario)
                .ToList();
        }

        public bool Agregar(Usuario usuario)
        {
            try
            {
                usuario.IdUsuario = ObtenerSiguienteId();
                usuario.Contrasena = HashearContrasena(usuario.Contrasena);
                coleccion.InsertOne(usuario);
                return true;
            }
            catch (MongoWriteException)
            {
                return false;
            }
        }

        public bool Modificar(Usuario usuario)
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.IdUsuario, usuario.IdUsuario);
            var update = Builders<Usuario>.Update
                .Set(u => u.NombreUsuario, usuario.NombreUsuario)
                .Set(u => u.Rol, usuario.Rol)
                .Set(u => u.Activo, usuario.Activo);

            if (!string.IsNullOrEmpty(usuario.Contrasena))
            {
                update = update.Set(
                    u => u.Contrasena, HashearContrasena(usuario.Contrasena));
            }

            return coleccion.UpdateOne(filtro, update).ModifiedCount > 0;
        }

        public bool Eliminar(int idUsuario)
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.IdUsuario, idUsuario);
            var update = Builders<Usuario>.Update.Set(u => u.Activo, false);
            return coleccion.UpdateOne(filtro, update).ModifiedCount > 0;
        }

        public bool Activar(int idUsuario)
        {
            var filtro = Builders<Usuario>.Filter.Eq(u => u.IdUsuario, idUsuario);
            var update = Builders<Usuario>.Update.Set(u => u.Activo, true);
            return coleccion.UpdateOne(filtro, update).ModifiedCount > 0;
        }

        public bool ExistenUsuarios()
        {
            return coleccion.CountDocuments(FilterDefinition<Usuario>.Empty) > 0;
        }

        private const string PrefijoPBKDF2 = "pbkdf2$";
        private const int IteracionesPBKDF2 = 10000;
        private const int TamanioSal = 16;    // bytes
        private const int TamanioClave = 32;  // bytes (largo de SHA-256)

        private static string HashearContrasena(string contrasena)
        {
            byte[] sal = new byte[TamanioSal];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(sal);

            byte[] clave = ObtenerClavePBKDF2(contrasena, sal);

            return PrefijoPBKDF2
                + Convert.ToBase64String(sal) + "$"
                + Convert.ToBase64String(clave);
        }

        private static bool VerificarContrasena(string contrasena, string hashAlmacenado)
        {
            if (string.IsNullOrEmpty(hashAlmacenado)) return false;

            // Hash viejo de v06 (SHA-256 sin sal, 64 hex): se acepta solo
            // para validar y migrar en Login.
            if (EsHashViejo(hashAlmacenado))
                return HashearContrasenaVieja(contrasena)
                    .Equals(hashAlmacenado, StringComparison.OrdinalIgnoreCase);

            if (!hashAlmacenado.StartsWith(PrefijoPBKDF2)) return false;

            string cuerpo = hashAlmacenado.Substring(PrefijoPBKDF2.Length);
            string[] partes = cuerpo.Split('$');
            if (partes.Length != 2) return false;

            byte[] sal = Convert.FromBase64String(partes[0]);
            byte[] claveEsperada = Convert.FromBase64String(partes[1]);
            byte[] claveCalculada = ObtenerClavePBKDF2(contrasena, sal);

            return TimingSafeEquals(claveEsperada, claveCalculada);
        }

        private static byte[] ObtenerClavePBKDF2(string contrasena, byte[] sal)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(contrasena, sal, IteracionesPBKDF2))
                return pbkdf2.GetBytes(TamanioClave);
        }

        private static bool EsHashViejo(string valor)
        {
            // SHA-256 en hex = 64 caracteres.
            if (valor.Length != 64) return false;
            foreach (char c in valor)
                if (!Uri.IsHexDigit(c)) return false;
            return true;
        }

        private static string HashearContrasenaVieja(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(contrasena);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder resultado = new StringBuilder();
                foreach (byte b in hash)
                    resultado.Append(b.ToString("x2"));
                return resultado.ToString();
            }
        }

        private static bool TimingSafeEquals(byte[] a, byte[] b)
        {
            // Comparación en tiempo constante (evita ataques de timing).
            uint diff = (uint)a.Length ^ (uint)b.Length;
            int n = Math.Min(a.Length, b.Length);
            for (int i = 0; i < n; i++)
                diff |= (uint)(a[i] ^ b[i]);
            return diff == 0;
        }
    }
}