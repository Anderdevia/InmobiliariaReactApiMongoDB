using MongoDB.Driver;
using WebApiMongoDB.Entities;

namespace WebApiMongoDB.Data
{
    public class OwnerService
    {
        private readonly IMongoCollection<Owner> _owners;

        public OwnerService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("DbConnection"));
            var database = client.GetDatabase("inmobiliariaApi");
            _owners = database.GetCollection<Owner>("owner");
        }

        public List<Owner> GetOwners() => _owners.Find(owner => true).ToList();

        public Owner GetOwner(string id) => _owners.Find(owner => owner.IdOwner == id).FirstOrDefault();

        public Owner PostOwner(Owner owner)
        {
            _owners.InsertOne(owner);
            return owner;
        }

        public Owner PutOwner(string id, Owner newOwner)
        {
            _owners.ReplaceOne(owner => owner.IdOwner == id, newOwner);
            return newOwner;
        }

        public Owner DeleteOwner(string id)
        {
            var owner = _owners.Find(owner => owner.IdOwner == id).FirstOrDefault();
            _owners.DeleteOne(owner => owner.IdOwner == id);
            return owner;
        }
    }
}
