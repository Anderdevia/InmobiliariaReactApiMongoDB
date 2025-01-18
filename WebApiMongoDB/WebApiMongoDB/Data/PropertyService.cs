using MongoDB.Driver;
using WebApiMongoDB.Entities;

namespace WebApiMongoDB.Data
{
    public class PropertyService
    {
        private readonly IMongoCollection<Property> _properties;

        public PropertyService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("DbConnection"));
            var database = client.GetDatabase("inmobiliariaApi");
            _properties = database.GetCollection<Property>("Property");
        }

        public List<Property> GetProperties() => _properties.Find(property => true).ToList();

        public Property GetProperty(string id) => _properties.Find(property => property.IdProperty == id).FirstOrDefault();

        public Property PostProperty(Property property)
        {
            _properties.InsertOne(property);
            return property;
        }

        public Property PutProperty(string id, Property newProperty)
        {
            _properties.ReplaceOne(property => property.IdProperty == id, newProperty);
            return newProperty;
        }

        public Property DeleteProperty(string id)
        {
            var property = _properties.Find(property => property.IdProperty == id).FirstOrDefault();
            _properties.DeleteOne(property => property.IdProperty == id);
            return property;
        }
    }
}
