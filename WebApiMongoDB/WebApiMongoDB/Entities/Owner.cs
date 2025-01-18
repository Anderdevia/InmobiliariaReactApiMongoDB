using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApiMongoDB.Entities
{
    public class Owner
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? IdOwner { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string? Name { get; set; }

        [BsonElement("address"), BsonRepresentation(BsonType.String)]
        public string? Address { get; set; }

        [BsonElement("photo"), BsonRepresentation(BsonType.String)]
        public string? Photo { get; set; }

        [BsonElement("birthday"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? Birthday { get; set; }
    }
}
