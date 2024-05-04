using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ScratchProject.Api.Intefaces;
using StackExchange.Redis;
using System.Security.Cryptography.Xml;

namespace ScratchProject.Api.Services
{
    public class RedisService : IRedisService
    {
        public readonly IDatabase _database;
        public readonly ILogger<RedisService> _logger;
        public RedisService(IConfiguration configuration, ILogger<RedisService> logger) 
        {
            _logger = logger;
            try
            {
                ConnectionMultiplexer redisConnection = ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionStringCloud"));
                _logger.LogInformation("Redis connection successfull");
                _database = redisConnection.GetDatabase();
                if( _database != null )
                {
                    _logger.LogInformation($"Redis database found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Redis connection failed: {ex.Message}");
            }
        }
        public void SaveString(string key, string value)
        {
            // string value can't be bigger than 512MB
            _database.StringSet(key, value);

        }

        public void StringIncrement(string key, long value)
        {
            _database.StringIncrement(key, value);
        }
        public string GetString(string key)
        {
           return _database.StringGet(key);
        }

        public void SaveHash(string key, object value)
        {
            if (value == null) return;
            var jsonString = JsonConvert.SerializeObject(value);
            var jObject = JObject.Parse(jsonString);
            //var jsonPaths = new JsonPath
                
        }
    }
}
