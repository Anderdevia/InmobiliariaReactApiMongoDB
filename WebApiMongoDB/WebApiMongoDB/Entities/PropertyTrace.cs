using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApiMongoDB.Entities
{
    public class PropertyTrace
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdPropertyTrace { get; set; }

        [BsonElement("dateSale")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? DateSale { get; set; }

        [BsonElement("name")]
        [BsonRepresentation(BsonType.String)]
        public string? Name { get; set; }

        [BsonElement("value")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? Value { get; set; }

        [BsonElement("tax")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal? Tax { get; set; }

        [BsonElement("idProperty")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdProperty { get; set; }
    }
}
