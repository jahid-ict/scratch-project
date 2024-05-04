using Microsoft.Identity.Client;
using MongoDB.Bson;
using MongoDB.Driver;
using ScratchProject.Api.Intefaces;
using static MongoDB.Driver.WriteConcern;

namespace ScratchProject.Api.Services
{
    public class MongoDbClientService : IMongoDbClientService
    {
        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _dbInstace;
        private readonly string _connectionString;
        private readonly string _dataBaseName;

        private readonly ILogger<MongoDbClientService> _logger;
        private readonly  IConfiguration _configuration;
        public MongoDbClientService(IConfiguration configuration, ILogger<MongoDbClientService> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MongoDbConnectionStringLocal");
            _dataBaseName = _configuration["Mongo:LocalDbName"];

            if (string.IsNullOrEmpty(_connectionString))
            {
                //_logger.LogError("MongoDB connection string is missing.");
                throw new Exception("mongodb connection string is null.");
            }

            _mongoClient = new MongoClient(_connectionString);
            _dbInstace = _mongoClient.GetDatabase(_dataBaseName);

            //InitilizeMongoClient();
        }

        private void InitilizeMongoClient()
        {

        }

        public object Get(string title = "")
        {
            var collection = _dbInstace.GetCollection<BsonDocument>("movies");
            var filter = Builders<BsonDocument>.Filter.Eq("title", title);
            var document = collection.Find(filter).First();
            return document;
        }

        public void Save<T>(T value)
        {
            _logger.LogDebug($"Saving {value.ToJson().ToString()}");
            var collection = _dbInstace.GetCollection<T>(typeof(T).Name);
            collection.InsertOne(value);
            _logger.LogDebug($"Saved {value.ToJson().ToString()}");
        }

        public void SaveMany<T>(List<T> values)
        {
            _logger.LogDebug($"Saving {values.ToJson().ToString()}");
            var collection = _dbInstace.GetCollection<T>(typeof(T).Name);
            collection.InsertMany(values);
            _logger.LogDebug($"Saved {values.ToJson().ToString()}");
        }

        public T GetItemByField<T>(string fieldName, string value)
        {
            var collection = _dbInstace.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq(fieldName, value);
            var document = collection.Find(filter).First();
            return document;
        }

        public List<T> GetItemsByField<T>(string fieldName, string value)
        {
            var collection = _dbInstace.GetCollection<T>(typeof(T).Name);
            var filter = Builders<T>.Filter.Eq(fieldName, value);
            var documents = collection.Find(filter).ToList();
            return documents ?? new List<T>();
        }
    }
}
