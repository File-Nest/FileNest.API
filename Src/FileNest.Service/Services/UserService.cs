using FileNest.Data.Entities;
using FileNest.Model.Models;
using FileNest.Model.Models.Constants;
using FileNest.Service.Configuration;
using MongoDB.Driver;

namespace FileNest.Service
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _mongoClient;
        private readonly DatabaseConnectionProvider _connectionProvider;
        public UserService(IMongoClient mongoClient, DatabaseConnectionProvider connectionProvider)
        {
            _mongoClient = mongoClient;
            _connectionProvider = connectionProvider;
            var settings = _connectionProvider.GetDatabaseConfiguration(DBConstants.DatabaseName);
            _database = _mongoClient.GetDatabase(settings.DatabaseName);
            _users = _database.GetCollection<User>(DBConstants.UserCollectionName);
        }
        public async Task CreateUserAsync(CreateUserRequestModel user)
        {
            var userModel = new User
            {
                Name = user.Name,
                Email = user.Email
            };
            userModel.UserId = Guid.NewGuid();
            await _users.InsertOneAsync(userModel);
        }
        public async Task<List<User>> GetUsersAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }
    }
}