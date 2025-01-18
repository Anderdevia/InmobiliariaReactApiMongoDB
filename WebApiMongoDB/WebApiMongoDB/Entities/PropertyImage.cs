using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApiMongoDB.Entities
{

    public class PropertyImage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdPropertyImage { get; set; }

        [BsonElement("idProperty")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdProperty { get; set; }

        [BsonElement("file")]
        [BsonRepresentation(BsonType.String)]
        public string? File { get; set; }

        [BsonElement("enabled")]
        [BsonRepresentation(BsonType.Boolean)]
        public bool? Enabled { get; set; }
    }
}
