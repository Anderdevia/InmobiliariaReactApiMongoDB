using MongoDB.Driver;
using WebApiMongoDB.Entities;

namespace WebApiMongoDB.Data
{
    public class PropertyTraceService
    {
        private readonly IMongoCollection<PropertyTrace> _propertyTraces;

        public PropertyTraceService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("DbConnection"));
            var database = client.GetDatabase("inmobiliariaApi");
            _propertyTraces = database.GetCollection<PropertyTrace>("PropertyTrace");
        }

        public List<PropertyTrace> GetPropertyTraces() => _propertyTraces.Find(trace => true).ToList();

        public PropertyTrace GetPropertyTrace(string id) => _propertyTraces.Find(trace => trace.IdPropertyTrace == id).FirstOrDefault();

        public PropertyTrace PostPropertyTrace(PropertyTrace trace)
        {
            _propertyTraces.InsertOne(trace);
            return trace;
        }

        public PropertyTrace PutPropertyTrace(string id, PropertyTrace newTrace)
        {
            _propertyTraces.ReplaceOne(trace => trace.IdPropertyTrace == id, newTrace);
            return newTrace;
        }

        public PropertyTrace DeletePropertyTrace(string id)
        {
            var trace = _propertyTraces.Find(trace => trace.IdPropertyTrace == id).FirstOrDefault();
            _propertyTraces.DeleteOne(trace => trace.IdPropertyTrace == id);
            return trace;
        }
    }
}
