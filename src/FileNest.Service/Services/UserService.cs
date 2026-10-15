using FileNest.Data.Entities;
using FileNest.Model.Models;
using FileNest.Model.Models.Constants;
using FileNest.Service.Configuration;
using FileNest.Service.Exceptions;
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
                Email = user.Email,
                UserId = Guid.NewGuid(),
                EmailVerified = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            try
            {
            await _users.InsertOneAsync(userModel);
            }
            catch (MongoWriteException ex) when (ex.WriteError?.Code == 11000)
            {
                throw  UserExceptions.EmailAlreadyExists(user.Email);
            }
        }
        public async Task<List<GetUserResponseModel>> GetUsersAsync()
        {
            var users = await _users.Find(_ => true).ToListAsync();
            return users.Select(user => new GetUserResponseModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Status = user.Status.ToString(),
                EmailVerified = user.EmailVerified,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                LastSeenAt = user.LastSeenAt
            }).ToList();
        }
    }
}