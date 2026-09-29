using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileNest.Data.Entities;
using FileNest.Model.Models;
using MongoDB.Driver;

namespace FileNest.Service
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        public UserService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("FileNest");
            _users = database.GetCollection<User>("Users");
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