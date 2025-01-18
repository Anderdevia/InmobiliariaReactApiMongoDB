using MongoDB.Driver;
using WebApiMongoDB.Entities;

namespace WebApiMongoDB.Data
{
    public class PropertyImageService
    {
        private readonly IMongoCollection<PropertyImage> _propertyImages;

        public PropertyImageService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("DbConnection"));
            var database = client.GetDatabase("inmobiliariaApi");
            _propertyImages = database.GetCollection<PropertyImage>("PropertyImage");
        }

        public List<PropertyImage> GetPropertyImages() => _propertyImages.Find(image => true).ToList();

        public PropertyImage GetPropertyImage(string id) => _propertyImages.Find(image => image.IdPropertyImage == id).FirstOrDefault();

        public PropertyImage PostPropertyImage(PropertyImage image)
        {
            _propertyImages.InsertOne(image);
            return image;
        }

        public PropertyImage PutPropertyImage(string id, PropertyImage newImage)
        {
            _propertyImages.ReplaceOne(image => image.IdPropertyImage == id, newImage);
            return newImage;
        }

        public PropertyImage DeletePropertyImage(string id)
        {
            var image = _propertyImages.Find(image => image.IdPropertyImage == id).FirstOrDefault();
            _propertyImages.DeleteOne(image => image.IdPropertyImage == id);
            return image;
        }
    }
}
