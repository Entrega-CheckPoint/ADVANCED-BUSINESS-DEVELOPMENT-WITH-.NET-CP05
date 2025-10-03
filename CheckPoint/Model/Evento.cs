using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CheckPoint.Model
{

    public class Evento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Titulo")]
        public required string Titulo { get; set; }

        [BsonElement("Descricao")]
        public required string descricao { get; set; }

        [BsonElement("Data")]
        public required DateTime Data { get; set; }

        [BsonElement("Local")]
        public string? Local { get; set; }

        [BsonElement("Categoria")]
        public string? Categoria { get; set; }

        [BsonElement("CapacidadeMaxima")]
        public int? CapacidadeMaxima { get; set; }

        [BsonElement("DataCriacao")]
        public required DateTime DataCriacao { get; set; }

    }
}
