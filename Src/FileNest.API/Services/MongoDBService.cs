using FileNest.API.Configuration;
using FileNest.API.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FileNest.API.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(DatabaseConnectionService connectionProvider)
        {
            var connectionString = connectionProvider.GetConnectionString("MongoDB");

            var client = new MongoClient(connectionString);

            _database = client.GetDatabase("FileNest");
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                await _database.RunCommandAsync<BsonDocument>(
                    new BsonDocument("ping", 1));

                return true;
            }
            catch (MongoException)
            {
                return false;
            }
        }
        public async Task InsertTestDocumentAsync(MongoTestDocument document)
        {
            var collection = _database.GetCollection<MongoTestDocument>("TestCollection");

            await collection.InsertOneAsync(document);
        }
    }
}