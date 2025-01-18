using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApiMongoDB.Entities
{
    public class Property
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdProperty { get; set; }

        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string? Name { get; set; }

        [BsonElement("address")]
        [BsonRepresentation(BsonType.String)]
        public string? Address { get; set; }

        [BsonElement("price")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? Price { get; set; }

        [BsonElement("codeInternal")]
        [BsonRepresentation(BsonType.String)]
        public string? CodeInternal { get; set; }

        [BsonElement("year")]
        [BsonRepresentation(BsonType.Int32)]
        public int? Year { get; set; }

        [BsonElement("idOwner")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdOwner { get; set; }
    }
}