using FileNest.Model;
using FileNest.Service.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
namespace FileNest.Service
{
    public class MongoDbService
    {
        private readonly IMongoClient _mongoClient;
        private readonly DatabaseConnectionProvider _connectionProvider;
        private readonly IMongoDatabase _database;
        public MongoDbService(IMongoClient mongoClient,DatabaseConnectionProvider connectionProvider)
        {
            _mongoClient = mongoClient;
            _connectionProvider = connectionProvider;
            var databaseSettings = connectionProvider.GetDatabaseConfiguration("MongoDB");
            _database = _mongoClient.GetDatabase(databaseSettings.DatabaseName);
        }
        // Check MongoDB connection
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                await _database.RunCommandAsync<BsonDocument>(new BsonDocument("ping", 1));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}